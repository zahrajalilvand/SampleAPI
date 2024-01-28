using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APISample.Models.Entities
{
	public class City
	{
        public City(string name)
        {
            this.Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
		public string Name { get; set; }
		public string? Description { get; set; }

        public ICollection<PointOfIntrest> PointOfIntrest { get; set; } = 
            new List<PointOfIntrest>();
    }
}
