namespace SaleTrackerBackend.Controllers;

using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleTrackerBackend.Models;
using SaleTrackerBackend.Models.Dto;
using SaleTrackerBackend.Repository;

[ApiController]
[Route("api/sale")]
//[Authorize]
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

        try
        {
            await saleRepo.CreateAsync(input.Adapt<Sale>());
            return Ok(new ResponseDto<string> { Success = true, Message = "Created" });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = $"Failed to create sale: {ex.Message}" });
        }
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

        try
        {
            await saleRepo.UpdateAsync(id, input.Adapt<Sale>());
            return Ok(new ResponseDto<string> { Success = true, Message = "Updated" });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto<string>>> Delete([FromRoute] int id)
    {
        try
        {
            await saleRepo.DeleteByIdAsync(id);
            return Ok(new ResponseDto<string> { Success = true, Message = "Deleted" });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<string> { Success = false, Message = $"Failed to delete: {ex.Message}" });
        }
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

    [HttpGet]
    public async Task<ActionResult<ResponseDto<List<GetCompleteSaleDto>>>> GetAllWithProducts([FromQuery] int? page, int? count)
    {
        var products = await saleRepo.GetCompleteSalesAsync(page ?? 1, count ?? 5) ?? [];
        return Ok(new ResponseDto<List<GetCompleteSaleDto>> { Data = products });
    }


    [HttpGet("revenue")]
    public async Task<ActionResult<ResponseDto<decimal>>> GetTotalRevenueLastMonth(DateTime? startDate, DateTime? endDate)
    {
        try
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;
            var totalRevenue = await saleRepo.GetTotalSaleRevenueBetweenIntervals((DateTime)startDate, (DateTime)endDate);
            return Ok(new ResponseDto<decimal> { Data = totalRevenue });
        }
        catch (Exception)
        {
            return BadRequest(new ResponseDto<decimal> { Message = "Error", Success = false });
        }
    }
}