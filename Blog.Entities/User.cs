using System;

namespace Blog.Entities
{
    public class User
    {
        public int Id { get; set; }
        //[DbColumnName("Nickname")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DbColumnNameAttribute : Attribute
    {
        private readonly string _columnName;

        public DbColumnNameAttribute(string columnName)
        {
            _columnName = columnName;
        }

        public string ColumnName{get { return _columnName; }}
    }
}