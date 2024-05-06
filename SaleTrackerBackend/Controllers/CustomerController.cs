namespace SaleTrackerBackend.Controllers;

using Mapster;
using Microsoft.AspNetCore.Mvc;
using SaleTrackerBackend.Dto;
using SaleTrackerBackend.Models;
using SaleTrackerBackend.Repository;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
  private readonly CustomerRepository customerRepo;
  public CustomerController(CustomerRepository _customerRepo)
  {
    customerRepo = _customerRepo;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<ResponseDto<GetCustomerDto?>>> GetById([FromRoute] Guid id, [FromQuery] bool includeDeleted = false)
  {
    try
    {
      var currentCustomer = await customerRepo.GetByIdAsync(id);
      if (currentCustomer is null)
      {
        return NotFound(new ResponseDto<GetCustomerDto?> { Success = false, Message = "Customer Not found" });
      }
      return Ok(new ResponseDto<GetCustomerDto> { Data = currentCustomer.Adapt<GetCustomerDto>() });
    }
    catch (Exception ex)
    {
      return BadRequest(new ResponseDto<GetCustomerDto?> { Success = false, Message = ex.Message });
    }
  }

  [HttpGet]
  public async Task<ActionResult<ResponseDto<List<GetCustomerDto>?>>> GetAll([FromQuery] int? page, int? count, bool? includeDeleted, [FromQuery] string? name)
  {
    try
    {
      var currentCustomer = await customerRepo.GetAllAsync(page ?? 1, count ?? 5, includeDeleted ?? false, name);
      currentCustomer ??= [];
      return Ok(new ResponseDto<List<GetCustomerDto>> { Data = currentCustomer.Select(c => c.Adapt<GetCustomerDto>()).ToList() });
    }
    catch (Exception ex)
    {
      return BadRequest(new ResponseDto<List<GetCustomerDto>?> { Success = false, Message = ex.Message, Data = null });
    }
  }

  [HttpPost]
  public async Task<ActionResult<ResponseDto<GetCustomerDto?>>> Create([FromBody] CreateCustomerDto customerDto)
  {

    if (!ModelState.IsValid)
    {
      return BadRequest(new ResponseDto<GetCustomerDto?> { Success = false, Message = "Invalid data input" });
    }

    try
    {
      var newCustomer = customerDto.Adapt<Customer>();
      await customerRepo.CreateAsync(newCustomer);
      return Ok(new ResponseDto<GetCustomerDto> { Data = newCustomer.Adapt<GetCustomerDto>() });
    }
    catch (Exception ex)
    {
      return BadRequest(new ResponseDto<GetCustomerDto?> { Success = false, Message = ex.Message });
    }
  }


  [HttpDelete("{id}")]
  public async Task<ActionResult<ResponseDto<GetCustomerDto?>>> Delete([FromRoute] Guid id)
  {
    try
    {
      await customerRepo.MarkDeletedAsync(id);
      await customerRepo.SaveAsync();
      return Ok(new ResponseDto<GetCustomerDto?> { Data = null });
    }
    catch (Exception ex)
    {
      return BadRequest(new ResponseDto<GetCustomerDto?> { Success = false, Message = ex.Message, Data = null });
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<ResponseDto<GetCustomerDto>>> Update([FromRoute] Guid id, [FromBody] UpdateCustomerDto customerDto)
  {

    if (!ModelState.IsValid)
    {
      return BadRequest(new ResponseDto<GetCustomerDto?> { Success = false, Message = "Invalid data input" });
    }

    try
    {
      var updatedCustomer = await customerRepo.UpdateAsync(id, customerDto);
      return Ok(new ResponseDto<GetCustomerDto> { Data = updatedCustomer.Adapt<GetCustomerDto>() });
    }
    catch (Exception ex)
    {
      return BadRequest(new ResponseDto<GetCustomerDto?> { Success = false, Message = ex.Message });
    }
  }

  [HttpGet("count")]
  public async Task<ActionResult<ResponseDto<int?>>> Count()
  {
    try
    {
      var count = await customerRepo.GetCountAsync();
      return Ok(new ResponseDto<int> { Data = count });
    }
    catch (Exception ex)
    {
      return BadRequest(new ResponseDto<int?> { Success = false, Message = ex.Message });
    }
  }

}