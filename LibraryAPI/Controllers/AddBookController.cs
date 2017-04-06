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
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public string Genre { get; set; }
        public bool IsCheckedOut { get; set; }

        const string connectionString =
               @"Server=localhost\SQLEXPRESS;Database=LibraryData;Trusted_Connection=True;";

        [HttpPost]
        public IHttpActionResult AddBooks()
        {
            var book = new LibraryCatalog { Title = "NikoleBook", Author = "McStanley", YearPublished = 2017, Genre = "Fiction", IsCheckedOut = false};

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

                //sqlCommand.Parameters.AddWithValue("@Title", Title);
                //sqlCommand.Parameters.AddWithValue("@Author", Author);
                //sqlCommand.Parameters.AddWithValue("@YearPublished", YearPublished);
                //sqlCommand.Parameters.AddWithValue("@Genre", Genre);
                //sqlCommand.Parameters.AddWithValue("@IsCheckedOut", IsCheckedOut);

                connection.Open();
               sqlCommand.ExecuteNonQuery();
                connection.Close();

                return Ok();
            }
     
        }
    }
}