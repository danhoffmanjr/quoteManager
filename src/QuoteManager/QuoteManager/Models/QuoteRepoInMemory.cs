using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteManager.Models
{
    public class QuoteRepoInMemory : IQuoteRepo
    {
        private static List<Quote> _quotes;
        private static int _nextId = 1;

        public QuoteRepoInMemory()
        {
            if (_quotes == null)
            {
                _quotes = new List<Quote>();
            }
        }

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

        public void UpdateQuote(Quote editedQuote)
        {
            Quote toBeUpdated = GetQuoteById(editedQuote.Id);
            toBeUpdated.FirstName = editedQuote.FirstName;
            toBeUpdated.LastName = editedQuote.LastName;
            toBeUpdated.QuoteText = editedQuote.QuoteText;
        }

        public void DeleteQuote(int id)
        {
            var original = GetQuoteById(id);
            _quotes.Remove(original);
        }
    }
}
