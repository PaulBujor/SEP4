﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data;

namespace Data.Persistence
{
	public interface IUser
	{
		Task<List<Device>> GetDevices(long userId);
		Task RegisterUser(User user);
		Task<User> LoginUser(User user);
		Task AddDevice(long userId, long deviceId);
		Task RemoveDevice(long id, long deviceId);
	}
}