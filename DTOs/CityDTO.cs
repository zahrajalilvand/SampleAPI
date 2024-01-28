namespace APISample.DTOs
{
	public class CityDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }

		public int CountOfPointOfIntrest
		{
			get { return pointOfIntrest.Count; }
		}

		public ICollection<PointOfIntrestDTO> pointOfIntrest { get; set; } =
			new List<PointOfIntrestDTO>();
	}
}
