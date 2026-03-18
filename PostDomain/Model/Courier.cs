using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class Courier : Entity
{
    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int? VehicleTypeId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public bool? IsAvailable { get; set; }

    public int BranchId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<ParcelCourier> ParcelCouriers { get; set; } = new List<ParcelCourier>();

    public virtual VehicleType? VehicleType { get; set; }
}
