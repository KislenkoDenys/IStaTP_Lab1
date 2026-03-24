using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostDomain.Model;

public partial class BranchLocation : Entity
{
    [Display(Name = "Назва міста")]
    public int CityId { get; set; }
    [Display(Name = "Вулиця")]
    public string Street { get; set; } = null!;
    [Display(Name = "Номер будинку")]
    public string Building { get; set; } = null!;
    [Display(Name = "Поштовий код")]
    public string PostalCode { get; set; } = null!;

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
    [Display(Name = "Місто")]
    public virtual City City { get; set; } = null!;
}
