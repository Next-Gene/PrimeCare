using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;


namespace eCommerceApp.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<GetCategoryDto>> GetAllAsync();
    Task<GetCategoryDto> GetByIdAsync(Guid id);
    Task<ServiceResponse> AddAsync(CreateCategoryDto category);
    Task<ServiceResponse> UpdateAsync(UpdateCategoryDto category);
    Task<ServiceResponse> DeleteAsync(Guid id);
}
