using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class BudgetTransactionJournal
{
    public uint Id { get; set; }

    public uint BudgetId { get; set; }

    public uint? BudgetLimitId { get; set; }

    public uint TransactionJournalId { get; set; }

    public virtual Budget Budget { get; set; } = null!;

    public virtual BudgetLimit? BudgetLimit { get; set; }

    public virtual TransactionJournal TransactionJournal { get; set; } = null!;
}
