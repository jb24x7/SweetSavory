using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Linq;

namespace LibraryCatalog.Controllers
{
  public class HomeController : Controller
  {
    private readonly LibraryCatalogContext _db;

    public HomeController(LibraryCatalogContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.Books = _db.Books
                      .Include(book => book.AuthorBooks)
                      .ThenInclude(join => join.Author)
                      .ToList();
      ViewBag.Authors = _db.Authors
                      .Include(author => author.AuthorBooks)
                      .ThenInclude(join => join.Book)
                      .ToList();
      return View();
    }
  }
}