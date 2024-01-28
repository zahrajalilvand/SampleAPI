using AutoMapper;

namespace APISample.Profiles
{
	public class PointOfIntrestProfile : Profile
	{
		public PointOfIntrestProfile()
		{
			CreateMap<Models.Entities.PointOfIntrest, DTOs.PointOfIntrestDTO>();
			CreateMap<Models.Entities.PointOfIntrest, Models.PointOfIntrestUpdateViewModel> ();
			CreateMap<Models.PointOfIntrestInsertViewModel, Models.Entities.PointOfIntrest>();
			CreateMap<Models.PointOfIntrestUpdateViewModel, Models.Entities.PointOfIntrest>();
		}
	}
}