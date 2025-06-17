using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class AuditLogEntry
{
    public ulong Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint AuditableId { get; set; }

    public string AuditableType { get; set; } = null!;

    public uint ChangerId { get; set; }

    public string ChangerType { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string? Before { get; set; }

    public string? After { get; set; }
}
