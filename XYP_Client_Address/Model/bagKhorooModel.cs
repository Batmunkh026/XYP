using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XYP_Client_Address.Model
{
    [XmlRoot("return")]
    public class bagKhorooModel
    {
        [XmlElement("requestId")]
        public virtual string RequestId { get; set; }
        [XmlElement("response")]
        public bagKhorooResponse resp { get; set; }
        [XmlElement("resultCode")]
        public virtual string ResultCode { get; set; }
        [XmlElement("resultMessage")]
        public virtual string ResultMessage { get; set; }
    }
    [XmlRoot("response")]
    public class bagKhorooResponse
    {
        [XmlElement("listData")]
        public virtual List<bagKhorooData> listData { get; set; }
    }
    [XmlRoot("listData")]
    public class bagKhorooData
    {
        [XmlElement("bagKhorooCode")]
        public virtual string bagKhorooCode { get; set; }
        [XmlElement("bagKhorooName")]
        public virtual string bagKhorooName { get; set; }
    }
}
