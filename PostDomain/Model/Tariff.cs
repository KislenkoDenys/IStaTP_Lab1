using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class Tariff : Entity
{
    public string Name { get; set; } = null!;

    public decimal PricePerKg { get; set; }

    public double MaxWeight { get; set; }

    public double MaxVolumeCm3 { get; set; }

    public virtual ICollection<Parcel> Parcels { get; set; } = new List<Parcel>();
}
