namespace SaleTrackerBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Mapster;
using SaleTrackerBackend.Models.Dto;
using SaleTrackerBackend.Repository;
using SaleTrackerBackend.Models;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/product")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly ProductRepository productRepo;
    public ProductController(ProductRepository productRepository)
    {
        productRepo = productRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto<string>>> Create([FromBody] CreateProductDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Invalid data" });
        }

        var result = await productRepo.CreateAsync(input.Adapt<Product>());

        if (!result)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Cannot created" });
        }

        return Ok(new ResponseDto<string> { Success = true, Message = "Created" });
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto<List<GetProductDto>>>> GetAll([FromQuery] int? page)
    {

        var products = await productRepo.GetAllAsync(page ?? 1);
        if (products is null)
        {
            products = [];
        }
        return Ok(new ResponseDto<List<GetProductDto>> { Data = products.Select(p => p.Adapt<GetProductDto>()).ToList() });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDto<GetProductDto>>> GetById([FromRoute] int id)
    {
        var product = await productRepo.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound(new ResponseDto<GetProductDto> { Success = false, Message = "Not found" });
        }
        return Ok(new ResponseDto<GetProductDto> { Data = product.Adapt<GetProductDto>() });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto<string>>> Update([FromRoute] int id, [FromBody] UpdateProductDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Invalid data" });
        }

        var result = await productRepo.UpdateAsync(id, input.Adapt<Product>());

        if (!result)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Cannot update" });
        }

        return Ok(new ResponseDto<string> { Success = true, Message = "Updated" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto<string>>> Delete([FromRoute] int id)
    {
        var result = await productRepo.DeleteByIdAsync(id);
        if (!result)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Cannot deleted" });
        }
        return Ok(new ResponseDto<string> { Success = true, Message = "Deleted" });
    }

    [HttpGet("count")]
    public async Task<ActionResult<ResponseDto<int>>> GetCount()
    {
        try
        {
            var count = await productRepo.GetCountAsync();
            return Ok(new ResponseDto<int> { Data = count });
        }
        catch (Exception)
        {
            return BadRequest(new ResponseDto<int> { Message = "error", Success = false });
        }
    }
}