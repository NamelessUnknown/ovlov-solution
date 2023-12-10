using calc_api.Models;
using congestion.calculator;
using congestion.calculator.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace calc_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxController : ControllerBase
    {
        private readonly ILogger<CongestionTaxController> _logger;

        private readonly ICongestionTaxCalculator _congestionTaxCalculator;

        public CongestionTaxController(ILogger<CongestionTaxController> logger, ICongestionTaxCalculator congestionTaxCalculator)
        {
            _logger = logger;
            _congestionTaxCalculator = congestionTaxCalculator;
        }

        [HttpPost]
        [Route("calculate")]
        public IActionResult CalculateTollTax([FromBody] TrafficData trafficData)
        {
            IVehicle vehicle = (IVehicle)_congestionTaxCalculator.ConvertStringToVehicle(trafficData.Vehicle);

            DateTime[] dateTimeArray = _congestionTaxCalculator.ConvertStringArrayIntoDateArray(trafficData.Dates);

            var calcResult = _congestionTaxCalculator.GetTax(vehicle, dateTimeArray);

            string serializedCalcResult = JsonConvert.SerializeObject(calcResult);

            return Ok(serializedCalcResult);
        }
    }
}
