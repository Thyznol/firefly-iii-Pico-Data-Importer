using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class Account
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint UserId { get; set; }

    public ulong? UserGroupId { get; set; }

    public uint AccountTypeId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? VirtualBalance { get; set; }

    public string? Iban { get; set; }

    public bool? Active { get; set; }

    public bool Encrypted { get; set; }

    public uint Order { get; set; }

    public virtual ICollection<AccountMetum> AccountMeta { get; } = new List<AccountMetum>();

    public virtual AccountType AccountType { get; set; } = null!;

    public virtual ICollection<PiggyBank> PiggyBanks { get; } = new List<PiggyBank>();

    public virtual ICollection<RecurrencesTransaction> RecurrencesTransactionDestinations { get; } = new List<RecurrencesTransaction>();

    public virtual ICollection<RecurrencesTransaction> RecurrencesTransactionSources { get; } = new List<RecurrencesTransaction>();

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;

    public virtual UserGroup? UserGroup { get; set; }
}
