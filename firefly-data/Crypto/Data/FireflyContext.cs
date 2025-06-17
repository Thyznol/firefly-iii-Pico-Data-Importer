using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using firefly_iii_odata.Models;

namespace firefly_iii_odata.Data;

public partial class FireflyContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FireflyContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public FireflyContext(IHttpContextAccessor httpContextAccessor, DbContextOptions<FireflyContext> options)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountMetum> AccountMeta { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<AuditLogEntry> AuditLogEntries { get; set; }

    public virtual DbSet<AutoBudget> AutoBudgets { get; set; }

    public virtual DbSet<AvailableBudget> AvailableBudgets { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<BudgetLimit> BudgetLimits { get; set; }

    public virtual DbSet<BudgetTransaction> BudgetTransactions { get; set; }

    public virtual DbSet<BudgetTransactionJournal> BudgetTransactionJournals { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryTransaction> CategoryTransactions { get; set; }

    public virtual DbSet<CategoryTransactionJournal> CategoryTransactionJournals { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<CurrencyExchangeRate> CurrencyExchangeRates { get; set; }

    public virtual DbSet<ExportJob> ExportJobs { get; set; }

    public virtual DbSet<FailedJob> FailedJobs { get; set; }

    public virtual DbSet<GroupJournal> GroupJournals { get; set; }

    public virtual DbSet<GroupMembership> GroupMemberships { get; set; }

    public virtual DbSet<ImportJob> ImportJobs { get; set; }

    public virtual DbSet<InvitedUser> InvitedUsers { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JournalLink> JournalLinks { get; set; }

    public virtual DbSet<JournalMetum> JournalMeta { get; set; }

    public virtual DbSet<LimitRepetition> LimitRepetitions { get; set; }

    public virtual DbSet<LinkType> LinkTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<OauthAccessToken> OauthAccessTokens { get; set; }

    public virtual DbSet<OauthAuthCode> OauthAuthCodes { get; set; }

    public virtual DbSet<OauthClient> OauthClients { get; set; }

    public virtual DbSet<OauthPersonalAccessClient> OauthPersonalAccessClients { get; set; }

    public virtual DbSet<OauthRefreshToken> OauthRefreshTokens { get; set; }

    public virtual DbSet<ObjectGroup> ObjectGroups { get; set; }

    public virtual DbSet<ObjectGroupable> ObjectGroupables { get; set; }

    public virtual DbSet<PasswordReset> PasswordResets { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<PiggyBank> PiggyBanks { get; set; }

    public virtual DbSet<PiggyBankEvent> PiggyBankEvents { get; set; }

    public virtual DbSet<PiggyBankRepetition> PiggyBankRepetitions { get; set; }

    public virtual DbSet<Preference> Preferences { get; set; }

    public virtual DbSet<Recurrence> Recurrences { get; set; }

    public virtual DbSet<RecurrencesMetum> RecurrencesMeta { get; set; }

    public virtual DbSet<RecurrencesRepetition> RecurrencesRepetitions { get; set; }

    public virtual DbSet<RecurrencesTransaction> RecurrencesTransactions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RtMetum> RtMeta { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }

    public virtual DbSet<RuleAction> RuleActions { get; set; }

    public virtual DbSet<RuleGroup> RuleGroups { get; set; }

    public virtual DbSet<RuleTrigger> RuleTriggers { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagTransactionJournal> TagTransactionJournals { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionCurrency> TransactionCurrencies { get; set; }

    public virtual DbSet<TransactionGroup> TransactionGroups { get; set; }

    public virtual DbSet<TransactionJournal> TransactionJournals { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Webhook> Webhooks { get; set; }

    public virtual DbSet<WebhookAttempt> WebhookAttempts { get; set; }

    public virtual DbSet<WebhookMessage> WebhookMessages { get; set; }

    public virtual DbSet<_2faToken> _2faTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=Firefly", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.2-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("accounts")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.AccountTypeId, "accounts_account_type_id_foreign");

            entity.HasIndex(e => e.UserGroupId, "accounts_to_ugi");

            entity.HasIndex(e => e.UserId, "accounts_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.AccountTypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_type_id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Encrypted).HasColumnName("encrypted");
            entity.Property(e => e.Iban)
                .HasMaxLength(255)
                .HasColumnName("iban");
            entity.Property(e => e.Name)
                .HasMaxLength(1024)
                .HasColumnName("name");
            entity.Property(e => e.Order)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");
            entity.Property(e => e.VirtualBalance)
                .HasPrecision(36, 24)
                .HasColumnName("virtual_balance");

            entity.HasOne(d => d.AccountType).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AccountTypeId)
                .HasConstraintName("accounts_account_type_id_foreign");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("accounts_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("accounts_user_id_foreign");
        });

        modelBuilder.Entity<AccountMetum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("account_meta")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.AccountId, "account_meta_account_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Data)
                .HasColumnType("text")
                .HasColumnName("data");
            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountMeta)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("account_meta_account_id_foreign");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("account_types")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Type, "account_types_type_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("attachments")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "attachments_to_ugi");

            entity.HasIndex(e => e.UserId, "attachments_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.AttachableId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("attachable_id");
            entity.Property(e => e.AttachableType)
                .HasMaxLength(255)
                .HasColumnName("attachable_type");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Filename)
                .HasMaxLength(1024)
                .HasColumnName("filename");
            entity.Property(e => e.Md5)
                .HasMaxLength(128)
                .HasColumnName("md5");
            entity.Property(e => e.Mime)
                .HasMaxLength(1024)
                .HasColumnName("mime");
            entity.Property(e => e.Size)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("size");
            entity.Property(e => e.Title)
                .HasMaxLength(1024)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.Uploaded)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("uploaded");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("attachments_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("attachments_user_id_foreign");
        });

        modelBuilder.Entity<AuditLogEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("audit_log_entries")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .HasColumnName("action");
            entity.Property(e => e.After)
                .HasColumnType("text")
                .HasColumnName("after");
            entity.Property(e => e.AuditableId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("auditable_id");
            entity.Property(e => e.AuditableType)
                .HasMaxLength(191)
                .HasColumnName("auditable_type");
            entity.Property(e => e.Before)
                .HasColumnType("text")
                .HasColumnName("before");
            entity.Property(e => e.ChangerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("changer_id");
            entity.Property(e => e.ChangerType)
                .HasMaxLength(191)
                .HasColumnName("changer_type");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<AutoBudget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("auto_budgets")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.BudgetId, "auto_budgets_budget_id_foreign");

            entity.HasIndex(e => e.TransactionCurrencyId, "auto_budgets_transaction_currency_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(36, 24)
                .HasColumnName("amount");
            entity.Property(e => e.AutoBudgetType)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("auto_budget_type");
            entity.Property(e => e.BudgetId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("budget_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Period)
                .HasMaxLength(50)
                .HasColumnName("period");
            entity.Property(e => e.TransactionCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_currency_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Budget).WithMany(p => p.AutoBudgets)
                .HasForeignKey(d => d.BudgetId)
                .HasConstraintName("auto_budgets_budget_id_foreign");

            entity.HasOne(d => d.TransactionCurrency).WithMany(p => p.AutoBudgets)
                .HasForeignKey(d => d.TransactionCurrencyId)
                .HasConstraintName("auto_budgets_transaction_currency_id_foreign");
        });

        modelBuilder.Entity<AvailableBudget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("available_budgets")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "available_budgets_to_ugi");

            entity.HasIndex(e => e.TransactionCurrencyId, "available_budgets_transaction_currency_id_foreign");

            entity.HasIndex(e => e.UserId, "available_budgets_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(36, 24)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TransactionCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_currency_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.TransactionCurrency).WithMany(p => p.AvailableBudgets)
                .HasForeignKey(d => d.TransactionCurrencyId)
                .HasConstraintName("available_budgets_transaction_currency_id_foreign");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.AvailableBudgets)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("available_budgets_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.AvailableBudgets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("available_budgets_user_id_foreign");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("bills")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "bills_to_ugi");

            entity.HasIndex(e => e.TransactionCurrencyId, "bills_transaction_currency_id_foreign");

            entity.HasIndex(e => e.UserId, "bills_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.AmountMax)
                .HasPrecision(36, 24)
                .HasColumnName("amount_max");
            entity.Property(e => e.AmountMin)
                .HasPrecision(36, 24)
                .HasColumnName("amount_min");
            entity.Property(e => e.Automatch)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("automatch");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ExtensionDate).HasColumnName("extension_date");
            entity.Property(e => e.Match)
                .HasMaxLength(1024)
                .HasColumnName("match");
            entity.Property(e => e.MatchEncrypted).HasColumnName("match_encrypted");
            entity.Property(e => e.Name)
                .HasMaxLength(1024)
                .HasColumnName("name");
            entity.Property(e => e.NameEncrypted).HasColumnName("name_encrypted");
            entity.Property(e => e.Order)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.RepeatFreq)
                .HasMaxLength(30)
                .HasColumnName("repeat_freq");
            entity.Property(e => e.Skip)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("skip");
            entity.Property(e => e.TransactionCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_currency_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.TransactionCurrency).WithMany(p => p.Bills)
                .HasForeignKey(d => d.TransactionCurrencyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("bills_transaction_currency_id_foreign");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Bills)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("bills_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.Bills)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("bills_user_id_foreign");
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("budgets")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "budgets_to_ugi");

            entity.HasIndex(e => e.UserId, "budgets_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Encrypted).HasColumnName("encrypted");
            entity.Property(e => e.Name)
                .HasMaxLength(1024)
                .HasColumnName("name");
            entity.Property(e => e.Order)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("budgets_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("budgets_user_id_foreign");
        });

        modelBuilder.Entity<BudgetLimit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("budget_limits")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.BudgetId, "budget_limits_budget_id_foreign");

            entity.HasIndex(e => e.TransactionCurrencyId, "budget_limits_transaction_currency_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(36, 24)
                .HasColumnName("amount");
            entity.Property(e => e.BudgetId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("budget_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Generated).HasColumnName("generated");
            entity.Property(e => e.Period)
                .HasMaxLength(12)
                .HasColumnName("period");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TransactionCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_currency_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Budget).WithMany(p => p.BudgetLimits)
                .HasForeignKey(d => d.BudgetId)
                .HasConstraintName("budget_limits_budget_id_foreign");

            entity.HasOne(d => d.TransactionCurrency).WithMany(p => p.BudgetLimits)
                .HasForeignKey(d => d.TransactionCurrencyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("budget_limits_transaction_currency_id_foreign");
        });

        modelBuilder.Entity<BudgetTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("budget_transaction")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.BudgetId, "budget_transaction_budget_id_foreign");

            entity.HasIndex(e => e.TransactionId, "budget_transaction_transaction_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.BudgetId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("budget_id");
            entity.Property(e => e.TransactionId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_id");

            entity.HasOne(d => d.Budget).WithMany(p => p.BudgetTransactions)
                .HasForeignKey(d => d.BudgetId)
                .HasConstraintName("budget_transaction_budget_id_foreign");

            entity.HasOne(d => d.Transaction).WithMany(p => p.BudgetTransactions)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("budget_transaction_transaction_id_foreign");
        });

        modelBuilder.Entity<BudgetTransactionJournal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("budget_transaction_journal")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.BudgetLimitId, "budget_id_foreign");

            entity.HasIndex(e => e.BudgetId, "budget_transaction_journal_budget_id_foreign");

            entity.HasIndex(e => e.TransactionJournalId, "budget_transaction_journal_transaction_journal_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.BudgetId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("budget_id");
            entity.Property(e => e.BudgetLimitId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("budget_limit_id");
            entity.Property(e => e.TransactionJournalId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_journal_id");

            entity.HasOne(d => d.Budget).WithMany(p => p.BudgetTransactionJournals)
                .HasForeignKey(d => d.BudgetId)
                .HasConstraintName("budget_transaction_journal_budget_id_foreign");

            entity.HasOne(d => d.BudgetLimit).WithMany(p => p.BudgetTransactionJournals)
                .HasForeignKey(d => d.BudgetLimitId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("budget_id_foreign");

            entity.HasOne(d => d.TransactionJournal).WithMany(p => p.BudgetTransactionJournals)
                .HasForeignKey(d => d.TransactionJournalId)
                .HasConstraintName("budget_transaction_journal_transaction_journal_id_foreign");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("categories")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "categories_to_ugi");

            entity.HasIndex(e => e.UserId, "categories_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Encrypted).HasColumnName("encrypted");
            entity.Property(e => e.Name)
                .HasMaxLength(1024)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Categories)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("categories_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.Categories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("categories_user_id_foreign");
        });

        modelBuilder.Entity<CategoryTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("category_transaction")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.CategoryId, "category_transaction_category_id_foreign");

            entity.HasIndex(e => e.TransactionId, "category_transaction_transaction_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CategoryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("category_id");
            entity.Property(e => e.TransactionId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_id");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryTransactions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("category_transaction_category_id_foreign");

            entity.HasOne(d => d.Transaction).WithMany(p => p.CategoryTransactions)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("category_transaction_transaction_id_foreign");
        });

        modelBuilder.Entity<CategoryTransactionJournal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("category_transaction_journal")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.CategoryId, "category_transaction_journal_category_id_foreign");

            entity.HasIndex(e => e.TransactionJournalId, "category_transaction_journal_transaction_journal_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CategoryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("category_id");
            entity.Property(e => e.TransactionJournalId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_journal_id");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryTransactionJournals)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("category_transaction_journal_category_id_foreign");

            entity.HasOne(d => d.TransactionJournal).WithMany(p => p.CategoryTransactionJournals)
                .HasForeignKey(d => d.TransactionJournalId)
                .HasConstraintName("category_transaction_journal_transaction_journal_id_foreign");
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("configuration")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Data)
                .HasColumnType("text")
                .HasColumnName("data");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<CurrencyExchangeRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("currency_exchange_rates")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "cer_to_ugi");

            entity.HasIndex(e => e.FromCurrencyId, "currency_exchange_rates_from_currency_id_foreign");

            entity.HasIndex(e => e.ToCurrencyId, "currency_exchange_rates_to_currency_id_foreign");

            entity.HasIndex(e => e.UserId, "currency_exchange_rates_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.FromCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("from_currency_id");
            entity.Property(e => e.Rate)
                .HasPrecision(36, 24)
                .HasColumnName("rate");
            entity.Property(e => e.ToCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("to_currency_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");
            entity.Property(e => e.UserRate)
                .HasPrecision(36, 24)
                .HasColumnName("user_rate");

            entity.HasOne(d => d.FromCurrency).WithMany(p => p.CurrencyExchangeRateFromCurrencies)
                .HasForeignKey(d => d.FromCurrencyId)
                .HasConstraintName("currency_exchange_rates_from_currency_id_foreign");

            entity.HasOne(d => d.ToCurrency).WithMany(p => p.CurrencyExchangeRateToCurrencies)
                .HasForeignKey(d => d.ToCurrencyId)
                .HasConstraintName("currency_exchange_rates_to_currency_id_foreign");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.CurrencyExchangeRates)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("cer_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.CurrencyExchangeRates)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("currency_exchange_rates_user_id_foreign");
        });

        modelBuilder.Entity<ExportJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("export_jobs")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "export_jobs_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Key)
                .HasMaxLength(12)
                .HasColumnName("key");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ExportJobs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("export_jobs_user_id_foreign");
        });

        modelBuilder.Entity<FailedJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("failed_jobs")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Uuid, "failed_jobs_uuid_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Connection)
                .HasColumnType("text")
                .HasColumnName("connection");
            entity.Property(e => e.Exception).HasColumnName("exception");
            entity.Property(e => e.FailedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("failed_at");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.Queue)
                .HasColumnType("text")
                .HasColumnName("queue");
            entity.Property(e => e.Uuid)
                .HasMaxLength(191)
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<GroupJournal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("group_journals")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.TransactionJournalId, "group_journals_transaction_journal_id_foreign");

            entity.HasIndex(e => new { e.TransactionGroupId, e.TransactionJournalId }, "unique_in_group").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.TransactionGroupId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_group_id");
            entity.Property(e => e.TransactionJournalId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_journal_id");

            entity.HasOne(d => d.TransactionGroup).WithMany(p => p.GroupJournals)
                .HasForeignKey(d => d.TransactionGroupId)
                .HasConstraintName("group_journals_transaction_group_id_foreign");

            entity.HasOne(d => d.TransactionJournal).WithMany(p => p.GroupJournals)
                .HasForeignKey(d => d.TransactionJournalId)
                .HasConstraintName("group_journals_transaction_journal_id_foreign");
        });

        modelBuilder.Entity<GroupMembership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("group_memberships")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "group_memberships_user_group_id_foreign");

            entity.HasIndex(e => new { e.UserId, e.UserGroupId, e.UserRoleId }, "group_memberships_user_id_user_group_id_user_role_id_unique").IsUnique();

            entity.HasIndex(e => e.UserRoleId, "group_memberships_user_role_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");
            entity.Property(e => e.UserRoleId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_role_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.GroupMemberships)
                .HasForeignKey(d => d.UserGroupId)
                .HasConstraintName("group_memberships_user_group_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.GroupMemberships)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("group_memberships_user_id_foreign");

            entity.HasOne(d => d.UserRole).WithMany(p => p.GroupMemberships)
                .HasForeignKey(d => d.UserRoleId)
                .HasConstraintName("group_memberships_user_role_id_foreign");
        });

        modelBuilder.Entity<ImportJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("import_jobs")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Key, "import_jobs_key_unique").IsUnique();

            entity.HasIndex(e => e.TagId, "import_jobs_tag_id_foreign");

            entity.HasIndex(e => e.UserId, "import_jobs_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Configuration)
                .HasColumnType("text")
                .HasColumnName("configuration");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Errors).HasColumnName("errors");
            entity.Property(e => e.ExtendedStatus)
                .HasColumnType("text")
                .HasColumnName("extended_status");
            entity.Property(e => e.FileType)
                .HasMaxLength(12)
                .HasColumnName("file_type");
            entity.Property(e => e.Key)
                .HasMaxLength(12)
                .HasColumnName("key");
            entity.Property(e => e.Provider)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("provider");
            entity.Property(e => e.Stage)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("stage");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .HasColumnName("status");
            entity.Property(e => e.TagId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("tag_id");
            entity.Property(e => e.Transactions).HasColumnName("transactions");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Tag).WithMany(p => p.ImportJobs)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("import_jobs_tag_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.ImportJobs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("import_jobs_user_id_foreign");
        });

        modelBuilder.Entity<InvitedUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("invited_users")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "invited_users_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Expires)
                .HasColumnType("datetime")
                .HasColumnName("expires");
            entity.Property(e => e.InviteCode)
                .HasMaxLength(64)
                .HasColumnName("invite_code");
            entity.Property(e => e.Redeemed).HasColumnName("redeemed");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.InvitedUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("invited_users_user_id_foreign");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("jobs")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Queue, "jobs_queue_index");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Attempts)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("attempts");
            entity.Property(e => e.AvailableAt)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("available_at");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("created_at");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.Queue)
                .HasMaxLength(191)
                .HasColumnName("queue");
            entity.Property(e => e.ReservedAt)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("reserved_at");
        });

        modelBuilder.Entity<JournalLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("journal_links")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.DestinationId, "journal_links_destination_id_foreign");

            entity.HasIndex(e => new { e.LinkTypeId, e.SourceId, e.DestinationId }, "journal_links_link_type_id_source_id_destination_id_unique").IsUnique();

            entity.HasIndex(e => e.SourceId, "journal_links_source_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DestinationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("destination_id");
            entity.Property(e => e.LinkTypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("link_type_id");
            entity.Property(e => e.SourceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("source_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Destination).WithMany(p => p.JournalLinkDestinations)
                .HasForeignKey(d => d.DestinationId)
                .HasConstraintName("journal_links_destination_id_foreign");

            entity.HasOne(d => d.LinkType).WithMany(p => p.JournalLinks)
                .HasForeignKey(d => d.LinkTypeId)
                .HasConstraintName("journal_links_link_type_id_foreign");

            entity.HasOne(d => d.Source).WithMany(p => p.JournalLinkSources)
                .HasForeignKey(d => d.SourceId)
                .HasConstraintName("journal_links_source_id_foreign");
        });

        modelBuilder.Entity<JournalMetum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("journal_meta")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.TransactionJournalId, "journal_meta_transaction_journal_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Data)
                .HasColumnType("text")
                .HasColumnName("data");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Hash)
                .HasMaxLength(64)
                .HasColumnName("hash");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TransactionJournalId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_journal_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.TransactionJournal).WithMany(p => p.JournalMeta)
                .HasForeignKey(d => d.TransactionJournalId)
                .HasConstraintName("journal_meta_transaction_journal_id_foreign");
        });

        modelBuilder.Entity<LimitRepetition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("limit_repetitions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.BudgetLimitId, "limit_repetitions_budget_limit_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(36, 24)
                .HasColumnName("amount");
            entity.Property(e => e.BudgetLimitId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("budget_limit_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.BudgetLimit).WithMany(p => p.LimitRepetitions)
                .HasForeignKey(d => d.BudgetLimitId)
                .HasConstraintName("limit_repetitions_budget_limit_id_foreign");
        });

        modelBuilder.Entity<LinkType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("link_types")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.Name, e.Outward, e.Inward }, "link_types_name_outward_inward_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Editable).HasColumnName("editable");
            entity.Property(e => e.Inward)
                .HasMaxLength(191)
                .HasColumnName("inward");
            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");
            entity.Property(e => e.Outward)
                .HasMaxLength(191)
                .HasColumnName("outward");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("locations")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Latitude)
                .HasPrecision(36, 24)
                .HasColumnName("latitude");
            entity.Property(e => e.LocatableId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("locatable_id");
            entity.Property(e => e.LocatableType)
                .HasMaxLength(255)
                .HasColumnName("locatable_type");
            entity.Property(e => e.Longitude)
                .HasPrecision(36, 24)
                .HasColumnName("longitude");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.ZoomLevel)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("zoom_level");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("migrations")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Batch)
                .HasColumnType("int(11)")
                .HasColumnName("batch");
            entity.Property(e => e.Migration1)
                .HasMaxLength(191)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("notes")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.NoteableId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("noteable_id");
            entity.Property(e => e.NoteableType)
                .HasMaxLength(191)
                .HasColumnName("noteable_type");
            entity.Property(e => e.Text)
                .HasColumnType("text")
                .HasColumnName("text");
            entity.Property(e => e.Title)
                .HasMaxLength(191)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("notifications")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.NotifiableType, e.NotifiableId }, "notifications_notifiable_type_notifiable_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Data)
                .HasColumnType("text")
                .HasColumnName("data");
            entity.Property(e => e.NotifiableId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("notifiable_id");
            entity.Property(e => e.NotifiableType)
                .HasMaxLength(191)
                .HasColumnName("notifiable_type");
            entity.Property(e => e.ReadAt)
                .HasColumnType("timestamp")
                .HasColumnName("read_at");
            entity.Property(e => e.Type)
                .HasMaxLength(191)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<OauthAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("oauth_access_tokens")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "oauth_access_tokens_user_id_index");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasColumnType("int(11)")
                .HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
            entity.Property(e => e.Scopes)
                .HasColumnType("text")
                .HasColumnName("scopes");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<OauthAuthCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("oauth_auth_codes")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasColumnType("int(11)")
                .HasColumnName("client_id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
            entity.Property(e => e.Scopes)
                .HasColumnType("text")
                .HasColumnName("scopes");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<OauthClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("oauth_clients")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "oauth_clients_user_id_index");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");
            entity.Property(e => e.PasswordClient).HasColumnName("password_client");
            entity.Property(e => e.PersonalAccessClient).HasColumnName("personal_access_client");
            entity.Property(e => e.Provider)
                .HasMaxLength(191)
                .HasColumnName("provider");
            entity.Property(e => e.Redirect)
                .HasColumnType("text")
                .HasColumnName("redirect");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
            entity.Property(e => e.Secret)
                .HasMaxLength(100)
                .HasColumnName("secret");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<OauthPersonalAccessClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("oauth_personal_access_clients")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ClientId, "oauth_personal_access_clients_client_id_index");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasColumnType("int(11)")
                .HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<OauthRefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("oauth_refresh_tokens")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.AccessTokenId, "oauth_refresh_tokens_access_token_id_index");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.AccessTokenId)
                .HasMaxLength(100)
                .HasColumnName("access_token_id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
        });

        modelBuilder.Entity<ObjectGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("object_groups")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "object_groups_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Order)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ObjectGroups)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("object_groups_user_id_foreign");
        });

        modelBuilder.Entity<ObjectGroupable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("object_groupables")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.ObjectGroupId)
                .HasColumnType("int(11)")
                .HasColumnName("object_group_id");
            entity.Property(e => e.ObjectGroupableId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("object_groupable_id");
            entity.Property(e => e.ObjectGroupableType)
                .HasMaxLength(255)
                .HasColumnName("object_groupable_type");
        });

        modelBuilder.Entity<PasswordReset>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("password_resets")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Email, "password_resets_email_index");

            entity.HasIndex(e => e.Token, "password_resets_token_index");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(191)
                .HasColumnName("email");
            entity.Property(e => e.Token)
                .HasMaxLength(191)
                .HasColumnName("token");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("permissions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Name, "permissions_name_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(191)
                .HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(191)
                .HasColumnName("display_name");
            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasMany(d => d.Roles).WithMany(p => p.Permissions)
                .UsingEntity<Dictionary<string, object>>(
                    "PermissionRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("permission_role_role_id_foreign"),
                    l => l.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("permission_role_permission_id_foreign"),
                    j =>
                    {
                        j.HasKey("PermissionId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("permission_role")
                            .UseCollation("utf8mb4_unicode_ci");
                        j.HasIndex(new[] { "RoleId" }, "permission_role_role_id_foreign");
                        j.IndexerProperty<uint>("PermissionId")
                            .HasColumnType("int(10) unsigned")
                            .HasColumnName("permission_id");
                        j.IndexerProperty<uint>("RoleId")
                            .HasColumnType("int(10) unsigned")
                            .HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("personal_access_tokens")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Token, "personal_access_tokens_token_unique").IsUnique();

            entity.HasIndex(e => new { e.TokenableType, e.TokenableId }, "personal_access_tokens_tokenable_type_tokenable_id_index");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Abilities)
                .HasColumnType("text")
                .HasColumnName("abilities");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.LastUsedAt)
                .HasColumnType("timestamp")
                .HasColumnName("last_used_at");
            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");
            entity.Property(e => e.Token)
                .HasMaxLength(64)
                .HasColumnName("token");
            entity.Property(e => e.TokenableId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("tokenable_id");
            entity.Property(e => e.TokenableType)
                .HasMaxLength(191)
                .HasColumnName("tokenable_type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PiggyBank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("piggy_banks")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.AccountId, "piggy_banks_account_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Encrypted)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("encrypted");
            entity.Property(e => e.Name)
                .HasMaxLength(1024)
                .HasColumnName("name");
            entity.Property(e => e.Order)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Targetamount)
                .HasPrecision(36, 24)
                .HasColumnName("targetamount");
            entity.Property(e => e.Targetdate).HasColumnName("targetdate");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Account).WithMany(p => p.PiggyBanks)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("piggy_banks_account_id_foreign");
        });

        modelBuilder.Entity<PiggyBankEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("piggy_bank_events")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.PiggyBankId, "piggy_bank_events_piggy_bank_id_foreign");

            entity.HasIndex(e => e.TransactionJournalId, "piggy_bank_events_transaction_journal_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(36, 24)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.PiggyBankId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("piggy_bank_id");
            entity.Property(e => e.TransactionJournalId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_journal_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.PiggyBank).WithMany(p => p.PiggyBankEvents)
                .HasForeignKey(d => d.PiggyBankId)
                .HasConstraintName("piggy_bank_events_piggy_bank_id_foreign");

            entity.HasOne(d => d.TransactionJournal).WithMany(p => p.PiggyBankEvents)
                .HasForeignKey(d => d.TransactionJournalId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("piggy_bank_events_transaction_journal_id_foreign");
        });

        modelBuilder.Entity<PiggyBankRepetition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("piggy_bank_repetitions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.PiggyBankId, "piggy_bank_repetitions_piggy_bank_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Currentamount)
                .HasPrecision(36, 24)
                .HasColumnName("currentamount");
            entity.Property(e => e.PiggyBankId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("piggy_bank_id");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Targetdate).HasColumnName("targetdate");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.PiggyBank).WithMany(p => p.PiggyBankRepetitions)
                .HasForeignKey(d => d.PiggyBankId)
                .HasConstraintName("piggy_bank_repetitions_piggy_bank_id_foreign");
        });

        modelBuilder.Entity<Preference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("preferences")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserId, "preferences_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Data)
                .HasColumnType("text")
                .HasColumnName("data");
            entity.Property(e => e.Name)
                .HasMaxLength(1024)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Preferences)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("preferences_user_id_foreign");
        });

        modelBuilder.Entity<Recurrence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("recurrences")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "recurrences_to_ugi");

            entity.HasIndex(e => e.TransactionTypeId, "recurrences_transaction_type_id_foreign");

            entity.HasIndex(e => e.UserId, "recurrences_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.ApplyRules)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("apply_rules");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.FirstDate).HasColumnName("first_date");
            entity.Property(e => e.LatestDate).HasColumnName("latest_date");
            entity.Property(e => e.RepeatUntil).HasColumnName("repeat_until");
            entity.Property(e => e.Repetitions)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("repetitions");
            entity.Property(e => e.Title)
                .HasMaxLength(1024)
                .HasColumnName("title");
            entity.Property(e => e.TransactionTypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_type_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Recurrences)
                .HasForeignKey(d => d.TransactionTypeId)
                .HasConstraintName("recurrences_transaction_type_id_foreign");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Recurrences)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("recurrences_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.Recurrences)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("recurrences_user_id_foreign");
        });

        modelBuilder.Entity<RecurrencesMetum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("recurrences_meta")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.RecurrenceId, "recurrences_meta_recurrence_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.RecurrenceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("recurrence_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.Value)
                .HasColumnType("text")
                .HasColumnName("value");

            entity.HasOne(d => d.Recurrence).WithMany(p => p.RecurrencesMeta)
                .HasForeignKey(d => d.RecurrenceId)
                .HasConstraintName("recurrences_meta_recurrence_id_foreign");
        });

        modelBuilder.Entity<RecurrencesRepetition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("recurrences_repetitions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.RecurrenceId, "recurrences_repetitions_recurrence_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.RecurrenceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("recurrence_id");
            entity.Property(e => e.RepetitionMoment)
                .HasMaxLength(50)
                .HasColumnName("repetition_moment");
            entity.Property(e => e.RepetitionSkip)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("repetition_skip");
            entity.Property(e => e.RepetitionType)
                .HasMaxLength(50)
                .HasColumnName("repetition_type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.Weekend)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("weekend");

            entity.HasOne(d => d.Recurrence).WithMany(p => p.RecurrencesRepetitions)
                .HasForeignKey(d => d.RecurrenceId)
                .HasConstraintName("recurrences_repetitions_recurrence_id_foreign");
        });

        modelBuilder.Entity<RecurrencesTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("recurrences_transactions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.DestinationId, "recurrences_transactions_destination_id_foreign");

            entity.HasIndex(e => e.ForeignCurrencyId, "recurrences_transactions_foreign_currency_id_foreign");

            entity.HasIndex(e => e.RecurrenceId, "recurrences_transactions_recurrence_id_foreign");

            entity.HasIndex(e => e.SourceId, "recurrences_transactions_source_id_foreign");

            entity.HasIndex(e => e.TransactionCurrencyId, "recurrences_transactions_transaction_currency_id_foreign");

            entity.HasIndex(e => e.TransactionTypeId, "type_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(36, 24)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .HasColumnName("description");
            entity.Property(e => e.DestinationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("destination_id");
            entity.Property(e => e.ForeignAmount)
                .HasPrecision(36, 24)
                .HasColumnName("foreign_amount");
            entity.Property(e => e.ForeignCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("foreign_currency_id");
            entity.Property(e => e.RecurrenceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("recurrence_id");
            entity.Property(e => e.SourceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("source_id");
            entity.Property(e => e.TransactionCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_currency_id");
            entity.Property(e => e.TransactionTypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_type_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Destination).WithMany(p => p.RecurrencesTransactionDestinations)
                .HasForeignKey(d => d.DestinationId)
                .HasConstraintName("recurrences_transactions_destination_id_foreign");

            entity.HasOne(d => d.ForeignCurrency).WithMany(p => p.RecurrencesTransactionForeignCurrencies)
                .HasForeignKey(d => d.ForeignCurrencyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("recurrences_transactions_foreign_currency_id_foreign");

            entity.HasOne(d => d.Recurrence).WithMany(p => p.RecurrencesTransactions)
                .HasForeignKey(d => d.RecurrenceId)
                .HasConstraintName("recurrences_transactions_recurrence_id_foreign");

            entity.HasOne(d => d.Source).WithMany(p => p.RecurrencesTransactionSources)
                .HasForeignKey(d => d.SourceId)
                .HasConstraintName("recurrences_transactions_source_id_foreign");

            entity.HasOne(d => d.TransactionCurrency).WithMany(p => p.RecurrencesTransactionTransactionCurrencies)
                .HasForeignKey(d => d.TransactionCurrencyId)
                .HasConstraintName("recurrences_transactions_transaction_currency_id_foreign");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.RecurrencesTransactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("type_foreign");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("roles")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Name, "roles_name_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(191)
                .HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(191)
                .HasColumnName("display_name");
            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<RtMetum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rt_meta")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.RtId, "rt_meta_rt_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.RtId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("rt_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.Value)
                .HasColumnType("text")
                .HasColumnName("value");

            entity.HasOne(d => d.Rt).WithMany(p => p.RtMeta)
                .HasForeignKey(d => d.RtId)
                .HasConstraintName("rt_meta_rt_id_foreign");
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rules")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.RuleGroupId, "rules_rule_group_id_foreign");

            entity.HasIndex(e => e.UserGroupId, "rules_to_ugi");

            entity.HasIndex(e => e.UserId, "rules_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Order)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.RuleGroupId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("rule_group_id");
            entity.Property(e => e.StopProcessing).HasColumnName("stop_processing");
            entity.Property(e => e.Strict)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("strict");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.RuleGroup).WithMany(p => p.Rules)
                .HasForeignKey(d => d.RuleGroupId)
                .HasConstraintName("rules_rule_group_id_foreign");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Rules)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("rules_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.Rules)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("rules_user_id_foreign");
        });

        modelBuilder.Entity<RuleAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rule_actions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.RuleId, "rule_actions_rule_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .HasColumnName("action_type");
            entity.Property(e => e.ActionValue)
                .HasMaxLength(255)
                .HasColumnName("action_value");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Order)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.RuleId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("rule_id");
            entity.Property(e => e.StopProcessing).HasColumnName("stop_processing");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Rule).WithMany(p => p.RuleActions)
                .HasForeignKey(d => d.RuleId)
                .HasConstraintName("rule_actions_rule_id_foreign");
        });

        modelBuilder.Entity<RuleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rule_groups")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "rule_groups_to_ugi");

            entity.HasIndex(e => e.UserId, "rule_groups_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Order)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.StopProcessing).HasColumnName("stop_processing");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.RuleGroups)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("rule_groups_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.RuleGroups)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("rule_groups_user_id_foreign");
        });

        modelBuilder.Entity<RuleTrigger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("rule_triggers")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.RuleId, "rule_triggers_rule_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Order)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.RuleId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("rule_id");
            entity.Property(e => e.StopProcessing).HasColumnName("stop_processing");
            entity.Property(e => e.TriggerType)
                .HasMaxLength(50)
                .HasColumnName("trigger_type");
            entity.Property(e => e.TriggerValue)
                .HasMaxLength(255)
                .HasColumnName("trigger_value");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Rule).WithMany(p => p.RuleTriggers)
                .HasForeignKey(d => d.RuleId)
                .HasConstraintName("rule_triggers_rule_id_foreign");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sessions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Id, "sessions_id_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.LastActivity)
                .HasColumnType("int(11)")
                .HasColumnName("last_activity");
            entity.Property(e => e.Payload)
                .HasColumnType("text")
                .HasColumnName("payload");
            entity.Property(e => e.UserAgent)
                .HasColumnType("text")
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("tags")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "tags_to_ugi");

            entity.HasIndex(e => e.UserId, "tags_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Latitude)
                .HasPrecision(36, 24)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasPrecision(36, 24)
                .HasColumnName("longitude");
            entity.Property(e => e.Tag1)
                .HasMaxLength(1024)
                .HasColumnName("tag");
            entity.Property(e => e.TagMode)
                .HasMaxLength(1024)
                .HasColumnName("tagMode");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");
            entity.Property(e => e.ZoomLevel)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("zoomLevel");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Tags)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tags_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.Tags)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("tags_user_id_foreign");
        });

        modelBuilder.Entity<TagTransactionJournal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("tag_transaction_journal")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.TagId, e.TransactionJournalId }, "tag_transaction_journal_tag_id_transaction_journal_id_unique").IsUnique();

            entity.HasIndex(e => e.TransactionJournalId, "tag_transaction_journal_transaction_journal_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.TagId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("tag_id");
            entity.Property(e => e.TransactionJournalId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_journal_id");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagTransactionJournals)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("tag_transaction_journal_tag_id_foreign");

            entity.HasOne(d => d.TransactionJournal).WithMany(p => p.TagTransactionJournals)
                .HasForeignKey(d => d.TransactionJournalId)
                .HasConstraintName("tag_transaction_journal_transaction_journal_id_foreign");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("transactions")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.AccountId, "transactions_account_id_foreign");

            entity.HasIndex(e => e.ForeignCurrencyId, "transactions_foreign_currency_id_foreign");

            entity.HasIndex(e => e.TransactionCurrencyId, "transactions_transaction_currency_id_foreign");

            entity.HasIndex(e => e.TransactionJournalId, "transactions_transaction_journal_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_id");
            entity.Property(e => e.Amount)
                .HasPrecision(36, 24)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .HasColumnName("description");
            entity.Property(e => e.ForeignAmount)
                .HasPrecision(36, 24)
                .HasColumnName("foreign_amount");
            entity.Property(e => e.ForeignCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("foreign_currency_id");
            entity.Property(e => e.Identifier)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("identifier");
            entity.Property(e => e.Reconciled).HasColumnName("reconciled");
            entity.Property(e => e.TransactionCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_currency_id");
            entity.Property(e => e.TransactionJournalId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_journal_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("transactions_account_id_foreign");

            entity.HasOne(d => d.ForeignCurrency).WithMany(p => p.TransactionForeignCurrencies)
                .HasForeignKey(d => d.ForeignCurrencyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("transactions_foreign_currency_id_foreign");

            entity.HasOne(d => d.TransactionCurrency).WithMany(p => p.TransactionTransactionCurrencies)
                .HasForeignKey(d => d.TransactionCurrencyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("transactions_transaction_currency_id_foreign");

            entity.HasOne(d => d.TransactionJournal).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionJournalId)
                .HasConstraintName("transactions_transaction_journal_id_foreign");
        });

        modelBuilder.Entity<TransactionCurrency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("transaction_currencies")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Code, "transaction_currencies_code_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(51)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DecimalPlaces)
                .HasDefaultValueSql("'2'")
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("decimal_places");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Enabled).HasColumnName("enabled");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Symbol)
                .HasMaxLength(51)
                .HasColumnName("symbol");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<TransactionGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("transaction_groups")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "transaction_groups_to_ugi");

            entity.HasIndex(e => e.UserId, "transaction_groups_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Title)
                .HasMaxLength(1024)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.TransactionGroups)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("transaction_groups_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.TransactionGroups)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("transaction_groups_user_id_foreign");
        });

        modelBuilder.Entity<TransactionJournal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("transaction_journals")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.BillId, "transaction_journals_bill_id_foreign");

            entity.HasIndex(e => e.UserGroupId, "transaction_journals_to_ugi");

            entity.HasIndex(e => e.TransactionCurrencyId, "transaction_journals_transaction_currency_id_foreign");

            entity.HasIndex(e => e.TransactionGroupId, "transaction_journals_transaction_group_id_foreign");

            entity.HasIndex(e => e.TransactionTypeId, "transaction_journals_transaction_type_id_foreign");

            entity.HasIndex(e => e.UserId, "transaction_journals_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.BillId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("bill_id");
            entity.Property(e => e.BookDate).HasColumnName("book_date");
            entity.Property(e => e.Completed)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("completed");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .HasColumnName("description");
            entity.Property(e => e.Encrypted)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("encrypted");
            entity.Property(e => e.InterestDate).HasColumnName("interest_date");
            entity.Property(e => e.Order)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.ProcessDate).HasColumnName("process_date");
            entity.Property(e => e.TagCount)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("tag_count");
            entity.Property(e => e.TransactionCurrencyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_currency_id");
            entity.Property(e => e.TransactionGroupId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_group_id");
            entity.Property(e => e.TransactionTypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("transaction_type_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Bill).WithMany(p => p.TransactionJournals)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("transaction_journals_bill_id_foreign");

            entity.HasOne(d => d.TransactionCurrency).WithMany(p => p.TransactionJournals)
                .HasForeignKey(d => d.TransactionCurrencyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transaction_journals_transaction_currency_id_foreign");

            entity.HasOne(d => d.TransactionGroup).WithMany(p => p.TransactionJournals)
                .HasForeignKey(d => d.TransactionGroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transaction_journals_transaction_group_id_foreign");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.TransactionJournals)
                .HasForeignKey(d => d.TransactionTypeId)
                .HasConstraintName("transaction_journals_transaction_type_id_foreign");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.TransactionJournals)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("transaction_journals_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.TransactionJournals)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("transaction_journals_user_id_foreign");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("transaction_types")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Type, "transaction_types_type_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("users")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UserGroupId, "type_user_group_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Blocked)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("blocked");
            entity.Property(e => e.BlockedCode)
                .HasMaxLength(25)
                .HasColumnName("blocked_code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Domain)
                .HasMaxLength(191)
                .HasColumnName("domain");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.MfaSecret)
                .HasMaxLength(50)
                .HasColumnName("mfa_secret");
            entity.Property(e => e.Objectguid).HasColumnName("objectguid");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .HasColumnName("password");
            entity.Property(e => e.RememberToken)
                .HasMaxLength(100)
                .HasColumnName("remember_token");
            entity.Property(e => e.Reset)
                .HasMaxLength(32)
                .HasColumnName("reset");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("type_user_group_id");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleUser",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("role_user_role_id_foreign"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("role_user_user_id_foreign"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("role_user")
                            .UseCollation("utf8mb4_unicode_ci");
                        j.HasIndex(new[] { "RoleId" }, "role_user_role_id_foreign");
                        j.IndexerProperty<uint>("UserId")
                            .HasColumnType("int(10) unsigned")
                            .HasColumnName("user_id");
                        j.IndexerProperty<uint>("RoleId")
                            .HasColumnType("int(10) unsigned")
                            .HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("user_groups")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Title, "user_groups_title_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("user_roles")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Title, "user_roles_title_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Webhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("webhooks")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Secret, "webhooks_secret_index");

            entity.HasIndex(e => e.Title, "webhooks_title_index");

            entity.HasIndex(e => e.UserGroupId, "webhooks_to_ugi");

            entity.HasIndex(e => new { e.UserId, e.Title }, "webhooks_user_id_title_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Delivery)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("delivery");
            entity.Property(e => e.Response)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("response");
            entity.Property(e => e.Secret)
                .HasMaxLength(32)
                .HasColumnName("secret");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Trigger)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("trigger");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.Url)
                .HasMaxLength(1024)
                .HasColumnName("url");
            entity.Property(e => e.UserGroupId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_group_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("webhooks_to_ugi");

            entity.HasOne(d => d.User).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("webhooks_user_id_foreign");
        });

        modelBuilder.Entity<WebhookAttempt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("webhook_attempts")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.WebhookMessageId, "webhook_attempts_webhook_message_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Logs).HasColumnName("logs");
            entity.Property(e => e.Response).HasColumnName("response");
            entity.Property(e => e.StatusCode)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("status_code");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.WebhookMessageId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("webhook_message_id");

            entity.HasOne(d => d.WebhookMessage).WithMany(p => p.WebhookAttempts)
                .HasForeignKey(d => d.WebhookMessageId)
                .HasConstraintName("webhook_attempts_webhook_message_id_foreign");
        });

        modelBuilder.Entity<WebhookMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("webhook_messages")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.WebhookId, "webhook_messages_webhook_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Errored).HasColumnName("errored");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Sent).HasColumnName("sent");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.Uuid)
                .HasMaxLength(64)
                .HasColumnName("uuid");
            entity.Property(e => e.WebhookId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("webhook_id");

            entity.HasOne(d => d.Webhook).WithMany(p => p.WebhookMessages)
                .HasForeignKey(d => d.WebhookId)
                .HasConstraintName("webhook_messages_webhook_id_foreign");
        });

        modelBuilder.Entity<_2faToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("2fa_tokens")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Token, "2fa_tokens_token_unique").IsUnique();

            entity.HasIndex(e => e.UserId, "2fa_tokens_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.Token)
                .HasMaxLength(64)
                .HasColumnName("token");
            entity.Property(e => e.UserId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p._2faTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("2fa_tokens_user_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
