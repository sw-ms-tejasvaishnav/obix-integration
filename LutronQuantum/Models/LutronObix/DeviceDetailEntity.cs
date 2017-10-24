using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LutronQuantum.Models.LutronObix
{
    public class DeviceDetailEntity
    {
        public string LightLevel { get; set; }

        public string LightState { get; set; }

        public string LightScene { get; set; }

        public int LightSceneValue { get; set; }
    }
}