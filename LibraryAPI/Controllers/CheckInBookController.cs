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
    public class CheckInBookController : ApiController
    {

        const string connectionString =
                @"Server=localhost\SQLEXPRESS;Database=LibraryData;Trusted_Connection=True;";

        [HttpPost]
        public IHttpActionResult CheckInBook()
        {

            using (var connection = new SqlConnection(connectionString))
            {

                var books = new List<UpdateCheckOutStatus>();
                var sqlCommand = new SqlCommand(@"SELECT Id, Title, IsCheckedOut, DueBackDate, LastCheckedOutDate
                                                FROM [dbo].[LibraryCatalog]
                                                WHERE Id = 20;", connection);
                connection.Open();
                var reader = sqlCommand.ExecuteReader();

                var book = new UpdateCheckOutStatus();
                while (reader.Read())
                {
                    book = new UpdateCheckOutStatus
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        IsCheckedOut = (bool)reader["IsCheckedOut"],
                        LastCheckedOutDate = reader["LastCheckedOutDate"] as DateTime?,
                        DueBackDate = reader["DueBackDate"] as DateTime?,

                    };

                }
                connection.Close();

                if (book.IsCheckedOut == false)
                {
                    return Ok(new UpdateCheckOutStatus
                    {
                        DueBackDate = book.DueBackDate,
                        Id = book.Id,
                        Title = book.Title,
                        LastCheckedOutDate = book.LastCheckedOutDate,
                        ResponseMessage = "ERROR! This book has already been returned"
                    });
                    
                }

                else
                {
                    var updateBookStatus = new List<UpdateCheckOutStatus>();
                    var cmd = new SqlCommand($"UPDATE LibraryCatalog SET IsCheckedOut = @IsCheckedOut" +
                                                        " WHERE Id= 20;", connection);

                    cmd.Parameters.AddWithValue("@IsCheckedOut", false);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    var UpdatedBook = new UpdateCheckOutStatus
                    {
                        Id = book.Id,
                        Title = book.Title,
                        IsCheckedOut = false,
                        LastCheckedOutDate = book.LastCheckedOutDate,
                        DueBackDate = null,
                        ResponseMessage = "You have successfully returned your book. We hope you enjoyed reading it!"
                    };

                    return Ok(UpdatedBook);

                }

            }
        }

    }
}