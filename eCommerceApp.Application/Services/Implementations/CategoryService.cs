using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IGeneric<Category> _categoryInterface;

    public CategoryService(IGeneric<Category> categoryInterface)
    {
        _categoryInterface = categoryInterface;
    }

    #region Methods
    public Task<ServiceResponse> AddAsync(CreateCategoryDto category)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GetCategoryDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetCategoryDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> UpdateAsync(UpdateCategoryDto category)
    {
        throw new NotImplementedException();
    }
    #endregion
}
