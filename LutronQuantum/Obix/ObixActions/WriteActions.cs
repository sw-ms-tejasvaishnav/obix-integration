using LutronQuantum.Models.LutronObix;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Obix_JACE_V1.Obix.ObixActions
{
    public class WriteActions
    {
        static ObixClientInit obixClientInit = new ObixClientInit();

        /// <summary>
        /// Save light level in obix device.
        /// </summary>
        /// <param name="deviceObj">Passes light level</param>
        public static void SaveLightLevel(DeviceEntity deviceObj)
        {
            var obixClient = obixClientInit.oBixClient;
            var lightLevelUrl = ConfigurationManager.AppSettings["ObixMasterUrl"].ToString() +
            ConfigurationManager.AppSettings["LightLevel"].ToString();
            XElement element = new XElement("real");
            element.SetAttributeValue("val", deviceObj.LightLevel);
            element.SetAttributeValue("is", "obix:WritePointIn");
            element.SetAttributeValue("unit", "obix:units/percent");
            obixClient.InvokeUriXml(new Uri(lightLevelUrl), element);

        }
    }
}