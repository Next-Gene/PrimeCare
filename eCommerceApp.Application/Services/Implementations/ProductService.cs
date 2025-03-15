using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations;

/// <summary>
/// Service class for managing products.
/// </summary>
public class ProductService : IProductService
{
    private readonly IGeneric<Product> _productInterface;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </summary>
    /// <param name="productInterface">The generic interface for product operations.</param>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    public ProductService(IGeneric<Product> productInterface, IMapper mapper)
    {
        _productInterface = productInterface;
        _mapper = mapper;
    }

    #region Methods

    /// <summary>
    /// Adds a new product asynchronously.
    /// </summary>
    /// <param name="product">The product data transfer object.</param>
    /// <returns>A service response indicating the result of the operation.</returns>
    public async Task<ServiceResponse> AddAsync(CreateProductDto product)
    {
        var mappedData = _mapper.Map<Product>(product);
        int result = await _productInterface.AddAsync(mappedData);

        return result > 0
            ? new ServiceResponse(true, "Product Added")
            : new ServiceResponse(false, "Product failed to be Added");
    }

    /// <summary>
    /// Deletes a product by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>A service response indicating the result of the operation.</returns>
    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        int result = await _productInterface.DeleteAsync(id);

        return result > 0
            ? new ServiceResponse(true, "Product Deleted")
            : new ServiceResponse(false, "Product Not Found or failed to be Deleted");
    }

    /// <summary>
    /// Retrieves all products asynchronously.
    /// </summary>
    /// <returns>A collection of product data transfer objects.</returns>
    public async Task<IEnumerable<GetProductDto>> GetAllAsync()
    {
        var rawData = await _productInterface.GetAllAsync();

        if (!rawData.Any()) return Enumerable.Empty<GetProductDto>();

        return _mapper.Map<IEnumerable<GetProductDto>>(rawData);
    }

    /// <summary>
    /// Retrieves a product by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>The product data transfer object.</returns>
    public async Task<GetProductDto> GetByIdAsync(Guid id)
    {
        var rawData = await _productInterface.GetByIdAsync(id);

        if (rawData == null) return new GetProductDto();

        return _mapper.Map<GetProductDto>(rawData);
    }

    /// <summary>
    /// Updates an existing product asynchronously.
    /// </summary>
    /// <param name="product">The product data transfer object.</param>
    /// <returns>A service response indicating the result of the operation.</returns>
    public async Task<ServiceResponse> UpdateAsync(UpdateProductDto product)
    {
        var mappedData = _mapper.Map<Product>(product);
        int result = await _productInterface.UpdateAsync(mappedData);

        return result > 0
            ? new ServiceResponse(true, "Product Updated")
            : new ServiceResponse(false, "Product failed to be Updated");
    }

    #endregion
}