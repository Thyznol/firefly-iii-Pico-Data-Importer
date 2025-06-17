using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class AccountMetum
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint AccountId { get; set; }

    public string Name { get; set; } = null!;

    public string Data { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;
}
