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
  public class TreatsController : Controller
  {
    private readonly SweetTreatContext _db;

    public TreatsController(SweetTreatContext db)
    {
      _db = db;
    }


    public ActionResult Index()
    {
      List<Treat> model = _db.Treats
                        .ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat, int flavorId)
    {
      if (!ModelState.IsValid)
      {
        return View(treat);
      }
      else
      {


        _db.Treats.Add(treat);
        _db.SaveChanges();

        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Treat thisTreat = _db.Treats
                      .Include(treat => treat.FlavorTreats)
                      .ThenInclude(join => join.Flavor)
                      .Include(user => user.ApplicationUser)
                      .ThenInclude(join => join.JoinEntites)
                      .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    
    public ActionResult Edit(int id)
    {
      Treat thisTreat = _db.Treats
                      .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (!ModelState.IsValid)
      {
        return View(treat);
      }
      else
      {
        _db.Treats.Update(treat);
        _db.SaveChanges();

      #nullable enable
      FlavorTreat? flavorTreats = _db.FlavorTreats.FirstOrDefault(join => (join.TreatId == FlavorId && join.FlavorId == treat.TreatId));
      #nullable disable
      if (flavorTreats == null && FlavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
        _db.SaveChanges();
      }
        return RedirectToAction("Index");
      }
    }

    public ActionResult Delete(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTreat(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(flavors => flavors.TreatId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Title");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int flavorId)
    {
      #nullable enable
      FlavorTreat? flavorTreats = _db.FlavorTreats.FirstOrDefault(join => (join.TreatId == flavorId && join.FlavorId == treat.TreatId));
      #nullable disable
      if (flavorTreats == null && flavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavorId, TreatId = treat.TreatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = treat.TreatId });
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
      List<Treat> model = _db.Treats.Where(model => model.Title.Contains(queryString)).ToList();
      return View("Index", model);
    }


  }
}