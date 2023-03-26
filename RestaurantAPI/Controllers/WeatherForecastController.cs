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

        [HttpGet("currentDay/{max}")] // dodanie konstruktora z sciezka to drugi spos�b
        //[Route("currentDay")] // pierwszy spos�b na dodanie dodatkowej scie�ki
        public IEnumerable<WeatherForecast> Get2([FromQuery]int take, [FromRoute]int max)
        {
            var result = _service.Get();
            return result;
        }

        [HttpPost]
        public ActionResult<string> Hello([FromBody]string name)
        {
            // aby doda� statuc zmieniono zwracanie stringa na ActionResult<string>
            // HttpContext.Response.StatusCode = 401; // Pierwszy spos�b na dodanie statusu
            //return $"Hello {name}";
            //return StatusCode(401, $"Hello {name}"); // Drugi spos�b na zwracanie statusu
            return NotFound($"Hello {name}"); // Trzeci spos�b na zwracanie statusu ( NotFound zwraca 404 )
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
         * 203: Non-Authoritative Informaction - Nieautoryzowana - zwr�cona informacja nie odpowiada dok�adnej odpowiedzi pierwotnego serwera, lecz zosta�a utworzona z lokalnych b�d� zewn�trznych kopii
         * 204: No content - bez zwrotu
         * 205: Reset content - zapytanie zrealizowane, klient powinien przywr�ci� pierwotny wygl�d dokumentu
         * 206: Partial Content - cz�� zawarto�ci - serwer zrealizowa� cz�� zapytania Get, odpowied� musi zawiera� nag��wek Content-Range informuj�cy o zakresie bajtowym zwr�conego elementu 
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
         * Kody b��du: 4xx
         * 400: Bad Request
         * 401: Unauthorized
         * 402: Payment Required - odpowied� zarezerwowana na przysz�o��
         * 403: Forbidden
         * 404: Not Found 
         * 405: Method Not Allowed
         * 
         * Kody b��du serwera: 5xx
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