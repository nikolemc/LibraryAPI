﻿using System;
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
                var books = new List<UpdateCheckOutStatus>();
                var sqlCommand = new SqlCommand(@"SELECT Id, IsCheckedOut, DueBackDate
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
                        IsCheckedOut = (bool)reader["IsCheckedOut"],
                        DueBackDate = reader["DueBackDate"] as DateTime?,

                    };
                }
                connection.Close();
                if (book.IsCheckedOut) //so the book is not in the library
                {
                    return Ok(new UpdateCheckOutStatus
                    {
                        DueBackDate = book.DueBackDate,
                        Id = book.Id,
                        ResponseMessage = "Sorry this book is not available"
                    });
                }

                else  // book is in the library
                {
                    // Update the book (UPDATE) to update the dueback date and checked out status

                    var updateBookStatus = new List<UpdateCheckOutStatus>();
                    var cmd = new SqlCommand($"UPDATE LibraryCatalog SET LastCheckedOutDate = @LastCheckedOutDate, DueBackDate = @DueBackDate, IsCheckedOut = @IsCheckedOut " +
                                                    " WHERE Id= 20;", connection);
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
                        Id = book.Id,
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