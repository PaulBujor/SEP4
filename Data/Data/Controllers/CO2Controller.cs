﻿using Data.Data;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Data.Controllers
{
	[ApiController]
	public class CO2Controller : ControllerBase
	{
		private readonly ICO2Service _service;

		public CO2Controller(ICO2Service service)
		{
			_service = service;
		}

		// gets all co2 measurement by device id
		[HttpGet("api/devices/{id}/measurements/co2")]
		public async Task<ActionResult<IEnumerable<Measurement>>> GetByDevice(long id)
		{
			try
			{
				//todo get by device
				return Ok(await _service.GetAllCO2s(id));
			} catch(Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				return StatusCode(500, e.Message);
			}
		}

		// gets latest measurement by device id
		[HttpGet("api/devices/{id}/measurements/last-co2")]
		public async Task<ActionResult<Measurement>> GetLastByDevice(long id)
		{
			try
			{
				//todo get by device
				return Ok(await _service.GetLastCO2(id));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				return StatusCode(500, e.Message);
			}
		}
	}
}
