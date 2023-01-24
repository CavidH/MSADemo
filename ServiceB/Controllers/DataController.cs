using Microsoft.AspNetCore.Mvc;


namespace ServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            List<string> data = new List<string>();
            for (int i = 0; i < 566; i++)
            {
                data.Add($"Service B: {i},");
            }

            return data;
        }
    }
}
