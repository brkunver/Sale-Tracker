namespace SaleTrackerBackend.Controllers;

using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleTrackerBackend.Dto;
using SaleTrackerBackend.Models;
using SaleTrackerBackend.Repository;

[ApiController]
[Route("api/sale")]
//[Authorize]
public class SaleController : ControllerBase
{
  private readonly SaleRepository saleRepo;
  private readonly ProductSaleRepository productSaleRepo;
  public SaleController(SaleRepository saleRepository, ProductSaleRepository productSaleRepository)
  {
    saleRepo = saleRepository;
    productSaleRepo = productSaleRepository;
  }

  [HttpGet]
  public async Task<ActionResult<ResponseDto<List<GetSaleDto>?>>> GetSales([FromQuery] int? count, [FromQuery] int? page)
  {
    try
    {
      var sales = await saleRepo.GetSalesAsync(count ?? 5, page ?? 1);
      return Ok(new ResponseDto<List<GetSaleDto>>
      {
        Success = true,
        Data = sales.Adapt<List<GetSaleDto>>()
      });
    }
    catch (Exception e)
    {
      return StatusCode(500, new ResponseDto<List<GetSaleDto>?>
      {
        Success = false,
        Message = e.Message,
      });
    }
  }


  [HttpGet("{id}")]
  public async Task<ActionResult> GetSaleById(Guid id)
  {
    try
    {
      var sale = await saleRepo.GetSaleByIdAsync(id);
      var saleDetails = await productSaleRepo.GetSaleDetailsForSaleAsync(id);
      if (sale is null)
      {
        return NotFound(new ResponseDto<GetSaleDto?>
        {
          Success = false,
          Message = "Sale not found",
        });
      }
      return Ok(new
      {
        Success = true,
        Data = new
        {
          sale = sale.Adapt<GetSaleDto>(),
          saleDetails = saleDetails.Adapt<List<GetProductSaleDto>?>()
        }
      });
    }
    catch (Exception e)
    {
      return StatusCode(500, new ResponseDto<GetSaleDto?>
      {
        Success = false,
        Message = e.Message,
        Data = null
      });
    }
  }

  [HttpPost]
  public async Task<ActionResult> CreateSale([FromBody] CreateSaleDto input)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(new ResponseDto<GetSaleDto?>
      {
        Success = false,
        Message = "Invalid input",
        Data = null
      });
    }

    try
    {
      if (input.ProductSales is null || input.ProductSales.Count == 0)
      {
        return Ok(new ResponseDto<GetSaleDto?>
        {
          Success = false,
          Message = "No product sales provided",
        });
      }

      var sale = await saleRepo.CreateSaleAsync(input.Adapt<Sale>());
      if (sale is not null)
      {
        sale.Total = await productSaleRepo.CalculateTotalForSaleAsync(sale.Id);
        await saleRepo.SaveAsync();
      }
      return Ok(new ResponseDto<GetSaleDto>
      {
        Success = true,
        Data = sale.Adapt<GetSaleDto>()
      });
    }

    catch (Exception e)
    {
      return StatusCode(500, new ResponseDto<GetSaleDto?>
      {
        Success = false,
        Message = e.Message,
        Data = null
      });
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<ResponseDto<GetSaleDto?>>> UpdateSale([FromRoute] Guid id, [FromForm] UpdateSaleDto input)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(new ResponseDto<GetSaleDto?>
      {
        Success = false,
        Message = "Invalid input",
        Data = null
      });
    }

    try
    {
      var updatedSale = await saleRepo.UpdateSaleAsync(id, input.Adapt<Sale>());
      return Ok(new ResponseDto<GetSaleDto>
      {
        Success = true,
        Data = updatedSale.Adapt<GetSaleDto>()
      });
    }
    catch (Exception e)
    {
      return StatusCode(500, new ResponseDto<GetSaleDto?>
      {
        Success = false,
        Message = e.Message,
        Data = null
      });
    }
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<ResponseDto<GetSaleDto?>>> DeleteSale([FromRoute] Guid id)
  {
    try
    {
      await saleRepo.DeleteSaleAsync(id);
      return Ok(new ResponseDto<GetSaleDto>
      {
        Success = true,
      });
    }
    catch (Exception e)
    {
      return StatusCode(500, new ResponseDto<GetSaleDto?>
      {
        Success = false,
        Message = e.Message,
        Data = null
      });
    }
  }

  [HttpGet("last-sales")]
  public async Task<ActionResult<ResponseDto<List<decimal>?>>> GetLastSales([FromQuery] int? count)
  {
    try
    {
      var lastSales = await saleRepo.GetLastSalesAsync(count ?? 10);
      return Ok(new ResponseDto<List<decimal>?>
      {
        Success = true,
        Data = lastSales
      });
    }
    catch (Exception e)
    {
      return StatusCode(500, new ResponseDto<List<decimal>?>
      {
        Success = false,
        Message = e.Message,
        Data = null
      });
    }
  }

  [HttpGet("sum-of-last-sales")]
  public async Task<ActionResult<ResponseDto<decimal>?>> GetSumOfLastSales([FromQuery] int? days)
  {
    try
    {
      decimal sum = await saleRepo.GetSumOfLastSalesAsync(days ?? 7);
      return Ok(new ResponseDto<decimal>
      {
        Success = true,
        Data = sum
      });
    }
    catch (Exception e)
    {
      return StatusCode(500, new ResponseDto<decimal?>
      {
        Success = false,
        Message = e.Message,
      });
    }
  }
}