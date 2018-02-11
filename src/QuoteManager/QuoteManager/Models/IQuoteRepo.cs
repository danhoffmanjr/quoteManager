using System.Collections.Generic;

namespace QuoteManager.Models
{
    public interface IQuoteRepo
    {
        void AddQuote(Quote quoteToAdd);
        void DeleteQuote(int id);
        List<Quote> GetAllQuotes();
        Quote GetQuoteById(int id);
    }
}