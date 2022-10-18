using MediatR;
using Quetzalcoatl.Domain.Models.WeatherForecast;

namespace Quetzalcoatl.BusinessLogic.CQRS.Weather.Query
{
    public class GetWeatherForecastQueryRequest : IRequest<IEnumerable<WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public DateTime DateTime { get; set; }
        public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQueryRequest, IEnumerable<WeatherForecast>>
        {
            //private ILogger<GetWeatherForecastQueryHandler> _logger;
            public GetWeatherForecastQueryHandler()
                //ILogger<GetWeatherForecastQueryHandler> logger
                //)
            {
                //_logger = logger;
            }
            public async Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastQueryRequest query, CancellationToken cancellationToken)
            {
                var task = Task.Run(() => CalculateWeatherForecast(query));
                return await task; 
            }
            private WeatherForecast[] CalculateWeatherForecast(GetWeatherForecastQueryRequest query) 
            {
                var dateTime = query.DateTime;
                var a = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = dateTime.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray();
                return a;
            }

        }
    }
}
