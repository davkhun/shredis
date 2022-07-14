using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using shredis.Infrastucture;
using shredis.Models;

namespace shredis.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShredisController : ControllerBase
    {
        private readonly ILogger<ShredisController> _logger;
        private readonly ShredisCache _memCache;
        public ShredisController(ILogger<ShredisController> logger, ShredisCache memCache)
        {
            _logger= logger;
            _memCache = memCache;
        }

        [HttpPost]
        [Route("Set")]
        public ActionResult Set(string key, string value)
        {
            _memCache.Cache.Set(key, value, new MemoryCacheEntryOptions().SetSize(1));
            return Ok();
        }

        [HttpGet]
        [Route("Get")]
        public ActionResult Get(string key)
        {
            var value = _memCache.Cache.Get(key);
            if (value == null)
            {
                return NotFound(new Result
                {
                    Code = -1,
                    Message = "Key not found"
                });
            }
            return Ok(value);
        }
    }
}
