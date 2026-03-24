using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class ParcelDimension
{
    public int ParcelId { get; set; }

    public decimal LengthCm { get; set; }

    public decimal WidthCm { get; set; }

    public decimal HeightCm { get; set; }

    public virtual Parcel Parcel { get; set; } = null!;
}
