using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        DishDto GetById(int restaurantId, int dishId);
        IEnumerable<DishDto> GetAll(int restaurantId);
        void RemoveAll(int restaurantId);
        void RemoveById(int restaurantId, int dishId);

    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishEntity = _mapper.Map<Dish>(dto);

            dishEntity.RestaurantID = restaurantId;

            _context.Dishes.Add(dishEntity);
            _context.SaveChanges();

            return dishEntity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);

            if (dish is null || dish.RestaurantID != restaurantId)
                throw new NotFoundException("Dish not found. ");

            var dishDto = _mapper.Map<DishDto>(dish);

            return dishDto;
        }

        public IEnumerable<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishes = restaurant.Dishes;

            var dishesDtos = _mapper.Map<List<DishDto>>(dishes);

            return dishesDtos;
        }

        public void RemoveAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            _context.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }

        public void RemoveById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);

            if (dish is null || dish.RestaurantID != restaurantId)
                throw new NotFoundException("Dish not found. ");

            _context.RemoveRange(dish);
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _context
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found. ");

            return restaurant;
        }

    }
}
