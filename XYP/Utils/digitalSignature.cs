using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace XYP.Utils
{
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
            catch(Exception ex)
            {
                retBase = -1;
                exceptionManager.ManageException(ex, TAG);
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
                retByte =  csp.SignData(data, CryptoConfig.MapNameToOID("SHA256"));
            }
            catch(Exception ex)
            {
                exceptionManager.ManageException(ex, TAG);
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
                string accessToken = System.Configuration.ConfigurationManager.AppSettings["token"]; ;
                int timeStamp = getUnixTime();
                var signature = sign(accessToken, timeStamp);
                signSt = Convert.ToBase64String(signature);
                timeSt = timeStamp.ToString();
                saat = true;
            }
            catch(Exception ex)
            {
                exceptionManager.ManageException(ex, TAG);
                saat = false;
            }
            return saat;
        }
    }
}