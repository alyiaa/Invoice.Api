
namespace Invoice.Infrastructure.Mapping
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<ViewUnifiedInvoice, InvoiceDto>();
            CreateMap<ViewUnifiedInvoice, InvoiceCurrencyDto>()
    .ForMember(
        dest => dest.CurrencyName,
        opt => opt.MapFrom(src => src.Currency)
    )
    .ForMember(
        dest => dest.ForeignCurrencyAmount,
        opt => opt.MapFrom(src => src.Dl)
    )
    .ForMember(
        dest => dest.EgyptianPoundAmount,
        opt => opt.MapFrom(src => src.Le)
    );
        }
    }
}
