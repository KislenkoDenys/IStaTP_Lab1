using System;
using System.Collections.Generic;

namespace PostDomain.Model;

public partial class BranchLocation : Entity
{
    public int CityId { get; set; }

    public string Street { get; set; } = null!;

    public string Building { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual City City { get; set; } = null!;
}
