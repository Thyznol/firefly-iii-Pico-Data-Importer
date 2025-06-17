using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class ExportJob
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint UserId { get; set; }

    public string Key { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
