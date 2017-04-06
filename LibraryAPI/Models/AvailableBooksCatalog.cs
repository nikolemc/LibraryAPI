using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryAPI.Models
{
    public class AvailableBooksCatalog
    {
        public string Title { get; set; }
        //public bool IsCheckedOut { get; set; }

        public AvailableBooksCatalog()
        {

        }

        public AvailableBooksCatalog(SqlDataReader reader)
        {
            Title = reader["Title"].ToString();
        }
    }
}