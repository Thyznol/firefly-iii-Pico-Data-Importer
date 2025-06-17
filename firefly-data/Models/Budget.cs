using System.Text.Json.Serialization;

namespace firefly_iii_odata.Models;

public partial class Budget
{
    public uint Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public uint UserId { get; set; }

    public ulong? UserGroupId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public bool Encrypted { get; set; }

    public uint Order { get; set; }

    public virtual ICollection<AutoBudget> AutoBudgets { get; } = new List<AutoBudget>();

    public virtual ICollection<BudgetLimit> BudgetLimits { get; } = new List<BudgetLimit>();

    public virtual ICollection<BudgetTransactionJournal> BudgetTransactionJournals { get; } = new List<BudgetTransactionJournal>();

    public virtual ICollection<BudgetTransaction> BudgetTransactions { get; } = new List<BudgetTransaction>();

    public virtual User User { get; set; } = null!;

    public virtual UserGroup? UserGroup { get; set; }
}
