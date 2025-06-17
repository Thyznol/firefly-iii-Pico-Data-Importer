using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class Permission
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}
