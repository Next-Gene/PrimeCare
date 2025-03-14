using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace eCommercecApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }


        [HttpGet("all")]

        public async Task<IActionResult> GetAllProducts()
        {
            var data = await _productService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);


        }


        [HttpGet("single/{id}")]


        public async Task<IActionResult> GetSingle(Guid id)
        {
            var data = await _productService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);


        }


        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateProductDto product)
        {
            var result = await _productService.AddAsync(product);
            return result.Success ? Ok(result) : BadRequest(result);





        }



        [HttpPut("update")]
        public async Task<IActionResult> update(UpdateProductDto updateProduct)
        {
            var result = await _productService.UpdateAsync(updateProduct);
            return result.Success ? Ok(result) : BadRequest(result);





        }



        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);





        }

    }

}
