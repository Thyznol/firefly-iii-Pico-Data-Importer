using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class JournalLink
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint LinkTypeId { get; set; }

    public uint SourceId { get; set; }

    public uint DestinationId { get; set; }

    public string? Comment { get; set; }

    public virtual TransactionJournal Destination { get; set; } = null!;

    public virtual LinkType LinkType { get; set; } = null!;

    public virtual TransactionJournal Source { get; set; } = null!;
}
