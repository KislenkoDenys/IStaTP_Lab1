using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostDomain.Model;

public partial class Tariff : Entity
{
    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Назва")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Ціна за кг")]
    public decimal PricePerKg { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Максимальна вага")]
    public double MaxWeight { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Максимальний об'єм")]
    public double MaxVolumeCm3 { get; set; }

    public virtual ICollection<Parcel> Parcels { get; set; } = new List<Parcel>();
}
