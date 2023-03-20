using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryCatalog.Models
{
  public class Author
  {
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "First Name can't be empty!")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name can't be empty!")]
    public string LastName { get; set; }

    public List<AuthorBook> AuthorBooks { get; }
  }
}