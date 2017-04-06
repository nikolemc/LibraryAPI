using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    public class CheckedOutController : ApiController
    {
        const string connectionString =
              @"Server=localhost\SQLEXPRESS;Database=LibraryData;Trusted_Connection=True;";


        [HttpGet]
        public IHttpActionResult CheckedOutBooks()
        {

            using (var connection = new SqlConnection(connectionString))
            {

                var books = new List<CheckedOutBooksCatalog>();
                var sqlCommand = new SqlCommand(@"SELECT Id, Title, LastCheckedOutDate, DATEADD(day,10,LastCheckedOutDate) AS DueBackDate
                                                FROM [dbo].[LibraryCatalog]
                                                WHERE [IsCheckedOut] = 1", connection);
                connection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var book = new CheckedOutBooksCatalog
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        LastCheckedOutDate = reader["LastCheckedOutDate"] as DateTime?,
                        DueBackDate = reader["DueBackDate"] as DateTime?,
                    };
                    books.Add(book);
                }
                // Close Connection
                connection.Close();
                return Ok(books);
            }



        }
    }
}