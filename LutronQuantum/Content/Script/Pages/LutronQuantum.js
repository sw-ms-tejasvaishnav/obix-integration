var oTable = null;
var selectedType = {
    LtgLevel: 2,
    LtgState: 3,
    Scene: 4
};

$(document).ready(function () {

    BindLutronDeviceTable(0);

    
    $('#ddlDeviceType').on('change', function () {

        var selectedValue = $('#ddlDeviceType').val();
        if (selectedValue != 0) {
            if (selectedType.LtgLevel == selectedValue)
            {
                var txtBox = AppendTextBox(selectedValue);
                $("#appendControl").append(txtBox);
                $("#btnSubmit").removeClass('displaynone');
                ValidatePage();
            }
        } else {
            $("#btnSubmit").addClass('displaynone');
        }
    });

    $('#ddldeviceArea').on('change', function () {

        var selectedValue = $('#ddldeviceArea').val();
        if (selectedValue != 0) {
            BindDeviceType(selectedValue);
        } else {
            $('#ddlDeviceType').html("");
            $("#appendControl").html("");
            var ddlDeviceType = $('#ddlDeviceType');
            ddlDeviceType.append('<option value=""> Please Select Device Type </option>');
            $("#btnSubmit").addClass('displaynone');
        }
    });

    $('#btnSearch').on('click', function () {
        var selectedDeviceTypeId = $("#ddlDeviceType").val();
        if (selectedDeviceTypeId != "") {
            BindDeviceListByTypeId(selectedDeviceTypeId);
        }
        else {
            BindLutronDeviceTable(0);
        }
    })

    $('#btnReferesh').on('click', function () {
        location.reload(true);
    })
});


//Bind device table vase on devicetype selection.
function BindLutronDeviceTable(deviceTypeId) {
    $.get("api/LutronQuantum/GetDeviceList/" + deviceTypeId, function (deviceLst) {
        var devices = deviceLst;
    }).success(function (devices) {
        BinddeviceTable(devices);

        var ddlDeviceArea = $("#ddldeviceArea");
        ddlDeviceArea.html("");
        ddlDeviceArea.append('<option value=""> Please Select Device Area </option>');

        ddlDeviceArea.append('<option value="' + 1 + '">Conference 4628A 1761035</option>');

        $('#ddlDeviceType').html("");
        var ddlDeviceType = $('#ddlDeviceType');
        ddlDeviceType.append('<option value=""> Please Select Device Type </option>');
    });
}

//Data table for device list.
function BinddeviceTable(deviceLst) {

    if (deviceLst.length > 0) {
        if (oTable != null) {
            oTable.clear().destroy();
        }
        $("#deviceListShow").show();
        $("#noDeviceLst").hide();
        oTable = $('#deviceRecords').DataTable({
            "data": deviceLst,
            "scrollY": false,
            "scrollX": true,
            "columns": [
                {
                    "title": "Device Type", "data": "DeviceType", "sort": true, "Width": "30%"
                },
                {
                    "title": "Device Name", "data": "DeviceName", "sort": true, "Width": "10%"
                },

                {
                    "title": "Url", "data": "DeviceUrl", "sort": true, "Width": "20%", "render": function (data, type, row) {

                        return "<a href='" + data + "'>" + data + "</a>";
                    }
                },
                {
                    "title": "ValueType", "data": "ValueType", "sort": false, "Width": "10%"
                },
                {
                    "title": "Value", "data": "Value", "sort": false, "className": "txtalignright", "Width": "5%"
                },
                {
                    "title": "Unit", "data": "Unit", "sort": false, "Width": "5%"
                },

                {
                    "title": "Status", "data": "Status", "sort": true, "Width": "10%"
                },
                {
                    "title": "Date", "data": "DateOfEntry", "sort": false, "Width": "10%", "render": function (data, type, row) {

                        return GetDay(data);
                    }

                }
            ]
     

        })
    } else {
        $("#deviceListShow").hide();
        $("#noDeviceLst").show();
    }


}


//Gets date in format dd/mm/yyyy
function GetDay(date) {

    var dt = new Date(date);
    var date = dt.getDay() + "/" + dt.getMonth() + "/" + dt.getFullYear();
    return date;
}



function BindDeviceType(deviceTypeId) {
    $.get("api/LutronQuantum/GetDeviceType", function (data) {
        var deviceType = data;
    }).success(function (deviceType) {
        var ddlDeviceType = "";
        ddlDeviceType = $("#ddlDeviceType");
        ddlDeviceType.html("");

        // var ddlObject = $('#ddlObject');
        ddlDeviceType.append('<option value=""> Please Select Device Type </option>');
        $.each(deviceType, function (key, value) {
            ddlDeviceType.append('<option value="' + value.DeviceTypeId + '">' + value.DeviceName + '</option>');
        });
    });
}

function BindDeviceListByTypeId(deviceTypeId) {
    BindLutronDeviceTable(deviceTypeId);
}


function AppendTextBox(selectedid) {
    var txthtml = "";
    txthtml = "<label >Light Level</label><input type='text' class=' form-control' name='txtLightLEvel' id='txtLightLEvel" + selectedid + "'/>";
    return txthtml;
}

function ValidatePage() {
    var frmLutronDevice = $('#frmLutronDevice').validate(
        {
            rules: {
                ddldeviceArea: {
                    required: true
                },
                txtLightLEvel: {
                    required: true
                },
                ddlDeviceType: {
                    required: true
                }
            },
            messages: {
                ddldeviceArea: {
                    required: "Please select device area."
                },
                txtLightLEvel: {
                    required: "Please enter light level."
                },
                ddlDeviceType: {
                    required: "Please select device type."
                }
            },
            submitHandler: function (form) {               //

                SaveDeviceProperty();

            }
        });
}


function SaveDeviceProperty() {
    var currentDeviceType = $('#ddlDeviceType').val();
    if (selectedType.LtgLevel == currentDeviceType)
    {
        SaveDeviceLightLevel(selectedType.LtgLevel);
    }
}

function SaveDeviceLightLevel(id) {
    var lightLevel = $("#txtLightLEvel" + id).val();
    var deviceObj = {
        LightLevel: lightLevel
    }
    $.post("api/LutronQuantum/SaveLightLevel", deviceObj, function () {

    }).success(function () {
        location.reload(true);
    });
}