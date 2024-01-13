namespace BudgetApp.Requests;

public class AddItemsToTransactionRequest
{
  public int TransactionId { get; set; }
  public List<int>? ItemIds { get; set; }
}