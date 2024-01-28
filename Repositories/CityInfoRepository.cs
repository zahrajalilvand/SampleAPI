using APISample.DbContexts;
using APISample.Models.Entities;
using APISample.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APISample.Repositories
{
	public class CityInfoRepository : ICityInfoRepository
	{
		private readonly ApplicationDBContext dbContext;
		public CityInfoRepository(ApplicationDBContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddPointOfInteresrAsync(int cityId, PointOfIntrest pointOfIntrest)
		{
			var city = await GetCityByIdAsync(cityId, false);
			if (city != null)
			{
				city.PointOfIntrest.Add(pointOfIntrest);
			}
		}

		public async Task<bool> CityExist(int cityId)
		{
			return await dbContext
				.Cities
				.AnyAsync(c => c.Id == cityId);
		}

		public void DeletePointOfInteresrAsync(PointOfIntrest pointOfIntrest)
		{
			dbContext.PointOfIntrests.Remove(pointOfIntrest);
		}

		public async Task<IEnumerable<City>> GetCitiesAsync()
		{
			return await dbContext
				.Cities
				.OrderBy(c => c.Name).ToListAsync();
		}

		public async Task<City?> GetCityByIdAsync(int cityId, bool includePointOfIntrest)
		{
			if (includePointOfIntrest)
			{
				return await dbContext
					.Cities
					.Include(c => c.PointOfIntrest)
					.Where(c => c.Id == cityId).FirstOrDefaultAsync();
			}
			return await dbContext
				.Cities
				.Where(c => c.Id == cityId).FirstOrDefaultAsync();
		}

		public async Task<PointOfIntrest?> GetPointOfIntrestByIdAsync(int CityId, int PointOfIntrestId)
		{
			return await dbContext.PointOfIntrests.Where(c => c.CityId == CityId && c.Id == PointOfIntrestId)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<PointOfIntrest>> GetPointsOfIntrestAsync(int cityId)
		{
			return await dbContext
				.PointOfIntrests
				.Where(p => p.CityId == cityId)
				.ToListAsync();
		}

		public async Task<bool> SaveChangesAsync()
		{
			return (await dbContext.SaveChangesAsync() > 0);
		}
	}
}
