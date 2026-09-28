

namespace Invoice.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceResponseDto> GetInvoicesAsync(
            DateTime? date = null,
            long? invoiceNumber = null,
            short? agencyNumber = null);
    }
}
