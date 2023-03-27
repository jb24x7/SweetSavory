using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetTreat.Models
{
  public class Treat
  {
    public int TreatId { get; set; }
    
    [Required(ErrorMessage = "Name can't be empty!")]
    public string Name { get; set; }
    public List<FlavorTreat> FlavorTreats { get; }


  }
}