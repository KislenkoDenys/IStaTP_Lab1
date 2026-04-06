using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PostDomain.Model;

public partial class VehicleType : Entity
{
    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Назва")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Courier> Couriers { get; set; } = new List<Courier>();
}
