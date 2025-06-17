using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class BudgetTransaction
{
    public uint Id { get; set; }

    public uint BudgetId { get; set; }

    public uint TransactionId { get; set; }

    public virtual Budget Budget { get; set; } = null!;

    public virtual Transaction Transaction { get; set; } = null!;
}
