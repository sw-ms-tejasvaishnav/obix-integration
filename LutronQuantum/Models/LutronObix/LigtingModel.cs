using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obix_JACE_V1.Models.Lutron
{
    public class LigtingModel
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string DeviceUrl { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
        public string ValueType { get; set; }
        public bool isActive { get; set; }
        public System.DateTime DateOfEntry { get; set; }


    }
}