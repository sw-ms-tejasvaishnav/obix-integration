using NetBIX.oBIX.Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Obix_JACE_V1.Obix.ObixActions
{
    public static class ReadActions
    {
        static ObixClientInit obixClientInit = new ObixClientInit();

        public static void GetLutronLightLevel()
        {
            ObixResult<XElement> LobbyUriResult = obixClientInit.oBixClient.ReadUriXml(obixClientInit.oBixClient.LobbyUri);
        }
    }
}