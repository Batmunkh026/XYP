using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using XYP_Client_Address.Lib;
using XYP_Client_Address.Model;

namespace XYP_Client_Address
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Service started.");
            Worker worker = new Worker();
        }
    }
    public class Worker
    {
        DBControl dbconn = new DBControl();
        string TAG = "WORKER";
        string dbres = string.Empty;
        public Worker()
        {
            AddressClient();
        }
        private void AddressClient()
        {
            try
            {
                if(dbconn.idbCheck(out dbres))
                {
                    string req1 = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:cit='http://citizen.xyp.gov.mn/'><soapenv:Header/><soapenv:Body><cit:WS100111_aimagCityInfo/></soapenv:Body></soapenv:Envelope>";
                    string aimagCityResp = string.Empty;
                    if (Poster.HttpPoster(req1, out aimagCityResp))
                    {
                        XmlSerializer xmlser = new XmlSerializer(typeof(aimagCityModel));
                        XNamespace nsS = "http://schemas.xmlsoap.org/soap/envelope/";
                        XNamespace ns2 = "http://citizen.xyp.gov.mn/";
                        XElement xaimagCity = XElement.Parse(aimagCityResp);
                        var soapResponseXml = xaimagCity.Element(nsS + "Body").Element(ns2 + "WS100111_aimagCityInfoResponse").Element("return");
                        string orgAimagCityData = soapResponseXml.ToString().Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xsi:type=\"ns2:aimagCityInfoData\"", "");
                        using (TextReader sertext = new StringReader(orgAimagCityData))
                        {
                            aimagCityModel aimagCity = (aimagCityModel)xmlser.Deserialize(sertext);
                            if (aimagCity.ResultCode == "0")
                            {
                                foreach (var item in aimagCity.resp.listData)
                                {
                                    string resAimag = dbconn.iDBCommand(sqlText.InsertAimag(item.AimagCityCode, item.AimagCityName));
                                    LogWriter._error(TAG, string.Format("AimagName: [{0}], Result: [{1}]", item.AimagCityName, resAimag));
                                    XmlSerializer xmlserSum = new XmlSerializer(typeof(sumDistrictModel));
                                    string req2 = string.Format(@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:cit='http://citizen.xyp.gov.mn/'><soapenv:Header/><soapenv:Body><cit:WS100112_soumDistrictInfo><request><aimagCityCode>{0}</aimagCityCode></request></cit:WS100112_soumDistrictInfo></soapenv:Body></soapenv:Envelope>", item.AimagCityCode);
                                    string sumDistrictResp = string.Empty;
                                    if(Poster.HttpPoster(req2, out sumDistrictResp))
                                    {
                                        XElement xSumDistrict = XElement.Parse(sumDistrictResp);
                                        var soapSumXml = xSumDistrict.Element(nsS + "Body").Element(ns2 + "WS100112_soumDistrictInfoResponse").Element("return");
                                        string orgSumDisrtictData = soapSumXml.ToString().Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xsi:type=\"ns2:soumDistrictInfoData\"", "");
                                        using (TextReader sumserText = new StringReader(orgSumDisrtictData))
                                        {
                                            sumDistrictModel sumDistrict = (sumDistrictModel)xmlserSum.Deserialize(sumserText);
                                            if(sumDistrict.ResultCode == "0")
                                            {
                                                foreach(var sumItem in sumDistrict.resp.listData)
                                                {
                                                    string resSum = dbconn.iDBCommand(sqlText.InsertSum(sumItem.soumDistrictCode, sumItem.soumDistrictName, item.AimagCityCode));
                                                    LogWriter._error(TAG, string.Format("SumName: [{0}], Result: [{1}]", sumItem.soumDistrictName, resSum));
                                                    XmlSerializer xmlserBag = new XmlSerializer(typeof(bagKhorooModel));
                                                    // to do Bag
                                                    string req3 = string.Format(@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:cit='http://citizen.xyp.gov.mn/'><soapenv:Header/><soapenv:Body><cit:WS100113_bagKhorooInfo><request><aimagCityCode>{0}</aimagCityCode><soumDistrictCode>{1}</soumDistrictCode></request></cit:WS100113_bagKhorooInfo></soapenv:Body></soapenv:Envelope>", item.AimagCityCode, sumItem.soumDistrictCode);
                                                    string bagKhorooResp = string.Empty;
                                                    if(Poster.HttpPoster(req3, out bagKhorooResp))
                                                    {
                                                        XElement xBagKhoroo = XElement.Parse(bagKhorooResp);
                                                        var soapBagXml = xBagKhoroo.Element(nsS + "Body").Element(ns2 + "WS100113_bagKhorooInfoResponse").Element("return");
                                                        string orgBagKhorooData = soapBagXml.ToString().Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xsi:type=\"ns2:bagKhorooInfoData\"", "");
                                                        using (TextReader bagserText = new StringReader(orgBagKhorooData))
                                                        {
                                                            bagKhorooModel bagKhoroo = (bagKhorooModel)xmlserBag.Deserialize(bagserText);
                                                            if(bagKhoroo.ResultCode == "0")
                                                            {
                                                                foreach(var bagItem in bagKhoroo.resp.listData)
                                                                {
                                                                    string resBag = dbconn.iDBCommand(sqlText.InsertBag(bagItem.bagKhorooCode, bagItem.bagKhorooName, item.AimagCityCode, sumItem.soumDistrictCode));
                                                                    LogWriter._error(TAG, string.Format("BagName: [{0}], Result: [{1}]", bagItem.bagKhorooName, resBag));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                LogWriter._error(TAG, string.Format("Алдаа гарлаа. ResultCode: [{0}], ResultMessage: [{1}]", bagKhoroo.ResultCode, bagKhoroo.ResultMessage));
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Баг хорооны мэдээлэл задлахад алдаа гарлаа.");
                                                        LogWriter._error(TAG, "Баг хорооны мэдээлэл задлахад алдаа гарлаа.");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                LogWriter._error(TAG, string.Format("Алдаа гарлаа. ResultCode: [{0}], ResultMessage: [{1}]", sumDistrict.ResultCode, sumDistrict.ResultMessage));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Сум дүүргийн мэдээлэл задлахад алдаа гарлаа.");
                                        LogWriter._error(TAG, "Сум дүүргийн мэдээлэл задлахад алдаа гарлаа.");
                                    }
                                }
                            }
                            else
                            {
                                LogWriter._error(TAG, string.Format("Алдаа гарлаа. ResultCode: [{0}], ResultMessage: [{1}]", aimagCity.ResultCode, aimagCity.ResultMessage));
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Аймгийн мэдээлэл задлахад алдаа гарлаа.");
                        LogWriter._error(TAG, "Аймгийн мэдээлэл задлахад алдаа гарлаа.");
                    }
                }
                else
                {
                    LogWriter._error(TAG, dbres);
                }

            }
            catch(Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
    }
}
