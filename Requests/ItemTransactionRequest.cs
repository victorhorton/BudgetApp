using System.ComponentModel.DataAnnotations;
using BudgetApp.Converters;
using Newtonsoft.Json;

namespace BudgetApp.Requests;

public class ItemTransactionRequest
{
  public int ItemId {get; set; }
  public decimal SplitAmount {get; set; }
}