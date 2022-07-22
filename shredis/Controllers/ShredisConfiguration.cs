using Microsoft.AspNetCore.Mvc;

namespace shredis.Controllers
{
    [Route("Configuration")]
    [ApiController]
    public class ShredisConfiguration : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ShredisConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetCacheSizeLimit")]
        public ActionResult GetCacheSizeLimit()
        {
            return Ok(_configuration.GetSection("CacheSizeLimit").Value);
        }
    }
}
