using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.Migrations
{
    /// <inheritdoc />
    public partial class ImproveManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransaction_Items_ItemsId",
                table: "ItemTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransaction_Transaction_TransactionsId",
                table: "ItemTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameColumn(
                name: "TransactionsId",
                table: "ItemTransaction",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "ItemsId",
                table: "ItemTransaction",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemTransaction_TransactionsId",
                table: "ItemTransaction",
                newName: "IX_ItemTransaction_TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTransaction_Items_ItemId",
                table: "ItemTransaction",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTransaction_Transactions_TransactionId",
                table: "ItemTransaction",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransaction_Items_ItemId",
                table: "ItemTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransaction_Transactions_TransactionId",
                table: "ItemTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "ItemTransaction",
                newName: "TransactionsId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ItemTransaction",
                newName: "ItemsId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemTransaction_TransactionId",
                table: "ItemTransaction",
                newName: "IX_ItemTransaction_TransactionsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTransaction_Items_ItemsId",
                table: "ItemTransaction",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTransaction_Transaction_TransactionsId",
                table: "ItemTransaction",
                column: "TransactionsId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
