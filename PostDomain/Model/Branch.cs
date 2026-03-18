using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class Branch : Entity
{
    public int LocationId { get; set; }

    public string Phone { get; set; } = null!;

    public string? WorkingHours { get; set; }

    public virtual ICollection<Courier> Couriers { get; set; } = new List<Courier>();

    public virtual BranchLocation Location { get; set; } = null!;

    public virtual ICollection<Parcel> ParcelReceiverBranches { get; set; } = new List<Parcel>();

    public virtual ICollection<Parcel> ParcelSenderBranches { get; set; } = new List<Parcel>();
}
