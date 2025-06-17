using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class AccountType
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
