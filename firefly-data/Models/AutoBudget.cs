using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class AutoBudget
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint BudgetId { get; set; }

    public uint TransactionCurrencyId { get; set; }

    public byte AutoBudgetType { get; set; }

    public decimal Amount { get; set; }

    public string Period { get; set; } = null!;

    public virtual Budget Budget { get; set; } = null!;

    public virtual TransactionCurrency? TransactionCurrency { get; set; } = null!;
}
