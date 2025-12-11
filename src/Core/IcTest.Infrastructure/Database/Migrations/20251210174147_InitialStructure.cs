using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IcTest.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockChains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Coin = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Chain = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockChains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlockHashes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hash = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Chain = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Fees = table.Column<long>(type: "bigint", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: true),
                    Vsize = table.Column<int>(type: "integer", nullable: true),
                    Ver = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReceivedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CoinbaseAddr = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    RelayedBy = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Bits = table.Column<long>(type: "bigint", nullable: false),
                    Nonce = table.Column<long>(type: "bigint", nullable: false),
                    NTx = table.Column<int>(type: "integer", nullable: false),
                    PrevBlock = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    MrklRoot = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Depth = table.Column<int>(type: "integer", nullable: false),
                    PrevBlockUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    TxUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    NextTxids = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockHashes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlockTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hash = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    BlockHashId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockTransactions_BlockHashes_BlockHashId",
                        column: x => x.BlockHashId,
                        principalTable: "BlockHashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockChains_Coin_Chain",
                table: "BlockChains",
                columns: new[] { "Coin", "Chain" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlockHashes_Hash",
                table: "BlockHashes",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlockTransactions_BlockHashId",
                table: "BlockTransactions",
                column: "BlockHashId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockTransactions_Hash",
                table: "BlockTransactions",
                column: "Hash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockChains");

            migrationBuilder.DropTable(
                name: "BlockTransactions");

            migrationBuilder.DropTable(
                name: "BlockHashes");
        }
    }
}
