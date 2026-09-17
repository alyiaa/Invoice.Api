namespace Invoice.Application.DTOS.Invoice
{
    public class UnifiedInvoiceDto
    {
        public long Id { get; set; }

        public string? InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? TravelDate { get; set; }

        public string? VesselImo { get; set; }

        public string? VesselName { get; set; }

        public string? AgencyNumber { get; set; }

        public decimal? GrossTonnage { get; set; }

        public decimal? NetTonnage { get; set; }

        public List<InvoiceCurrencyDto> Currencies { get; set; } = new();
    }
}
