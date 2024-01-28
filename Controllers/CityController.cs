using APISample.DTOs;
using APISample.Models.Entities;
using APISample.Repositories.Interfaces;
using APISample.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APISample.Controllers
{
	[Route("api/Cities")]
	[Authorize]
	[ApiController]
	public class CityController : ControllerBase
	{
		#region properties
		private readonly ILogger<CityController> logger;
		private readonly IMailService mailService;
		private readonly ICityInfoRepository cityInfoRepository;
		private readonly IMapper mapper;
		#endregion

		#region constructor
		public CityController(ICityInfoRepository cityInfoRepository,
			IMapper mapper,
			ILogger<CityController> logger,
			IMailService mailService)
		{
			this.cityInfoRepository = cityInfoRepository ??
				throw new ArgumentNullException(nameof(cityInfoRepository));

			this.mapper = mapper ?? throw new ArgumentNullException(nameof(cityInfoRepository));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
		}
		#endregion

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ListOfCityDTO>>> GetCities()
		{
			var cities = await cityInfoRepository.GetCitiesAsync();


			return Ok(
				mapper.Map<IEnumerable<ListOfCityDTO>>(cities)
				);

		}

		[HttpGet("{id}")]
		public async Task<IActionResult>
			GetCity(int id, bool includePointOfIntrest = false)
		{
			var city = await cityInfoRepository.GetCityByIdAsync(id, includePointOfIntrest);

			if (city == null) { return NotFound(); }
			
			if (includePointOfIntrest) { return Ok(mapper.Map<CityDTO>(city)); }

			return Ok(mapper.Map<ListOfCityDTO>(city));
		}

	}
}
