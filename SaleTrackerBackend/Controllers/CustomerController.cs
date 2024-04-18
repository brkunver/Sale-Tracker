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
  public async Task<ActionResult<ResponseDto<Customer>>> GetById([FromRoute] int id)
  {
    try
    {
      var currentCustomer = await customerRepo.GetByIdAsync(id);
      if (currentCustomer is null)
      {
        return NotFound(new ResponseDto<Customer> { Success = false, Message = "Not found" });
      }
      return Ok(new ResponseDto<Customer> { Data = currentCustomer });
    }
    catch (System.Exception)
    {

      return BadRequest(new ResponseDto<Customer> { Success = false, Message = "Error" });
    }
  }

}