namespace SaleTrackerBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Mapster;
using SaleTrackerBackend.Models.Dto;
using SaleTrackerBackend.Repository;
using SaleTrackerBackend.Models;
using Microsoft.AspNetCore.Authorization;
using SixLabors.ImageSharp;
using SaleTrackerBackend.Services;

[ApiController]
[Route("api/product")]
//[Authorize]
public class ProductController : ControllerBase
{
    private readonly ProductRepository productRepo;
    private readonly CreateImageService createImageService;
    private readonly DeleteImageService deleteImageService;

    public ProductController(ProductRepository productRepository, IWebHostEnvironment webHostEnvironment, CreateImageService _createImageService, DeleteImageService _deleteImageService)
    {
        productRepo = productRepository;
        createImageService = _createImageService;
        deleteImageService = _deleteImageService;

    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto<string>>> Create([FromForm] CreateProductDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Invalid data" });
        }
        var fileName = "default.jpg";
        if (input.FormFile is not null)
        {
            fileName = await createImageService.CreateImage(input.FormFile);
        }
        var product = input.Adapt<Product>();
        product.ImageUrl = fileName;

        try
        {
            await productRepo.CreateAsync(product);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = ex.Message });
        }

        return Ok(new ResponseDto<string> { Success = true, Message = "Created" });
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto<List<GetProductDto>>>> GetAll([FromQuery] int? page, int? count)
    {
        try
        {
            var products = await productRepo.GetAllAsync(page ?? 1, count ?? 5);
            products ??= [];
            return Ok(new ResponseDto<List<GetProductDto>> { Data = products.Select(p => p.Adapt<GetProductDto>()).ToList() });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<List<GetProductDto>> { Success = false, Message = ex.Message });
        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDto<GetProductDto>>> GetById([FromRoute] int id)
    {
        try
        {
            var product = await productRepo.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound(new ResponseDto<GetProductDto> { Success = false, Message = "Not found" });
            }
            return Ok(new ResponseDto<GetProductDto> { Data = product.Adapt<GetProductDto>() });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<GetProductDto> { Success = false, Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto<string>>> Update([FromRoute] int id, [FromForm] UpdateProductDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Invalid data input" });
        }


        var existingProduct = await productRepo.GetByIdAsync(id);

        if (existingProduct is null)
        {
            return NotFound(new ResponseDto<string> { Success = false, Message = "Not Found" });
        }

        var updatedProduct = input.Adapt<Product>();

        if (input.FormFile is not null)
        {
            deleteImageService.DeleteImage(existingProduct.ImageUrl);
            updatedProduct.ImageUrl = await createImageService.CreateImage(input.FormFile);
        }

        try
        {
            await productRepo.UpdateAsync(id, updatedProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = ex.Message });
        }

        return Ok(new ResponseDto<string> { Success = true, Message = "Updated" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto<string>>> Delete([FromRoute] int id)
    {
        try
        {
            await productRepo.MarkDeletedAsync(id);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = ex.Message });
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