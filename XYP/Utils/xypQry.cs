using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYP.Utils
{
    public class xypQry
    {
        public static string checkReq(string fName, string lName, string regNo)
        {
            string qry = string.Format("SELECT FIRST_NAME, LAST_NAME, REG_NO, IS_MATCHED FROM XYP_MATCHED_LIST WHERE FIRST_NAME = '{0}' AND LAST_NAME = '{1}' AND REG_NO = '{2}' AND EXPIRE_DATE > SYSDATE ORDER BY CREATED_DATE DESC", fName.ToLower(), lName.ToLower(), regNo.ToLower());
            return qry;
        }
        public static string setReq(string fName, string lName, string regNo, string match)
        {
            string qry = string.Format("INSERT INTO XYP_MATCHED_LIST (FIRST_NAME, LAST_NAME, REG_NO, IS_MATCHED) VALUES ('{0}', '{1}', '{2}', '{3}')", fName.ToLower(), lName.ToLower(), regNo.ToLower(), match);
            return qry;
        }
    }
}