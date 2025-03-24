using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Interfaces.Cart;

namespace eCommerceApp.Application.Services.Implementations.Cart;

/// <summary>
/// Service for managing payment methods in the e-commerce application.
/// </summary>
public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethod _paymentMethod;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentMethodService"/> class.
    /// </summary>
    /// <param name="paymentMethod">The repository interface for accessing payment methods.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
    public PaymentMethodService(IPaymentMethod paymentMethod,
      IMapper mapper)
    {
        _paymentMethod = paymentMethod;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all payment methods and maps them to DTOs.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a collection of <see cref="GetPaymentMethodDto"/>.
    /// </returns>
    public async Task<IEnumerable<GetPaymentMethodDto>> GetPaymentMethods()
    {
        var method = await _paymentMethod.GetPaymentMethods();
        if (!method.Any()) return [];

        return _mapper.Map<IEnumerable<GetPaymentMethodDto>>(method);
    }
}
