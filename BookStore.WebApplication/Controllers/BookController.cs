using BookStore.Data.Repositories;
using BookStore.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApplication.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var listBook = _bookRepository.GetAll();
            return View(listBook);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Route("book/{id}")]
        public IActionResult Details(int Id)
        {
            var book = _bookRepository.GetDetails(Id);
            return View(book);
        }
    }
}
