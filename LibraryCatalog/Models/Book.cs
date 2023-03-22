using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCatalog.Models
{
  public class Book
  {
    public int BookId { get; set; }
    
    [Required(ErrorMessage = "Title can't be empty!")]
    public string Title { get; set; }
    public int Copies { get; set; }
    public List<AuthorBook> AuthorBooks { get; }



    public string UserID { get; set; }
    [ForeignKey("UserID")]
    public virtual ApplicationUser ApplicationUser { get; set; }
  }
}