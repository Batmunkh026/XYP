using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace XYP.Models
{
    [Serializable()]
    [XmlRoot("return")]
    public class AddressInfo
    {
        [XmlElement("request")]
        public Request reqs { get; set; }
        [XmlElement("requestId")]
        public virtual string RequestId { get; set; }
        [XmlElement("response")]
        public Response_AddressInfo resp { get; set; }
        [XmlElement("resultCode")]
        public virtual string ResultCode { get; set; }
        [XmlElement("resultMessage")]
        public virtual string ResultMessage { get; set; }
    }

    [XmlRoot("response")]
    public class Response_AddressInfo
    {
        [XmlElement("addressApartmentName")]
        public virtual string AddressApartmentName { get; set; }
        [XmlElement("addressDetail")]
        public virtual string AddressDetail { get; set; }
        [XmlElement("addressRegionName")]
        public virtual string AddressRegionName { get; set; }
        [XmlElement("addressStreetName")]
        public virtual string AddressStreetName { get; set; }
        [XmlElement("aimagCityCode")]
        public virtual string AimagCityCode { get; set; }
        [XmlElement("aimagCityName")]
        public virtual string AimagCityName { get; set; }
        [XmlElement("bagKhorooCode")]
        public virtual string BagKhorooCode { get; set; }
        [XmlElement("bagKhorooName")]
        public virtual string BagKhorooName { get; set; }
        [XmlElement("firstname")]
        public virtual string Firstname { get; set; }
        [XmlElement("fullAddress")]
        public virtual string FullAddress { get; set; }
        [XmlElement("lastname")]
        public virtual string Lastname { get; set; }
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