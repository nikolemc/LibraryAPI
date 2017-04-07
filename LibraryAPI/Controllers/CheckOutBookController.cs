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
    public class CheckOutBookController : ApiController
    {

        const string connectionString =
               @"Server=localhost\SQLEXPRESS;Database=LibraryData;Trusted_Connection=True;";

        [HttpPost]
        public IHttpActionResult CheckOutBook()
        {

            using (var connection = new SqlConnection(connectionString))
            {
                var books = new List<LibraryCatalog>();
                var sqlCommand = new SqlCommand(@"SELECT Title, IsCheckedOut, DueBackDate
                                                FROM [dbo].[LibraryCatalog]
                                                WHERE Title = 'Coding 101';", connection);
                connection.Open();
                var reader = sqlCommand.ExecuteReader();

                var book = new LibraryCatalog();
                while (reader.Read())
                {

                    book = new LibraryCatalog
                    {
                        Title = reader["Title"].ToString(),
                        IsCheckedOut = (bool)reader["IsCheckedOut"],
                        //    LastCheckedOutDate = reader["LastCheckedOutDate"] as DateTime?,
                        DueBackDate = reader["DueBackDate"] as DateTime?,
                        //   ResponseMessage = reader["ResponseMessage"].ToString()

                    };
                }
                connection.Close();
                if (book.IsCheckedOut) //so the book is not in the library
                {
                    return Ok(new CheckedOutBooksCatalog
                    {
                        DueBackDate = book.DueBackDate,
                        Title = book.Title
                    });
                }

                else  // book is in the library
                {
                    // Update the book (UPDATE) to update the dueback date and checked out status

                    var updateBookStatus = new List<UpdateCheckOutStatus>();
                    var cmd = new SqlCommand($"UPDATE LibraryCatalog SET LastCheckedOutDate = @LastCheckedOutDate, DueBackDate = @DueBackDate, IsCheckedOut = @IsCheckedOut " +
                                                    " WHERE [Title] = '101 Coding';", connection);
                    book.LastCheckedOutDate = DateTime.Now;
                    book.DueBackDate = DateTime.Now.AddDays(+10);
                    book.IsCheckedOut = true;

                    cmd.Parameters.AddWithValue("@LastCheckedOutDate", book.LastCheckedOutDate);
                    cmd.Parameters.AddWithValue("@DueBackDate", book.DueBackDate);
                    cmd.Parameters.AddWithValue("@IsCheckedOut", book.IsCheckedOut);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    var UpdatedBook = new UpdateCheckOutStatus
                    {
                        Title = book.Title,
                        IsCheckedOut = book.IsCheckedOut,
                        LastCheckedOutDate = book.LastCheckedOutDate,
                        DueBackDate = book.DueBackDate,
                        ResponseMessage = "Successfully checked out"
                    };

                    return Ok(UpdatedBook);

                }



            }
        }

    }
}