using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace XYP.Models
{
    [XmlRoot("return")]
    public class IDCardInfo
    {
        [XmlElement("request")]
        public Request reqs { get; set; }
        [XmlElement("requestId")]
        public virtual string RequestId { get; set; }
        [XmlElement("response")]
        public Response resp{ get; set; }
        [XmlElement("resultCode")]
        public virtual string ResultCode { get; set; }
        [XmlElement("resultMessage")]
        public virtual string ResultMessage { get; set; }
    }
    [XmlRoot("request")]
    public class Request
    {
        [XmlElement("auth")]
        public Auth authe { get; set; }
        [XmlElement("civilId")]
        public virtual string CivilId { get; set; }
        [XmlElement("regnum")]
        public virtual string  RegNum { get; set; }
    }
    [XmlRoot("auth")]
    public class Auth
    {
        [XmlElement("citizen")]
        public Citizen citi { get; set; }
        [XmlElement("operator")]
        public Operator oper { get; set; }
    }
    [XmlRoot("citizen")]
    public class Citizen
    {
        [XmlElement("fingerprint")]
        public virtual string FingerPrint { get; set; }
        [XmlElement("regnum")]
      [XmlElement("signature")]
           public virtual string RegNum { get; set; }
       public virtual string Signature { get; set; }
    }
    [XmlRoot("operator")]
    public class Operator
    {
        [XmlElement("fingerprint")]
        public virtual string FingerPrint { get; set; }
        [XmlElement("regnum")]
        public virtual string RegNum { get; set; }
        [XmlElement("signature")]
        public virtual string Signature { get; set; }
    }
    [XmlRoot("response")]
    public class Response
    {
        [XmlElement("addressApartmentName")]
        public virtual string AddressApartmentName { get; set; }
        [XmlElement("addressDetail")]
        public virtual string AddressDetail { get; set; }
        [XmlElement("addressRegionName")]
        public virtual string AddressRegionName { get; set; }
        [XmlElement("aimagCityCode")]
        public virtual string AimagCityCode { get; set; }
        [XmlElement("aimagCityName")]
        public virtual string AimagCityName { get; set; }
        [XmlElement("bagKhorooCode")]
        public virtual string BagKhorooCode { get; set; }
        [XmlElement("bagKhorooName")]
        public virtual string BagKhorooName { get; set; }
        [XmlElement("birthDateAsText")]
        public virtual string BirthDateAsText { get; set; }
        [XmlElement("birthPlace")]
        public virtual string BirthPlace { get; set; }
        [XmlElement("civilId")]
        public virtual string CivilId { get; set; }
        [XmlElement("firstname")]
        public virtual string Firstname { get; set; }
        [XmlElement("gender")]
        public virtual string Gender { get; set; }
        [XmlElement("image")]
        public virtual string Image { get; set; }
        [XmlElement("lastname")]
        public virtual string Lastname { get; set; }
        [XmlElement("nationality")]
        public virtual string Nationality { get; set; }
        [XmlElement("passportAddress")]
        public virtual string PassportAddress { get; set; }
        [XmlElement("passportExpireDate")]
        public virtual string PassportExpireDate { get; set; }
        [XmlElement("passportIssueDate")]
        public virtual string PassportIssueDate { get; set; }
        [XmlElement("personId")]
        public virtual string PersonId { get; set; }
        [XmlElement("regnum")]
        public virtual string Regnum { get; set; }
        [XmlElement("soumDistrictCode")]
        public virtual string SoumDistrictCode { get; set; }
        [XmlElement("soumDistrictName")]
        public virtual string SoumDistrictName { get; set; }
        [XmlElement("surname")]
        public virtual string Surname { get; set; }
    }
}