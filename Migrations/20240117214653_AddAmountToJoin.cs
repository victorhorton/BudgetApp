using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountToJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "ItemTransactions",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ItemTransactions");
        }
    }
}
