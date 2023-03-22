using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryCatalog.Models
{
  public class Copy
  {
    public int CopyId { get; set; }
    public Book Book { get; set; }
  }
}