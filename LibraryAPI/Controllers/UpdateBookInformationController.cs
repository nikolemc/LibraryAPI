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
    public class UpdateBookInformationController : ApiController
    {
        public int Id { get; set; }
        public string Author { get; set; }

        const string connectionString =
               @"Server=localhost\SQLEXPRESS;Database=LibraryData;Trusted_Connection=True;";

        [HttpPost]
        public IHttpActionResult UpdateBook()
        {
            using (var connection = new SqlConnection(connectionString))
            {
               
                using (SqlCommand cmd = new SqlCommand($"UPDATE LibraryCatalog SET Title = @Title, Author = @Author, YearPublished = @YearPublished, Genre = @Genre" +
                    " WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", 23); //update row 23
                    cmd.Parameters.AddWithValue("@Title", "Coding 101");
                    cmd.Parameters.AddWithValue("@Author", "Jen Wales");
                    cmd.Parameters.AddWithValue("@YearPublished", 2017);
                    cmd.Parameters.AddWithValue("@Genre", "MATHS");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    return Ok();
                }
                
            }            
     
        }
    }
    
}