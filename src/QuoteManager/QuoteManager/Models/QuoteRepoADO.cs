using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManager.Models
{
    public class QuoteRepoADO : IQuoteRepo
    {
        private string connStr = "Server=(localdb)\\mssqllocaldb;Database=aspnet-QuoteManager-0D50DCD5-457D-458F-BCD0-73839EA78F1D;Trusted_Connection=True;MultipleActiveResultSets=true";

        private string selectAllQuery = "SELECT * FROM Quotes ";

        private string selectByIdClause = "WHERE ID = @id";

        private string insertQuoteQuery = "INSERT INTO Quotes " 
            + "(AuthorFirstName, AuthorLastName, Quote) " 
            + "VALUES(@FirstName, @LastName, @Quote)";


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
                            LastName = reader[2].ToString(),
                            QuoteText = reader[3].ToString(),
                            //DateSubmit = Convert.ToDateTime(reader[3].ToString(), CultureInfo.GetCultureInfo("en-US"))
                            //DateSubmit = DateTime.ParseExact(reader[4].ToString(), "M/d/yyyy H:mm:ss", CultureInfo.InvariantCulture)
                            DateSubmit = reader[4].ToString()
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
                            LastName = reader[2].ToString(),
                            QuoteText = reader[3].ToString(),
                            DateSubmit = reader[4].ToString()
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

        public void DeleteQuote(int id)
        {
            //TODO
        }
    }
}
