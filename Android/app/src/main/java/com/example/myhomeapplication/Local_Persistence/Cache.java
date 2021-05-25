package com.example.myhomeapplication.Local_Persistence;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;

import com.example.myhomeapplication.Models.Device;
import com.example.myhomeapplication.Models.Measurement;
import com.example.myhomeapplication.Models.Thresholds;
import com.example.myhomeapplication.Remote.DeviceAPI;
import com.example.myhomeapplication.Remote.DeviceServiceGenerator;
import com.example.myhomeapplication.Remote.MeasurementAPI;
import com.example.myhomeapplication.Remote.TemperatureServiceGenerator;

import android.util.Log;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.internal.EverythingIsNonNull;

public class Cache {

    private static Cache instance;

    private final MutableLiveData<Measurement> latestTemperatureMeasurement;
    private final MutableLiveData<Measurement> latestHumidityMeasurement;
    private final MutableLiveData<Measurement> latestCO2Measurement;
    private final MutableLiveData<Measurement> latestAlarmMeasurement;
    private final MutableLiveData<List<Device>> devices;


    //TODO avoid public constructor
    public Cache() {
        this.latestTemperatureMeasurement = new MutableLiveData<>();
        this.latestHumidityMeasurement = new MutableLiveData<>();
        this.latestCO2Measurement = new MutableLiveData<>();
        this.latestAlarmMeasurement = new MutableLiveData<>();
        devices = new MutableLiveData<>();
    }

    public static synchronized Cache getInstance() {
        if (instance == null)
            instance = new Cache();
        return instance;
    }

    public LiveData<Measurement> getLatestTemperatureMeasurement() {
        return latestTemperatureMeasurement;
    }

    public LiveData<Measurement> getLatestHumidityMeasurement() {
        return latestHumidityMeasurement;
    }

    public LiveData<Measurement> getLatestCO2Measurement() {
        return latestCO2Measurement;
    }

    public LiveData<Measurement> getLatestAlarmMeasurement() {
        return latestAlarmMeasurement;
    }


    public LiveData<List<Measurement>> getAllMeasurements(int deviceID, String measurementType) {
        MutableLiveData<List<Measurement>> allMeasurements = new MutableLiveData<>();

        MeasurementAPI measurementAPI = TemperatureServiceGenerator.getMeasurementAPI();
        Call<List<Measurement>> call = measurementAPI.getMeasurements(deviceID, measurementType);
        call.enqueue(new Callback<List<Measurement>>() {

            @EverythingIsNonNull
            @Override
            public void onResponse(Call<List<Measurement>> call, Response<List<Measurement>> response) {
                if (response.code() == 200) {
                    allMeasurements.setValue(response.body());
                    Log.i("HTTPResponseCode", String.valueOf(response.code()) + " " + measurementType);
                } else {
                    Log.i("HTTPResponseCodeFAILURE", String.valueOf(response.code() + " " + measurementType + "\n" + response.message()));
                }
            }

            @EverythingIsNonNull
            @Override
            public void onFailure(Call<List<Measurement>> call, Throwable t) {
                Log.i("Retrofit", "Something went wrong :(");
                Log.i("Retrofit", t.getMessage());
                t.printStackTrace();
            }
        });

        return allMeasurements;
    }

    public void receiveLatestMeasurement(int deviceID, String measurementType) {
        MeasurementAPI measurementAPI = TemperatureServiceGenerator.getMeasurementAPI();
        Call<Measurement> call = measurementAPI.getLatestMeasurement(deviceID, measurementType);
        call.enqueue(new Callback<Measurement>() {

            @EverythingIsNonNull
            @Override
            public void onResponse(Call<Measurement> call, Response<Measurement> response) {
                if (response.code() == 200) {
                    switch (measurementType) {
                        case MeasurementTypes.TYPE_TEMPERATURE:
                            latestTemperatureMeasurement.setValue(response.body());
                            break;
                        case MeasurementTypes.TYPE_HUMIDITY:
                            latestHumidityMeasurement.setValue(response.body());
                            break;
                        case MeasurementTypes.TYPE_CO2:
                            latestCO2Measurement.setValue(response.body());
                            break;
                        case MeasurementTypes.TYPE_ALARM:
                            latestAlarmMeasurement.setValue(response.body());
                            break;
                        default:
                            Log.wtf("Repository", "Wrong measurement type");
                    }
                    Log.i("HTTPResponseCode", String.valueOf(response.code()) + " " + measurementType);
                } else {
                    Log.i("HTTPResponseCodeFAILURE", String.valueOf(response.code() + " " + measurementType + "\n" + response.message()));
                }
            }

            @EverythingIsNonNull
            @Override
            public void onFailure(Call<Measurement> call, Throwable t) {
                Log.i("Retrofit", "Something went wrong :(");
                Log.i("Retrofit", t.getMessage());
                t.printStackTrace();
            }
        });
    }

    public MutableLiveData<List<Device>> getAllDevices(long userID) {


        DeviceAPI deviceAPI = DeviceServiceGenerator.getDeviceAPI();
        Call<List<Device>> call = deviceAPI.getAllDevices(userID);


        call.enqueue(new Callback<List<Device>>() {
            @EverythingIsNonNull
            @Override
            public void onResponse(Call<List<Device>> call, Response<List<Device>> response) {
                if (response.code() == 200) {
                    devices.setValue(response.body());

                    Log.i("HTTP_Devices", String.valueOf(response.code()));

                } else
                    Log.i("HTTPResponseCodeFAILURE", String.valueOf(response.code() + "\n" + response.message()));

            }

            @EverythingIsNonNull
            @Override
            public void onFailure(Call<List<Device>> call, Throwable t) {
                Log.i("Retrofit", "Something went wrong :(");
                Log.i("Retrofit", t.getMessage());
                t.printStackTrace();

            }
        });

       return devices;
    }

   /* public LiveData<Thresholds> getTresholds(long deviceID){
        DeviceAPI deviceAPI = DeviceServiceGenerator.getDeviceAPI();
        Call<Thresholds> call = deviceAPI.getThresholdsByDevice(deviceID);
        call.enqueue(new Callback<Thresholds>() {
            @EverythingIsNonNull
            @Override
            public void onResponse(Call<Thresholds> call, Response<Thresholds> response) {
                if(response.code() == 200){
                    thresholds.setValue(response.body());
                }
                else Log.i("HTTPResponseCodeFAILURE", String.valueOf(response.code() + "\n" + response.message()));
            }

            @EverythingIsNonNull
            @Override
            public void onFailure(Call<Thresholds> call, Throwable t) {
                Log.i("Retrofit", "Something went wrong :(");
                Log.i("Retrofit", t.getMessage());
                t.printStackTrace();
            }
        });
        return thresholds;
    }*/

 /*   public MutableLiveData<List<Device>> getDevices() {
        return devices;
    }

    public MutableLiveData<Thresholds> getThresholds() {
        return thresholds;
    }*/
}
