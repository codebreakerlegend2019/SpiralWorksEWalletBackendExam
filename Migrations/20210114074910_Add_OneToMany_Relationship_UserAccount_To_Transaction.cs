using Microsoft.EntityFrameworkCore.Migrations;

namespace SpiralWorksWalletBackendExam.Migrations
{
    public partial class Add_OneToMany_Relationship_UserAccount_To_Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserAccountId",
                table: "Transactions",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserAccounts_UserAccountId",
                table: "Transactions",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserAccounts_UserAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Transactions");
        }
    }
}
