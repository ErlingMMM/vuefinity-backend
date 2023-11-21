using AutoMapper;
using Vuefinity.Data.DTO.User;  
using Vuefinity.Data.Models;


namespace Vuefinity.Mappers
{
    public class UserProfile : Profile  
    {
        public UserProfile()
        {
            CreateMap<UserPostDTO, User>().ReverseMap();  

            CreateMap<User, UserDTO>()
             
                .ReverseMap();
        }
    }
}
