using Invoice.Application.DTOS.Invoice;
using Invoice.Application.Interfaces;
using Invoice.Application.Interfaces.Repositories;
using Invoice.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ModelContext _context;

        public InvoiceRepository(ModelContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceDto>> GetInvoicesAsync(
            DateTime? date = null,
            long? invoiceNumber = null,
            short? agencyNumber = null)
        {
            var query = _context.ViewUnifiedInvoices
                .AsNoTracking()
                .AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(x =>
                    x.InvoiceDate.Date == date.Value.Date);
            }

            if (invoiceNumber.HasValue)
            {
                query = query.Where(x =>
                    x.InvoiceNumber == invoiceNumber.Value);
            }

            if (agencyNumber.HasValue)
            {
                query = query.Where(x =>
                    x.AgencyNumber == agencyNumber.Value);
            }

            var data = await query
                .Select(x => new
                {
                    x.Id,
                    x.InvoiceNumber,
                    x.InvoiceDate,
                    x.TravelDate,
                    x.Dl,
                    x.Le,
                    x.VesselImo,
                    x.VesselName,
                    x.AgencyNumber,
                    Currency = x.Currency.HasValue
                        ? (decimal?)x.Currency.Value
                        : null,
                    x.GrossTonnage,
                    x.NetTonnage
                })
                .ToListAsync();

            var result = data
                .GroupBy(x => new
                {
                    x.InvoiceNumber,
                    x.InvoiceDate,
                    x.TravelDate,
                    x.VesselImo,
                    x.VesselName,
                    x.AgencyNumber,
                    x.GrossTonnage,
                    x.NetTonnage
                })
                .Select(g => new InvoiceDto
                {
                    Id = g.First().Id,

                    InvoiceNumber = g.Key.InvoiceNumber,
                    InvoiceDate = g.Key.InvoiceDate,
                    TravelDate = g.Key.TravelDate,

                    VesselImo = g.Key.VesselImo,
                    VesselName = g.Key.VesselName,

                    AgencyNumber = g.Key.AgencyNumber,

                    GrossTonnage = g.Key.GrossTonnage,
                    NetTonnage = g.Key.NetTonnage,

                    Currencies = g
                        .GroupBy(x => x.Currency)
                        .Select(c => new InvoiceCurrencyDto
                        {
                            Currency = c.Key,

                            Dl = c.Sum(x => x.Dl ?? 0),
                            Le = c.Sum(x => x.Le ?? 0)
                        })
                        .ToList()
                })
                .ToList();

            return result;
        }
    }
}