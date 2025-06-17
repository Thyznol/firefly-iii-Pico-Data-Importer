using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class ObjectGroupable
{
    public int ObjectGroupId { get; set; }

    public uint ObjectGroupableId { get; set; }

    public string ObjectGroupableType { get; set; } = null!;
}
