// namespace SaleTrackerBackend.Controllers;

// using Mapster;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using SaleTrackerBackend.Models;
// using SaleTrackerBackend.Models.Dto;
// using SaleTrackerBackend.Repository;

// [ApiController]
// [Route("api/sale")]
// //[Authorize]
// public class SaleController : ControllerBase
// {

//   private readonly SaleRepository saleRepo;
//   public SaleController(SaleRepository saleRepository)
//   {
//     saleRepo = saleRepository;
//   }


//   [HttpGet("{id}")]
//   public async Task<ActionResult<ResponseDto<Sale?>>> GetSaleById(int id)
//   {
//     try
//     {
//       var sale = await saleRepo.GetSaleByIdAsync(id);
//       if (sale == null)
//       {
//         return NotFound(new ResponseDto<Sale?>
//         {
//           Success = false,
//           Message = "Sale not found",
//           Data = null
//         });
//       }
//       return Ok(new ResponseDto<Sale>
//       {
//         Success = true,
//         Data = sale
//       });
//     }
//     catch (Exception e)
//     {
//       return StatusCode(500, new ResponseDto<Sale?>
//       {
//         Success = false,
//         Message = e.Message
//       });
//     }

//   }

// }