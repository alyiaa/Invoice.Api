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
        private readonly IMapper _mapper;
        public InvoiceRepository(ModelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<InvoiceResponseDto> GetInvoicesAsync(
                   DateTime? date = null,
                   long? invoiceNumber = null,
                   short? agencyNumber = null)
        {
            var query = _context.ViewUnifiedInvoices
                .AsNoTracking()
                .AsQueryable();

            // Filter by invoice date
            if (date.HasValue)
            {
                query = query.Where(x =>
                    x.InvoiceDate.Date == date.Value.Date);
            }

            // Filter by invoice number
            if (invoiceNumber.HasValue)
            {
                query = query.Where(x =>
                    x.InvoiceNumber == invoiceNumber.Value);
            }

            // Filter by agency number
            if (agencyNumber.HasValue)
            {
                query = query.Where(x =>
                    x.AgencyNumber == agencyNumber.Value);
            }

            var data = await query.ToListAsync();

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
                .Select(g =>
                {
                    var invoice = _mapper.Map<InvoiceDto>(g.First());

                    invoice.Currencies = g
                        .GroupBy(x => new
                        {
                            x.CurrencyCode,
                            x.Currency
                        })
                        .Select(c => new InvoiceCurrencyDto
                        {
                            CurrencyCode = c.Key.CurrencyCode,
                            CurrencyName = c.Key.Currency ?? string.Empty,
                            ForeignCurrencyAmount = c.Sum(x => x.Dl ?? 0),
                            EgyptianPoundAmount = c.Sum(x => x.Le ?? 0)
                        })
                        .ToList();

                    return invoice;
                })
                .OrderBy(x => x.InvoiceDate)
                .ToList();

            var totals = new InvoiceTotalDto
            {
                TotalCurrencyAmount = result
          .SelectMany(x => x.Currencies)
          .Sum(x => x.ForeignCurrencyAmount ?? 0),

                TotalPoundAmount = result
          .SelectMany(x => x.Currencies)
          .Sum(x => x.EgyptianPoundAmount ?? 0)
            };
            return new InvoiceResponseDto
            {
                Invoices = result,
                Totals = totals
            } ;
        }
    }
}