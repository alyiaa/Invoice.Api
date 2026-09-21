using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetInvoicesAsync(
            DateTime? date = null,
            long? invoiceNumber = null,
            short? agencyNumber = null);
    }
}
