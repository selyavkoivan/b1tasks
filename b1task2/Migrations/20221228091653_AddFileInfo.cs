using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace b1task2.Migrations
{
    /// <inheritdoc />
    public partial class AddFileInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceFiles",
                columns: table => new
                {
                    BalanceFileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceFiles", x => x.BalanceFileId);
                });

            migrationBuilder.CreateTable(
                name: "BalanceSheetClasses",
                columns: table => new
                {
                    BalanceSheetClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceFileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceSheetClasses", x => x.BalanceSheetClassId);
                    table.ForeignKey(
                        name: "FK_BalanceSheetClasses_BalanceFiles_BalanceFileId",
                        column: x => x.BalanceFileId,
                        principalTable: "BalanceFiles",
                        principalColumn: "BalanceFileId");
                });

            migrationBuilder.CreateTable(
                name: "BalanceLineBlocks",
                columns: table => new
                {
                    BalanceLineBlockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BalanceLineBlockNumber = table.Column<int>(type: "int", nullable: false),
                    BalanceSheetClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceLineBlocks", x => x.BalanceLineBlockId);
                    table.ForeignKey(
                        name: "FK_BalanceLineBlocks_BalanceSheetClasses_BalanceSheetClassId",
                        column: x => x.BalanceSheetClassId,
                        principalTable: "BalanceSheetClasses",
                        principalColumn: "BalanceSheetClassId");
                });

            migrationBuilder.CreateTable(
                name: "BalanceLines",
                columns: table => new
                {
                    BalanceLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BalanceLineNumber = table.Column<int>(type: "int", nullable: false),
                    OpeningBalanceAsset = table.Column<double>(type: "float", nullable: false),
                    OpeningBalanceLiability = table.Column<double>(type: "float", nullable: false),
                    TurnoverDebit = table.Column<double>(type: "float", nullable: false),
                    TurnoverCredit = table.Column<double>(type: "float", nullable: false),
                    BalanceLineBlockId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceLines", x => x.BalanceLineId);
                    table.ForeignKey(
                        name: "FK_BalanceLines_BalanceLineBlocks_BalanceLineBlockId",
                        column: x => x.BalanceLineBlockId,
                        principalTable: "BalanceLineBlocks",
                        principalColumn: "BalanceLineBlockId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceLineBlocks_BalanceSheetClassId",
                table: "BalanceLineBlocks",
                column: "BalanceSheetClassId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceLines_BalanceLineBlockId",
                table: "BalanceLines",
                column: "BalanceLineBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheetClasses_BalanceFileId",
                table: "BalanceSheetClasses",
                column: "BalanceFileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceLines");

            migrationBuilder.DropTable(
                name: "BalanceLineBlocks");

            migrationBuilder.DropTable(
                name: "BalanceSheetClasses");

            migrationBuilder.DropTable(
                name: "BalanceFiles");
        }
    }
}
