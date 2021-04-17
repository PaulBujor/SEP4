﻿using Data.Data;
using Data.Data.ConcreteMeasurements;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Data.Controllers
{
	[ApiController]
	public class AlarmController : ControllerBase
	{
		private readonly IAlarmService _service;

		public AlarmController(IAlarmService service)
		{
			_service = service;
		}

		// gets alarm trigger by id
		[HttpGet("api/alarms/{id}")]
		public async Task<ActionResult<Measurement>> Get(long id)
		{
			//todo implement in service
			return Ok(new AlarmMeasurement
			{
				MeasurementID = 0,
				Timestamp = DateTime.Now,
				Value = 0
			});
		}

		// gets all alarm trigger by device id
		[HttpGet("api/devices/{id}/alarms")]
		public async Task<ActionResult<IEnumerable<Measurement>>> GetByDevice(long id)
		{
			try
			{
				//todo get by device
				return Ok(await _service.GetAllMotions());
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
				return StatusCode(500, e.Message);
			}
		}

		// gets latest measurement by device id
		[HttpGet("api/devices/{id}/last_alarm")]
		public async Task<ActionResult<Measurement>> GetLastByDevice(long id)
		{
			try
			{
				//todo get by device
				return await _service.GetLastMotion();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
				return StatusCode(500, e.Message);
			}
		}

		// Adds new alarm trigger to device
		[HttpPost("api/devices/{id}/alarms")]
		public async Task<ActionResult> Post(long id, [FromBody] Measurement value)
		{
			try
			{
				//todo add to device
				return Ok(await _service.AddMotion(value));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
				return StatusCode(500, e.Message);
			}
		}

		// deletes alarm trigger with ID
		[HttpDelete("api/alarms/{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			//probably won't use this at all
			return StatusCode(404, "NO.");
		}
	}
}
