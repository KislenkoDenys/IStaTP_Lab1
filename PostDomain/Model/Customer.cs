using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostDomain.Model;

public partial class Customer : Entity
{
    public int CityId { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Адреса")]
    public string? Adress { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Ім'я")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Прізвище")]
    public string Surname { get; set; } = null!;

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Номер телефону")]
    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string PasswordHash { get; set; } = null!;

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Parcel> ParcelReceivers { get; set; } = new List<Parcel>();

    public virtual ICollection<Parcel> ParcelSenders { get; set; } = new List<Parcel>();
}
