using System;
using System.IO;
using System.Net;

namespace AngryWasp.Helpers
{
    public static class NetHelper
    {
        public static bool HttpRequest(string url, out string returnString, out string errorString)
        {
            errorString = null;
            returnString = null;

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                using (Stream stream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    returnString = reader.ReadToEnd();
                }
                    
                return true;
            }
            catch (Exception ex)
            {
                errorString = ex.Message;
                return false;
            }
        }
    }
}