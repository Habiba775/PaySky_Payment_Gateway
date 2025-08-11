using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addeduserIdAsForeignKeytoTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SavingsAccounts_UserId",
                table: "SavingsAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckingAccounts_UserId",
                table: "CheckingAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_AspNetUsers_UserId",
                table: "AuditLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckingAccounts_AspNetUsers_UserId",
                table: "CheckingAccounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavingsAccounts_AspNetUsers_UserId",
                table: "SavingsAccounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_AspNetUsers_UserId",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckingAccounts_AspNetUsers_UserId",
                table: "CheckingAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SavingsAccounts_AspNetUsers_UserId",
                table: "SavingsAccounts");

            migrationBuilder.DropIndex(
                name: "IX_SavingsAccounts_UserId",
                table: "SavingsAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CheckingAccounts_UserId",
                table: "CheckingAccounts");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs");
        }
    }
}
