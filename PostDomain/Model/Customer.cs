using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class Customer : Entity
{
    public int CityId { get; set; }

    public string? Adress { get; set; }

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string PasswordHash { get; set; } = null!;

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Parcel> ParcelReceivers { get; set; } = new List<Parcel>();

    public virtual ICollection<Parcel> ParcelSenders { get; set; } = new List<Parcel>();
}
