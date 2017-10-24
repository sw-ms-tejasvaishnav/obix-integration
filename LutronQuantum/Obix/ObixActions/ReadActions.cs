using LutronQuantum.Common;
using LutronQuantum.Data_Modal;
using LutronQuantum.Models.LutronObix;
using NetBIX.oBIX.Client.Framework;
using Obix_JACE_V1.Common;
using Obix_JACE_V1.Models.Lutron;
using Obix_JACE_V1.Obix;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace LutronQuantum.Obix.ObixActions
{
    public static class ReadActions
    {
        static ESD_LutronEntities _dbcontext = new ESD_LutronEntities();
        static ObixClientInit obixClientInit = new ObixClientInit();

        /// <summary>
        /// Save current lutron quatum conference device detail.
        /// </summary>
        public static void SaveCurrentLutronQuatumDetail()
        {
            var obixClient = obixClientInit.oBixClient;
            ObixResult<XElement> LobbyUriResult = obixClient.ReadUriXml(obixClient.LobbyUri);
            ObixResult<XElement> WatchUriResult = obixClient.ReadUriXml(obixClient.WatchUri);
            ObixResult<XElement> AboutUriResult = obixClient.ReadUriXml(obixClient.AboutUri);
            ObixResult<XElement> BatchUriResult = obixClient.ReadUriXml(obixClient.BatchUri);


            string deviceUrl = ConfigurationManager.AppSettings["ObixMasterUrl"].ToString() +
                ConfigurationManager.AppSettings["DeviceData"].ToString();

            var xmlsineWaveRollupResult = obixClient.ReadUriXml(new Uri(deviceUrl));

            foreach (XNode node in xmlsineWaveRollupResult.Result.Document.Nodes())
            {
                if (node is XElement)
                {
                    IEnumerable<XNode> nodeList = node.Document.DescendantNodes();
                    foreach (XNode nodes in nodeList.ToList())
                    {
                        XElement element = nodes as XElement;
                        if (element == null || element.Attribute("displayName") == null
                            ? false : element.Attribute("displayName").Value == DeviceNameType.Conference4628A1761035)
                        {
                            var lutronQuantumDevicesUrl = deviceUrl + "/" + element.FirstAttribute.NextAttribute.Value;
                            var allConferenceDeviceResult = obixClientInit.oBixClient.ReadUriXml(new Uri(lutronQuantumDevicesUrl));
                            if (allConferenceDeviceResult.ResultSucceeded == true)
                            {
                                IEnumerable<XNode> conferenceNodeLst = allConferenceDeviceResult.Result.Document.DescendantNodes();
                                foreach (XNode conferenceNode in conferenceNodeLst.ToList())
                                {
                                    XElement celement = conferenceNode as XElement;
                                    if (celement == null || celement.Attribute("displayName") == null
                                        ? false : celement.Attribute("displayName").Value == DeviceNameType.Points)
                                    {
                                        var lutronQuantumDevicesPointsUrl = lutronQuantumDevicesUrl + celement.FirstAttribute.NextAttribute.Value;
                                        var allConferenceDevicePointsResult = obixClientInit.oBixClient.ReadUriXml(new Uri(lutronQuantumDevicesPointsUrl));
                                        if (allConferenceDevicePointsResult.ResultSucceeded == true)
                                        {
                                            IEnumerable<XNode> conferenceDevicePointsLst = allConferenceDevicePointsResult.Result.Document.DescendantNodes();
                                            foreach (XNode pointsNode in conferenceDevicePointsLst.ToList())
                                            {
                                                XElement pointelement = pointsNode as XElement;
                                                if (pointelement == null || pointelement.FirstAttribute.Value == null
                                                    ? false : pointelement.FirstAttribute.Value == DeviceNameType.Basic)
                                                {
                                                    var basicPointsUrl = lutronQuantumDevicesPointsUrl + pointelement.FirstAttribute.NextAttribute.Value;
                                                    var basicPointsResult = obixClientInit.oBixClient.ReadUriXml(new Uri(basicPointsUrl));
                                                    if (basicPointsResult.ResultSucceeded == true)
                                                    {
                                                        IEnumerable<XNode> pointsBasicNodeLst = basicPointsResult.Result.Document.DescendantNodes();
                                                        foreach (var basicNode in pointsBasicNodeLst)
                                                        {
                                                            XElement basicNodeElement = basicNode as XElement;
                                                            if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                              ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.LightLevel)
                                                            {
                                                                var lightLevelUrl = basicPointsUrl + basicNodeElement.Attribute("href").Value;
                                                                var lightLevelXmlData = obixClientInit.oBixClient.ReadUriXml(new Uri(lightLevelUrl));
                                                                if (lightLevelXmlData.ResultSucceeded == true)
                                                                {
                                                                    SaveCurrentLightLevel(lightLevelXmlData, DeviceNameType.Conference4628A1761035);
                                                                }
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                            ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.LightState)
                                                            {
                                                                var lightStateUrl = basicPointsUrl + basicNodeElement.Attribute("href").Value;
                                                                var lightStateXmlData = obixClientInit.oBixClient.ReadUriXml(new Uri(lightStateUrl));
                                                                if (lightStateXmlData.ResultSucceeded == true)
                                                                {
                                                                    SaveCurrentLightState(lightStateXmlData, DeviceNameType.Conference4628A1761035);
                                                                }
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                             ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.Scene)
                                                            {
                                                                var lightSceneUrl = basicPointsUrl + basicNodeElement.Attribute("href").Value;
                                                                var lightSceneXmlData = obixClientInit.oBixClient.ReadUriXml(new Uri(lightSceneUrl));
                                                                if (lightSceneXmlData.ResultSucceeded == true)
                                                                {
                                                                    SaveCurrentScene(lightSceneXmlData, DeviceNameType.Conference4628A1761035);
                                                                }
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                             ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.OccupancyState)
                                                            {
                                                                var lightOccupancyStateUrl = basicPointsUrl + basicNodeElement.Attribute("href").Value;
                                                                var lightOccupancyStateXmlData = obixClientInit.oBixClient.ReadUriXml(new Uri(lightOccupancyStateUrl));
                                                                if (lightOccupancyStateXmlData.ResultSucceeded == true)
                                                                {
                                                                    SaveCurrentOccupancyState(lightOccupancyStateXmlData, DeviceNameType.Conference4628A1761035);
                                                                }
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                             ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.LtgPowerUsed)
                                                            {
                                                                var lightPowerUsedUrl = basicPointsUrl + basicNodeElement.Attribute("href").Value;
                                                                var lightPowerUsedXmlData = obixClientInit.oBixClient.ReadUriXml(new Uri(lightPowerUsedUrl));
                                                                if (lightPowerUsedXmlData.ResultSucceeded == true)
                                                                {
                                                                    SaveCurrentLightPowerUsed(lightPowerUsedXmlData, DeviceNameType.Conference4628A1761035);
                                                                }
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                             ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.DaylightSensor)
                                                            {
                                                                var daylightSensorUsedUrl = basicPointsUrl + basicNodeElement.Attribute("href").Value;
                                                                var daylightSensorUsedXmlData = obixClientInit.oBixClient.ReadUriXml(new Uri(daylightSensorUsedUrl));
                                                                if (daylightSensorUsedXmlData.ResultSucceeded == true)
                                                                {
                                                                    SaveCurrentDaylightSensor(daylightSensorUsedXmlData, DeviceNameType.Conference4628A1761035);
                                                                }
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }


        /// <summary>
        /// Saves current light level.
        /// </summary>
        /// <param name="lightLevelXmlData">Passes read xml light level object.</param>
        /// <param name="DeviceType">Passes device type.</param>
        private static void SaveCurrentLightLevel(ObixResult<XElement> lightLevelXmlData, string DeviceType)
        {
            IEnumerable<XNode> lighLevelLst = lightLevelXmlData.Result.Document.DescendantNodes();
            XElement lightLevelelement = lighLevelLst.LastOrDefault() as XElement;
            if (lightLevelelement != null)
            {
                var deviceDetail = _dbcontext.ObixDevices.Where(y => y.object_instance == (int)DeviceEnum.LightLevel).FirstOrDefault();
                if (deviceDetail == null)
                {
                    var lightLevelObj = new ObixDevice
                    {
                        DeviceName = lightLevelelement.Attribute("displayName").Value,
                        Unit = lightLevelelement.Attribute("unit").Value,
                        DeviceUrl = lightLevelelement.Attribute("href").Value,
                        Status = lightLevelelement.Attribute("status") != null ? lightLevelelement.Attribute("status").Value : null,
                        Value = lightLevelelement.Attribute("val").Value,
                        DeviceType = DeviceType,
                        isActive = true,
                        DateOfEntry = DateTime.UtcNow,
                        ValueType = lightLevelelement.Name.LocalName,
                        object_instance = (int)DeviceEnum.LightLevel
                    };

                    _dbcontext.ObixDevices.Add(lightLevelObj);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    deviceDetail.DeviceName = lightLevelelement.Attribute("displayName").Value;
                    deviceDetail.Unit = lightLevelelement.Attribute("unit").Value;
                    deviceDetail.DeviceUrl = lightLevelelement.Attribute("href").Value;
                    deviceDetail.Status = lightLevelelement.Attribute("status") != null ? lightLevelelement.Attribute("status").Value : null;
                    deviceDetail.Value = lightLevelelement.Attribute("val").Value;
                    deviceDetail.DeviceType = DeviceType;
                    deviceDetail.isActive = true;
                    deviceDetail.DateOfEntry = DateTime.UtcNow;
                    deviceDetail.ValueType = lightLevelelement.Name.LocalName;
                    deviceDetail.object_instance = (int)DeviceEnum.LightLevel;

                    _dbcontext.Entry(deviceDetail).State = System.Data.Entity.EntityState.Modified;
                    _dbcontext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Saves current occupancy state level detail.
        /// </summary>
        /// <param name="occupancyStateXmlData">Passes read xml ocupancy state detail.</param>
        /// <param name="DeviceType">Passes device type.</param>
        private static void SaveCurrentOccupancyState(ObixResult<XElement> occupancyStateXmlData, string DeviceType)
        {
            IEnumerable<XNode> occupancyStateLst = occupancyStateXmlData.Result.Document.DescendantNodes();
            XElement occupancyStateElement = occupancyStateLst.LastOrDefault() as XElement;
            if (occupancyStateElement != null)
            {
                var deviceDetail = _dbcontext.ObixDevices.Where(y => y.object_instance == (int)DeviceEnum.OccupancyState).FirstOrDefault();
                if (deviceDetail == null)
                {
                    var occupancyStateObj = new ObixDevice
                    {
                        DeviceName = occupancyStateElement.Attribute("displayName").Value,
                        DeviceUrl = occupancyStateElement.Attribute("href").Value,

                        Value = occupancyStateElement.Attribute("val").Value,
                        DeviceType = DeviceType,
                        isActive = true,
                        DateOfEntry = DateTime.UtcNow,
                        ValueType = occupancyStateElement.Name.LocalName,
                        object_instance = (int)DeviceEnum.OccupancyState
                    };

                    _dbcontext.ObixDevices.Add(occupancyStateObj);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    deviceDetail.DeviceName = occupancyStateElement.Attribute("displayName").Value;
                    deviceDetail.DeviceUrl = occupancyStateElement.Attribute("href").Value;
                    deviceDetail.Value = occupancyStateElement.Attribute("val").Value;
                    deviceDetail.DeviceType = DeviceType;
                    deviceDetail.isActive = true;
                    deviceDetail.DateOfEntry = DateTime.UtcNow;
                    deviceDetail.ValueType = occupancyStateElement.Name.LocalName;
                    deviceDetail.object_instance = (int)DeviceEnum.OccupancyState;
                    _dbcontext.ObixDevices.Attach(deviceDetail);
                    _dbcontext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Saves current light state level detail.
        /// </summary>
        /// <param name="lightStateXmlData">Passes read xml light state detail.</param>
        /// <param name="DeviceType">Passes device type.</param>
        private static void SaveCurrentLightState(ObixResult<XElement> lightStateXmlData, string DeviceType)
        {
            IEnumerable<XNode> lightStateLst = lightStateXmlData.Result.Document.DescendantNodes();
            XElement lightStateElement = lightStateLst.LastOrDefault() as XElement;
            if (lightStateElement != null)
            {
                var deviceDetail = _dbcontext.ObixDevices.Where(y => y.object_instance == (int)DeviceEnum.LightState).FirstOrDefault();
                if (deviceDetail == null)
                {
                    var lightStateObj = new ObixDevice
                    {
                        DeviceName = lightStateElement.Attribute("displayName").Value,

                        DeviceUrl = lightStateElement.Attribute("href").Value,
                        Status = lightStateElement.Attribute("status") == null ? string.Empty : lightStateElement.Attribute("status").Value,
                        Value = lightStateElement.Attribute("val").Value,
                        DeviceType = DeviceType,
                        isActive = true,
                        DateOfEntry = DateTime.UtcNow,
                        ValueType = lightStateElement.Name.LocalName,
                        object_instance = (int)DeviceEnum.LightState
                    };

                    _dbcontext.ObixDevices.Add(lightStateObj);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    deviceDetail.DeviceName = lightStateElement.Attribute("displayName").Value;
                    deviceDetail.DeviceUrl = lightStateElement.Attribute("href").Value;
                    deviceDetail.Status = lightStateElement.Attribute("status") == null ? string.Empty : lightStateElement.Attribute("status").Value;
                    deviceDetail.Value = lightStateElement.Attribute("val").Value;
                    deviceDetail.DeviceType = DeviceType;
                    deviceDetail.isActive = true;
                    deviceDetail.DateOfEntry = DateTime.UtcNow;
                    deviceDetail.ValueType = lightStateElement.Name.LocalName;
                    deviceDetail.object_instance = (int)DeviceEnum.LightState;
                    _dbcontext.Entry(deviceDetail).State = System.Data.Entity.EntityState.Modified;
                    _dbcontext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Saves current scene detail.
        /// </summary>
        /// <param name="sceneXmlData">Passes read xml scene detail.</param>
        /// <param name="DeviceType">Passes device type.</param>
        private static void SaveCurrentScene(ObixResult<XElement> sceneXmlData, string DeviceType)
        {
            IEnumerable<XNode> lighSceneLst = sceneXmlData.Result.Document.DescendantNodes();
            XElement lightSceneElement = lighSceneLst.LastOrDefault() as XElement;
            if (lightSceneElement != null)
            {
                var deviceDetail = _dbcontext.ObixDevices.Where(y => y.object_instance == (int)DeviceEnum.Scene).FirstOrDefault();
                if (deviceDetail == null)
                {
                    var lightSceneObj = new ObixDevice
                    {
                        DeviceName = lightSceneElement.Attribute("displayName").Value,
                        DeviceUrl = lightSceneElement.Attribute("href").Value,
                        Value = lightSceneElement.Attribute("val").Value,
                        DeviceType = DeviceType,
                        isActive = true,
                        DateOfEntry = DateTime.UtcNow,
                        ValueType = lightSceneElement.Name.LocalName,
                        object_instance = (int)DeviceEnum.Scene
                    };

                    _dbcontext.ObixDevices.Add(lightSceneObj);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    deviceDetail.DeviceName = lightSceneElement.Attribute("displayName").Value;
                    deviceDetail.DeviceUrl = lightSceneElement.Attribute("href").Value;
                    deviceDetail.Value = lightSceneElement.Attribute("val").Value;
                    deviceDetail.DeviceType = DeviceType;
                    deviceDetail.isActive = true;
                    deviceDetail.DateOfEntry = DateTime.UtcNow;
                    deviceDetail.ValueType = lightSceneElement.Name.LocalName;
                    deviceDetail.object_instance = (int)DeviceEnum.Scene;
                    _dbcontext.Entry(deviceDetail).State = System.Data.Entity.EntityState.Modified;
                    _dbcontext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Saves current light power used detail.
        /// </summary>
        /// <param name="lightPowerUsedXmlData">Passes read light power used.</param>
        /// <param name="DeviceType">Passes device type.</param>
        private static void SaveCurrentLightPowerUsed(ObixResult<XElement> lightPowerUsedXmlData, string DeviceType)
        {
            IEnumerable<XNode> lighPowerUsedLst = lightPowerUsedXmlData.Result.Document.DescendantNodes();
            XElement lightPowerUsedelement = lighPowerUsedLst.LastOrDefault() as XElement;
            if (lightPowerUsedelement != null)
            {
                var deviceDetail = _dbcontext.ObixDevices.Where(y => y.object_instance == (int)DeviceEnum.LtgPowerUsed).FirstOrDefault();
                if (deviceDetail == null)
                {
                    var lightPowerUsedObj = new ObixDevice
                    {
                        DeviceName = lightPowerUsedelement.Attribute("displayName").Value,
                        Unit = lightPowerUsedelement.Attribute("unit").Value,
                        DeviceUrl = lightPowerUsedelement.Attribute("href").Value,

                        Value = lightPowerUsedelement.Attribute("val").Value,
                        DeviceType = DeviceType,
                        isActive = true,
                        DateOfEntry = DateTime.UtcNow,
                        ValueType = lightPowerUsedelement.Name.LocalName,
                        object_instance = (int)DeviceEnum.LtgPowerUsed
                    };

                    _dbcontext.ObixDevices.Add(lightPowerUsedObj);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    deviceDetail.DeviceName = lightPowerUsedelement.Attribute("displayName").Value;
                    deviceDetail.Unit = lightPowerUsedelement.Attribute("unit").Value;
                    deviceDetail.DeviceUrl = lightPowerUsedelement.Attribute("href").Value;
                    deviceDetail.Value = lightPowerUsedelement.Attribute("val").Value;
                    deviceDetail.DeviceType = DeviceType;
                    deviceDetail.isActive = true;
                    deviceDetail.DateOfEntry = DateTime.UtcNow;
                    deviceDetail.ValueType = lightPowerUsedelement.Name.LocalName;
                    deviceDetail.object_instance = (int)DeviceEnum.LtgPowerUsed;
                    _dbcontext.Entry(deviceDetail).State = System.Data.Entity.EntityState.Modified;
                    _dbcontext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Saves current day light sensor detail.
        /// </summary>
        /// <param name="daylightSensorXmlData">Passses read xml day light sensor object detail.</param>
        /// <param name="DeviceType">Passes device type.</param>
        private static void SaveCurrentDaylightSensor(ObixResult<XElement> daylightSensorXmlData, string DeviceType)
        {
            IEnumerable<XNode> daylightSensorLst = daylightSensorXmlData.Result.Document.DescendantNodes();
            XElement daylightSensorelement = daylightSensorLst.LastOrDefault() as XElement;
            if (daylightSensorelement != null)
            {
                var deviceDetail = _dbcontext.ObixDevices.Where(y => y.object_instance == (int)DeviceEnum.DaylightSensor).FirstOrDefault();
                if (deviceDetail == null)
                {
                    var daylightSensorObj = new ObixDevice
                    {
                        DeviceName = daylightSensorelement.Attribute("displayName").Value,
                        Unit = daylightSensorelement.Attribute("unit").Value,
                        DeviceUrl = daylightSensorelement.Attribute("href").Value,
                        Value = daylightSensorelement.Attribute("val").Value,
                        DeviceType = DeviceType,
                        isActive = true,
                        DateOfEntry = DateTime.UtcNow,
                        ValueType = daylightSensorelement.Name.LocalName,
                        object_instance = (int)DeviceEnum.DaylightSensor
                    };

                    _dbcontext.ObixDevices.Add(daylightSensorObj);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    deviceDetail.DeviceName = daylightSensorelement.Attribute("displayName").Value;
                    deviceDetail.Unit = daylightSensorelement.Attribute("unit").Value;
                    deviceDetail.DeviceUrl = daylightSensorelement.Attribute("href").Value;
                    deviceDetail.Value = daylightSensorelement.Attribute("val").Value;
                    deviceDetail.DeviceType = DeviceType;
                    deviceDetail.isActive = true;
                    deviceDetail.DateOfEntry = DateTime.UtcNow;
                    deviceDetail.ValueType = daylightSensorelement.Name.LocalName;
                    deviceDetail.object_instance = (int)DeviceEnum.DaylightSensor;
                    _dbcontext.Entry(deviceDetail).State = System.Data.Entity.EntityState.Modified;
                    _dbcontext.SaveChanges();
                }
            }
        }



        /// <summary>
        /// Gets device list from data base.
        /// </summary>
        /// <param name="deviceType">Passes selected device type.4</param>
        /// <returns>Selected device type resule from data base.</returns>
        public static List<LigtingModel> GetDeviceList(int deviceType)
        {
            var devicedetail = (from bd in _dbcontext.ObixDevices
                                where bd.isActive == true && deviceType == 0 ? true : bd.object_instance == deviceType
                                select new LigtingModel
                                {
                                    DeviceName = bd.DeviceName,
                                    DeviceType = bd.DeviceType,
                                    DeviceUrl = bd.DeviceUrl,
                                    Unit = bd.Unit,
                                    DateOfEntry = bd.DateOfEntry,
                                    Status = bd.Status,
                                    ValueType = bd.ValueType,
                                    Value = bd.Value
                                }).ToList();

            return devicedetail;
        }

        /// <summary>
        /// Gets device type from device.
        /// </summary>
        /// <returns>Device type list base on device area.</returns>
        public static List<DeviceType> GetDeviceType()
        {
            var deviceTypeLst = new List<DeviceType>();
            string deviceUrl = ConfigurationManager.AppSettings["ObixMasterUrl"].ToString() +
               ConfigurationManager.AppSettings["DeviceData"].ToString();

            var xmlsineWaveRollupResult = obixClientInit.oBixClient.ReadUriXml(new Uri(deviceUrl));

            foreach (XNode node in xmlsineWaveRollupResult.Result.Document.Nodes())
            {
                if (node is XElement)
                {
                    IEnumerable<XNode> nodeList = node.Document.DescendantNodes();
                    foreach (XNode nodes in nodeList.ToList())
                    {
                        XElement element = nodes as XElement;
                        if (element == null || element.Attribute("displayName") == null
                            ? false : element.Attribute("displayName").Value == DeviceNameType.Conference4628A1761035)
                        {
                            var lutronQuantumDevicesUrl = deviceUrl + "/" + element.FirstAttribute.NextAttribute.Value;
                            var allConferenceDeviceResult = obixClientInit.oBixClient.ReadUriXml(new Uri(lutronQuantumDevicesUrl));
                            if (allConferenceDeviceResult.ResultSucceeded == true)
                            {
                                IEnumerable<XNode> conferenceNodeLst = allConferenceDeviceResult.Result.Document.DescendantNodes();
                                foreach (XNode conferenceNode in conferenceNodeLst.ToList())
                                {
                                    XElement celement = conferenceNode as XElement;
                                    if (celement == null || celement.Attribute("displayName") == null
                                        ? false : celement.Attribute("displayName").Value == DeviceNameType.Points)
                                    {
                                        var lutronQuantumDevicesPointsUrl = lutronQuantumDevicesUrl + celement.FirstAttribute.NextAttribute.Value;
                                        var allConferenceDevicePointsResult = obixClientInit.oBixClient.ReadUriXml(new Uri(lutronQuantumDevicesPointsUrl));
                                        if (allConferenceDevicePointsResult.ResultSucceeded == true)
                                        {
                                            IEnumerable<XNode> conferenceDevicePointsLst = allConferenceDevicePointsResult.Result.Document.DescendantNodes();
                                            foreach (XNode pointsNode in conferenceDevicePointsLst.ToList())
                                            {
                                                XElement pointelement = pointsNode as XElement;
                                                if (pointelement == null || pointelement.FirstAttribute.Value == null
                                                    ? false : pointelement.FirstAttribute.Value == DeviceNameType.Basic)
                                                {
                                                    var basicPointsUrl = lutronQuantumDevicesPointsUrl + pointelement.FirstAttribute.NextAttribute.Value;
                                                    var basicPointsResult = obixClientInit.oBixClient.ReadUriXml(new Uri(basicPointsUrl));
                                                    if (basicPointsResult.ResultSucceeded == true)
                                                    {
                                                        IEnumerable<XNode> pointsBasicNodeLst = basicPointsResult.Result.Document.DescendantNodes();
                                                        foreach (var basicNode in pointsBasicNodeLst)
                                                        {
                                                            XElement basicNodeElement = basicNode as XElement;
                                                            if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                              ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.LightLevel)
                                                            {
                                                                var deviceType = new DeviceType
                                                                {
                                                                    DeviceName = basicNodeElement.FirstAttribute.Value,
                                                                    DeviceTypeId = (int)DeviceEnum.LightLevel
                                                                };
                                                                deviceTypeLst.Add(deviceType);
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                             ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.LightState)
                                                            {
                                                                var deviceType = new DeviceType
                                                                {
                                                                    DeviceName = basicNodeElement.FirstAttribute.Value,
                                                                    DeviceTypeId = (int)DeviceEnum.LightState
                                                                };
                                                                deviceTypeLst.Add(deviceType);
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                             ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.Scene)
                                                            {
                                                                var deviceType = new DeviceType
                                                                {
                                                                    DeviceName = basicNodeElement.FirstAttribute.Value,
                                                                    DeviceTypeId = (int)DeviceEnum.Scene
                                                                };
                                                                deviceTypeLst.Add(deviceType);
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                             ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.OccupancyState)
                                                            {
                                                                var deviceType = new DeviceType
                                                                {
                                                                    DeviceName = basicNodeElement.FirstAttribute.Value,
                                                                    DeviceTypeId = (int)DeviceEnum.OccupancyState
                                                                };
                                                                deviceTypeLst.Add(deviceType);
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                             ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.LtgPowerUsed)
                                                            {
                                                                var deviceType = new DeviceType
                                                                {
                                                                    DeviceName = basicNodeElement.FirstAttribute.Value,
                                                                    DeviceTypeId = (int)DeviceEnum.LtgPowerUsed
                                                                };
                                                                deviceTypeLst.Add(deviceType);
                                                            }
                                                            else if (basicNodeElement == null || basicNodeElement.FirstAttribute.Value == null
                                                             ? false : basicNodeElement.FirstAttribute.Value == DeviceNameType.DaylightSensor)
                                                            {
                                                                var deviceType = new DeviceType
                                                                {
                                                                    DeviceName = basicNodeElement.FirstAttribute.Value,
                                                                    DeviceTypeId = (int)DeviceEnum.DaylightSensor
                                                                };
                                                                deviceTypeLst.Add(deviceType);
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            return deviceTypeLst;

        }


        public static List<SceneEntity> GetEnumList<T>()
        {
            var list = new List<SceneEntity>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                list.Add(new SceneEntity { SceneId = (int)e, SceneName = e.ToString() });
            }
            return list;
        }


        public static LigtingModel GetCurrentLightState( int deviceType)
        {
            var devicedetail = (from bd in _dbcontext.ObixDevices
                                where bd.isActive == true
                                //&& bd.DeviceId == deviceId 
                                //need to add deviceid so that light state can get based on device id.
                                && bd.object_instance == deviceType
                                select new LigtingModel
                                {
                                    DeviceName = bd.DeviceName,
                                    DeviceType = bd.DeviceType,
                                    DeviceUrl = bd.DeviceUrl,
                                    Unit = bd.Unit,
                                    DateOfEntry = bd.DateOfEntry,
                                    Status = bd.Status,
                                    ValueType = bd.ValueType,
                                    Value = bd.Value
                                }).FirstOrDefault();

            return devicedetail;
        }

        public static DeviceDetailEntity GetsCurrentDeviceLevel()
        {
            var devicedetail = new DeviceDetailEntity
            {
                LightLevel = _dbcontext.ObixDevices.Where(y => y.object_instance == (int)DeviceEnum.LightLevel && y.isActive == true).Select(y => y.Value).FirstOrDefault(),
                LightScene = _dbcontext.ObixDevices.Where(y => y.object_instance == (int)DeviceEnum.Scene && y.isActive == true).Select(y => y.Value).FirstOrDefault(),
                LightState = _dbcontext.ObixDevices.Where(y => y.object_instance == (int)DeviceEnum.LightState && y.isActive == true).Select(y => y.Value).FirstOrDefault()
            };
            if(devicedetail!= null)
            {                 
               devicedetail.LightSceneValue = (int)Enum.Parse(typeof(LightSceneEnum), devicedetail.LightScene.Replace("$20","").Replace("20", ""));


            }
            return devicedetail;
        }
    }
}