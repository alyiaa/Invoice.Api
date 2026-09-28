using System.Text.Json.Serialization;

namespace Invoice.Application.DTOS.Invoice
{
    public class InvoiceCurrencyDto
    {
        [JsonIgnore]
        public decimal? CurrencyCode { get; set; }

        public string CurrencyName { get; set; } = string.Empty;

        public decimal? ForeignCurrencyAmount { get; set; }

        public decimal? EgyptianPoundAmount { get; set; }
    }
}
