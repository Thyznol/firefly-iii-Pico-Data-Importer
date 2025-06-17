using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class Bill
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint UserId { get; set; }

    public ulong? UserGroupId { get; set; }

    public uint? TransactionCurrencyId { get; set; }

    public string Name { get; set; } = null!;

    public string Match { get; set; } = null!;

    public decimal AmountMin { get; set; }

    public decimal AmountMax { get; set; }

    public DateOnly Date { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateOnly? ExtensionDate { get; set; }

    public string RepeatFreq { get; set; } = null!;

    public ushort Skip { get; set; }

    public bool? Automatch { get; set; }

    public bool? Active { get; set; }

    public bool NameEncrypted { get; set; }

    public bool MatchEncrypted { get; set; }

    public uint Order { get; set; }

    public virtual TransactionCurrency? TransactionCurrency { get; set; }

    public virtual ICollection<TransactionJournal> TransactionJournals { get; } = new List<TransactionJournal>();

    public virtual User User { get; set; } = null!;

    public virtual UserGroup? UserGroup { get; set; }
}
