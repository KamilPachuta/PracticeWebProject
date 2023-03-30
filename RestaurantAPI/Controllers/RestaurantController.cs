using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPut("{id}")]
        public ActionResult Modify([FromRoute]int id, [FromBody]UpdateRestaurantDto dto)
        {
            _restaurantService.Modify(id, dto);        

            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            
            return NoContent();

        }


        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Manager")]
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {

            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        //[Authorize(Policy = "HasNationality")]
        //[Authorize(Policy = "Atleast18")]// - wyrzuca błąd serwera dla niezalogowanych uzytkowników
        [Authorize(Policy = "Atleast2Restaurants")]// - wyrzuca błąd serwera dla niezalogowanych uzytkowników
        public ActionResult<PageResult<RestaurantDto>> GetAll([FromQuery]RestaurantQuery query)
        {
            var restaurantsDtos = _restaurantService.GetAll(query);

            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // zezwala na zapytanie bez autoryzacji pomimo autoryzacji calej klasy
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);
            
        }

        
    }
}
