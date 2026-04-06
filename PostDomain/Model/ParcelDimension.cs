using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostDomain.Model;

public partial class ParcelDimension
{
    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Id посилки")]
    public int ParcelId { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Довжина в см")]
    public decimal LengthCm { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Ширина в см")]
    public decimal WidthCm { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Висота в см")]
    public decimal HeightCm { get; set; }

    public virtual Parcel Parcel { get; set; } = null!;
}
