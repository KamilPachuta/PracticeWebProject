﻿using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();

                if(pendingMigrations is not null)
                {
                    _dbContext.Database.Migrate();
                }

                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);

                    _dbContext.SaveChanges();
                }

                if(!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);

                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Kfc - description",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "123456789",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Chicken nuggets",
                            Price = 5.20M,
                        },

                        new Dish()
                        {
                            Name = "Curitto",
                            Price = 7.40M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Katowice",
                        Street = "Kurczakowa 420",
                        PostalCode = "40-003"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonalds",
                    Category = "Fast Food",
                    Description = "McDonalds - description",
                    ContactEmail = "contact@mcdonalds.com",
                    ContactNumber = "987654321",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "BigMac",
                            Price = 5.20M,
                        },

                        new Dish()
                        {
                            Name = "Drwal",
                            Price = 7.40M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Katowice",
                        Street = "krotka 2",
                        PostalCode = "40-003"
                    }
                }

            };

            return restaurants;
        }

    }
}
