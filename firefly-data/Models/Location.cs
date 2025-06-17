using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class Location
{
    public ulong Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint LocatableId { get; set; }

    public string LocatableType { get; set; } = null!;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public ushort? ZoomLevel { get; set; }
}
