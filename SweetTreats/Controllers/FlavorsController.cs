using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetTreat.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SweetTreat.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly SweetTreatContext _db;

    public FlavorsController(SweetTreatContext db)
    {
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors
                          .ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      if (!ModelState.IsValid)
      {
          return View(flavor);
      }
      else
      {
        _db.Flavors.Add(flavor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Flavor thisFlavor = _db.Flavors
                      .Include(flavor => flavor.FlavorTreats)
                      .ThenInclude(join => join.Treat)
                      .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    public ActionResult Edit(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Flavors.Update(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTreat(int id)
    {
      Flavor thisFlavor = _db.Flavors
                          .Include(flavor => flavor.FlavorTreats)
                          .ThenInclude(join => join.Treat)
                          .FirstOrDefault(flavors => flavors.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Title");
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int TreatId)
    {
      #nullable enable
      FlavorTreat? flavorTreats = _db.FlavorTreats.FirstOrDefault(join => (join.TreatId == TreatId && join.FlavorId == flavor.FlavorId));
      #nullable disable
      if (flavorTreats == null && TreatId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = flavor.FlavorId });
    }   

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreats.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    } 


    [HttpPost]
    public ActionResult Find(string queryString)
    {
      List<Flavor> model = _db.Flavors
                          .Where model.Taste
                          .Contains(queryString)
                          .ToList();
        return View("Index", model);
    }
  }
}
