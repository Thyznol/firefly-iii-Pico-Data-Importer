using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class OauthPersonalAccessClient
{
    public uint Id { get; set; }

    public int ClientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
