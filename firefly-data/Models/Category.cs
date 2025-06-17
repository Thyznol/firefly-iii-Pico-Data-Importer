using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class Category
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint UserId { get; set; }

    public ulong? UserGroupId { get; set; }

    public string Name { get; set; } = null!;

    public bool Encrypted { get; set; }

    public virtual ICollection<CategoryTransactionJournal> CategoryTransactionJournals { get; } = new List<CategoryTransactionJournal>();

    public virtual ICollection<CategoryTransaction> CategoryTransactions { get; } = new List<CategoryTransaction>();

    public virtual User User { get; set; } = null!;

    public virtual UserGroup? UserGroup { get; set; }
}
