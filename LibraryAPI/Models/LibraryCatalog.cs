using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryAPI.Models
{
    public class LibraryCatalog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }
        public string Genre { get; set; }
        public bool IsCheckedOut { get; set; }
        public DateTime? LastCheckedOutDate { get; set; }
        public DateTime? DueBackDate { get; set; }

        public LibraryCatalog()
        {

        }

        public LibraryCatalog(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Title = reader["Title"].ToString();
            YearPublished = (int)reader["YearPublished"];
            Genre = reader["Genre"].ToString();
            IsCheckedOut = (bool)reader["IsCheckedOut"];
            LastCheckedOutDate = (DateTime)reader["LastCheckedOutDate"];
            DueBackDate = (DateTime)reader["DueBackDate"];

        }
    }
}