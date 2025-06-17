using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class CategoryTransaction
{
    public uint Id { get; set; }

    public uint CategoryId { get; set; }

    public uint TransactionId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Transaction Transaction { get; set; } = null!;
}
