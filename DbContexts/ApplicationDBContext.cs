using APISample.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace APISample.DbContexts
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{

		}

		public DbSet<City> Cities { get; set; } = null!;
		public DbSet<PointOfIntrest> PointOfIntrests { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<City>()
				.HasData(

				new City("Tehran")
				{
					Id = 1,
					Description = "Tehran Is capital of Iran"
				},
				new City("Tabriz")
				{
					Id = 2,
					Description = "Tabriz is a big city"
				},
				new City("Shiraz")
				{
					Id = 3,
					Description = "Tabriz is a big city"
				}
				);

			modelBuilder.Entity<PointOfIntrest>()
				.HasData(
				new PointOfIntrest("Borje Azadi")
				{
					Id = 1,
					CityId = 1,
					Description = "Meydan Azadi"
				},
				new PointOfIntrest("Borje Milad")
				{
					Id = 2,
					CityId = 1,
					Description = "Fazollah Highway"
				},
				new PointOfIntrest("Il Goli")
				{ 
					Id = 3,
					CityId = 2,
					Description = "Il Goli sq"
				},
				new PointOfIntrest("Baghe Eram")
				{ 
					Id = 4,
					CityId = 3,
					Description = "Eram st"
				}
				);

			base.OnModelCreating(modelBuilder);
		}
	}
}
