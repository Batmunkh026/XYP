using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace XYP.Models
{
    [XmlRoot("return")]
    public class CitizenInfo
    {
        [XmlElement("request")]
        public CitizenRequest citizenRequest { get; set; }
        [XmlElement("requestId")]
        public virtual string RequestId { get; set; }
        [XmlElement("response")]
        public CitizenResponse citizenResponse { get; set; }
        [XmlElement("resultCode")]
        public virtual string ResultCode { get; set; }
        [XmlElement("resultMessage")]
        public virtual string ResultMessage { get; set; }
    }
    [XmlRoot("request")]
    public class CitizenRequest
    {
        [XmlElement("firstName")]
        public virtual string firstName { get; set; }
        [XmlElement("lastName")]
        public virtual string lastName { get; set; }
        [XmlElement("regnum")]
        public virtual string regnum { get; set; }
    }
    [XmlRoot("response")]
    public class CitizenResponse
    {
        [XmlElement("matched")]
        public virtual bool matched { get; set; }
    }
}