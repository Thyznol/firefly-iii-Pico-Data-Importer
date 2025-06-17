using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class GroupMembership
{
    public ulong Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint UserId { get; set; }

    public ulong UserGroupId { get; set; }

    public ulong UserRoleId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual UserGroup UserGroup { get; set; } = null!;

    public virtual UserRole UserRole { get; set; } = null!;
}
