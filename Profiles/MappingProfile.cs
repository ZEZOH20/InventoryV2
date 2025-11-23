using AutoMapper;
using InventoryV2.Dtos.AuthDtos.Requests;
using InventoryV2.Dtos.ProfileDto.Responses;
using InventoryV2.Models;

namespace InventoryV2.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //From Dto to Model
            CreateMap<RegisterDto, ApplicationUser>();

            //From Model to Dto
           
            //both Sides
            //CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
        }
    }
}
