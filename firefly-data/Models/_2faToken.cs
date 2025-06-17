using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class _2faToken
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public DateTime ExpiresAt { get; set; }

    public string Token { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
