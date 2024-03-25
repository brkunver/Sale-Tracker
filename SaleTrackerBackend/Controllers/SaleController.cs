namespace SaleTrackerBackend.Controllers;

using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleTrackerBackend.Models;
using SaleTrackerBackend.Models.Dto;
using SaleTrackerBackend.Repository;

[ApiController]
[Route("api/sale")]
[Authorize]
public class SaleController : ControllerBase
{

    private readonly SaleRepository saleRepo;
    public SaleController(SaleRepository saleRepository)
    {
        saleRepo = saleRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto<string>>> Create([FromBody] CreateSaleDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Invalid data" });
        }

        var result = await saleRepo.CreateAsync(input.Adapt<Sale>());

        if (!result)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Cannot created" });
        }

        return Ok(new ResponseDto<string> { Success = true, Message = "Created" });
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto<List<GetSaleDto>>>> GetAll([FromQuery] int? page)
    {
        var products = await saleRepo.GetAllAsync(page ?? 1) ?? [];
        return Ok(new ResponseDto<List<GetSaleDto>> { Data = products.Select(p => p.Adapt<GetSaleDto>()).ToList() });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDto<GetSaleDto>>> GetById([FromRoute] int id)
    {
        var product = await saleRepo.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound(new ResponseDto<GetSaleDto> { Success = false, Message = "Not found" });
        }
        return Ok(new ResponseDto<GetSaleDto> { Data = product.Adapt<GetSaleDto>() });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto<string>>> Update([FromRoute] int id, [FromBody] UpdateSaleDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Invalid data" });
        }

        var result = await saleRepo.UpdateAsync(id, input.Adapt<Sale>());

        if (!result)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = "Cannot update" });
        }

        return Ok(new ResponseDto<string> { Success = true, Message = "Updated" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto<string>>> Delete([FromRoute] int id)
    {
        var result = await saleRepo.DeleteByIdAsync(id);
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
            var count = await saleRepo.GetCountAsync();
            return Ok(new ResponseDto<int> { Data = count });
        }
        catch (Exception)
        {
            return BadRequest(new ResponseDto<int> { Message = "error", Success = false });
        }
    }
}