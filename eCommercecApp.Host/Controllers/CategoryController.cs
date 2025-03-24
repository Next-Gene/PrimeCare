using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommercecApp.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _categoryService.GetAllAsync();

        return data.Any() ? Ok(data) : NotFound(data);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(Guid id)
    {
        var data = await _categoryService.GetByIdAsync(id);

        return data != null ? Ok(data) : NotFound(data);
    }


    [HttpPost("add")]
    public async Task<IActionResult> Add(CreateCategoryDto createCategory)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _categoryService.AddAsync(createCategory);

        return result.Success ? Ok(result) : BadRequest(result);
    }



    [HttpPut("update")]
    public async Task<IActionResult> update(UpdateCategoryDto updateCategory)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _categoryService.UpdateAsync(updateCategory);

        return result.Success ? Ok(result) : BadRequest(result);
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _categoryService.DeleteAsync(id);

        return result.Success ? Ok(result) : BadRequest(result);
    }

}


