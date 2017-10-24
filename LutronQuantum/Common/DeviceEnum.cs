using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LutronQuantum.Common
{
    public enum DeviceEnum
    {
        LightLevel =2,
        LightState=3,
        Scene=4,
        OccupancyState=8,
        LtgPowerUsed=18,
        DaylightSensor=6000
    }

    public enum LightSceneEnum
    {
        Off = 1,
      
        FullOn = 2,
        High = 3,
        Medium = 4,
        Low = 5,
        High1 = 6,
        VTC = 7,
        Low1 = 8,
        Unknown = 9
    }
}