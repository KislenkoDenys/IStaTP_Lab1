using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostDomain.Model;

public partial class Branch : Entity
{
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Номер телефону")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Години роботи")]
    public string? WorkingHours { get; set; }

    public virtual ICollection<Courier> Couriers { get; set; } = new List<Courier>();

    [Display(Name = "№ Локації")]
    public virtual BranchLocation Location { get; set; } = null!;

    public virtual ICollection<Parcel> ParcelReceiverBranches { get; set; } = new List<Parcel>();

    public virtual ICollection<Parcel> ParcelSenderBranches { get; set; } = new List<Parcel>();
}
