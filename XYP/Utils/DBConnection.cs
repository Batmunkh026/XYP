using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;

namespace XYP.Utils
{
    public class DBConnection
    {
        private string TAG = "DBCONNECTION";
        OracleConnection conn;
        public DBConnection()
        {
            string dbip1 = ConfigurationManager.AppSettings["dbIP1"];
            string dbip2 = ConfigurationManager.AppSettings["dbIP2"];
            string dbIns = ConfigurationManager.AppSettings["dbInstance"];
            string constr = string.Format(@"Data Source=(DESCRIPTION=(LOAD_BALANCE=on)(FAILOVER=off)(ADDRESS_LIST=(SOURCE_ROUTE=yes)(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521))(ADDRESS=(PROTOCOL=TCP)(HOST={1})(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME={2}))); User Id=uni_dish;Password=Qk6cGhLSWmpL;", dbip1, dbip2, dbIns);
            //string constr = string.Format(@"Data Source=(DESCRIPTION=(LOAD_BALANCE=on)(FAILOVER=off)(ADDRESS_LIST=(SOURCE_ROUTE=yes)(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521))(ADDRESS=(PROTOCOL=TCP)(HOST={1})(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME={2}))); User Id=uni_dish;Password=F8zV7zeT8wY2awXa;", dbip1, dbip2, dbIns);
            conn = new OracleConnection(constr);
        }
        public bool iOpen()
        {
            bool retVal = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    retVal = true;
                }
                else
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                LogWriter._error(TAG, ex.ToString());
            }
            return retVal;
        }
        public bool iClose()
        {
            bool retVal = false;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    retVal = true;
                }
                else
                {
                    conn.Close();
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                LogWriter._error(TAG, ex.ToString());
            }
            return retVal;
        }
        public bool idbStatOK()
        {
            bool res = false;
            if (iOpen())
            {
                if (iClose())
                {
                    res = true;
                }
            }
            return res;
        }
        public bool idbCommand(string qry)
        {
            bool res = false;
            try
            {
                iOpen();
                OracleCommand cmd = new OracleCommand(qry, conn);
                int stt = cmd.ExecuteNonQuery();
                iClose();
                res = true;
            }
            catch (Exception ex)
            {
                LogWriter._error(TAG, string.Format(@"SQL: [{0}], Exception: [{1}]", qry, ex.ToString()));
                res = false;
            }
            return res;
        }
        public bool iDBCommandRetID(string qry, out string retID)
        {
            bool retVal = false;
            retID = string.Empty;
            try
            {
                iOpen();
                OracleCommand cmd = new OracleCommand(qry, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ID", OracleDbType.Int32, 10).Direction = ParameterDirection.Output;
                int stt = cmd.ExecuteNonQuery();
                retID = cmd.Parameters["ID"].Value.ToString();
                iClose();
                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
                LogWriter._error(TAG, ex.ToString());
            }
            return retVal;
        }
        public DataTable getTable(string qry)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleDataAdapter da = new OracleDataAdapter(qry, conn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                LogWriter._error(TAG, string.Format(@"SQL: {0}, Exception: {1}", qry, ex.ToString()));
                dt.Clear();
            }
            return dt;
        }
    }
}