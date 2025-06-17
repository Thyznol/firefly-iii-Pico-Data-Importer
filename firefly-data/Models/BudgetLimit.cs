using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class BudgetLimit
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint BudgetId { get; set; }

    public uint? TransactionCurrencyId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal Amount { get; set; }

    public string? Period { get; set; }

    public bool Generated { get; set; }

    public virtual Budget Budget { get; set; } = null!;

    public virtual ICollection<BudgetTransactionJournal> BudgetTransactionJournals { get; } = new List<BudgetTransactionJournal>();

    public virtual ICollection<LimitRepetition> LimitRepetitions { get; } = new List<LimitRepetition>();

    public virtual TransactionCurrency? TransactionCurrency { get; set; }
}
