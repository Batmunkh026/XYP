using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XYP.Utils;
using XYP.Models;
using System.Web.Script.Serialization;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

namespace XYP.Controllers
{
    public class getCustomerInfoController : ApiController
    {
        private string TAG = "getCustomerInfo";
        JavaScriptSerializer serialzer = new JavaScriptSerializer();
        public string Get()
        {
            string message = string.Empty ;
            string req = "";
            if (httpPoster.Poster(req, out message))
            {
                LogWriter._error(TAG, "successful execution");
            }
            else
            {
                message = "failed";
            }

            return message;
        }
        public HttpResponseMessage Post([FromBody] jsonOperatorRequest injson)
        {
            HttpResponseMessage message = null;
            jsonCustomerInfoResponse response = new jsonCustomerInfoResponse();
            try
            {
                string idcardinfo = string.Format(constantValue.REQUESTXML, constantValue.IDCARDWS, injson.customerFingerPrint, injson.customerRegNo, injson.operatorFingerPrint, injson.operatorRegNo);
                string addressinfo = string.Format(constantValue.REQUESTXML, constantValue.ADDRESSWS, injson.customerFingerPrint, injson.customerRegNo, injson.operatorFingerPrint, injson.operatorRegNo);
                string idcardresponse = string.Empty;
                string addressresponse = string.Empty;
                bool _workingStatus = false;
                string __workingStatusMessage = string.Empty;
                if(httpPoster.Poster(idcardinfo, out idcardresponse))
                {
                    if(httpPoster.Poster(addressinfo, out addressresponse))
                    {
                        LogWriter._xyp(TAG, string.Format("WS: [{0}], OPERATOR:[{1}], RESPONSE: [{2}], REQUEST:[{3}]", constantValue.IDCARDWS, injson.loginName, idcardresponse, idcardinfo));
                        LogWriter._xyp(TAG, string.Format("WS: [{0}], OPERATOR:[{1}], RESPONSE: [{2}], REQUEST:[{3}]", constantValue.ADDRESSWS, injson.loginName, addressresponse, addressinfo));
                        XmlSerializer xmlserIDCARD = new XmlSerializer(typeof(IDCardInfo));
                        XmlSerializer xmlserADDR = new XmlSerializer(typeof(AddressInfo));
                        XNamespace nsS = "http://schemas.xmlsoap.org/soap/envelope/";
                        XNamespace ns2 = "http://citizen.xyp.gov.mn/";
                        
                        #region Parse IDCARDINFO
                        XElement xIDCARDresponse = XElement.Parse(idcardresponse);
                        var soapResponseXml = xIDCARDresponse.Element(nsS + "Body").Element(ns2 + "WS100101_getCitizenIDCardInfoResponse").Element("return");
                        string orgIDCARDres = soapResponseXml.ToString().Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xsi:type=\"ns2:citizenRequestData\"", "").Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xsi:type=\"ns2:citizenData\"", "");//.Replace(" ","");
                        //LogWriter._error(TAG, orgIDCARDres);
                        using (TextReader sertext = new StringReader(orgIDCARDres))
                        {
                            IDCardInfo idcard = (IDCardInfo)xmlserIDCARD.Deserialize(sertext);
                            try
                            {
                                if(idcard.ResultCode == "0")
                                {
                                    response.aimagCityCode = idcard.resp.AimagCityCode;
                                    response.aimagCityName = idcard.resp.AimagCityName;
                                    response.bagKhorooCode = idcard.resp.BagKhorooCode;
                                    response.bagKhorooName = idcard.resp.BagKhorooName;
                                    response.birthDateAsText = idcard.resp.BirthDateAsText;
                                    response.birthPlace = idcard.resp.BirthPlace;
                                    response.civilId = idcard.resp.CivilId;
                                    response.firstName = idcard.resp.Firstname;
                                    response.gender = idcard.resp.Gender;
                                    response.lastName = idcard.resp.Lastname;
                                    response.nationality = idcard.resp.Nationality;
                                    response.image = idcard.resp.Image;
                                    response.passportAddress = idcard.resp.PassportAddress;
                                    response.passportExpireDate = idcard.resp.PassportExpireDate;
                                    response.passportIssueDate = idcard.resp.PassportIssueDate;
                                    response.personId = idcard.resp.PersonId;
                                    response.regnum = idcard.resp.Regnum;
                                    response.soumDistrictCode = idcard.resp.SoumDistrictCode;
                                    response.soumDistrictName = idcard.resp.SoumDistrictName;
                                    response.surname = idcard.resp.Surname;
                                    _workingStatus = true;
                                    __workingStatusMessage = "success";
                                }
                                else
                                {
                                    _workingStatus = false;
                                    __workingStatusMessage = string.Format("[XYP] CODE: [{0}], MESSAGE: [{1}]", idcard.ResultCode, idcard.ResultMessage);
                                }
                            }
                            catch(Exception ex)
                            {
                                exceptionManager.ManageException(ex, TAG);
                                _workingStatus = false;
                                __workingStatusMessage = "Иргэний үнэмлэхний мэдээлэл задлахад алдаа гарлаа. Систем админд хандана уу.";
                            }
                        }
                        #endregion
                        #region Parse ADDRESSINFO
                        XElement xADDRESSresponse = XElement.Parse(addressresponse);
                        var soapaddrResponseXml = xADDRESSresponse.Element(nsS + "Body").Element(ns2 + "WS100103_getCitizenAddressInfoResponse").Element("return");
                        string orgADDRres = soapaddrResponseXml.ToString().Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xsi:type=\"ns2:citizenRequestData\"", "").Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("xsi:type=\"ns2:citizenAddressData\"", "");//.Replace(" ", "");
                        //LogWriter._error(TAG, orgADDRres);

                        using (TextReader seraddrtext = new StringReader(orgADDRres))
                        {
                            try
                            {
                                AddressInfo addrInfo = (AddressInfo)xmlserADDR.Deserialize(seraddrtext);
                                if(_workingStatus)
                                {
                                    if (addrInfo.ResultCode == "0")
                                    {
                                        CustomerAddressInfo cusAddrinfo = new CustomerAddressInfo();
                                        cusAddrinfo.addressApartmentName = addrInfo.resp.AddressApartmentName;
                                        cusAddrinfo.addressDetail = addrInfo.resp.AddressDetail;
                                        cusAddrinfo.addressRegionName = addrInfo.resp.AddressRegionName;
                                        cusAddrinfo.aimagCityCode = addrInfo.resp.AimagCityCode;
                                        cusAddrinfo.aimagCityName = addrInfo.resp.AimagCityName;
                                        cusAddrinfo.bagKhorooCode = addrInfo.resp.BagKhorooCode;
                                        cusAddrinfo.bagKhorooName = addrInfo.resp.BagKhorooName;
                                        cusAddrinfo.fullAddress = addrInfo.resp.FullAddress;
                                        cusAddrinfo.soumDistrictCode = addrInfo.resp.SoumDistrictCode;
                                        cusAddrinfo.soumDistrictName = addrInfo.resp.SoumDistrictName;
                                        response.customerAddressInfo = cusAddrinfo;
                                        _workingStatus = true;
                                        __workingStatusMessage = "success";
                                    }
                                    else
                                    {
                                        _workingStatus = false;
                                        __workingStatusMessage = string.Format("[XYP] CODE: [{0}], MESSAGE: [{1}]", addrInfo.ResultCode, addrInfo.ResultMessage);
                                    }
                                }
                            }
                            catch(Exception EX)
                            {
                                exceptionManager.ManageException(EX, TAG);
                                _workingStatus = false;
                                __workingStatusMessage = "Иргэний хаягийн мэдээлэл задлахад алдаа гарлаа. Систем админд хандана уу.";
                            }

                        }
                        #endregion
                        if(_workingStatus)
                        {
                            response.isSuccess = true;
                            response.resultMessage = "success";
                        }
                        else
                        {
                            response.isSuccess = false;
                            response.resultMessage = __workingStatusMessage;
                        }

                    }
                    else
                    {
                        response.isSuccess = false;
                        response.resultMessage = "ХУР системээс иргэний хаягийн мэдээлэл авахад алдаа гарлаа. Систем админд хандана уу.";
                    }
                }
                else
                {
                    response.isSuccess = false;
                    response.resultMessage = "ХУР системээс Иргэний үнэмлэхний мэдээлэл авахад алдаа гарлаа. Систем админд хандана уу.";
                }
            }
            catch(Exception ex)
            {
                exceptionManager.ManageException(ex, TAG);
                response.isSuccess = false;
                response.resultMessage = ex.Message;
            }
            message = Request.CreateResponse(HttpStatusCode.OK, response);
            LogWriter._service(TAG, string.Format("IP: [{0}], REQUEST: [{1}], RESPONSE: [{2}]", httpPoster.GetClientIPAddress(HttpContext.Current.Request), serialzer.Serialize(injson), serialzer.Serialize(response)));
            return message;
        }
    }
}
