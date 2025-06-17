using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class ObjectGroup
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Title { get; set; } = null!;

    public uint Order { get; set; }

    public virtual User User { get; set; } = null!;
}
