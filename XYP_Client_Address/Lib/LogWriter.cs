using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace XYP_Client_Address.Lib
{
    public class LogWriter
    {
        public static void _error(string TAG, string logtxt)
        {
            try
            {
                var logfolder = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\Error";
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(logfolder);
                FileSystemAccessRule fsar = new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow);
                DirectorySecurity ds = null;
                if (!di.Exists)
                {
                    Directory.CreateDirectory(logfolder);
                }
                ds = di.GetAccessControl();
                ds.AddAccessRule(fsar);
                StreamWriter sw = new StreamWriter(logfolder + "\\error" + DateTime.Today.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(string.Format(@"{0} [{1}] {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), TAG, logtxt));
                sw.Close();
                logtxt = string.Empty;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
