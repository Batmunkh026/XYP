using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYP_Client_Address.Lib
{
    public class sqlText
    {
        public static string InsertAimag(string code, string name)
        {
            string sql = string.Format("insert into xyp_aimag_city (code, aimag_city_name) values ('{0}', '{1}')", int.Parse(code), name);
            return sql;
        }
        public static string InsertSum(string code, string name, string aimagCityCode)
        {
            string sql = string.Format("insert into xyp_sum_district (code, sum_dirtict_name, aimag_city_code) values ('{0}', '{1}', '{2}')", int.Parse(code), name, int.Parse(aimagCityCode));
            return sql;
        }
        public static string InsertBag(string code, string name, string aimagCityCode, string sumDistrictCode)
        {
            string sql = string.Format("insert into xyp_bag_khoroo (code, bag_khoroo_name, aimag_city_code, sum_district_code) values ('{0}', '{1}', '{2}', '{3}')", int.Parse(code), name, int.Parse(aimagCityCode), int.Parse(sumDistrictCode));
            return sql;
        }
    }
}
