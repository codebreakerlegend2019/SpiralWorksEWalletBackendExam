using Microsoft.EntityFrameworkCore.Migrations;

namespace SpiralWorksWalletBackendExam.Migrations
{

    public partial class Add_SenderAccountNumber_Column_Transactions_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderAccountNumber",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderAccountNumber",
                table: "Transactions");
        }
    }
}
