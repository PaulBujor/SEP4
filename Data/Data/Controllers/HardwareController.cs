﻿using Data.Data;
using Data.Data.ConcreteConfigurations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Data.Controllers
{
	[ApiController]
	public class HardwareController : ControllerBase
	{
		private readonly IService _service;

		public HardwareController(IService service)
		{
			_service = service;
		}

		// gets settings by device id
		[HttpGet("api/devices/{id}/settings")]
		public async Task<Settings> Get(int id)
		{
			return new Settings
			{
				SettingsID = 0,
				DeviceConfiguration = new DeviceConfiguration { Active = true, ConfigurationID = 2, Max = 0, MinOrDefault = 0},
				AlarmConfiguration = new AlarmConfiguration { Active = true, ConfigurationID = 0, Max = 0, MinOrDefault = 0.5},
				CO2Configuration = new CO2Configuration { Active = false, ConfigurationID = 1, Max = 700, MinOrDefault = 500},
				HumidityConfiguration = new HumidityConfiguration { Active = true, ConfigurationID = 3, Max = 90.3, MinOrDefault = 30 },
				TemperatureConfiguration = new TemperatureConfiguration { Active = true, ConfigurationID = 4, Max = 24, MinOrDefault = 20 }
			};
		}

		//patches all settings
		[HttpPatch("api/devices/{id}/settings")]
		public async Task Patch(int id)
		{
			//patch all settings
		}

		// resets settings of device
		[HttpDelete("api/devices/{id}/settings")]
		public async Task Delete(int id)
		{
		}
	}
}
