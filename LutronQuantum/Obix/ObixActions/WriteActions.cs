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
        public static void WriteLightLevel(DeviceEntity deviceObj)
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

        public static void WriteLightState(DeviceEntity deviceObj)
        {
            var obixClient = obixClientInit.oBixClient;
            var lightLevelUrl = ConfigurationManager.AppSettings["ObixMasterUrl"].ToString() +
            ConfigurationManager.AppSettings["LightState"].ToString();
            XElement element = new XElement("bool");
            element.SetAttributeValue("val", deviceObj.CurrentStatus ? false : true);
            element.SetAttributeValue("is", "/obix/def/baja:StatusBoolean");
            element.SetAttributeValue("status", "overridden");
            // element.SetAttributeValue("range", "LtgState/~bool");
            obixClient.InvokeUriXml(new Uri(lightLevelUrl), element);
        }

        public static void WriteDeviceScene(SceneEntity deviceObj)
        {
            var obixClient = obixClientInit.oBixClient;
            var lightSceneUrl = ConfigurationManager.AppSettings["ObixMasterUrl"].ToString() +
            ConfigurationManager.AppSettings["LightScene"].ToString();
            XElement element = new XElement("enum");
            element.SetAttributeValue("val", deviceObj.SceneId);
            element.SetAttributeValue("is", "/obix/def/baja:StatusEnum");
            element.SetAttributeValue("status", "overridden");
            obixClient.InvokeUriXml(new Uri(lightSceneUrl), element);
        }
    }
}