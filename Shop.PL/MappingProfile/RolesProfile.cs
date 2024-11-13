using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.PL.Models.Roles;

namespace Shop.PL.MappingProfile
{
    public class RolesProfile:Profile
    {
        public RolesProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>()
                .ForMember(F => F.RoleName, O => O.MapFrom(S => S.Name)).ReverseMap();

        }
    }
}
