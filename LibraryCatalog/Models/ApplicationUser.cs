using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCatalog.Models
{
  public class ApplicationUser : IdentityUser
  {
    public virtual List<Book> Books { get; set; }

    public List<BooksHistory> JoinEntites { get; }

  }
}