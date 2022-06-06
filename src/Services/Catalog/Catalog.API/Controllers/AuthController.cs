using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        /*
        curl --location --request POST 'https://localhost:5011/connect/token' \
            --header 'Content-Type: application/x-www-form-urlencoded' \
            --data-urlencode 'grant_type=client_credentials' \
            --data-urlencode 'scope=Catalog' \
            --data-urlencode 'client_id=CatalogClient' \
            --data-urlencode 'client_secret=cmA1Z5oHWB'
         */

        /// <summary>Get user claims with key/value type</summary>
        /// <returns>List of key/value claims</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
