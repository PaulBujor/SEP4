﻿using Data.Data;
using Data.Data.ConcreteMeasurements;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Data.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HumidityController : ControllerBase
	{
		private readonly IService _service;

		public HumidityController(IService service)
		{
			_service = service;
		}

		// gets humidity measurement by id
		[HttpGet("api/humidity/{id}")]
		public async Task<Measurement> Get(int id)
		{
			return new HumidityMeasurement
			{
				MeasurementID = 0,
				Timestamp = DateTime.Now,
				Value = 0
			};
		}

		// gets all humidity measurement by device id
		[HttpGet("api/devices/{id}/humidity")]
		public async Task<IEnumerable<Measurement>> GetByDevice(int id)
		{
			return new Measurement[] {
				new HumidityMeasurement
				{
					MeasurementID = 0,
					Timestamp = DateTime.Now,
					Value = 0
				},
				new HumidityMeasurement
				{
					MeasurementID = 1,
					Timestamp = DateTime.Now,
					Value = 1
				}
			};
		}

		// gets latest measurement by device id
		[HttpGet("api/devices/{id}/last_humidity")]
		public async Task<Measurement> GetLastByDevice(int id)
		{
			return new HumidityMeasurement
			{
				MeasurementID = 0,
				Timestamp = DateTime.Now,
				Value = 0
			};
		}

		// Adds new humidity measurement to device
		[HttpPost("api/devices/{id}/humidity")]
		public async Task Post(int id, [FromBody] Measurement value)
		{
		}

		// deletes humidity measurement with ID
		[HttpDelete("api/humidity/{id}")]
		public async Task Delete(int id)
		{
		}
	}
}
