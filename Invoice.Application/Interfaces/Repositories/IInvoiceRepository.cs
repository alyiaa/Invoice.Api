using Invoice.Domain.Entities;
namespace Invoice.Application.Interfaces.Repositories
{
    public interface IInvoiceRepository { 
        Task<List<InvoiceEntity>> GetInvoicesAsync(DateTime? invoiceDate = null,
            short? agencyNumber = null);
    }
}
