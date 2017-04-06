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
    public class LibraryController : ApiController
    {
        const string connectionString =
               @"Server=localhost\SQLEXPRESS;Database=LibraryData;Trusted_Connection=True;";

        [HttpGet]
        public IHttpActionResult GetAllBooks()
        {

            using (var connection = new SqlConnection(connectionString))
            {

                var books = new List<LibraryCatalog>();
                var sqlCommand = new SqlCommand(@"SELECT [Id],[Title],[Author],[YearPublished],[Genre],[IsCheckedOut],[LastCheckedOutDate],[DueBackDate]
                                            FROM [dbo].[LibraryCatalog]", connection);
                connection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    //var course = new Course(reader);
                    var book = new LibraryCatalog
                    {
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString(),
                    YearPublished = (int)reader["YearPublished"],
                    Genre = reader["Genre"].ToString(),
                    IsCheckedOut = (bool)reader["IsCheckedOut"],
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