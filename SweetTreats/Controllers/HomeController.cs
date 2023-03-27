using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetTreat.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SweetTreat.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    private readonly SweetTreatContext _db;

    public HomeController(SweetTreatContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.Treats = _db.Treats
                      .Include(treat => treat.FlavorTreats)
                      .ThenInclude(join => join.Flavor)
                      .ToList();
      ViewBag.Flavors = _db.Flavors
                      .Include(flavor => flavor.FlavorTreats)
                      .ThenInclude(join => join.Treat)
                      .ToList();
      return View();
    }





//     if (User.Identity.IsAuthenticated)
// {
//     var userID = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
//     var calls = _applicationDbContext.Users.Include(u => u.Calls).First(u => u.Id == userID).Calls.ToList();

//     //or
//     var callsa = _applicationDbContext.Calls.Where(p => p.UserID == userID).ToList();
// }


  }
}