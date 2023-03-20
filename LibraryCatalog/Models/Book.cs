using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryCatalog.Models
{
  public class Book
  {
    public int BookId { get; set; }
    
    [Required(ErrorMessage = "Title can't be empty!")]
    public string Title { get; set; }
    
    public List<AuthorBook> AuthorBooks { get; }
  }
}