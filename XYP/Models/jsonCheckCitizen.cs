using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYP.Models
{
    public class jsonCheckCitizen
    {
        public virtual string customerFirstName { get; set; }
        public virtual string customerLastName { get; set; }
        public virtual string customerRegNo { get; set; }
        public virtual string loginName { get; set; }
    }
    public class responseCheckCitizen
    {
        public virtual bool isSuccess { get; set; }
        public virtual string resultMessage { get; set; }
        public virtual bool matched { get; set; }
    }
}