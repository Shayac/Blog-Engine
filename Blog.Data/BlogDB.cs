﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Blog.Entities;

namespace Blog.Data
{
    public class BlogDB
    {
        private readonly string _connectionString;

        public BlogDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region readerAbstraction
        private T DBCrud<T>(string commandText, Func<SqlDataReader, T> func)
        {
            return DBCrud(commandText, null, func);
        }
        private T DBCrud<T>(string commandText, Dictionary<string, object> parameters, Func<SqlDataReader, T> func)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = commandText;
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> keyValuePair in parameters)
                    {
                        command.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                    }
                }

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                return func(reader);
            }
        }
        public T MapSingleFromDb<T>(SqlDataReader reader)
            where T : new()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            reader.Read();
            var item = new T();
            foreach (PropertyInfo prop in properties)
            {
                var name = prop.Name;
                prop.SetValue(item, reader[name]);
            }
            return item;

        }
        public IEnumerable<T> MapAllFromDB<T>(SqlDataReader reader)
            where T : new()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var result = new List<T>();
            while (reader.Read())
            {
                var item = new T();
                foreach (var prop in properties)
                {
                    //var attribute = prop.GetCustomAttribute<DbColumnNameAttribute>();
                    //var name = attribute == null ? prop.Name : attribute.ColumnName;
                    var name = prop.Name;
                    var value = reader[name];
                    if (value == DBNull.Value)
                    {
                        prop.SetValue(item, null);
                    }
                    else
                    {
                        prop.SetValue(item, reader[name]);
                    }
                }
                result.Add(item);
            }

            return result;
        }
        #endregion

        #region GetFromDB
        public IEnumerable<BlogPost> GetBlogPosts()
        {

            return DBCrud("SELECT * FROM BlogPosts", null, MapAllFromDB<BlogPost>);
        }

        public BlogPost GetBlogPost(int id)
        {
            return DBCrud("SELECT * FROM BlogPosts WHERE Id = @Id", new Dictionary<string, object>() { { "@Id", id } },
                          MapSingleFromDb<BlogPost>);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return DBCrud("SELECT * FROM Authors ORDER BY LastName", MapAllFromDB<Author>);
        }

        public IEnumerable<User> GetUsers()
        {
            return DBCrud("SELECT * FROM Users ORDER BY LastName", MapAllFromDB<User>);
        }

        public IEnumerable<Comment> GetComments(int id)
        {
            return DBCrud("SELECT * FROM Comments WHERE BlogPostId = @id",
                          new Dictionary<string, object>() { { "@id", id } }, MapAllFromDB<Comment>);
        }
        #endregion

        #region InsertToDb

        public int CreatePost(BlogPost post)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO BlogPosts (AuthorId, PostBody, Date, Title) VALUES (@AuthorId, @PostBody, @Date, @Title); SELECT @@Identity;";
                PropertyInfo[] properties = typeof(BlogPost).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var prop in properties)
                {
                    if (prop.Name == "Id")
                    {
                        continue;
                    }

                    command.Parameters.AddWithValue(prop.Name, prop.GetValue(post));
                }
                connection.Open();
                return (int)(decimal)command.ExecuteScalar();
            }
        }



        public void CreateAuthor(Author author)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO Authors (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email);";
                command.Parameters.AddWithValue("@FirstName", author.FirstName);
                command.Parameters.AddWithValue("@LastName", author.LastName);
                command.Parameters.AddWithValue("@Email", author.Email);
                //@todo refactor to helper method
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void CreateComment(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO Comments (UserId, BlogPostId, CommentBody, Date) VALUES (@UserId, @BlogPostId, @CommentBody, @Date);";
                PropertyInfo[] properties = typeof(Comment).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var prop in properties)
                {
                    if (prop.Name == "Id")
                    {
                        continue;
                    }
                    if (prop.Name == "ReplyId")
                    {
                        command.Parameters.AddWithValue(prop.Name, DBNull.Value);
                        continue;
                    }

                    command.Parameters.AddWithValue(prop.Name, prop.GetValue(comment));

                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void CreateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO Users (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email)";
                PropertyInfo[] properties = typeof(User).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var prop in properties)
                {
                    if (prop.Name == "Id")
                    {
                        continue;
                    }

                    command.Parameters.AddWithValue(prop.Name, prop.GetValue(user));
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
