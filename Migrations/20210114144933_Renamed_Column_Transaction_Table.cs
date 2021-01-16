using Microsoft.EntityFrameworkCore.Migrations;

namespace SpiralWorksWalletBackendExam.Migrations
{
    public partial class Renamed_Column_Transaction_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReciepientAccountNumber",
                table: "Transactions",
                newName: "RecipientAccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipientAccountNumber",
                table: "Transactions",
                newName: "ReciepientAccountNumber");
        }
    }
}
