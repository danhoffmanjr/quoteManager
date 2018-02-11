using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteManager.Models;

namespace QuoteManager.Controllers
{
    public class QuotesController : Controller
    {
        private readonly IQuoteRepo _quotesRepo;

        public QuotesController(IQuoteRepo quotesRepo)
        {
            _quotesRepo = quotesRepo;
        }

        
        // GET: Quotes
        public ActionResult Index()
        {
            return View(_quotesRepo.GetAllQuotes());
        }

        // GET: Quotes/Details/5
        public ActionResult Details(int id)
        {
            return View(_quotesRepo.GetQuoteById(id));
        }

        // GET: Quotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quotes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quote newQuote, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(newQuote);
            }

            try
            {
                _quotesRepo.AddQuote(newQuote);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(newQuote);
            }
        }

        // GET: Quotes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Quotes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Quotes/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_quotesRepo.GetQuoteById(id));
        }

        // POST: Quotes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _quotesRepo.DeleteQuote(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}