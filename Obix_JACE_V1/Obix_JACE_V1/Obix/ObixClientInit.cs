using NetBIX.oBIX.Client;
using NetBIX.oBIX.Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Obix_JACE_V1.Obix
{
    public class ObixClientInit
    {
        public XmlObixClient oBixClient;
        public ObixResult oBixResult;

        public ObixClientInit()
        {
            oBixClient = new XmlObixClient(new Uri("http://173.165.100.105/obix"));
            oBixClient.WebClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("obix:obix")));

            oBixResult = oBixClient.Connect();
            if (oBixResult != ObixResult.kObixClientSuccess)
            {
                throw new InvalidOperationException();
            }
        }

    }
}