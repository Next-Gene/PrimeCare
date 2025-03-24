using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositories.Cart;

/// <summary>
/// Repository for managing payment methods in the e-commerce application.
/// </summary>
public class PaymentMethodRepository : IPaymentMethod
{
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentMethodRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context used to access payment methods.</param>
    public PaymentMethodRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves all payment methods from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="PaymentMethod"/>.</returns>
    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
    {
        return await _dbContext.PaymentMethods
            .AsNoTracking().ToListAsync();
    }
}
