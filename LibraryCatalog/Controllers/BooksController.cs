using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace LibraryCatalog.Controllers
{
  [Authorize(Roles = "Admin")]
  public class BooksController : Controller
  {
    private readonly LibraryCatalogContext _db;

    public BooksController(LibraryCatalogContext db)
    {
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Book> model = _db.Books
                        .ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Book book, int authorId)
    {
      if (!ModelState.IsValid)
      {
        return View(book);
      }
      else
      {


        _db.Books.Add(book);
        _db.SaveChanges();

        return RedirectToAction("Index");
      }
    }
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Book thisBook = _db.Books
                      .Include(book => book.AuthorBooks)
                      .ThenInclude(join => join.Author)
                      .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    
    public ActionResult Edit(int id)
    {
      Book thisBook = _db.Books
                      .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book book, int AuthorId)
    {
      if (!ModelState.IsValid)
      {
        return View(book);
      }
      else
      {
        _db.Books.Update(book);
        _db.SaveChanges();

      #nullable enable
      AuthorBook? authorBooks = _db.AuthorBooks.FirstOrDefault(join => (join.BookId == AuthorId && join.AuthorId == book.BookId));
      #nullable disable
      if (authorBooks == null && AuthorId != 0)
      {
        _db.AuthorBooks.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
        _db.SaveChanges();
      }
        return RedirectToAction("Index");
      }
    }

    public ActionResult Delete(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBook(int id)
    {
      Book thisBook = _db.Books.FirstOrDefault(authors => authors.BookId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddAuthor(Book book, int authorId)
    {
      #nullable enable
      AuthorBook? authorBooks = _db.AuthorBooks.FirstOrDefault(join => (join.BookId == authorId && join.AuthorId == book.BookId));
      #nullable disable
      if (authorBooks == null && authorId != 0)
      {
        _db.AuthorBooks.Add(new AuthorBook() { AuthorId = authorId, BookId = book.BookId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = book.BookId });
    }   

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      AuthorBook joinEntry = _db.AuthorBooks.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBooks.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    } 

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Find(string queryString)
    {
      List<Book> model = _db.Books.Where(model => model.Title.Contains(queryString)).ToList();
      return View("Index", model);
    }
  }
}
