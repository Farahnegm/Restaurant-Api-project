﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>();
            CreateMap<CreateRestaurantCommand,Restaurant>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode
                }));
            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));

        }
    }
}