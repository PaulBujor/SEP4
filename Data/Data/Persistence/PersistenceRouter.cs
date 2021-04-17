using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Data;
using Data.Properties.Persistence.Impl;

namespace Data.Properties.Persistence
{
    public class PersistenceRouter : IConfiguration,IDevice,IMeasurement,ISettings
    {
         private IConfiguration _configuration;
         private IDevice _device;
         private IMeasurement _measurement;
         private ISettings _settings;

         public PersistenceRouter()
         {
             Database context = new Database();
             _configuration = new ConfigurationImpl(context);
             _device = new DeviceImpl(context);
             _measurement = new MeasurementImpl(context);
             _settings = new SettingsImpl(context);
         }
        
        public async Task AddConfiguration(Configuration configuration)
        {
          await _configuration.AddConfiguration(configuration);
        }

        public async Task<Configuration> GetConfiguration(long id)
        {
            return await _configuration.GetConfiguration(id);
        }

        public async Task<List<Configuration>> GetConfigurations()
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateConfiguration(Configuration configuration)
        {
           await _configuration.UpdateConfiguration(configuration);
        }

        public async Task AddDevice(Device device)
        {
            await _device.AddDevice(device);
        }

        public async Task<Device> GetDevice(long id)
        {
            return await _device.GetDevice(id);
        }

        public async Task<List<Device>> GetDevices()
        {
            return await _device.GetDevices();
        }

        public async Task UpdateDevice(Device device)
        {
            await _device.UpdateDevice(device);
        }

        public async Task RemoveDevice(long id)
        {
            await _device.RemoveDevice(id);
        }

        public async Task AddMeasurement(Measurement measurement)
        {
            await _measurement.AddMeasurement(measurement);
        }

        public async Task<Measurement> GetMeasurement(long id)
        {
          return  await _measurement.GetMeasurement(id);
        }

        public async Task<List<Measurement>> GetMeasurements()
        {
            return await _measurement.GetMeasurements();
        }

        public async Task RemoveMeasurement(long id)
        {
            await _measurement.RemoveMeasurement(id);
        }

        public async Task AddSetting(Settings setting)
        {
            await _settings.AddSetting(setting);
        }

        public async Task<Settings> GetSetting(long id)
        {
            return await _settings.GetSetting(id);
        }

        public async Task<List<Settings>> GetSettings()
        {
            return await _settings.GetSettings();
        }

        public async Task UpdateSetting(Settings setting)
        {
            await _settings.UpdateSetting(setting);
        }

        public async Task RemoveSetting(long id)
        {
            await _settings.RemoveSetting(id);
        }
    }
}