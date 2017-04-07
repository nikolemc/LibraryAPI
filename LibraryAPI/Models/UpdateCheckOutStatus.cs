using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryAPI.Models
{
    public class UpdateCheckOutStatus
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCheckedOut { get; set; }
        public DateTime? LastCheckedOutDate { get; set; }
        public DateTime? DueBackDate { get; set; }
        public string ResponseMessage { get; set; }

        public UpdateCheckOutStatus()
        {

        }

        public UpdateCheckOutStatus(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Title = reader["Title"].ToString();
            IsCheckedOut = (bool)reader["IsCheckedOut"];
            LastCheckedOutDate = (DateTime)reader["LastCheckedOutDate"];
            DueBackDate = (DateTime)reader["DueBackDate"];
            ResponseMessage = reader[" ResponseMessage "].ToString();

        }

    }
}