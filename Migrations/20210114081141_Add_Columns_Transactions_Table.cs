using Microsoft.EntityFrameworkCore.Migrations;

namespace SpiralWorksWalletBackendExam.Migrations
{
    public partial class Add_Columns_Transactions_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmountDeposited",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AmountTransferred",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AmountWithdrew",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ReciepientAccountNumber",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountDeposited",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AmountTransferred",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AmountWithdrew",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ReciepientAccountNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");
        }
    }
}
