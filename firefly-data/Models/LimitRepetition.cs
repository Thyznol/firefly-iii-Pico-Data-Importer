using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class LimitRepetition
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint BudgetLimitId { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public decimal Amount { get; set; }

    public virtual BudgetLimit BudgetLimit { get; set; } = null!;
}
