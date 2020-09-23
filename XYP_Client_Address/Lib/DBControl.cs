using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYP_Client_Address.Lib
{
    public class DBControl
    {
        private string TAG = "DBCONTROL";
        OracleConnection conn;
        public DBControl()
        {
            string dbip1 = "192.168.10.240";
            string dbip2 = "192.168.10.241";
            string dbIns = "unidish";
            string username = "uni_dish";
            string password = "Qk6cGhLSWmpL";
            string constr = string.Format(@"Data Source=(DESCRIPTION=(LOAD_BALANCE=on)(FAILOVER=off)(ADDRESS_LIST=(SOURCE_ROUTE=yes)(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521))(ADDRESS=(PROTOCOL=TCP)(HOST={1})(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME={2}))); User Id={3};Password={4};", dbip1, dbip2, dbIns, username, password);
            conn = new OracleConnection(constr);
        }
        public string iOpen()
        {
            string retVal = string.Empty;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    retVal = "0000";
                }
                else
                {
                    retVal = "0000";
                }
            }
            catch (Exception ex)
            {
                retVal = string.Format("FFFFx[{0}]", ex.Message);
            }
            return retVal;
        }
        public string iClose()
        {
            string retVal = string.Empty;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    retVal = "0000";
                }
                else
                {
                    conn.Close();
                    retVal = "0000";
                }
            }
            catch (Exception ex)
            {
                retVal = string.Format("FFFFx[{0}]", ex.Message);
            }
            return retVal;
        }
        public bool idbCheck(out string sttCode)
        {
            sttCode = string.Empty;
            bool retVal = false;
            try
            {
                string stateCode = iOpen();
                if (stateCode == "0000")
                {
                    string cclose = iClose();
                    if (cclose == "0000")
                    {
                        sttCode = string.Empty;
                        retVal = true;
                    }
                    else
                    {
                        retVal = false;
                        sttCode = cclose;
                    }
                }
                else
                {
                    retVal = false;
                    sttCode = stateCode;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                sttCode = string.Format("FFFFx[{0}]", ex.Message);
            }
            return retVal;
        }
        public string iDBCommand(string qry)
        {
            string retVal = string.Empty;
            try
            {
                string op = iOpen();
                OracleCommand cmd = new OracleCommand(qry, conn);
                int stt = cmd.ExecuteNonQuery();
                string cl = iClose();
                retVal = string.Format(@"{0}{1}", stt, "000");
            }
            catch (Exception ex)
            {
                retVal = string.Format("FFFFx[{0}]", ex.Message);
            }
            return retVal;
        }
        public DataTable getTable(string qry)
        {
            DataTable dt = new DataTable();
            try
            {
                if (qry.Length > 0)
                {
                    OracleDataAdapter da = new OracleDataAdapter(qry, conn);
                    da.Fill(dt);
                }
                else
                {
                    dt.Clear();
                }
            }
            catch (Exception ex)
            {
                //SystemLogWriter.LogWriter._error(TAG, ex.ToString());
                dt.Clear();
            }
            return dt;
        }
        public bool callProcedure(string _cardNo, string _tvChannel, string _smsCode, string _watchDate, string _orderType, out string msgENG, out string msgMON, out string msgCRY)
        {
            bool retValue = false;
            msgENG = string.Empty;
            msgMON = string.Empty;
            msgCRY = string.Empty;
            try
            {
                iOpen();
                OracleCommand ocm = new OracleCommand("UNI_DISH.ADDNVODAPI", conn);
                ocm.CommandType = CommandType.StoredProcedure;
                ocm.Parameters.Add("CardNo", OracleDbType.Varchar2, 30).Value = _cardNo;
                ocm.Parameters.Add("TvChannel", OracleDbType.Varchar2, 30).Value = _tvChannel;
                ocm.Parameters.Add("SmsCode", OracleDbType.Varchar2, 30).Value = _smsCode;
                ocm.Parameters.Add("WatchDate", OracleDbType.Varchar2, 30).Value = _watchDate;
                ocm.Parameters.Add("OrderType", OracleDbType.Varchar2, 30).Value = _orderType;
                ocm.Parameters.Add("isSuccess", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                ocm.Parameters.Add("outMsgENG", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;
                ocm.Parameters.Add("outMsgMON", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;
                ocm.Parameters.Add("outMsgCRY", OracleDbType.Varchar2, 2000).Direction = ParameterDirection.Output;
                ocm.ExecuteNonQuery();
                iClose();
                string result = string.Empty;
                result = ocm.Parameters["isSuccess"].Value.ToString();
                msgENG = ocm.Parameters["outMsgENG"].Value.ToString();
                msgMON = ocm.Parameters["outMsgMON"].Value.ToString();
                msgCRY = ocm.Parameters["outMsgCRY"].Value.ToString();
                if (result == "0000")
                {
                    retValue = true;
                }
                else
                {
                    retValue = false;

                }
            }
            catch (Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
                msgENG = "INTERNAL ERROR";
                msgMON = "ДОТООД АЛДАА";
                msgCRY = "DOTOOD ALDAA";
                retValue = false;
            }
            return retValue;
        }
    }
}
