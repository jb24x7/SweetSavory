using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryCatalog.Models;

namespace LibraryCatalog.Models
{

  public class LibraryCatalogContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }
    public LibraryCatalogContext(DbContextOptions options) : base(options) { }
  }
}