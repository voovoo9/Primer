using AutoMapper;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
