using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XYP_Client_Address.Model
{
    [XmlRoot("return")]
    public class sumDistrictModel
    {
        [XmlElement("requestId")]
        public virtual string RequestId { get; set; }
        [XmlElement("response")]
        public sumDistrictResponse resp { get; set; }
        [XmlElement("resultCode")]
        public virtual string ResultCode { get; set; }
        [XmlElement("resultMessage")]
        public virtual string ResultMessage { get; set; }
    }
    [XmlRoot("response")]
    public class sumDistrictResponse
    {
        [XmlElement("listData")]
        public virtual List<sumDistrictData> listData { get; set; }
    }
    [XmlRoot("listData")]
    public class sumDistrictData
    {
        [XmlElement("soumDistrictCode")]
        public virtual string soumDistrictCode { get; set; }
        [XmlElement("soumDistrictName")]
        public virtual string soumDistrictName { get; set; }
    }
}
