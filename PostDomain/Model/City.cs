using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostDomain.Model;

public partial class City : Entity
{
    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display (Name = "Назва")]
    public string Name { get; set; } = null!;

    [Display(Name = "Країна")]
    public string Country { get; set; } = null!;

    public virtual ICollection<BranchLocation> BranchLocations { get; set; } = new List<BranchLocation>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Parcel> Parcels { get; set; } = new List<Parcel>();
}
