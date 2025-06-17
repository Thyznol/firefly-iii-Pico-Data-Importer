using System;
using System.Collections.Generic;

namespace firefly_iii_odata.Models;

public partial class CurrencyExchangeRate
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint UserId { get; set; }

    public ulong? UserGroupId { get; set; }

    public uint FromCurrencyId { get; set; }

    public uint ToCurrencyId { get; set; }

    public DateOnly Date { get; set; }

    public decimal Rate { get; set; }

    public decimal? UserRate { get; set; }

    public virtual TransactionCurrency FromCurrency { get; set; } = null!;

    public virtual TransactionCurrency ToCurrency { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual UserGroup? UserGroup { get; set; }
}
