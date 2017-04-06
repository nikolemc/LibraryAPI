using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryAPI.Models
{
    public class CheckedOutBooksCatalog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public bool IsCheckedOut { get; set; }
        public DateTime? LastCheckedOutDate { get; set; }
        public DateTime? DueBackDate { get; set; }

        public CheckedOutBooksCatalog()
        {

        }

        public CheckedOutBooksCatalog(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Title = reader["Title"].ToString();
            LastCheckedOutDate = (DateTime)reader["LastCheckedOutDate"];
            DueBackDate = (DateTime)reader["DueBackDate"];

        }

    }
}