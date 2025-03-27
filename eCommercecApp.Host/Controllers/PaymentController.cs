using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eCommercecApp.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;
    public PaymentController(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPaymentMethodDto>>> GetPaymentMethods()
    {
        var methods = await _paymentMethodService.GetPaymentMethods();
        if (!methods.Any()) 
            return NotFound();
        else
            return Ok(methods);
    }
}
