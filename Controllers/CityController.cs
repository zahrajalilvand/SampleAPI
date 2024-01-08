using Microsoft.AspNetCore.Mvc;

namespace APISample.Controllers
{
	[Route("api/Cities")]
	[ApiController]
	public class CityController : ControllerBase
	{
		private readonly CitiesDataStore citiesDataStore;

		public CityController(CitiesDataStore citiesDataStore)
        {
			this.citiesDataStore = citiesDataStore;
		}

        [HttpGet]
		public JsonResult GetCity()
		{
			return new JsonResult(citiesDataStore);
			
		}

		[HttpGet("{id}")]
		public JsonResult GetCity(int id)
		{
			return new JsonResult(citiesDataStore.cities.FirstOrDefault(c=> c.Id == id));

		}

	}
}
