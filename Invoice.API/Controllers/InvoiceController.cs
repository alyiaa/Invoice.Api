using Invoice.Application.DTOs.ApiResponse;
using Invoice.Application.DTOS.Invoice;
using Invoice.Application.Interfaces;
using Invoice.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("GetInvoices")]

        public async Task<IActionResult> GetInvoices(
            DateTime? date = null,
            long? invoiceNumber = null,
            short? agencyNumber = null)
        {
            var result = await _invoiceService.GetInvoicesAsync(
                date,
                invoiceNumber,
                agencyNumber);

            return Ok(ApiResponse<InvoiceResponseDto>.SuccessResponse(result,"Invoices retrieved successfully"));
        }
    }
}
