using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Testing1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            throw new Exception("Error!");
        }
    }
}
