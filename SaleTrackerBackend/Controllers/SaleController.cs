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
  public SaleController(SaleRepository saleRepository)
  {
    saleRepo = saleRepository;
  }

  [HttpGet]
  public async Task<ActionResult<ResponseDto<List<GetSaleDto>>>> GetSales()
  {
    try
    {
      var sales = await saleRepo.GetSalesAsync();
      return Ok(new ResponseDto<List<GetSaleDto>>
      {
        Success = true,
        Data = sales.Adapt<List<GetSaleDto>>()
      });
    }
    catch (Exception e)
    {
      return StatusCode(500, new ResponseDto<List<GetSaleDto>>
      {
        Success = false,
        Message = e.Message,
        Data = null
      });
    }
  }


  [HttpGet("{id}")]
  public async Task<ActionResult<ResponseDto<GetSaleDto?>>> GetSaleById(Guid id)
  {
    try
    {
      var sale = await saleRepo.GetSaleByIdAsync(id);
      if (sale == null)
      {
        return NotFound(new ResponseDto<GetSaleDto?>
        {
          Success = false,
          Message = "Sale not found",
          Data = null
        });
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

  [HttpPost]
  public async Task<ActionResult<ResponseDto<GetSaleDto?>>> CreateSale([FromForm] CreateSaleDto input)
  {
    try
    {
      var newSale =  input.Adapt<Sale>();
      var createdSale = await saleRepo.CreateSaleAsync(newSale);
      return Ok(new ResponseDto<GetSaleDto>
      {
        Success = true,
        Data = createdSale.Adapt<GetSaleDto>()
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

}