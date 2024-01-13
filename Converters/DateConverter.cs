using Newtonsoft.Json.Converters;
namespace BudgetApp.Converters;
public class DateConverter : IsoDateTimeConverter
{
    public DateConverter()
    {
        DateTimeFormat = "yyyy-MM-dd";
    }
}