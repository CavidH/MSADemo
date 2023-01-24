using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
       
        [HttpGet]
        [Authorize(Policy = "ReadServiceA")]
        public List<string> Get()
        {
            List<string> data = new List<string>();
            for (int i = 0; i < 566; i++)
            {
                data.Add($"Service A: {i},");
            }

            return data;
        }

         
    }
}
