using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManager.Models
{
    public class QuoteRepoADO : IQuoteRepo
    {
        public static IConfiguration Configuration { get; set; }

        public static string DbConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration["ConnectionStrings:DefaultConnection"];
        }

        private string connStr = DbConfig();

        private string selectAllQuery = "SELECT * FROM Quotes ";

        private string selectByIdClause = "WHERE ID = @id";

        private string insertQuoteQuery = "INSERT INTO Quotes " 
            + "(AuthorFirstName, AuthorLastName, Quote) " 
            + "VALUES(@FirstName, @LastName, @Quote)";

        private string updateQuery = "UPDATE Quotes "
            + "SET AuthorFirstName = @FirstName, "
            + "AuthorLastName = @LastName, "
            + "Quote = @Quote ";

        private string deleteQuery = "DELETE FROM Quotes ";


        public List<Quote> GetAllQuotes()
        {
            List<Quote> quotes = new List<Quote>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(selectAllQuery, conn);

                try
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Quote newQuote = new Quote
                        {
                            Id = int.Parse(reader[0].ToString()),
                            FirstName = reader[1].ToString(),
                            LastName = reader[4].ToString(),
                            QuoteText = reader[2].ToString(),
                            DateSubmit = reader.GetDateTime(3)
                        };

                        quotes.Add(newQuote);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return quotes;
        }

        public Quote GetQuoteById(int id)
        {
            Quote quote = new Quote();

            using (var conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(selectAllQuery + selectByIdClause, conn);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        quote = new Quote
                        {
                            Id = int.Parse(reader[0].ToString()),
                            FirstName = reader[1].ToString(),
                            LastName = reader[4].ToString(),
                            QuoteText = reader[2].ToString(),
                            DateSubmit = reader.GetDateTime(3)
                        };
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return quote;
        }

        public void AddQuote(Quote quoteToAdd)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertQuoteQuery, conn);
                    command.Parameters.AddWithValue("@FirstName", quoteToAdd.FirstName);
                    command.Parameters.AddWithValue("@LastName", quoteToAdd.LastName);
                    command.Parameters.AddWithValue("@Quote", quoteToAdd.QuoteText);

                    conn.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }

        public void UpdateQuote(Quote editedQuote)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(updateQuery + selectByIdClause, conn);

                try
                {
                    command.Parameters.AddWithValue("@FirstName", editedQuote.FirstName);
                    command.Parameters.AddWithValue("@LastName", editedQuote.LastName);
                    command.Parameters.AddWithValue("@Quote", editedQuote.QuoteText);
                    command.Parameters.AddWithValue("@id", editedQuote.Id);

                    conn.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void DeleteQuote(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteQuery + selectByIdClause, conn);
                    command.Parameters.AddWithValue("@id", id);

                    conn.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
