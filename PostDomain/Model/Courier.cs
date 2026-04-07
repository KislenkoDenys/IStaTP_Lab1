using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostDomain.Model;

public partial class Courier : Entity
{
    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Ім'я")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Прізвище")]
    public string Surname { get; set; } = null!;

    public int? VehicleTypeId { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Номер телефона")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Доступність")]
    public bool? IsAvailable { get; set; }

    public int BranchId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<ParcelCourier> ParcelCouriers { get; set; } = new List<ParcelCourier>();

    public virtual VehicleType? VehicleType { get; set; }
}
