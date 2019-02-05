using System.Threading.Tasks;
using KeyValueService.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyValueService.Controllers
{
    [Route("api/v1/storage")]
    public class KeyValuesController : Controller
    {
        private readonly IKeyValueService _keyValueService;

        public KeyValuesController(IKeyValueService keyValueService)
        {
            _keyValueService = keyValueService;
        }

        /// <summary>
        /// Get a list of all keys that have values.
        /// </summary>
        /// <returns></returns>
        [HttpGet("keys")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _keyValueService.GetKeysAsync());
        }

        /// <summary>
        /// Get key value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="format">If format=true that return formatting value.</param>
        /// <returns></returns>
        [HttpGet("keys/{key}")]
        public async Task<IActionResult> Get(string key, bool format = false)
        {
            var result = await _keyValueService.GetValueAsync(key, format);
            if (result.Item1)
                return Ok(result.Item2);
            else
                return NotFound();
        }

        /// <summary>
        /// Set key value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("keys/{key}")]
        public async Task<IActionResult> Post(string key, string value)
        {
            if (value == null)
            {
                ModelState.AddModelError("value", "Required");
                return BadRequest(ModelState);
            }
            var result = await _keyValueService.SetValueAsync(key, value);
            if (result.Item1)
                return Ok(result.Item2);
            else
                return BadRequest();
        }

        /// <summary>
        /// Delete key value.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete("keys/{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            var result = await _keyValueService.RemoveValueAsync(key);
            return Ok(result);
        }
    }
}
