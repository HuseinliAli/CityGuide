using AutoMapper;
using CityGuideAPI.Dtos;
using CityGuideAPI.Models;
using System.Linq;

namespace CityGuideAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityForListDto>().ForMember(dest=>dest.PhotoUrl, opt =>
            {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            });
            CreateMap<City, CityForDetailDto>();
        }
    }
}
