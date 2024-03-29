﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Multitenant.TenancyContext;
using Multitenant.TenancyContext.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitenant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// Get the value
        /// </summary>
        /// <param name="definitionId"></param>
        /// <returns></returns>
        [HttpGet("tenant")]
        public async Task<string> GetValue(Guid definitionId)
        {
            return await Task.FromResult(HttpContext.GetTenant().Id);
        }
    }
}
