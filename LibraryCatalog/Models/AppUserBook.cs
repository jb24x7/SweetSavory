using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryCatalog.Models
{
  public class AppUserBook
  {
    public int AppUserBookId { get; set; }
    public string UserID { get; set; }
    [ForeignKey("UserID")]
    public virtual ApplicationUser ApplicationUser { get; set; }
    public int BookId { get; set; } 
    public Book Book { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public Boolean Returned { get; set ; }
  }
}