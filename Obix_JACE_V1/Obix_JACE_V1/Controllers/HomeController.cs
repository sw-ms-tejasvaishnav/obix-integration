using NetBIX.oBIX.Client.Framework;
using Obix_JACE_V1.Obix;
using Obix_JACE_V1.Obix.ObixActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Obix_JACE_V1.Controllers
{
    public class HomeController : Controller
    {
        ObixClientInit obixClientInit;
        public HomeController()
        {
            obixClientInit = new ObixClientInit();
        }

        public ActionResult Index()
        {
            //samle connection check
            ObixResult<XElement> LobbyUriResult = obixClientInit.oBixClient.ReadUriXml(obixClientInit.oBixClient.LobbyUri);


            //// called obix read action
            ReadActions.GetLutronLightLevel();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}