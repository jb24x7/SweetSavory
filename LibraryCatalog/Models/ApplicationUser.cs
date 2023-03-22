using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCatalog.Models
{
  public class ApplicationUser : IdentityUser
  {
    public virtual List<Book> Books { get; set; }

    [NotMapped]
    public virtual List<Book> BooksHistory { get; set; }

  }
}