using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace XYP_Client_Address.Lib
{
    public class Poster
    {
        public static string  TAG = "POSTER";
        public static bool HttpPoster(string requestXml, out string responseXml)
        {
            digitalSignature sign = new digitalSignature();
            bool funcStatus = false;
            responseXml = string.Empty;
            string stamp = string.Empty;
            string digitalsignature = string.Empty;
            try
            {
                if (sign.signature(out stamp, out digitalsignature))
                {
                    string xypUrl = string.Format("https://xyp.gov.mn/citizen-1.3.0/ws");
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender1, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(xypUrl);
                    httpRequest.Method = "POST";
                    httpRequest.ProtocolVersion = HttpVersion.Version10;
                    httpRequest.ContentType = "text/xml;charset=UTF-8";
                    httpRequest.Timeout = 3 * 60 * 1000;
                    httpRequest.Headers["accessToken"] = "3e4bfc4d7f874e1802ee22c0750d7178";
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
            catch (Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
                funcStatus = false;
            }
            return funcStatus;
        }
    }
    public class digitalSignature
    {
        private string TAG = "digitalSignature";
        public int getUnixTime()
        {
            int retBase = -1;
            try
            {
                retBase = Convert.ToInt32((DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local))).TotalSeconds);
            }
            catch (Exception ex)
            {
                retBase = -1;
                LogWriter._error(TAG, ex.ToString());
            }
            return retBase;
        }
        public byte[] sign(string accessToken, int timeStamp)
        {
            byte[] retByte = null;
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + @"ddishtv.p12";
                X509Certificate2 cert = new X509Certificate2(filePath, "Admin123", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PrivateKey;
                byte[] data = Encoding.UTF8.GetBytes(accessToken + "." + timeStamp);
                retByte = csp.SignData(data, CryptoConfig.MapNameToOID("SHA256"));
            }
            catch (Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
                retByte = null;
            }
            return retByte;
        }
        public bool signature(out string timeSt, out string signSt)
        {
            bool saat = false;
            timeSt = string.Empty;
            signSt = string.Empty;
            try
            {
                string accessToken = "3e4bfc4d7f874e1802ee22c0750d7178";
                int timeStamp = getUnixTime();
                var signature = sign(accessToken, timeStamp);
                signSt = Convert.ToBase64String(signature);
                timeSt = timeStamp.ToString();
                saat = true;
            }
            catch (Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
                saat = false;
            }
            return saat;
        }
    }
}
