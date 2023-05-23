using Microsoft.AspNetCore.Mvc;

namespace RestaurantAppi.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {
     
    }
}
