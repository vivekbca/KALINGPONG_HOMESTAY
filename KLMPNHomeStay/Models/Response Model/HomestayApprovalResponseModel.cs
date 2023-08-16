using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Models.Response_Model
{
    public class HomestayApprovalResponseModel
    {
        public string hsId { get; set; }
        public string hsName { get; set; }
        public string address { get; set; }
        public string pincode { get; set; }
        public string villId { get; set; }
        public string villName { get; set; }
        public string blockId { get; set; }
        public string blockName { get; set; }
        public string districtId { get; set; }
        public string districtName { get; set; }
    }

    public class HSDetailResponseModel
    {
        public string HsId { get; set; }
        public string hsName { get; set; }
        public string homeStayDesc { get; set; }
        public string localAttraction { get; set; }
        public string hwtReach { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string txtPinCode { get; set; }
        public string destinationId { get; set; }
        public string addonService { get; set; }
        public string ddlVillageCat { get; set; }
        public string ddlVillageId { get; set; }
        public string ddlBlock { get; set; }
        public string ddlDistrict { get; set; }
        public string ddlState { get; set; }
        public string GuCountry { get; set; }
        public string txtContactPerson { get; set; }
        public string txtContactNo1 { get; set; }
        public string txtContactNo2 { get; set; }
        public string txtEmailId { get; set; }
        public int HsNoOfRooms { get; set; }
        public string HsBankName { get; set; }
        public string HsBankBranch { get; set; }
        public int HsAccountNo { get; set; }
        public string HsAccountType { get; set; }
        public string HsIfsc { get; set; }
        public string HsMicr { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public short IsActive { get; set; }
        public DateTime ActiveSince { get; set; }
        public string DeactivatedBy { get; set; }
        public DateTime? DeactivatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string UserName { get; set; }
    }
}
