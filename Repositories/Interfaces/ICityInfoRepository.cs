using APISample.Models.Entities;

namespace APISample.Repositories.Interfaces
{
	public interface ICityInfoRepository
	{
		Task<IEnumerable<City>> GetCitiesAsync();

		Task<City?> GetCityByIdAsync(int cityId,
			bool includePointOfIntrest);

		Task<IEnumerable<PointOfIntrest>> GetPointsOfIntrestAsync(int cityId);

		Task<PointOfIntrest?> GetPointOfIntrestById(
			int CityId,
			int PointOfIntrestId);
	}
}
