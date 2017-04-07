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
    public class AvailableBooksController : ApiController
    {
        const string connectionString =
               @"Server=localhost\SQLEXPRESS;Database=LibraryData;Trusted_Connection=True;";

        [HttpGet]
        public IHttpActionResult AvailableBooks()
        {

            using (var connection = new SqlConnection(connectionString))
            {

                var books = new List<AvailableBooksCatalog>();
                var sqlCommand = new SqlCommand(@"SELECT Title
                                                 FROM [dbo].[LibraryCatalog]
                                                 WHERE [IsCheckedOut] = 0", connection);
                connection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var book = new AvailableBooksCatalog
                    {
                        Title = reader["Title"].ToString(),
                    };
                    books.Add(book);
                }
                connection.Close();
                return Ok(books);

            }

        }
    }
}