using Invoice.Application.Interfaces.Repositories;
using Invoice.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Services
{
        public class InvoiceService : IInvoiceService
        {
            private readonly IInvoiceRepository _invoiceRepository;

            public InvoiceService(IInvoiceRepository invoiceRepository)
            {
                _invoiceRepository = invoiceRepository;
            }

            public async Task<List<InvoiceDto>> GetInvoicesAsync(
                DateTime? date = null,
                long? invoiceNumber = null,
                short? agencyNumber = null)
            {
                return await _invoiceRepository.GetInvoicesAsync(
                    date,
                    invoiceNumber,
                    agencyNumber);
            }
        }
}
