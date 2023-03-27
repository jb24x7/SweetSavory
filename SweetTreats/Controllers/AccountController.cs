using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SweetTreat.Models;
using System.Threading.Tasks;
using SweetTreat.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;

namespace SweetTreat.Controllers
{
  public class AccountController : Controller
  {
    private readonly SweetTreatContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, SweetTreatContext db)
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
                      .ThenInclude(join => join.Treat)
                      .FirstOrDefault(user => user.Id == id);
      return View(thisUser);
    }


  }
}