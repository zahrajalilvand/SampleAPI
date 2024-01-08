using System.ComponentModel.DataAnnotations;

namespace APISample.Models
{
	public class PointOfIntrestUpdateViewModel
	{
		[Required(ErrorMessage = "Name is not valid")]
		[MaxLength(100)]
		public string Name { get; set; } = string.Empty;

		[MaxLength(200)]
		public string? Description { get; set; }
	}
}
