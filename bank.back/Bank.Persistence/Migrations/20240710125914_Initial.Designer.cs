﻿// <auto-generated />
using System;
using Bank.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bank.Persistence.Migrations
{
    [DbContext(typeof(BankDbContext))]
    [Migration("20240710125914_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bank.Core.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("account_id")
                        .HasDefaultValueSql("nextval('\"PersonalAccount_PersonalAccountId_seq\"'::regclass)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("money")
                        .HasColumnName("balance");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("currency_id");

                    b.Property<string>("Name")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)")
                        .HasColumnName("name");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("number");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("owner_id");

                    b.HasKey("AccountId")
                        .HasName("account_pkey");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("OwnerId");

                    b.HasIndex(new[] { "Number" }, "accounts_number_uq")
                        .IsUnique()
                        .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("Bank.Core.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("currency_id")
                        .HasDefaultValueSql("nextval('currency_currency_id_seq'::regclass)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("CurrencyId")
                        .HasName("currency_pkey");

                    b.HasIndex(new[] { "Code" }, "currencies_code_uq")
                        .IsUnique()
                        .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

                    b.ToTable("currencies", (string)null);
                });

            modelBuilder.Entity("Bank.Core.Models.ExchangeRate", b =>
                {
                    b.Property<int>("ExchangeRateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("exchange_rate_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExchangeRateId"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CurrencyFrom")
                        .HasColumnType("integer")
                        .HasColumnName("currency_from");

                    b.Property<int>("CurrencyTo")
                        .HasColumnType("integer")
                        .HasColumnName("currency_to");

                    b.Property<decimal>("Rate")
                        .HasPrecision(10, 6)
                        .HasColumnType("numeric(10,6)")
                        .HasColumnName("rate");

                    b.HasKey("ExchangeRateId")
                        .HasName("exchange_rates_pkey");

                    b.HasIndex("CurrencyFrom");

                    b.HasIndex("CurrencyTo");

                    b.ToTable("exchange_rates", (string)null);
                });

            modelBuilder.Entity("Bank.Core.Models.RefreshToken", b =>
                {
                    b.Property<int>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("refresh_token_id")
                        .HasDefaultValueSql("nextval('\"RefreshToken_RefreshTokenId_seq\"'::regclass)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expiration_date");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("RefreshTokenId")
                        .HasName("refresh_token_pkey");

                    b.HasIndex(new[] { "UserId" }, "uq_refresh_tokens")
                        .IsUnique()
                        .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("Bank.Core.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("transaction_id")
                        .HasDefaultValueSql("nextval('\"Transaction_TransactionId_seq\"'::regclass)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money")
                        .HasColumnName("amount");

                    b.Property<int>("FromAccountId")
                        .HasColumnType("integer")
                        .HasColumnName("from_account_Id");

                    b.Property<int>("FromCurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("from_currency_id");

                    b.Property<int>("ToAccountId")
                        .HasColumnType("integer")
                        .HasColumnName("to_account_id");

                    b.Property<int>("ToCurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("to_currency_id");

                    b.Property<DateTime>("TransferDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("transfer_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("TransactionId")
                        .HasName("transaction_pkey");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("FromCurrencyId");

                    b.HasIndex("ToAccountId");

                    b.HasIndex("ToCurrencyId");

                    b.ToTable("transactions", (string)null);
                });

            modelBuilder.Entity("Bank.Core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id")
                        .HasDefaultValueSql("nextval('\"User_UserId_seq\"'::regclass)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.HasKey("UserId")
                        .HasName("user_pkey");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Bank.Core.Models.Account", b =>
                {
                    b.HasOne("Bank.Core.Models.Currency", "Currency")
                        .WithMany("Accounts")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("accounts_currency_id_fkey");

                    b.HasOne("Bank.Core.Models.User", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("accounts_owner_id_fkey");

                    b.Navigation("Currency");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Bank.Core.Models.ExchangeRate", b =>
                {
                    b.HasOne("Bank.Core.Models.Currency", "CurrencyFromNavigation")
                        .WithMany("ExchangeRateCurrencyFromNavigations")
                        .HasForeignKey("CurrencyFrom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("exchange_rates_currency_from_fkey");

                    b.HasOne("Bank.Core.Models.Currency", "CurrencyToNavigation")
                        .WithMany("ExchangeRateCurrencyToNavigations")
                        .HasForeignKey("CurrencyTo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("exchange_rates_currency_to_fkey");

                    b.Navigation("CurrencyFromNavigation");

                    b.Navigation("CurrencyToNavigation");
                });

            modelBuilder.Entity("Bank.Core.Models.RefreshToken", b =>
                {
                    b.HasOne("Bank.Core.Models.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("Bank.Core.Models.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("refresh_tokens_user_id_fkey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bank.Core.Models.Transaction", b =>
                {
                    b.HasOne("Bank.Core.Models.Account", "FromAccount")
                        .WithMany("TransactionFromAccounts")
                        .HasForeignKey("FromAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transactions_from_account_Id_fkey");

                    b.HasOne("Bank.Core.Models.Currency", "FromCurrency")
                        .WithMany("TransactionFromCurrencies")
                        .HasForeignKey("FromCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transactions_from_currency_id_fkey");

                    b.HasOne("Bank.Core.Models.Account", "ToAccount")
                        .WithMany("TransactionToAccounts")
                        .HasForeignKey("ToAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transactions_to_account_id_fkey");

                    b.HasOne("Bank.Core.Models.Currency", "ToCurrency")
                        .WithMany("TransactionToCurrencies")
                        .HasForeignKey("ToCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("transactions_to_currency_id_fkey");

                    b.Navigation("FromAccount");

                    b.Navigation("FromCurrency");

                    b.Navigation("ToAccount");

                    b.Navigation("ToCurrency");
                });

            modelBuilder.Entity("Bank.Core.Models.Account", b =>
                {
                    b.Navigation("TransactionFromAccounts");

                    b.Navigation("TransactionToAccounts");
                });

            modelBuilder.Entity("Bank.Core.Models.Currency", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("ExchangeRateCurrencyFromNavigations");

                    b.Navigation("ExchangeRateCurrencyToNavigations");

                    b.Navigation("TransactionFromCurrencies");

                    b.Navigation("TransactionToCurrencies");
                });

            modelBuilder.Entity("Bank.Core.Models.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("RefreshToken");
                });
#pragma warning restore 612, 618
        }
    }
}
