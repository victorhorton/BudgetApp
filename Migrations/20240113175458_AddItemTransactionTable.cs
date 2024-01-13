using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.Migrations
{
    /// <inheritdoc />
    public partial class AddItemTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransaction_Items_ItemId",
                table: "ItemTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransaction_Transactions_TransactionId",
                table: "ItemTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemTransaction",
                table: "ItemTransaction");

            migrationBuilder.RenameTable(
                name: "ItemTransaction",
                newName: "ItemTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_ItemTransaction_TransactionId",
                table: "ItemTransactions",
                newName: "IX_ItemTransactions_TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemTransactions",
                table: "ItemTransactions",
                columns: new[] { "ItemId", "TransactionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTransactions_Items_ItemId",
                table: "ItemTransactions",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTransactions_Transactions_TransactionId",
                table: "ItemTransactions",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransactions_Items_ItemId",
                table: "ItemTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTransactions_Transactions_TransactionId",
                table: "ItemTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemTransactions",
                table: "ItemTransactions");

            migrationBuilder.RenameTable(
                name: "ItemTransactions",
                newName: "ItemTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_ItemTransactions_TransactionId",
                table: "ItemTransaction",
                newName: "IX_ItemTransaction_TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemTransaction",
                table: "ItemTransaction",
                columns: new[] { "ItemId", "TransactionId" });

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
    }
}
