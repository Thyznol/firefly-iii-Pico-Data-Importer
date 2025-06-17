using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class OauthRefreshToken
{
    public string Id { get; set; } = null!;

    public string AccessTokenId { get; set; } = null!;

    public bool Revoked { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
