using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class Parcel : Entity
{
    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public int? SenderBranchId { get; set; }

    public int? ReceiverBranchId { get; set; }

    public int? DeliveryCityId { get; set; }

    public string? DeliveryAddress { get; set; }

    public int TariffId { get; set; }

    public double Weight { get; set; }

    public ParcelStatus Status { get; set; }

    public virtual City? DeliveryCity { get; set; }

    public virtual ICollection<ParcelCourier> ParcelCouriers { get; set; } = new List<ParcelCourier>();

    public virtual ParcelDimension? ParcelDimension { get; set; }

    public virtual Customer Receiver { get; set; } = null!;

    public virtual Branch? ReceiverBranch { get; set; }

    public virtual Customer Sender { get; set; } = null!;

    public virtual Branch? SenderBranch { get; set; }

    public virtual Tariff Tariff { get; set; } = null!;
}
