using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookJump.Data.Migrations
{
    public partial class AddTradeRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeRequests",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    BorrowerId = table.Column<string>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeRequests", x => new { x.BookId, x.BorrowerId });
                    table.ForeignKey(
                        name: "FK_TradeRequests_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeRequests_AspNetUsers_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeRequests_BookId",
                table: "TradeRequests",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeRequests_BorrowerId",
                table: "TradeRequests",
                column: "BorrowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeRequests");
        }
    }
}
