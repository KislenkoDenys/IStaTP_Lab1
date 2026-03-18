using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class VehicleType : Entity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Courier> Couriers { get; set; } = new List<Courier>();
}
