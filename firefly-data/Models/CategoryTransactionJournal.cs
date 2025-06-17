using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class CategoryTransactionJournal
{
    public uint Id { get; set; }

    public uint CategoryId { get; set; }

    public uint TransactionJournalId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual TransactionJournal TransactionJournal { get; set; } = null!;
}
