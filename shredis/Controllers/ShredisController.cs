using Microsoft.AspNetCore.Mvc;
using shredis.Infrastucture;
using shredis.Models;
using Swashbuckle.AspNetCore.Annotations;

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
            _logger = logger;
            _memCache = memCache;
        }

        [HttpPost]
        [Route("InsertValue")]
        [SwaggerResponse(400, "key already exists", typeof(Result))]
        [SwaggerResponse(200, "key added successfully", typeof(Result))]
        [SwaggerOperation(Summary = "Insert a keyvalue object in store")]
        public ActionResult InsertValue([FromBody] Request keyValue)
        {
            if (_memCache.KeyExists(keyValue.Key))
                return BadRequest(new Result
                {
                    Code = -1,
                    Message = $"key: [{keyValue.Key}] already exists."
                });
            _memCache.SetValue(keyValue);
            return Ok(new Result
            {
                Code = 1,
                Message = $"key: [{keyValue.Key}] with type: [{keyValue.Value.GetType()}] added."
            });
        }

        [HttpPatch]
        [Route("UpdateValue")]
        [SwaggerResponse(400, "key does not exists", typeof(Result))]
        [SwaggerResponse(200, "key updated successfully", typeof(Result))]
        [SwaggerOperation(Summary = "Update a keyvalue object in store")]
        public ActionResult UpdateValue([FromBody] Request keyValue)
        {
            if (!_memCache.KeyExists(keyValue.Key))
                return BadRequest(new Result
                {
                    Code = -1,
                    Message = $"key: [{keyValue.Key}] does not exists."
                });
            _memCache.SetValue(keyValue);
            return Ok(new Result
            {
                Code = 1,
                Message = $"key: [{keyValue.Key}] with type: [{keyValue.Value.GetType()}] added."
            });
        }

        [HttpGet]
        [Route("GetValue/{key}")]
        [SwaggerResponse(404, "key not found", typeof(Result))]
        [SwaggerResponse(200, "object found", typeof(Result))]
        [SwaggerOperation(Summary = "Get a keyvalue object in store")]
        public ActionResult GetValue(string key)
        {
            var exists = _memCache.Cache.TryGetValue(key, out var value);
            if (!exists)
            {
                return NotFound(new Result
                {
                    Code = -1,
                    Message = $"key: [{key}] not found."
                });
            }
            return Ok(new Result
            {
                Code = 1,
                Message = "Ok",
                Value = (ValueObject)value
            });
        }

        [HttpDelete]
        [Route("DeleteKey/{key}")]
        [SwaggerResponse(404, "key not found", typeof(Result))]
        [SwaggerResponse(200, "key deleted successfully", typeof(Result))]
        [SwaggerOperation(Summary = "Delete a keyvalue object in store")]
        public ActionResult DeleteKey(string key)
        {
            var exists = _memCache.Cache.TryGetValue(key, out var _);
            if (!exists)
            {
                return NotFound(new Result
                {
                    Code = -1,
                    Message = $"key: [{key}] not found."
                });
            }
            _memCache.Cache.Remove(key);
            return Ok(new Result
            {
                Code = 1,
                Message = $"key: [{key}] has been deleted."
            });
        }

        [HttpGet]
        [Route("KeyExists/{key}")]
        [SwaggerResponse(200, "key added successfully", typeof(Result))]
        [SwaggerOperation(Summary = "Check is key exists in store")]
        public ActionResult KeyExists(string key)
        {
            var exists = _memCache.KeyExists(key);
            if (!exists)
                return NotFound(new Result
                {
                    Code = 0,
                    Message = $"key: [{key}] does not exists"
                });
            return Ok(new Result
            {
                Code = 1,
                Message = $"key: [{key}] exists."
            });
        }
    }
}
