using Bank.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Persistence.Context;

public partial class BankDbContext : DbContext
{
    public BankDbContext()
    {
    }

    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //Для использования миграций, закомментировать OnConfiguring
    
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=bank;Username=postgres;Password=P@ssw0rd;");
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("account_pkey");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.Number, "accounts_number_uq")
                .IsUnique()
                .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

            entity.Property(e => e.AccountId)
                .HasDefaultValueSql("nextval('\"PersonalAccount_PersonalAccountId_seq\"'::regclass)")
                .HasColumnName("account_id");
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .HasColumnName("balance");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(20)
                .HasColumnName("number");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");

            entity.HasOne(d => d.Currency).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("accounts_currency_id_fkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("accounts_owner_id_fkey");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("currency_pkey");

            entity.ToTable("currencies");

            entity.HasIndex(e => e.Code, "currencies_code_uq")
                .IsUnique()
                .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

            entity.Property(e => e.CurrencyId)
                .HasDefaultValueSql("nextval('currency_currency_id_seq'::regclass)")
                .HasColumnName("currency_id");
            entity.Property(e => e.Code)
                .HasMaxLength(3)
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ExchangeRate>(entity =>
        {
            entity.HasKey(e => e.ExchangeRateId).HasName("exchange_rates_pkey");

            entity.ToTable("exchange_rates");

            entity.Property(e => e.ExchangeRateId).HasColumnName("exchange_rate_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CurrencyFrom).HasColumnName("currency_from");
            entity.Property(e => e.CurrencyTo).HasColumnName("currency_to");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 6)
                .HasColumnName("rate");

            entity.HasOne(d => d.CurrencyFromNavigation).WithMany(p => p.ExchangeRateCurrencyFromNavigations)
                .HasForeignKey(d => d.CurrencyFrom)
                .HasConstraintName("exchange_rates_currency_from_fkey");

            entity.HasOne(d => d.CurrencyToNavigation).WithMany(p => p.ExchangeRateCurrencyToNavigations)
                .HasForeignKey(d => d.CurrencyTo)
                .HasConstraintName("exchange_rates_currency_to_fkey");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("refresh_token_pkey");

            entity.ToTable("refresh_tokens");

            entity.HasIndex(e => e.UserId, "uq_refresh_tokens")
                .IsUnique()
                .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

            entity.Property(e => e.RefreshTokenId)
                .HasDefaultValueSql("nextval('\"RefreshToken_RefreshTokenId_seq\"'::regclass)")
                .HasColumnName("refresh_token_id");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiration_date");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.RefreshToken)
                .HasForeignKey<RefreshToken>(d => d.UserId)
                .HasConstraintName("refresh_tokens_user_id_fkey");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("transaction_pkey");

            entity.ToTable("transactions");

            entity.Property(e => e.TransactionId)
                .HasDefaultValueSql("nextval('\"Transaction_TransactionId_seq\"'::regclass)")
                .HasColumnName("transaction_id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.FromAccountId).HasColumnName("from_account_Id");
            entity.Property(e => e.FromCurrencyId).HasColumnName("from_currency_id");
            entity.Property(e => e.ToAccountId).HasColumnName("to_account_id");
            entity.Property(e => e.ToCurrencyId).HasColumnName("to_currency_id");
            entity.Property(e => e.TransferDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("transfer_date");

            entity.HasOne(d => d.FromAccount).WithMany(p => p.TransactionFromAccounts)
                .HasForeignKey(d => d.FromAccountId)
                .HasConstraintName("transactions_from_account_Id_fkey");

            entity.HasOne(d => d.FromCurrency).WithMany(p => p.TransactionFromCurrencies)
                .HasForeignKey(d => d.FromCurrencyId)
                .HasConstraintName("transactions_from_currency_id_fkey");

            entity.HasOne(d => d.ToAccount).WithMany(p => p.TransactionToAccounts)
                .HasForeignKey(d => d.ToAccountId)
                .HasConstraintName("transactions_to_account_id_fkey");

            entity.HasOne(d => d.ToCurrency).WithMany(p => p.TransactionToCurrencies)
                .HasForeignKey(d => d.ToCurrencyId)
                .HasConstraintName("transactions_to_currency_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("nextval('\"User_UserId_seq\"'::regclass)")
                .HasColumnName("user_id");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}