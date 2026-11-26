using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartPlantApi.Dtos;

namespace SmartPlantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromBody] SensorDataRequest request)
        {
            if (double.IsNaN(request.SoilMoisturePercent) ||
                double.IsInfinity(request.SoilMoisturePercent) ||
                request.SoilMoisturePercent < 0 ||
                request.SoilMoisturePercent > 100)
            {
                return BadRequest("Jordfugtighed skal være mellem 0 og 100.");
            }
            if (double.IsNaN(request.LightLevelLux) ||
                 double.IsInfinity(request.LightLevelLux) ||
                  request.LightLevelLux < 0)
            {
                return BadRequest("Lysniveau skal være et gyldigt tal og må ikke være negativt.");
            }
            if (request.RecordedAt == default)
            {
                return BadRequest("Tidspunktet for målingen er ugyldigt.");
            }

            return Ok(request);
        }

    }
}
