using AspNetCore3Base.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore3Base.RestAPI.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {

        public AutoMapperConfiguration()
        {

            //Create the application mappings here
            CreateMap<IdentityUser, ApplicationUser>();
            CreateMap<IdentityRole, ApplicationRole>();
        }
    }
}
