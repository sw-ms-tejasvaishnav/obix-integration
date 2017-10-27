var oTable = null;
var selectedType = {
    LtgLevel: 2,
    LtgState: 3,
    Scene: 4
};

$(document).ready(function () {
    BindDropDown();

    $('#ddlSceneType').on('change', function () {
        var selectedValue = $('#ddlSceneType').val();
        if (selectedValue != 0) {
            $("#loader").removeClass('displaynone');
            SaveDeviceScene();
        }
    });

    $('#ddldeviceArea').on('change', function () {

        var selectedValue = $('#ddldeviceArea').val();
        if (selectedValue != 0) {
            
            $("#lightLevelSlidar").attr("disabled", false);
        } else {
            ClearDeviceType();
        }
    });
    
    $('#btnReferesh').on('click', function () {
        location.reload(true);
    })

    $('.toggle-group').change(function () {
        var selectedValue = $('#ddlDeviceType').val();
        if (selectedValue != 0) {
            if (selectedType.LtgState == selectedValue) {
                SaveDeviceLightState(selectedValue, $(this).prop('checked'));
            }
        }
    })


});

function BindDropDown() {
    RangeSlider();
    var ddlDeviceArea = $("#ddldeviceArea");
    ddlDeviceArea.html("");
    ddlDeviceArea.append('<option value=""> Please Select Device Area </option>');

    ddlDeviceArea.append('<option value="' + 1 + '">Conference 4628A 1761035</option>');
    ddlDeviceArea.val(1);
    $("#lightLevelSlidar").attr("disabled", false);
    BindAllDropDownList();
}

function ClearDeviceType() {
    $("#lightLevelSlidar").attr("disabled", true);
}

function BindDeviceListByTypeId(deviceTypeId) {
    //    BindLutronDeviceTable(deviceTypeId);
}

function GetCurrentLightState(deviceTypeId) {
    $.get("api/LutronQuantum/GetCurrentValue/" + deviceTypeId, function (deviceLst) {
        var devices = deviceLst;
    }).success(function (devices) {
        var currentStatus = devices.Value;
        $('#inputLightState').prop('checked', true).change();
    });

}

function GetCurrentLightLevel(deviceTypeId) {
    $.get("api/LutronQuantum/GetCurrentValue/" + deviceTypeId, function (currentVal) {
        var devices = currentVal;
    }).success(function (devices) {
        var currentstatus = devices.Value;
        $('input[type="range"]').val(currentstatus).change();
        $(".range-slider__value").html(currentstatus);
    });
};

function GetDeviceSceneList() {
    return $.get("api/LutronQuantum/GetSceneRangeList");
    //ValidatePage();
}

function BindAllDropDownList() {
    $.when(GetDeviceSceneList(), GetAllDeviceDetail()).then(function (devicesSceneLst, deviceDetail) {
        
        var scenelist = devicesSceneLst[0];
        var deviceInfo = deviceDetail[0];
        var ddlSceneType = "";
        ddlSceneType = $("#ddlSceneType");
        ddlSceneType.html("");
        ddlSceneType.append('<option value=""> Please Select Light Scense</option>');
        $.each(scenelist, function (key, value) {
            if (value.SceneId == 9) {
                ddlSceneType.append('<option value="' + value.SceneId + '" disabled>' + value.SceneName + '</option>');
            } else {
                ddlSceneType.append('<option value="' + value.SceneId + '">' + value.SceneName + '</option>');
            }
        });

        var lightStatus = deviceInfo.LightState;
        $('#inputLightState').prop('checked', lightStatus).change();


        var lightLevel = deviceInfo.LightLevel;
        $('input[type="range"]').val(lightLevel).change();
        $(".range-slider__value").html(lightLevel);
        
        $("#lightLevel").css({ "color": "hsl(43," + lightLevel + "%," + "57%)" })
        //$("#lightLevel").css({ "color": "hsl(22," + lightLevel + "%," + "51%)" })
        $("#ddlSceneType").val(deviceInfo.LightSceneValue);
    });
}

function SetDeviceValue(deviceInfo) {
    var lightStatus = deviceInfo.LightState;
    $('#inputLightState').prop('checked', lightStatus).change();
    

    var currentstatus = deviceInfo.LightLevel;
    $('input[type="range"]').val(currentstatus);
    $(".range-slider__value").html(currentstatus);

    $("#ddlSceneType").val(deviceInfo.LightSceneValue);
    var lightLevel = deviceInfo.LightLevel;
    $("#lightLevel").css({ "color": "hsl(43," + lightLevel + "%," + "57%)" })
    //$("#lightLevel").css({ "color": "hsl(22," + lightLevel + "%," + "51%)" })
    //$("#lightLevel").css({ "color": "hsl(60," + lightLevel + "%," + "50%)" })
    $("#loader").addClass('displaynone');
}

function GetAllDeviceDetail() {
    return $.get("api/LutronQuantum/GetsCurrentDeviceLevel");
}

function SaveDeviceProperty() {
    var currentDeviceType = $('#ddlDeviceType').val();
    if (selectedType.Scene == currentDeviceType) {
        SaveDeviceScene();
    }
}

function SaveDeviceLightLevel(id, value) {
    var lightLevel = value;
    var deviceObj = {
        LightLevel: lightLevel
    }
    $.post("api/LutronQuantum/SaveLightLevel", deviceObj, function () {

    }).success(function () {
        
        GetDeviceLevelDetail();
        
    });
}

function GetDeviceLevelDetail() {
    $.get("api/LutronQuantum/GetsCurrentDeviceLevel", function (data) {
        var deviceInfo = data;
    }).success(function (deviceInfo) {
        SetDeviceValue(deviceInfo);

    });
}

function SaveDeviceLightState(id, currentStatus) {
    //var lightLevel = $("#txtLightLEvel" + id).val();
    var deviceObj = {
        DeviceId: id,
        CurrentStatus: currentStatus
    }
    $.post("api/LutronQuantum/SaveLightState", deviceObj, function () {

    }).success(function () {
        //location.reload(true);
        GetDeviceLevelDetail();
    });
}

function SaveDeviceScene() {
    var selectedSceneValue = $('#ddlSceneType').val();
    var sceneObj = {
        SceneId: selectedSceneValue
    }
    $.post("api/LutronQuantum/SaveDeviceScene", sceneObj, function () {

    }).success(function () {
        // location.reload(true);
        // GetCurrentLightState(selectedType.LtgLevel);
        
        GetDeviceLevelDetail();
    });
}

var RangeSlider = function () {
    var slider = $('.range-slider'),
        range = $('.range-slider__range'),
        value = $('.range-slider__value');

    slider.each(function () {

        value.each(function () {
            var value = $(this).prev().attr('value');
            $(this).html(value);

        });

        range.on('input', function () {
            
            $(this).next(value).html(this.value);
            //$("#lightLevel").css({ "color": "hsl(60," + this.value + "%," + "50%)" })
            $("#lightLevel").css({ "color": "hsl(43," + this.value + "%," + "57%)" })
        });

        range.on("change", function (event, ui) {
            $("#loader").removeClass('displaynone');
            var selectedValue = this.value;
            
            SaveDeviceLightLevel(selectedType.LtgLevel, selectedValue);

        });
    });
};