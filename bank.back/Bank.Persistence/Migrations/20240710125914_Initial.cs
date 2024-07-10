using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    currency_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("currency_pkey", x => x.currency_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "exchange_rates",
                columns: table => new
                {
                    exchange_rate_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currency_from = table.Column<int>(type: "integer", nullable: false),
                    rate = table.Column<decimal>(type: "numeric(10,6)", precision: 10, scale: 6, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    currency_to = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("exchange_rates_pkey", x => x.exchange_rate_id);
                    table.ForeignKey(
                        name: "exchange_rates_currency_from_fkey",
                        column: x => x.currency_from,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "exchange_rates_currency_to_fkey",
                        column: x => x.currency_to,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    owner_id = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    balance = table.Column<decimal>(type: "money", nullable: false),
                    currency_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("account_pkey", x => x.account_id);
                    table.ForeignKey(
                        name: "accounts_currency_id_fkey",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "accounts_owner_id_fkey",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    refresh_token_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("refresh_token_pkey", x => x.refresh_token_id);
                    table.ForeignKey(
                        name: "refresh_tokens_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    transaction_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    from_account_Id = table.Column<int>(type: "integer", nullable: false),
                    to_account_id = table.Column<int>(type: "integer", nullable: false),
                    transfer_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    from_currency_id = table.Column<int>(type: "integer", nullable: false),
                    to_currency_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transaction_pkey", x => x.transaction_id);
                    table.ForeignKey(
                        name: "transactions_from_account_Id_fkey",
                        column: x => x.from_account_Id,
                        principalTable: "accounts",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "transactions_from_currency_id_fkey",
                        column: x => x.from_currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "transactions_to_account_id_fkey",
                        column: x => x.to_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "transactions_to_currency_id_fkey",
                        column: x => x.to_currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "accounts_number_uq",
                table: "accounts",
                column: "number",
                unique: true)
                .Annotation("Npgsql:StorageParameter:deduplicate_items", "true");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_currency_id",
                table: "accounts",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_owner_id",
                table: "accounts",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "currencies_code_uq",
                table: "currencies",
                column: "code",
                unique: true)
                .Annotation("Npgsql:StorageParameter:deduplicate_items", "true");

            migrationBuilder.CreateIndex(
                name: "IX_exchange_rates_currency_from",
                table: "exchange_rates",
                column: "currency_from");

            migrationBuilder.CreateIndex(
                name: "IX_exchange_rates_currency_to",
                table: "exchange_rates",
                column: "currency_to");

            migrationBuilder.CreateIndex(
                name: "uq_refresh_tokens",
                table: "refresh_tokens",
                column: "user_id",
                unique: true)
                .Annotation("Npgsql:StorageParameter:deduplicate_items", "true");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_from_account_Id",
                table: "transactions",
                column: "from_account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_from_currency_id",
                table: "transactions",
                column: "from_currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_to_account_id",
                table: "transactions",
                column: "to_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_to_currency_id",
                table: "transactions",
                column: "to_currency_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exchange_rates");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
