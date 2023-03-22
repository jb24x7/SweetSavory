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
  public class AuthorsController : Controller
  {
    private readonly LibraryCatalogContext _db;

    public AuthorsController(LibraryCatalogContext db)
    {
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Author> model = _db.Authors
                          .ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Author author)
    {
      if (!ModelState.IsValid)
      {
          return View(author);
      }
      else
      {
        _db.Authors.Add(author);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Author thisAuthor = _db.Authors
                      .Include(author => author.AuthorBooks)
                      .ThenInclude(join => join.Book)
                      .FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    public ActionResult Edit(int id)
    {
      Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost]
    public ActionResult Edit(Author author)
    {
      _db.Authors.Update(author);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Author thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      _db.Authors.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBook(int id)
    {
      Author thisAuthor = _db.Authors
                          .Include(author => author.AuthorBooks)
                          .ThenInclude(join => join.Book)
                          .FirstOrDefault(authors => authors.AuthorId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View(thisAuthor);
    }

    [HttpPost]
    public ActionResult AddBook(Author author, int BookId)
    {
      #nullable enable
      AuthorBook? authorBooks = _db.AuthorBooks.FirstOrDefault(join => (join.BookId == BookId && join.AuthorId == author.AuthorId));
      #nullable disable
      if (authorBooks == null && BookId != 0)
      {
        _db.AuthorBooks.Add(new AuthorBook() { BookId = BookId, AuthorId = author.AuthorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = author.AuthorId });
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
      List<Author> model = _db.Authors
                          .Where(model => model.LastName.Contains(queryString) || model.FirstName.Contains(queryString))
                          .ToList();
        return View("Index", model);
    }
  }
}
