using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class ParcelCourier : Entity
{
    public int ParcelId { get; set; }

    public int CourierId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public DateTime? PickedUpAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public virtual Courier Courier { get; set; } = null!;

    public virtual Parcel Parcel { get; set; } = null!;
}
