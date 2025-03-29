using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;

namespace eCommerceApp.Infrastructure.Repositories.Cart;

/// <summary>
/// Repository for handling cart-related database operations.
/// </summary>
public class CartRepository : ICart
{
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The application database context.</param>
    public CartRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Saves checkout history by adding multiple checkout records to the database.
    /// </summary>
    /// <param name="checkouts">A collection of checkout records to be saved.</param>
    /// <returns>The number of affected rows in the database.</returns>
    public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts)
    {
        _dbContext.CheckoutAchieves.AddRange(checkouts);
        return await _dbContext.SaveChangesAsync();
    }
}
