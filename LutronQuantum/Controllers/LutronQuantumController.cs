using LutronQuantum.Common;
using LutronQuantum.Models.LutronObix;
using LutronQuantum.Obix.ObixActions;
using Obix_JACE_V1.Obix;
using Obix_JACE_V1.Obix.ObixActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LutronQuantum.Controllers
{
    [RoutePrefix("api/LutronQuantum")]
    public class LutronQuantumController : ApiController
    {
        ObixClientInit obixClientInit;
        public LutronQuantumController()
        {
            obixClientInit = new ObixClientInit();
        }


        /// <summary>
        /// Gets device list base on device type selection.
        /// </summary>
        /// <param name="deviceTypeId">Passes selected device id.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDeviceList/{deviceTypeId:int}")]
        public IHttpActionResult GetDeviceList(int deviceTypeId)
        {
            var deviceLst = ReadActions.GetDeviceList(deviceTypeId);
            return Ok(deviceLst);
        }

        /// <summary>
        /// Gets device type list base on device area.
        /// </summary>
        /// <returns>Device Type list.</returns>
        [HttpGet]
        [Route("GetDeviceType")]
        public IHttpActionResult GetDeviceType()
        {
            var deviceTypeLst = ReadActions.GetDeviceType();
            return Ok(deviceTypeLst);
        }

        /// <summary>
        /// Gets updated device type list.
        /// </summary>
        /// <returns>Device Type list.</returns>
        [HttpGet]
        [Route("GetUpdateDeviceList")]
        public IHttpActionResult GetUpdateDeviceList()
        {
            ReadActions.SaveCurrentLutronQuatumDetail();
            var deviceLst = ReadActions.GetDeviceList(0);
            return Ok(deviceLst);
        }

        [HttpGet]
        [Route("GetSceneRangeList")]
        public IHttpActionResult GetSceneRangeList()
        {
            var sceneRangeList = ReadActions.GetEnumList<LightSceneEnum>();
            return Ok(sceneRangeList);
        }

        [HttpGet]
        [Route("GetCurrentValue/{deviceTypeId:int}")]
        public IHttpActionResult GetCurrentLightState(int deviceTypeId)
        {
            var deviceLst = ReadActions.GetCurrentLightState(deviceTypeId);
            return Ok(deviceLst);
        }

      
        [HttpPost]
        [Route("SaveLightLevel")]
        public IHttpActionResult SaveLightLevel(DeviceEntity deviceObj)
        {
            WriteActions.WriteLightLevel(deviceObj);
            ReadActions.SaveCurrentLutronQuatumDetail();
            return Ok();
        }

        [HttpPost]
        [Route("SaveLightState")]
        public IHttpActionResult SaveLightState(DeviceEntity deviceObj)
        {
            WriteActions.WriteLightState(deviceObj);
            ReadActions.SaveCurrentLutronQuatumDetail();
            return Ok();
        }

        [HttpPost]
        [Route("SaveDeviceScene")]
        public IHttpActionResult SaveDeviceScene(SceneEntity sceneObj)
        {
            WriteActions.WriteDeviceScene(sceneObj);
            ReadActions.SaveCurrentLutronQuatumDetail();
            return Ok();
        }



        [HttpGet]
        [Route("GetsCurrentDeviceLevel")]
        public IHttpActionResult GetsCurrentDeviceLevel()
        {           
            var deviceDetail= ReadActions.GetsCurrentDeviceLevel();
            return Ok(deviceDetail);
        }

    }
}
