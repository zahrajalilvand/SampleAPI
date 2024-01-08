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
    }
}
