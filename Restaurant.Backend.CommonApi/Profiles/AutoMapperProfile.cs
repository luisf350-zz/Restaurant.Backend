using AutoMapper;
using Restaurant.Backend.Dto.Entities;
using Restaurant.Backend.Entities.Entities;

namespace Restaurant.Backend.CommonApi.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IdentificationTypeDto, IdentificationType>();
            CreateMap<IdentificationType, IdentificationTypeDto>();
        }
    }
}
