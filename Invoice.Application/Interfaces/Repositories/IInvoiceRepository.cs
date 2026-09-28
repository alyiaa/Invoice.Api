
namespace Invoice.Application.Interfaces.Repositories
{
    public interface IInvoiceRepository
    {
        Task<InvoiceResponseDto> GetInvoicesAsync(
            DateTime? date = null,
            long? invoiceNumber = null,
            short? agencyNumber = null);
    }
}

