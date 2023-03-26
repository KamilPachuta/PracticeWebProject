using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = _service.Get();
            return result;
        }

        [HttpGet("currentDay/{max}")] // dodanie konstruktora z sciezka to drugi sposób
        //[Route("currentDay")] // pierwszy sposób na dodanie dodatkowej scie¿ki
        public IEnumerable<WeatherForecast> Get2([FromQuery]int take, [FromRoute]int max)
        {
            var result = _service.Get();
            return result;
        }

        [HttpPost]
        public ActionResult<string> Hello([FromBody]string name)
        {
            // aby dodaæ statuc zmieniono zwracanie stringa na ActionResult<string>
            // HttpContext.Response.StatusCode = 401; // Pierwszy sposób na dodanie statusu
            //return $"Hello {name}";
            //return StatusCode(401, $"Hello {name}"); // Drugi sposób na zwracanie statusu
            return NotFound($"Hello {name}"); // Trzeci sposób na zwracanie statusu ( NotFound zwraca 404 )
        }

        /*
         * 
         * Kody informacyjne: 1xx
         * 100: Continue
         * 101: Switching Protocols
         * 110: Connection Timed Out
         * 111: Connection Refused
         * 
         * Kody powodzenia: 2xx
         * 200: OK
         * 201: Created
         * 202: Accepted
         * 203: Non-Authoritative Informaction - Nieautoryzowana - zwrócona informacja nie odpowiada dok³adnej odpowiedzi pierwotnego serwera, lecz zosta³a utworzona z lokalnych b¹dŸ zewnêtrznych kopii
         * 204: No content - bez zwrotu
         * 205: Reset content - zapytanie zrealizowane, klient powinien przywróciæ pierwotny wygl¹d dokumentu
         * 206: Partial Content - czêœæ zawartoœci - serwer zrealizowa³ czêœæ zapytania Get, odpowiedŸ musi zawieraæ nag³ówek Content-Range informuj¹cy o zakresie bajtowym zwróconego elementu 
         *
         * Kody przekierowania: 3xx
         * 300: Multiple Choices
         * 301: Moved Permanently
         * 302: Found
         * 303: See Other
         * 304: Not Modified
         * 305: Use Proxy
         * 306: Switch Proxy
         * 
         * Kody b³êdu: 4xx
         * 400: Bad Request
         * 401: Unauthorized
         * 402: Payment Required - odpowiedŸ zarezerwowana na przysz³oœæ
         * 403: Forbidden
         * 404: Not Found 
         * 405: Method Not Allowed
         * 
         * Kody b³êdu serwera: 5xx
         * 500: Internal Server Error
         * 501: Not Implemented
         * 502: Bad Gateway
         * 503: Service Unavailable
         * 504: Gateway Timeout
         */


        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Hello([FromQuery]int count,[FromBody]WeatherSpecs weatherSpecs)
        {
            if(count < 1 || weatherSpecs.minTemp > weatherSpecs.maxTemp)
            {
                return BadRequest(); // Albo StatusCode(400)
            }

            return StatusCode(200, _service.Post(count, weatherSpecs.minTemp, weatherSpecs.maxTemp));

        }

    }
}