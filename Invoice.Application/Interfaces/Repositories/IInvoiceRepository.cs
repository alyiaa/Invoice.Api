
namespace Invoice.Application.Interfaces.Repositories
{
    public interface IInvoiceRepository
    {
        Task<List<InvoiceDto>> GetInvoicesAsync(
            DateTime? date = null,
            long? invoiceNumber = null,
            short? agencyNumber = null);
    }
}

