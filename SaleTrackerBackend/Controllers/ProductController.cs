namespace SaleTrackerBackend.Controllers;

using Mapster;
using Microsoft.AspNetCore.Mvc;
using SaleTrackerBackend.Dto;
using SaleTrackerBackend.Models;
using SaleTrackerBackend.Repository;
using SaleTrackerBackend.Services;
using SixLabors.ImageSharp;

[ApiController]
[Route("api/product")]
//[Authorize]
public class ProductController : ControllerBase
{
    private readonly ProductRepository productRepo;
    private readonly CreateImageService createImageService;
    private readonly DeleteImageService deleteImageService;

#pragma warning disable IDE0290 // Use primary constructor
    public ProductController(ProductRepository productRepository, CreateImageService _createImageService, DeleteImageService _deleteImageService)

    {
        productRepo = productRepository;
        createImageService = _createImageService;
        deleteImageService = _deleteImageService;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto<Product?>>> Create([FromForm] CreateProductDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<Product?> { Success = false, Message = "Invalid data" });
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
            return BadRequest(new ResponseDto<Product?> { Success = false, Message = ex.Message });
        }
        return Ok(new ResponseDto<Product?> { Success = true, Message = "Created", Data = product });
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto<List<GetProductDto>?>>> GetAll([FromQuery] int? page, int? count, [FromQuery] string? name, bool? returnDeleted = false)
    {
        try
        {
            var products = await productRepo.GetAllAsync(page ?? 1, count ?? 5, name, returnDeleted);
            products ??= [];
            return Ok(new ResponseDto<List<GetProductDto>> { Data = products.Select(p => p.Adapt<GetProductDto>()).ToList() });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<List<GetProductDto?>> { Success = false, Message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDto<GetProductDto?>>> GetById([FromRoute] Guid id)
    {
        try
        {
            var product = await productRepo.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound(new ResponseDto<GetProductDto?> { Success = false, Message = "Product not found" });
            }
            return Ok(new ResponseDto<GetProductDto> { Data = product.Adapt<GetProductDto>() });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<GetProductDto?> { Success = false, Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto<GetProductDto?>>> Update([FromRoute] Guid id, [FromForm] UpdateProductDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<GetProductDto?> { Success = false, Message = "Invalid data input" });
        }

        try
        {
            var existingProduct = await productRepo.GetByIdAsync(id);

            if (existingProduct is null)
            {
                return NotFound(new ResponseDto<GetProductDto?> { Success = false, Message = "Product not found" });
            }

            var updatedProduct = input.Adapt<Product>();

            if (input.FormFile is not null)
            {
                deleteImageService.DeleteImage(existingProduct.ImageUrl);
                updatedProduct.ImageUrl = await createImageService.CreateImage(input.FormFile);
            }

            await productRepo.UpdateAsync(id, updatedProduct);
            return Ok(new ResponseDto<GetProductDto> { Success = true, Message = "Updated", Data = updatedProduct.Adapt<GetProductDto>() });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<GetProductDto?> { Success = false, Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto<string?>>> Delete([FromRoute] Guid id)
    {
        try
        {
            var product = await productRepo.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound(new ResponseDto<string?> { Success = false, Message = "Product not found" });
            }
            deleteImageService.DeleteImage(product.ImageUrl);
            product.ImageUrl = "default.jpg";

            await productRepo.MarkDeletedAsync(id);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<string?> { Success = false, Message = ex.Message });
        }
        return Ok(new ResponseDto<string> { Success = true, Message = "Deleted" });
    }

    [HttpGet("count")]
    public async Task<ActionResult<ResponseDto<int?>>> GetCount()
    {
        try
        {
            var count = await productRepo.GetCountAsync();
            return Ok(new ResponseDto<int?> { Data = count });
        }
        catch (Exception)
        {
            return BadRequest(new ResponseDto<int?> { Message = "error", Success = false, Data = null });
        }
    }


}