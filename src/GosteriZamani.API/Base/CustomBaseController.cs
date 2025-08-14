using Microsoft.AspNetCore.Mvc;

namespace GosteriZamani.API.Base;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]
    public IActionResult CreateActionResult<T>(AppResult<T> response)
    {
        if (response.StatusCode == 204)
            return new ObjectResult(null) { StatusCode = response.StatusCode };
        return new ObjectResult(response) { StatusCode = response.StatusCode };
    }
}
