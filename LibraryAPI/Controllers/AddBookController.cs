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

    public class AddBookController : ApiController
    {
    
        const string connectionString =
               @"Server=localhost\SQLEXPRESS;Database=LibraryData;Trusted_Connection=True;";

        [HttpPost]
        public IHttpActionResult AddBooks()
        {
            var book = new LibraryCatalog { Title = "Once Upon A Time", Author = "Jen Wales", YearPublished = 2016, Genre = "Fiction", IsCheckedOut = false};

            using (var connection = new SqlConnection(connectionString))
            {

                var books = @"INSERT INTO LibraryCatalog (Title,Author,YearPublished,Genre,IsCheckedOut)" +
                "Values (@Title, @Author, @YearPublished, @Genre, @IsCheckedOut)";

                var sqlCommand = new SqlCommand(books, connection);

                sqlCommand.Parameters.AddWithValue("@Title", book.Title);
                sqlCommand.Parameters.AddWithValue("@Author", book.Author);
                sqlCommand.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                sqlCommand.Parameters.AddWithValue("@Genre", book.Genre);
                sqlCommand.Parameters.AddWithValue("@IsCheckedOut", book.IsCheckedOut);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                return Ok();
            }
     
        }
    }
}