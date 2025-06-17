using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class AvailableBudget
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint UserId { get; set; }

    public ulong? UserGroupId { get; set; }

    public uint TransactionCurrencyId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual TransactionCurrency TransactionCurrency { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual UserGroup? UserGroup { get; set; }
}
