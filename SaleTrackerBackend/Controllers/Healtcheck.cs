namespace SaleTrackerBackend.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleTrackerBackend.Dto;

[ApiController]
[Route("/health")]
public class Healthcheck : ControllerBase
{
    [HttpGet]
    [Authorize]
    public ActionResult<ResponseDto<string>> Get()
    {
        var result = new ResponseDto<string>();
        return Ok(result);
    }
}