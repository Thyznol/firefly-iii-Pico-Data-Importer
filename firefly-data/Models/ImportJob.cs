using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class ImportJob
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint UserId { get; set; }

    public uint? TagId { get; set; }

    public string Key { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Stage { get; set; } = null!;

    public string? Configuration { get; set; }

    public string? ExtendedStatus { get; set; }

    public string? Transactions { get; set; }

    public string? Errors { get; set; }

    public virtual Tag? Tag { get; set; }

    public virtual User User { get; set; } = null!;
}
