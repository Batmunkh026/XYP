using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Text;
using System.Web;

namespace XYP.Utils
{
    public class httpPoster
    {
        public static bool Poster(string requestXml, out string responseXml)
        {
            digitalSignature sign = new digitalSignature();
            bool funcStatus = false;
            responseXml = string.Empty;
            string stamp = string.Empty;
            string digitalsignature = string.Empty;
            try
            {
                if(sign.signature(out stamp, out digitalsignature))
                {
                    string xypUrl = string.Format("https://xyp.gov.mn/citizen-1.3.0/ws");
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender1, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(xypUrl);
                    httpRequest.Method = "POST";
                    httpRequest.ProtocolVersion = HttpVersion.Version10;
                    httpRequest.ContentType = "text/xml;charset=UTF-8";
                    httpRequest.Timeout = 3 * 60 * 1000;
                    httpRequest.Headers["accessToken"] = ConfigurationManager.AppSettings["token"];
                    httpRequest.Headers["signature"] = digitalsignature;
                    httpRequest.Headers["timeStamp"] = stamp;
                    byte[] postBytes = Encoding.UTF8.GetBytes(requestXml);
                    httpRequest.ContentLength = postBytes.Length;
                    Stream requestStream = httpRequest.GetRequestStream();
                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();
                    HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
                    using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
                    {
                        responseXml = responseStream.ReadToEnd();
                        responseStream.Close();
                    }
                    funcStatus = true;
                }
                else
                {
                    funcStatus = false;
                }
            }
            catch(Exception ex)
            {
                exceptionManager.ManageException(ex, "POSTER");
                funcStatus = false;
            }
            return funcStatus;
        }
        public static string GetClientIPAddress(HttpRequest httpRequest)
        {
            string OriginalIP = string.Empty;
            string RemoteIP = string.Empty;

            OriginalIP = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

            RemoteIP = httpRequest.ServerVariables["REMOTE_ADDR"];

            if (OriginalIP != null && OriginalIP.Trim().Length > 0)
            {
                return OriginalIP + "(" + RemoteIP + ")";
            }

            return RemoteIP;
        }
    }
    
}