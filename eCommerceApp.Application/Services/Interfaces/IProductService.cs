using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;

namespace eCommerceApp.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<GetProductDto>> GetAllAsync();
    Task<GetProductDto> GetByIdAsync(Guid id);
    Task<ServiceResponse> AddAsync(CreateProductDto product);
    Task<ServiceResponse> UpdateAsync(UpdateProductDto product);
    Task<ServiceResponse> DeleteAsync(Guid id);
}
