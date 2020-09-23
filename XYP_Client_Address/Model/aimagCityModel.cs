using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XYP_Client_Address.Model
{
    [XmlRoot("return")]
    public class aimagCityModel
    {
        [XmlElement("requestId")]
        public virtual string RequestId { get; set; }
        [XmlElement("response")]
        public aimagCityResponse resp { get; set; }
        [XmlElement("resultCode")]
        public virtual string ResultCode { get; set; }
        [XmlElement("resultMessage")]
        public virtual string ResultMessage { get; set; }

    }
    [XmlRoot("response")]
    public class aimagCityResponse
    {
        [XmlElement("listData")]
        public virtual List<aimagCityData> listData { get; set; }
    }
    [XmlRoot("listData")]
    public class aimagCityData
    {
        [XmlElement("aimagCityCode")]
        public virtual string AimagCityCode { get; set; }
        [XmlElement("aimagCityName")]
        public virtual string AimagCityName { get; set; }
    }
}
