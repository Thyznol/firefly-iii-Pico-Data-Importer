using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class OauthAccessToken
{
    public string Id { get; set; } = null!;

    public int? UserId { get; set; }

    public int ClientId { get; set; }

    public string? Name { get; set; }

    public string? Scopes { get; set; }

    public bool Revoked { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
