using AutoMapper;

namespace APISample.Profiles
{
	public class CityProfile : Profile
	{
		public CityProfile() 
		{
			CreateMap<Models.Entities.City, DTOs.ListOfCityDTO>();
			CreateMap<Models.Entities.City, DTOs.CityDTO>();
		}
	}
}
