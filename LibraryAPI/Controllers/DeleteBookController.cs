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
    public class DeleteBookController : ApiController
    {
        public int Id { get; set; }

        const string connectionString =
              @"Server=localhost\SQLEXPRESS;Database=LibraryData;Trusted_Connection=True;";

        [HttpPost]
        public IHttpActionResult DeleteBook()
        {
            
            using (var connection = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(@"Delete from LibraryCatalog WHERE Id = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", 21); //21 is the row I'm deleting

                   connection.Open();
                   cmd.ExecuteNonQuery();
                   connection.Close();

                    return Ok();

                }
                
            }

        }
    }
}