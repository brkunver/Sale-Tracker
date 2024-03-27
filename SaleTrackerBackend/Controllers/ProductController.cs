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
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly CreateImageService createImageService;
    private readonly DeleteImageService deleteImageService;

    public ProductController(ProductRepository productRepository, IWebHostEnvironment webHostEnvironment, CreateImageService _createImageService, DeleteImageService _deleteImageService)
    {
        productRepo = productRepository;
        createImageService = _createImageService;
        deleteImageService = _deleteImageService;
        _webHostEnvironment = webHostEnvironment;
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
        var result = await productRepo.CreateAsync(product);

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
        products ??= [];
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
    public async Task<ActionResult<ResponseDto<string>>> Update([FromRoute] int id, [FromForm] UpdateProductDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Invalid data" });
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

        var result = await productRepo.UpdateAsync(id, updatedProduct);

        if (!result)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Cannot update" });
        }

        return Ok(new ResponseDto<string> { Success = true, Message = "Updated" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto<string>>> Delete([FromRoute] int id)
    {
        var product = await productRepo.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound(new ResponseDto<string> { Success = false, Message = "Not found" });
        }

        var result = await productRepo.DeleteByIdAsync(id);
        if (!result)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Cannot deleted" });
        }

        deleteImageService.DeleteImage(product.ImageUrl);
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


    [HttpGet("image/{imagename}")]
    public IActionResult GetImage(string imagename)
    {
        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", imagename);
        if (!System.IO.File.Exists(imagePath))
        {
            return NotFound();
        }
        var imageBytes = System.IO.File.ReadAllBytes(imagePath);
        return File(imageBytes, "image/jpeg");
    }

}