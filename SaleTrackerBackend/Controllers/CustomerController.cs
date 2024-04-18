using Mapster;
using Microsoft.AspNetCore.Mvc;
using SaleTrackerBackend.Models;
using SaleTrackerBackend.Models.Dto;
using SaleTrackerBackend.Repository;

namespace SaleTrackerBackend.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{

  private readonly CustomerRepository customerRepo;
  public CustomerController(CustomerRepository customerRepo)
  {
    this.customerRepo = customerRepo;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<ResponseDto<GetCustomerDto>>> GetById([FromRoute] int id)
  {
    try
    {
      var currentCustomer = await customerRepo.GetByIdAsync(id);
      if (currentCustomer is null)
      {
        return NotFound(new ResponseDto<GetCustomerDto> { Success = false, Message = "Not found" });
      }
      return Ok(new ResponseDto<GetCustomerDto> { Data = currentCustomer.Adapt<GetCustomerDto>() });
    }
    catch (System.Exception)
    {

      return BadRequest(new ResponseDto<GetCustomerDto> { Success = false, Message = "Error" });
    }
  }

}