package com.example.myhomeapplication.ViewModel;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.example.myhomeapplication.Authorization.UserManager;
import com.example.myhomeapplication.Local_Persistence.Cache;
import com.example.myhomeapplication.Models.Device;
import com.example.myhomeapplication.Models.DeviceItem;
import com.example.myhomeapplication.Models.Thresholds;

import java.util.ArrayList;
import java.util.List;

public class DeviceSettingsViewModel extends ViewModel {

    private Cache repository;
    private List<Device> devices;
    private MutableLiveData<List<Device>> devicesMutable;
    private MutableLiveData<Thresholds> thresholdsMutable;
    private MutableLiveData<Long> deviceIDMutable;
    private MutableLiveData<String> responseInformation;
    private UserManager userManager;

    public DeviceSettingsViewModel() {
        repository = Cache.getInstance();
        devices = new ArrayList<>();
        devicesMutable = new MutableLiveData<>();
        thresholdsMutable = new MutableLiveData<>();
        deviceIDMutable= new MutableLiveData<>();
        responseInformation = new MutableLiveData<>();
        //TODO add observer to get all devices

        repository.getThresholds().observeForever(thresholds -> {
            thresholdsMutable.setValue(thresholds);
        });

        repository.getResponseInformation().observeForever(message->{
            responseInformation.setValue(message);
        });
        /*userManager.getLiveUser().observeForever(user->{
            repository.getAllDevices(user.getUserID());
        });*/

        repository.getAllDevices(1);

        repository.getDevices().observeForever(list->{
            devicesMutable.setValue(list);
        });


    }

    public void getThresholds(String id) {

        repository.getThresholdsByDevice(Long.parseLong(id));
    }

    public LiveData<Thresholds> getThresholdsMutable(){
        return thresholdsMutable;
    }





    public MutableLiveData<List<Device>> getDevicesMutable() {
        return devicesMutable;
    }

    public void setDevicesMutable(MutableLiveData<List<Device>> devicesMutable) {
        this.devicesMutable = devicesMutable;
    }

    public ArrayList<DeviceItem> getAllDevices() {
        ArrayList<DeviceItem> deviceItems = new ArrayList<>();
        devices.clear();
        List<Device> devices = devicesMutable.getValue();

        for (Device item : devices
        ) {
            deviceItems.add(new DeviceItem(String.valueOf(item.getDeviceID()), item.getDeviceName()));
        }
        return deviceItems;
    }

    public void updateThresholds(long deviceId, Thresholds thresholds) {
        repository.updateThresholds(deviceId, thresholds);
    }

    public MutableLiveData<Long> getDeviceIDMutable() {
        return deviceIDMutable;
    }

    public void setDeviceIDMutable(long deviceId) {
        this.deviceIDMutable.setValue(deviceId);
    }

    public void deleteDevice(long deviceID) {
    repository.deleteDevice(deviceID);
    }

    public void addDevice(Device tmpDevice) {
        repository.addDevice (tmpDevice);
    }

    public MutableLiveData<String> getResponseInformation() {
        return responseInformation;
    }

    public void setResponseInformation(MutableLiveData<String> responseInformation) {
        this.responseInformation = responseInformation;
    }
}
