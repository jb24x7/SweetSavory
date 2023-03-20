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
      return View();
    }
  }
}