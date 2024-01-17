namespace BudgetApp.Requests;

public class AddItemToTransactionRequest
{
  public int TransactionId { get; set; }
  public int ItemId { get; set; }
}