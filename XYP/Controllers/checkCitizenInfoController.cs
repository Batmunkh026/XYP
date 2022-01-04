using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XYP.Models;
using XYP.Utils;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data;

namespace XYP.Controllers
{
    public class checkCitizenInfoController : ApiController
    {
        string TAG = "checkCitizenInfo";
        JavaScriptSerializer serialzer = new JavaScriptSerializer();
        DBConnection dbconn = new DBConnection();
        public HttpResponseMessage Post([FromBody] jsonCheckCitizen injson)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            responseCheckCitizen response = new responseCheckCitizen();
            LogWriter._service(TAG, string.Format(@"[>>] Request: [{0}]", serialzer.Serialize(injson)));
            try
            {
                string isNotWorkXyp = ConfigurationManager.AppSettings["notXyp"];
                if (isNotWorkXyp == "Y")
                {
                    response.isSuccess = true;
                    response.resultMessage = "XYP is not work";
                    response.matched = true;
                }
                else
                {
                    if (dbconn.idbStatOK())
                    {
                        DataTable dt = dbconn.getTable(xypQry.checkReq(injson.customerFirstName, injson.customerLastName, injson.customerRegNo));
                        if(dt.Rows.Count != 0)
                        {
                            response.isSuccess = true;
                            response.resultMessage = "success";
                            response.matched = dt.Rows[0]["IS_MATCHED"].ToString() == "True" ? true : false;
                        }
                        else
                        {
                            XmlSerializer xmlser = new XmlSerializer(typeof(CitizenInfo));
                            string request = string.Format("<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:cit='http://citizen.xyp.gov.mn/'><soapenv:Header/><soapenv:Body><cit:WS100107_checkCitizenInfo><request><firstName>{0}</firstName><lastName>{1}</lastName><regnum>{2}</regnum></request></cit:WS100107_checkCitizenInfo></soapenv:Body></soapenv:Envelope>", injson.customerFirstName, injson.customerLastName, injson.customerRegNo);
                            string responseCheckCitizen = string.Empty;
                            if (httpPoster.Poster(request, out responseCheckCitizen))
                            {
                                LogWriter._xypCheckCitizen(TAG, string.Format("WS: [{0}], OPERATOR:[{1}], RESPONSE: [{2}], REQUEST:[{3}]", constantValue.CHECKCITIZENINFO, injson.loginName, responseCheckCitizen, request));
                                XNamespace nsS = "http://schemas.xmlsoap.org/soap/envelope/";
                                XNamespace ns2 = "http://citizen.xyp.gov.mn/";
                                XElement xcitizenInfo = XElement.Parse(responseCheckCitizen);
                                var soapResponseXml = xcitizenInfo.Element(nsS + "Body").Element(ns2 + "WS100107_checkCitizenInfoResponse").Element("return");
                                string orgCheckCitizen = soapResponseXml.ToString().Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xsi:type=\"ns2:checkCitizenInfoRequestData\"", "").Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xsi:type=\"ns2:checkCitizenData\"", "");
                                using (TextReader sertext = new StringReader(orgCheckCitizen))
                                {
                                    CitizenInfo idcard = (CitizenInfo)xmlser.Deserialize(sertext);
                                    try
                                    {
                                        bool isSave = false;
                                        switch (idcard.ResultCode)
                                        {
                                            case "0":
                                                isSave = true;
                                                break;
                                            case "1":
                                                isSave = true;
                                                break;
                                            default:
                                                isSave = false;
                                                break;
                                        }
                                        if (isSave)
                                        {
                                            response.isSuccess = true;
                                            response.resultMessage = "success";
                                            response.matched = idcard.citizenResponse.matched;
                                            bool dbres = dbconn.idbCommand(xypQry.setReq(injson.customerFirstName, injson.customerLastName, injson.customerRegNo, idcard.citizenResponse.matched.ToString(), idcard.ResultCode));
                                            LogWriter._service(TAG, $"[save] RESULT_CODE: [{idcard.ResultCode}] SET RESULT: [{dbres}]");
                                        }
                                        else
                                        {
                                            response.isSuccess = false;
                                            response.resultMessage = string.Format("[XYP] CODE: [{0}], MESSAGE: [{1}]", idcard.ResultCode, idcard.ResultMessage);
                                            LogWriter._service(TAG, $"[nosave] RESULT_CODE: [{idcard.ResultCode}]");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        exceptionManager.ManageException(ex, TAG);
                                        response.isSuccess = false;
                                        response.resultMessage = "Иргэний мэдээлэл тулгахад алдаа гарлаа. Та систем админд хандана уу.";
                                    }
                                }
                            }
                            else
                            {
                                response.isSuccess = false;
                                response.resultMessage = "Иргэний мэдээлэл тулгахад алдаа гарлаа. Та систем админд хандана уу.";
                            }
                        }
                    }
                    else
                    {
                        response.isSuccess = false;
                        response.resultMessage = "Internal Error: [DB Connection failure]";
                        response.matched = false;
                    }
                }

            }
            catch(Exception ex)
            {
                exceptionManager.ManageException(ex, TAG);
                response.isSuccess = false;
                response.resultMessage = ex.Message;

            }
            message = Request.CreateResponse(HttpStatusCode.OK, response);
            LogWriter._service(TAG, string.Format("[<<] IP: [{0}], RESPONSE: [{1}]", httpPoster.GetClientIPAddress(HttpContext.Current.Request), serialzer.Serialize(response)));
            return message;
        }
    }
}
