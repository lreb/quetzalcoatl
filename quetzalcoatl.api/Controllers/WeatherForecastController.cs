using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quetzalcoatl.BusinessLogic.CQRS.Weather.Query;
using Quetzalcoatl.Domain.Models.WeatherForecast;
using Quetzalcoatl.Infrastructure.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quetzalcoatl.Api.Controllers
{
    /// <summary>
    /// demo controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<WeatherForecastController> _logger;
        /// <summary>
        /// 
        /// </summary>
        private IMediator _mediator;

        /// <summary>
        /// Demo controller constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// test weather
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            try
            {
                return await Mediator.Send(new GetWeatherForecastQueryRequest { DateTime = DateTime.Now });
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
