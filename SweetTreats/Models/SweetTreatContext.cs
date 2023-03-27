using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SweetTreat.Models;

namespace SweetTreat.Models
{

  public class SweetTreatContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Flavor> Flavors { get; set; }
    public virtual DbSet<Treat> Treats { get; set; }
    public DbSet<FlavorTreat> FlavorTreats { get; set; }

    public SweetTreatContext(DbContextOptions options) : base(options) { }
  }
}