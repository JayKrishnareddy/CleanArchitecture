using AutoMapper;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Services.ViewModels.CommonModel;

namespace Services.Mapper
{
   public class Mapper : Profile
    {
        public Mapper()
        {
            AllowNullDestinationValues = true;
            //Source -> Destination
            CreateMap<UserRoles, RolesModel>()
                .ForMember(dto => dto.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dto => dto.RoleName, opt => opt.MapFrom(src => src.RoleName));
            CreateMap<User, LoginModel>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
