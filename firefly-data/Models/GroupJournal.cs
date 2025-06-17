using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class GroupJournal
{
    public uint Id { get; set; }

    public uint TransactionGroupId { get; set; }

    public uint TransactionJournalId { get; set; }

    public virtual TransactionGroup TransactionGroup { get; set; } = null!;

    public virtual TransactionJournal TransactionJournal { get; set; } = null!;
}
