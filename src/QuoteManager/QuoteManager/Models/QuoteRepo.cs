using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManager.Models
{
    public class QuoteRepo
    {
        private static List<Quote> _quotes = new List<Quote>();
        private static int _nextId = 1;

        public List<Quote> GetAllQuotes()
        {
            return _quotes;
        }

        public Quote GetQuoteById(int id)
        {
            return _quotes.Find(quote => quote.Id == id);
        }

        public void AddQuote(Quote quoteToAdd)
        {
            quoteToAdd.Id = _nextId++;
            _quotes.Add(quoteToAdd);
        }

        public void DeleteQuote(Quote quoteToDelete)
        {
            var original = GetQuoteById(quoteToDelete.Id);
            _quotes.Remove(original);
        }
    }
}
