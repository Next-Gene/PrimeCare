using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IGeneric<Category> _categoryInterface;
    private readonly IMapper _mapper;

    public CategoryService(IGeneric<Category> categoryInterface, IMapper mapper)
    {
        _categoryInterface = categoryInterface;
        _mapper = mapper;
    }

    #region Methods
    public async Task<ServiceResponse> AddAsync(CreateCategoryDto category)
    {

        var mappedData = _mapper.Map<Category>(category);
        int result = await _categoryInterface.AddAsync(mappedData);
        return result > 0 ? new ServiceResponse(true, "Category Added") :
       new ServiceResponse(false, "Category failed to be Added");


    }

    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {

        int result = await _categoryInterface.DeleteAsync(id);

  


        return result > 0 ? new ServiceResponse(true, "Category Deleated") :
        new ServiceResponse(false, "Category not found or failed to be Deleated");

    }

    public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
    {



        var rawData = await _categoryInterface.GetAllAsync();
        if (!rawData.Any()) return [];
        return _mapper.Map<IEnumerable<GetCategoryDto>>(rawData);

    }

    public async Task<GetCategoryDto> GetByIdAsync(Guid id)
    {


        var rawData = await _categoryInterface.GetByIdAsync(id);
        if (rawData == null) return new GetCategoryDto();
        return _mapper.Map<GetCategoryDto>(rawData);

    }

    public async Task<ServiceResponse> UpdateAsync(UpdateCategoryDto category)
    {

        var mappedData = _mapper.Map<Category>(category);
        int result = await _categoryInterface.UpdateAsync(mappedData);
        return result > 0 ? new ServiceResponse(true, "Category Updated") :
       new ServiceResponse(false, "Category failed to be Updated");



    }
    #endregion
}

//////////////////////////////////
///

