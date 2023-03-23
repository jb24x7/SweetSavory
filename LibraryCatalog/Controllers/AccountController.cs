using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LibraryCatalog.Models;
using System.Threading.Tasks;
using LibraryCatalog.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;

namespace LibraryCatalog.Controllers
{
  public class AccountController : Controller
  {
    private readonly LibraryCatalogContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, LibraryCatalogContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        ApplicationUser user = new ApplicationUser { UserName = model.Email };
        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        // Note that CreateAsync() takes two arguments:
          // 1. An ApplicationUser with user information;
          // 2. A password that will be encrypted when the user is added to the database.
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          foreach (IdentityError error in result.Errors)
          {
            ModelState.AddModelError("", error.Description);
          }
          return View(model);
          // ^^ This code will re-display the Register() GET action with our same model that has the error messages associated with it. If we did not pass in our model variable to the view, the Register() GET action would display again, but it would have no conception of any errors — it would be a brand new model, just like hitting the refresh button.
        }
      }
    }

        public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          ModelState.AddModelError("", "There is something wrong with your email or username. Please try again.");
          return View(model);
        }
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    public ActionResult Details(string id)
    {
      ApplicationUser thisUser = _db.Users
                      .Include(User => User.JoinEntites)
                      .ThenInclude(join => join.Book)
                      .FirstOrDefault(user => user.Id == id);
      return View(thisUser);
    }

        [AllowAnonymous]
    public ActionResult Checkout(int bookId)
    {
      Book thisBook = _db.Books

                      .Include(user => user.ApplicationUser)
                      .ThenInclude(join => join.JoinEntites)
                      .Include(join => join.AuthorBooks)
                      .ThenInclude(author => author.Author)
                      .FirstOrDefault(book => book.BookId == bookId);
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Checkout(Book book, string userId, DateTime CheckoutDate, int bookId)
    {
      #nullable enable
      AppUserBook? joinEntity = _db.AppUserBooks.FirstOrDefault(join => (join.UserID == userId && join.BookId == book.BookId));
      Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == bookId);
      #nullable disable
      if (joinEntity == null && userId != null)
      {
        _db.AppUserBooks.Add(new AppUserBook() { UserID = userId, BookId = book.BookId, CheckoutDate = CheckoutDate, DueDate = CheckoutDate.AddDays(14)});
        thisBook.Copies = (thisBook.Copies -1);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = userId});
    }  

            [AllowAnonymous]
    public ActionResult Return(int bookId)
    {
      Book thisBook = _db.Books

                      .Include(user => user.ApplicationUser)
                      .ThenInclude(join => join.JoinEntites)
                      .Include(join => join.AuthorBooks)
                      .ThenInclude(author => author.Author)
                      .FirstOrDefault(book => book.BookId == bookId);
      return View(thisBook);
    }


    [HttpPost]
public ActionResult Return(Book book, string userId, DateTime ReturnDate, int bookId)
{
  #nullable enable
  AppUserBook? joinEntity = _db.AppUserBooks.FirstOrDefault(join => (join.UserID == userId && join.BookId == book.BookId && !join.Returned));
  Book thisBook = _db.Books.FirstOrDefault(book => book.BookId == bookId);

  #nullable disable
  if (joinEntity != null && userId != null)
  {
    joinEntity.Returned = true;
    joinEntity.ReturnDate = ReturnDate;
    thisBook.Copies = (thisBook.Copies + 1);
    _db.SaveChanges();
  }
  return RedirectToAction("Details", new { id = userId});
}
  }
}