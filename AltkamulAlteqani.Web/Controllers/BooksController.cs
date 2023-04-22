using AltkamulAlteqani.Entities.Models;
using AltkamulAlteqani.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltkamulAlteqani.Web.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult BookList()
        {
            var books = _service.GetBookList()
                .Select(b => new BookModel
                {
                    Author = b.Author.AuthorName,
                    BookType = b.BookType.BookTypeName,
                    Title = b.Title,
                    Price = b.Price,
                    PublishDate = b.PublishDate.ToString()
                }).ToList();

            return Json(new { @data = books }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BooksWithTypes()
        {
            return View();
        }
    }
}