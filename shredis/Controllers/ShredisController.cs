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
        [Route("SetValue")]
        public ActionResult SetValue([FromBody] Request keyValue)
        {
            var obj = new ValueObject
            {
                Type = keyValue.Value.GetType().ToString(),
                Value = keyValue.Value
            };
            _memCache.Cache.Set(keyValue.Key, obj, new MemoryCacheEntryOptions
            {
                 Size = 1
            });
            return Ok(new Result
            {
                Code = 1,
                Message = $"{keyValue.Key} with type {keyValue.Value.GetType()} added."
            });
        }

        [HttpGet]
        [Route("GetValue")]
        public ActionResult GetValue(string key)
        {
            var exists = _memCache.Cache.TryGetValue(key, out var value);
            if (!exists)
            {
                return NotFound(new Result
                {
                    Code = -1,
                    Message = $"key: {key} not found."
                });
            }
            return Ok(value);
        }

        [HttpGet]
        [Route("KeyExists")]
        public ActionResult KeyExists(string key)
        {
            var exists = _memCache.Cache.TryGetValue(key, out var _);
            return Ok(new Result
            {
                Code = exists ? 1 : 0,
                Message = exists ? $"key: {key} exists." : $"key: {key} does not exists."
            });
        }
    }
}
