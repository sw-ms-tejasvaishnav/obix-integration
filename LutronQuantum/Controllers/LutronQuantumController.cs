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
