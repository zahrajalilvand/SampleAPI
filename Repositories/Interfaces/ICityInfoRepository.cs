using APISample.Models.Entities;

namespace APISample.Repositories.Interfaces
{
	public interface ICityInfoRepository
	{
		Task<IEnumerable<City>> GetCitiesAsync();

		Task<bool> CityExist(int cityId);

		Task<City?> GetCityByIdAsync(int cityId, bool includePointOfIntrest);

		Task<IEnumerable<PointOfIntrest>> GetPointsOfIntrestAsync(int cityId);

		Task<PointOfIntrest?> GetPointOfIntrestByIdAsync(int CityId, int PointOfIntrestId);

		Task AddPointOfInteresrAsync(int cityId, PointOfIntrest pointOfIntrest);

		Task<bool> SaveChangesAsync();

		void DeletePointOfInteresrAsync(PointOfIntrest pointOfIntrest);
	}
}
