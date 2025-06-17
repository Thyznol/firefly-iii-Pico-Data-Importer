using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class LinkType
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Name { get; set; } = null!;

    public string Outward { get; set; } = null!;

    public string Inward { get; set; } = null!;

    public bool Editable { get; set; }

    public virtual ICollection<JournalLink> JournalLinks { get; } = new List<JournalLink>();
}
