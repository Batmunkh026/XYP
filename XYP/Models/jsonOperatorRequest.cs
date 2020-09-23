using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYP.Models
{
    public class jsonOperatorRequest
    {
        public virtual string customerRegNo { get; set; }
        public virtual string customerFingerPrint { get; set; }
        public virtual string operatorRegNo { get; set; }
        public virtual string operatorFingerPrint { get; set; }
        public virtual string loginName { get; set; }
    }
    public class jsonCustomerInfoResponse
    {
        public virtual bool isSuccess { get; set; }
        public virtual string resultMessage { get; set; }
        public virtual string firstName { get; set; }
        public virtual string lastName { get; set; }
        public virtual string gender { get; set; }
        public virtual string nationality { get; set; }
        public virtual string aimagCityCode { get; set; }
        public virtual string aimagCityName { get; set; }
        public virtual string bagKhorooCode { get; set; }
        public virtual string bagKhorooName { get; set; }
        public virtual string birthDateAsText { get; set; }
        public virtual string birthPlace { get; set; }
        public virtual string civilId { get; set; }
        public virtual string image { get; set; }
        public virtual string passportAddress { get; set; }
        public virtual string passportExpireDate { get; set; }
        public virtual string passportIssueDate { get; set; }
        public virtual string personId { get; set; }
        public virtual string regnum { get; set; }
        public virtual string soumDistrictCode { get; set; }
        public virtual string soumDistrictName { get; set; }
        public virtual string surname { get; set; }
        public CustomerAddressInfo customerAddressInfo { get; set; }
    }
    public class CustomerAddressInfo
    {
        public virtual string addressApartmentName { get; set; }
        public virtual string addressDetail { get; set; }
        public virtual string addressRegionName { get; set; }
        public virtual string aimagCityCode { get; set; }
        public virtual string aimagCityName { get; set; }
        public virtual string bagKhorooCode { get; set; }
        public virtual string bagKhorooName { get; set; }
        public virtual string fullAddress { get; set; }
        public virtual string soumDistrictCode { get; set; }
        public virtual string soumDistrictName { get; set; }
    }
}