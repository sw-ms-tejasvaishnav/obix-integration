using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using NetBIX.oBIX.Client.Framework;
using Obix_JACE_V1.Obix;
using Obix_JACE_V1.Obix.ObixActions;
using LutronQuantum.Obix.ObixActions;

namespace LutronQuantum.Controllers
{
    public class HomeController : Controller
    {
        ObixClientInit obixClientInit;
        public HomeController()
        {
            obixClientInit = new ObixClientInit();
        }
        public ActionResult LutronQuantum()
        {
            //samle connection check
            ObixResult<XElement> LobbyUriResult = obixClientInit.oBixClient.ReadUriXml(obixClientInit.oBixClient.LobbyUri);



            //// called obix read action
            ReadActions.SaveCurrentLutronQuatumDetail();
            return View();
        }
    }
}
