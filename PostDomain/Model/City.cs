using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class City : Entity
{
    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<BranchLocation> BranchLocations { get; set; } = new List<BranchLocation>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Parcel> Parcels { get; set; } = new List<Parcel>();
}
