using System;
using System.Collections.Generic;
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

    public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }

    public virtual DbSet<PersonalAccount> PersonalAccounts { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyType>(entity =>
        {
            entity.HasKey(e => e.CurrencyTypeId).HasName("CurrencyType_pkey");

            entity.ToTable("CurrencyType");

            entity.Property(e => e.CurrencyTypeId).HasDefaultValueSql("nextval('\"ExchangeRateType_ExchangeRateTypeId_seq\"'::regclass)");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Name).HasMaxLength(10);
            entity.Property(e => e.Rate).HasPrecision(10, 6);
        });

        modelBuilder.Entity<PersonalAccount>(entity =>
        {
            entity.HasKey(e => e.PersonalAccountId).HasName("PersonalAccount_pkey");

            entity.ToTable("PersonalAccount");

            entity.HasIndex(e => e.Number, "UQ_PersonalAccount").IsUnique();

            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.Number).HasMaxLength(20);

            entity.HasOne(d => d.CurrencyType).WithMany(p => p.PersonalAccounts)
                .HasForeignKey(d => d.CurrencyTypeId)
                .HasConstraintName("PersonalAccount_CurrencyTypeId_fkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.PersonalAccounts)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("PersonalAccount_OwnerId_fkey");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("RefreshToken_pkey");

            entity.ToTable("RefreshToken");

            entity.HasIndex(e => e.UserId, "UQ_RefreshToken")
                .IsUnique()
                .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

            entity.Property(e => e.ExpirationDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.User).WithOne(p => p.RefreshToken)
                .HasForeignKey<RefreshToken>(d => d.UserId)
                .HasConstraintName("RefreshToken_UserId_fkey");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("Transaction_pkey");

            entity.ToTable("Transaction");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.TransferDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.CurrencyType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CurrencyTypeId)
                .HasConstraintName("Transaction_CurrencyTypeId_fkey");

            entity.HasOne(d => d.FromAccount).WithMany(p => p.TransactionFromAccounts)
                .HasForeignKey(d => d.FromAccountId)
                .HasConstraintName("Transaction_FromAccountId_fkey");

            entity.HasOne(d => d.ToAccount).WithMany(p => p.TransactionToAccounts)
                .HasForeignKey(d => d.ToAccountId)
                .HasConstraintName("Transaction_ToAccountId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "UQ_User")
                .IsUnique()
                .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
