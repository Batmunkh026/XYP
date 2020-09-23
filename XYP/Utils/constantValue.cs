using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYP.Utils
{
    public class constantValue
    {
        public const string FORMAT_MONTH = "yyyy-MM";
        public const string FORMAT_DATE = "yyyy-MM-dd";
        public const string FORMAT_DATE_TIME = "yyyy-MM-dd HH:mm:ss";
        public const string FORMAT_TIME = "HH:mm:ss";
        public const string FORMAT_DECIMAL = "0.00";
        public const string DB_SCHEMA = "UNI_DISH";
        public const string DB_SCHEMA_PASSWORD = "Qk6cGhLSWmpL";
        public const string FORMAT_DATE_TIME_LONG = "yyyyMMddHHmmss";
        public const string FORMATDATE = "yyyyMMdd";
        public const string FORMATTIME = "HHmmss";
        public const string REQUESTXML = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:cit='http://citizen.xyp.gov.mn/'><soapenv:Header /><soapenv:Body><cit:{0}><request><auth><citizen><fingerprint>{1}</fingerprint><regnum>{2}</regnum></citizen><operator><fingerprint>{3}</fingerprint><regnum>{4}</regnum></operator></auth></request></cit:{0}></soapenv:Body></soapenv:Envelope>";
        public const string IDCARDWS = "WS100101_getCitizenIDCardInfo";
        public const string ADDRESSWS = "WS100103_getCitizenAddressInfo";
        public const string CHECKCITIZENINFO = "WS100107_checkCitizenInfo";
    }
}