using AutoMapper;
using Shop.DAL.Entites.Identity;
using Shop.PL.Models.Users;

namespace Shop.PL.MappingProfile
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,UpdateUsersViewModel>().ReverseMap();
        }
    }
}
