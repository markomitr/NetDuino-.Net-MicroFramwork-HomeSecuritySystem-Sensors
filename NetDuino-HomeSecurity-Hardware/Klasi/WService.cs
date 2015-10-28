using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using Toolbox.NETMF.NET;
namespace ndSysKukaDiplomska.Klasi
{
    public class WService
    {
        public static string urlWS = "http://192.168.2.99/TestServis/Sensori.svc/";

        //public static bool ZapisiPrekuWS(String kojSenzor, String podatok)
        //{
        //    string url = "Sensor/" + kojSenzor + "/" + podatok;
        //    return Prati(url);
        //}
        public static bool ZapisiPrekuWS(String kojSenzor, String podatok1, String podatok2)
        {
            
            string url = "Sensor/" + kojSenzor + "/" + podatok1 + "/" + podatok2;
            return PratiDe(url);
        }
        //public static bool ZapisiPrekuWS(String kojSenzor, String podatok1, String podatok2,String podatok3)
        //{
        //    string url = "Sensor/" + kojSenzor + "/" + podatok1 + "/" + podatok2 + "/" + podatok3;
        //    return Prati(url);
        //}

        private static bool PratiDe(String spremnoUrl)
        {
            return true;
            try
            {
          
            // Creates a new web session
            HTTP_Client WebSession = new HTTP_Client(new IntegratedSocket("192.168.2.99", 80));
 
            // Requests the latest source
            HTTP_Client.HTTP_Response Response = WebSession.Get("/TestServis/Sensori.svc/" + spremnoUrl);

            // Did we get the expected response? (a "200 OK")
            //if (Response.ResponseCode != 200)
            //    throw new ApplicationException("Unexpected HTTP response code: " + Response.ResponseCode.ToString());

            // Fetches a response header
           // Debug.Print("Current date according to www.netmftoolbox.com: " + Response.ResponseHeader("date"));

            // Gets the response as a string
           // Debug.Print(Response.ToString());
            }
            catch (Exception ex)
            {
            }
            return true;
        }

    }
}
