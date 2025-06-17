using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class JournalMetum
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint TransactionJournalId { get; set; }

    public string Name { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }

    public virtual TransactionJournal TransactionJournal { get; set; } = null!;
}
