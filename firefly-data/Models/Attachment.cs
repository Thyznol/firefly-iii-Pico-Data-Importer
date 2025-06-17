using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class Attachment
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint UserId { get; set; }

    public ulong? UserGroupId { get; set; }

    public uint AttachableId { get; set; }

    public string AttachableType { get; set; } = null!;

    public string Md5 { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string Mime { get; set; } = null!;

    public uint Size { get; set; }

    public bool? Uploaded { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual UserGroup? UserGroup { get; set; }
}
