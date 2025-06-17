using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class InvitedUser
{
    public ulong Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint UserId { get; set; }

    public string Email { get; set; } = null!;

    public string InviteCode { get; set; } = null!;

    public DateTime Expires { get; set; }

    public bool Redeemed { get; set; }

    public virtual User User { get; set; } = null!;
}
