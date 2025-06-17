using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class OauthClient
{
    public uint Id { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Secret { get; set; }

    public string Redirect { get; set; } = null!;

    public bool PersonalAccessClient { get; set; }

    public bool PasswordClient { get; set; }

    public bool Revoked { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Provider { get; set; }
}
