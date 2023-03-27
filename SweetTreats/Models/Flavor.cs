using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace SweetTreat.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }

    [Required(ErrorMessage = "First Name can't be empty!")]
    public string Taste { get; set; }
}

    public List<FlavorTreat> FlavorTreats { get; }
}