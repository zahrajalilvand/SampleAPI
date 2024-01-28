using APISample.DTOs;
using APISample.Models;
using APISample.Repositories.Interfaces;
using APISample.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Drawing;

namespace APISample.Controllers
{
	[Route("api/cities/{cityId}/PointOfIntrest")]
	[ApiController]
	public class PointOfIntrestController : ControllerBase
	{
		#region properties
		private readonly ILogger<PointOfIntrestController> logger;
		private readonly IMailService mailService;
		private readonly IMapper mapper;
		private readonly ICityInfoRepository cityInfoRepository;
		private readonly CitiesDataStore citiesDataStore;
		#endregion

		#region constructor
		public PointOfIntrestController(ILogger<PointOfIntrestController> logger,
			IMailService mailService,
			IMapper mapper,
			ICityInfoRepository cityInfoRepository,
			CitiesDataStore citiesDataStore)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
			this.mapper = mapper;
			this.cityInfoRepository = cityInfoRepository;
			this.citiesDataStore = citiesDataStore;
		}
		#endregion

		#region Methods
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PointOfIntrestDTO>>> GetPointsOfIntrest(int cityId)
		{
			if (!await cityInfoRepository.CityExist(cityId))
			{
				logger.LogInformation($"{cityId} is not exist.");
				return NotFound();
			}

			var pointOfIntrest = await cityInfoRepository.GetPointsOfIntrestAsync(cityId);
			return Ok(mapper.Map<IEnumerable<PointOfIntrestDTO>>(pointOfIntrest));
		}

		[HttpGet("{pointOfInterestId}", Name = "GetPointsOfIntrest")]
		public async Task<ActionResult<PointOfIntrestDTO>> GetPointsOfIntrest(int cityId, int pointOfIntrestId)
		{
			if (!await cityInfoRepository.CityExist(cityId))
			{
				logger.LogInformation($"{cityId} is not exist.");
				return NotFound();
			}

			var pointOfIntrest = await cityInfoRepository.GetPointOfIntrestByIdAsync(cityId, pointOfIntrestId);

			if (pointOfIntrest == null) { return NotFound(); }
			return Ok(mapper.Map<DTOs.PointOfIntrestDTO>(pointOfIntrest));
		}

		[HttpPost]
		public async Task<ActionResult<PointOfIntrestDTO>> NewPointOfIntrest(int cityId,
			[FromBody] PointOfIntrestInsertViewModel pointOfIntrest
			)
		{
			if (!await cityInfoRepository.CityExist(cityId))
			{
				return NotFound();
			}

			var result = mapper.Map<Models.Entities.PointOfIntrest>(pointOfIntrest);
			await cityInfoRepository.AddPointOfInteresrAsync(cityId, result);
			await cityInfoRepository.SaveChangesAsync();

			var resultMapped = mapper.Map<DTOs.PointOfIntrestDTO>(result);
			return Ok(resultMapped);/*CreatedAtRoute("GetPointsOfIntrest", 
				new {cityId = cityId, pointOfIntrestId = resultMapped.Id}, 
				resultMapped);*/
		}

		[HttpPut("{pointOfInterstId}")]
		public async Task<ActionResult<PointOfIntrestDTO>> UpdatePointOfIntrest(int cityId,
			int pointOfInterstId,
			PointOfIntrestUpdateViewModel pointOfIntrest)
		{
			if (!await cityInfoRepository.CityExist(cityId)) { return NotFound(); }

			var resultPoint = await cityInfoRepository.GetPointOfIntrestByIdAsync(cityId, pointOfInterstId);
			if (resultPoint == null) { return NotFound(); }

			mapper.Map(pointOfIntrest, resultPoint);
			await cityInfoRepository.SaveChangesAsync();

			return NoContent();
		}

		[HttpPatch("{pointOfInterestId}")]
		public async Task<ActionResult> PartiallyUpdatePointOfIntrest(
			int cityId,
			int pointOfIntrestId,
			JsonPatchDocument<PointOfIntrestUpdateViewModel> patchDocument
			)
		{
			if (!await cityInfoRepository.CityExist(cityId)) { return NotFound(); }

			var pointResult = await cityInfoRepository.GetPointOfIntrestByIdAsync(cityId, pointOfIntrestId);
			if (pointResult == null) { return NotFound(); }

			var pointResultMapped = mapper.Map<PointOfIntrestUpdateViewModel>(pointResult);
			patchDocument.ApplyTo(pointResultMapped, ModelState);

			if (!ModelState.IsValid) { return BadRequest(); }
			if (!TryValidateModel(pointResultMapped)) { return BadRequest(); }

			mapper.Map(pointResultMapped, pointResult);
			await cityInfoRepository.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{pointOfInterestId}")]
		public async Task<ActionResult> DeletePointOfIntrest(int cityId, int PointOfInterestId)
		{
			if (!await cityInfoRepository.CityExist(cityId)) { return NotFound(); }

			var delObject = await cityInfoRepository.GetPointOfIntrestByIdAsync(cityId, PointOfInterestId);
			if (delObject == null) { return BadRequest(); }

			cityInfoRepository.DeletePointOfInteresrAsync(delObject);
			await cityInfoRepository.SaveChangesAsync();

			mailService.Send(
				"PointOfInterest deleted",
				$"Point of Interest {delObject.Name} with id {delObject.Id}"
				);

			return NoContent();
		}

		#endregion
	}
}
