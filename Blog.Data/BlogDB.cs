using System;
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
            where T: new ()
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
            where T: new ()
        {
            var properties = typeof (T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var result = new List<T>();
            while (reader.Read())
            {
                var item = new T();
                foreach (var prop in properties)
                {
                    //var attribute = prop.GetCustomAttribute<DbColumnNameAttribute>();
                    //var name = attribute == null ? prop.Name : attribute.ColumnName;
                    var name = prop.Name;
                    prop.SetValue(item, reader[name]);
                }
                result.Add(item);
            }

            return result;
        }


        public IEnumerable<BlogPost> GetBlogPosts()
        {

            return DBCrud("SELECT * FROM BlogPosts", null, MapAllFromDB<BlogPost>);
        } 

        public BlogPost GetBlogPost(int id)
        {
            return DBCrud("SELECT * FROM BlogPosts WHERE Id = @Id", new Dictionary<string, object>() { { "@Id", id } },
                          MapSingleFromDb<BlogPost>);
        } 
    }
}
