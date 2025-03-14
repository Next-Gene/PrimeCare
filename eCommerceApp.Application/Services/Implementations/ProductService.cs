using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IGeneric<Product> _productInterface;
    private readonly IMapper _mapper;

    public ProductService(IGeneric<Product> productInterface ,IMapper mapper)
    {
        _productInterface = productInterface;
        _mapper = mapper;
    }

    #region Mehtods
    public async Task<ServiceResponse> AddAsync(CreateProductDto product)
    {

        var mappedData=_mapper.Map<Product>(product);
        int result = await _productInterface.AddAsync(mappedData);
        return result > 0 ? new ServiceResponse(true, "Product Added") :
       new ServiceResponse(false, "Product failed to be Added");


    }

    public  async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        int result = await _productInterface.DeleteAsync(id);
     
        return result > 0 ? new ServiceResponse(true, "Product Deleated") : 
       new ServiceResponse(false, "Product Not Found or  failed to be Deleated");
        





    }

    public async Task<IEnumerable<GetProductDto>> GetAllAsync()
    {

        var rawData = await _productInterface.GetAllAsync();
        if (!rawData.Any()) return [];
        return _mapper.Map<IEnumerable<GetProductDto>>(rawData);



    }

    public async Task<GetProductDto> GetByIdAsync(Guid id)
    {

        var rawData = await _productInterface.GetByIdAsync(id);
        if (rawData == null) return new GetProductDto();
        return _mapper.Map<GetProductDto>(rawData);





    }

    public  async Task<ServiceResponse> UpdateAsync(UpdateProductDto product)
    {

        var mappedData = _mapper.Map<Product>(product);
        int result = await _productInterface.UpdateAsync(mappedData);
        return result > 0 ? new ServiceResponse(true, "Product Updated") :
       new ServiceResponse(false, "Product failed to be Updated");


    }
    #endregion
}
