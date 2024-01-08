using APISample.DTOs;
using APISample.Models;
using APISample.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace APISample.Controllers
{
	[Route("api/cities/{cityId}/PointOfIntrest")]
	[ApiController]
	public class PointOfIntrestController : ControllerBase
	{
		#region properties
		private readonly ILogger<PointOfIntrestController> logger;
		private readonly IMailService mailService;
		private readonly CitiesDataStore citiesDataStore;
		#endregion

		#region constructor
		public PointOfIntrestController(ILogger<PointOfIntrestController> logger,
			IMailService mailService,
			CitiesDataStore citiesDataStore)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
			this.citiesDataStore = citiesDataStore;
		}
		#endregion

		#region Methods
		[HttpGet]
		public ActionResult<IEnumerable<PointOfIntrestDTO>> GetPointsOfIntrest(int cityId)
		{
			try
			{
				//throw new Exception("Error .. ");
				var city = citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);

				if (city == null)
				{
					logger.LogInformation($"city by id= {cityId} is not found");
					return NotFound();
				}

				return Ok(city.pointOfIntrests);
			}
			catch (Exception ex)
			{
				logger.LogCritical($"Exception getting {cityId} ", ex);
				return StatusCode(500, "City Id is not found");
			}
		}

		[HttpGet("{pointOfInterestId}", Name = "GetPointsOfIntrest")]
		public ActionResult<IEnumerable<PointOfIntrestDTO>> GetPointsOfIntrest(int cityId, int pointOfIntrestId)
		{
			var city = citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var point = city.pointOfIntrests.FirstOrDefault(p => p.Id == pointOfIntrestId);

			if (point == null)
			{
				return NotFound();
			}

			return Ok(point);
		}

		[HttpPost]
		public ActionResult<PointOfIntrestDTO> NewPointOfIntrest(int cityId,
			[FromBody] PointOfIntrestInsertViewModel pointOfIntrest
			)
		{
			var city = citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
				return NotFound();

			var maxPointOfIntrestId = citiesDataStore.cities.SelectMany(c => c.pointOfIntrests).Max(p => p.Id);

			var result = new PointOfIntrestDTO()
			{
				Id = ++maxPointOfIntrestId,
				Name = pointOfIntrest.Name,
				Description = pointOfIntrest.Description
			};

			city.pointOfIntrests.Add(result);

			return CreatedAtAction("GetPointsOfIntrest",
				new
				{
					cityId = cityId,
					pointOfIntrestId = result.Id,

				},
				result
				);
		}

		[HttpPut("{pointOfInterstId}")]
		public ActionResult<PointOfIntrestDTO> UpdatePointOfIntrest(int cityId, int pointOfInterstId,
			PointOfIntrestUpdateViewModel pointOfIntrest)
		{
			var city = citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null) return NotFound();

			var point = city.pointOfIntrests.FirstOrDefault(p => p.Id == pointOfInterstId);

			if (point == null) return NotFound();

			point.Name = pointOfIntrest.Name;
			point.Description = pointOfIntrest.Description;

			mailService.Send("Point of Intrest",
				$"Point of Intrest {point.Name} has been changed");

			return NoContent();
		}

		[HttpPatch("{pointOfInterestId}")]
		public ActionResult PartiallyUpdatePointOfIntrest(
			int cityId,
			int pointOfIntrestId,
			JsonPatchDocument<PointOfIntrestUpdateViewModel> patchDocument
			)
		{
			var city = citiesDataStore.cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null) return NotFound();

			var pointOfIntrestFromStore = city.pointOfIntrests.FirstOrDefault(p => p.Id == pointOfIntrestId);
			if (pointOfIntrestFromStore == null) return NotFound();

			var pointOfIntrestToPatch = new PointOfIntrestUpdateViewModel()
			{
				Name = pointOfIntrestFromStore.Name,
				Description = pointOfIntrestFromStore.Description
			};

			patchDocument.ApplyTo(pointOfIntrestToPatch, ModelState);

			if (!ModelState.IsValid) return BadRequest();

			if (!TryValidateModel(pointOfIntrestToPatch)) return BadRequest(modelState: ModelState);

			pointOfIntrestFromStore.Name = pointOfIntrestToPatch.Name;
			pointOfIntrestFromStore.Description = pointOfIntrestToPatch.Description;

			return NoContent();
		}

		#endregion
	}
}
