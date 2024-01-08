using APISample.DTOs;

namespace APISample
{
	public class CitiesDataStore
	{
		public List<CityDTO> cities { get; set; }

		//public static CitiesDataStore Current { get; } = new CitiesDataStore();

		public CitiesDataStore()
		{
			cities = new List<CityDTO>()
			{
				new CityDTO(){ Id = 1,
							   Name = "Tehran",
							   Description = "Capital of Iran",
							   pointOfIntrests = new List<PointOfIntrestDTO>()
							   {
									new PointOfIntrestDTO()
									{
										Id = 1,
										Name = "Jaye didani 1",
										Description = "jay didani 1"
									},
									new PointOfIntrestDTO()
									{
										Id = 2,
										Name = "Jaye didani 2",
										Description = "jay didani 2"
									}
							   }
				}
				, new CityDTO(){ Id = 2,
								 Name = "Shiraz",
								 Description = "Historical City",
								 pointOfIntrests = new List<PointOfIntrestDTO>()
								 {
									new PointOfIntrestDTO()
									{
										Id = 3,
										Name = "Jaye didani 3",
										Description = "jay didani 3"
									},
									new PointOfIntrestDTO()
									{
										Id = 4,
										Name = "Jaye didani 4",
										Description = "jay didani 4"
									}
								 }
				}
				, new CityDTO(){ Id = 3,
								 Name = "Ahwaz",
								 Description = "Oil City",
								 pointOfIntrests= new List<PointOfIntrestDTO>() 
								 {
									new PointOfIntrestDTO()
									{
										Id = 5,
										Name = "Jaye didani 5",
										Description = "jay didani 5"
									},
									new PointOfIntrestDTO()
									{
										Id = 6,
										Name = "Jaye didani 6",
										Description = "jay didani 6"
									}
								 }
				}
			};
		}
	}
}
