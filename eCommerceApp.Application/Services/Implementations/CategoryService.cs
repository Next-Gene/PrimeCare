using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations;

/// <summary>
/// Service class for managing categories.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly IGeneric<Category> _categoryInterface;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="categoryInterface">The generic interface for category operations.</param>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    public CategoryService(IGeneric<Category> categoryInterface, IMapper mapper)
    {
        _categoryInterface = categoryInterface;
        _mapper = mapper;
    }

    #region Methods

    /// <summary>
    /// Adds a new category asynchronously.
    /// </summary>
    /// <param name="category">The category data transfer object.</param>
    /// <returns>A service response indicating the result of the operation.</returns>
    public async Task<ServiceResponse> AddAsync(CreateCategoryDto category)
    {
        var mappedData = _mapper.Map<Category>(category);
        int result = await _categoryInterface.AddAsync(mappedData);

        return result > 0
            ? new ServiceResponse(true, "Category Added")
            : new ServiceResponse(false, "Category failed to be Added");
    }

    /// <summary>
    /// Deletes a category by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <returns>A service response indicating the result of the operation.</returns>
    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        int result = await _categoryInterface.DeleteAsync(id);

        return result > 0
            ? new ServiceResponse(true, "Category Deleted")
            : new ServiceResponse(false, "Category not found or failed to be Deleted");
    }

    /// <summary>
    /// Retrieves all categories asynchronously.
    /// </summary>
    /// <returns>A collection of category data transfer objects.</returns>
    public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
    {
        var rawData = await _categoryInterface.GetAllAsync();

        if (!rawData.Any()) return Enumerable.Empty<GetCategoryDto>();

        return _mapper.Map<IEnumerable<GetCategoryDto>>(rawData);
    }

    /// <summary>
    /// Retrieves a category by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <returns>The category data transfer object.</returns>
    public async Task<GetCategoryDto> GetByIdAsync(Guid id)
    {
        var rawData = await _categoryInterface.GetByIdAsync(id);

        if (rawData == null) return new GetCategoryDto();

        return _mapper.Map<GetCategoryDto>(rawData);
    }

    /// <summary>
    /// Updates an existing category asynchronously.
    /// </summary>
    /// <param name="category">The category data transfer object.</param>
    /// <returns>A service response indicating the result of the operation.</returns>
    public async Task<ServiceResponse> UpdateAsync(UpdateCategoryDto category)
    {
        var mappedData = _mapper.Map<Category>(category);
        int result = await _categoryInterface.UpdateAsync(mappedData);

        return result > 0
            ? new ServiceResponse(true, "Category Updated")
            : new ServiceResponse(false, "Category failed to be Updated");
    }

    #endregion
}