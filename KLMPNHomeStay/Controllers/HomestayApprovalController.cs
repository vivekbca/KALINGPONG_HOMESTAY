using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using KLMPNHomeStay.Models.Request_Model;
using KLMPNHomeStay.Models.Response_Model;
using KLMPNHomeStay.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomestayApprovalController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        private readonly IEmailService _emailService;
        public HomestayApprovalController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService, IEmailService emailService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
            _emailService = emailService;
        }
        [HttpGet("hsApprovalList")]
        public async Task<IActionResult> HsApprovalList()
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                List<HomestayApprovalResponseModel> homestayApprovalResponseModels = new List<HomestayApprovalResponseModel>();
                var pendingHsList = await _context.TmHomestay.Where(m => m.ApprovedBy == null && m.IsActive == 1).OrderByDescending(m => m.CreatedOn).Include(m => m.HsBlock).Include(m => m.HsVill).Include(m => m.HsDistrict).ToListAsync();

                if(pendingHsList.Count > 0)
                {
                    foreach(var item in pendingHsList)
                    {
                        HomestayApprovalResponseModel homestayApprovalResponseModel = new HomestayApprovalResponseModel();
                        homestayApprovalResponseModel.hsId = item.HsId;
                        homestayApprovalResponseModel.hsName = item.HsName;
                        homestayApprovalResponseModel.villId = item.HsVillId;
                        homestayApprovalResponseModel.villName = item.HsVill.VillName;
                        homestayApprovalResponseModel.blockId = item.HsBlockId;
                        homestayApprovalResponseModel.blockName = item.HsBlock.BlockName;
                        homestayApprovalResponseModel.districtId = item.HsDistrictId;
                        homestayApprovalResponseModel.districtName = item.HsDistrict.DistrictName;
                        homestayApprovalResponseModel.pincode = item.Pincode;
                        homestayApprovalResponseModel.address = item.HsAddress1;
                        homestayApprovalResponseModels.Add(homestayApprovalResponseModel);
                    }
                }
                apiResponse.Data = homestayApprovalResponseModels;
                apiResponse.Msg = "Displaying Pending Homestays";
                apiResponse.Result = ResponseTypes.Success;

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("approveHS")]
        public async Task<IActionResult> ApproveRejectHS(HSApproveRjectRequestModel hSApproveRjectRequest)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                if(hSApproveRjectRequest.approvalType == "A")
                {
                    var hsDet = await _context.TmHomestay.Where(m => m.HsId == hSApproveRjectRequest.hsId).FirstOrDefaultAsync();
                    hsDet.ApprovedBy = hSApproveRjectRequest.userId;
                    hsDet.ApprovedOn = DateTime.Now;
                    _context.TmHomestay.Update(hsDet);
                    await _context.SaveChangesAsync();
                    var HomestayUserId = await _context.TmHomestay.Where(m => m.HsId == hSApproveRjectRequest.hsId).FirstOrDefaultAsync();
                    var HomestayCredetialDet = await _context.TmUser.Where(m => m.UserId == HomestayUserId.UserId).FirstOrDefaultAsync();
                    await SendApprovalEmail(HomestayCredetialDet.UserEmailId, HomestayCredetialDet.UserPassword, HomestayUserId.HsName);
                    apiResponse.Msg = "Homestay Approved";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else if (hSApproveRjectRequest.approvalType == "R")
                {
                    var hsDet = await _context.TmHomestay.Where(m => m.HsId == hSApproveRjectRequest.hsId).FirstOrDefaultAsync();
                    hsDet.DeactivatedBy = hSApproveRjectRequest.userId;
                    hsDet.DeactivatedOn = DateTime.Now;
                    hsDet.IsActive = 0;
                    _context.TmHomestay.Update(hsDet);
                    await _context.SaveChangesAsync();
                    var HomestayUserId = await _context.TmHomestay.Where(m => m.HsId == hSApproveRjectRequest.hsId).FirstOrDefaultAsync();
                    var HomestayCredetialDet = await _context.TmUser.Where(m => m.UserId == HomestayUserId.UserId).FirstOrDefaultAsync();
                    await SendRejectionEmail(HomestayCredetialDet.UserEmailId, HomestayUserId.HsName);
                    apiResponse.Msg = "Homestay Rejected";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    apiResponse.Msg = "Approval type not matched";
                    apiResponse.Result = ResponseTypes.Error;
                }
               

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [NonAction]
        private async Task SendApprovalEmail(string Email, string password, string HsName)
        {
            try
            {
                var path = Path.Combine(_env.ContentRootPath, "Template/Homestay-Approval.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Password>>",password)
                        .Replace("<<Email>>",Email)
                        .Replace("<<HSName>>", HsName);
                //send password to email asynchronously
                await _emailService.Send(Email, HsName, "Welcome - New User", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [NonAction]
        private async Task SendRejectionEmail(string Email, string HsName)
        {
            try
            {
                var path = Path.Combine(_env.ContentRootPath, "Template/Homestay-Approval.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<HSName>>", HsName);
                //send password to email asynchronously
                await _emailService.Send(Email, HsName, "Welcome - New User", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("hsListById/{id}")]
        public async Task<IActionResult> HsListById(string id)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                List<HSDetailResponseModel> hSDetailResponseModelList = new List<HSDetailResponseModel>();
                var pendingHsList = await _context.TmHomestay.Where(m => m.HsId==id).OrderByDescending(m => m.CreatedOn).Include(m => m.HsBlock).Include(m => m.HsVill).Include(m => m.HsDistrict).Include(m=>m.User).ToListAsync();

                if (pendingHsList.Count > 0)
                {
                    foreach (var item in pendingHsList)
                    {
                        HSDetailResponseModel hSDetailResponseModel = new HSDetailResponseModel();
                        hSDetailResponseModel.HsId = item.HsId;
                        hSDetailResponseModel.hsName = item.HsName;
                        hSDetailResponseModel.homeStayDesc = item.HomestayDescription;
                        hSDetailResponseModel.localAttraction = item.LocalAttraction;
                        hSDetailResponseModel.hwtReach = item.HwtReach;
                        hSDetailResponseModel.address1 = item.HsAddress1;
                        hSDetailResponseModel.address2 = item.HsAddress2;
                        hSDetailResponseModel.address3 = item.HsAddress3;
                        hSDetailResponseModel.txtPinCode = item.Pincode;
                        hSDetailResponseModel.destinationId = item.DestinationId;
                        hSDetailResponseModel.addonService = item.AddonServices;
                        hSDetailResponseModel.ddlVillageCat = item.VillageCategoryId;
                        hSDetailResponseModel.ddlVillageId = item.HsVillId;
                        hSDetailResponseModel.ddlBlock = item.HsBlockId;
                        hSDetailResponseModel.ddlDistrict = item.HsDistrictId;
                        hSDetailResponseModel.ddlState = item.HsStateId;
                        hSDetailResponseModel.GuCountry = item.HsCountryId;
                        hSDetailResponseModel.txtContactPerson = item.HsContactName;
                        hSDetailResponseModel.txtContactNo1 = item.HsContactMob1;
                        hSDetailResponseModel.txtContactNo2 = item.HsContactMob2;
                        hSDetailResponseModel.txtEmailId = item.HsContactEmail;
                        hSDetailResponseModel.HsNoOfRooms = item.HsNoOfRooms;
                        hSDetailResponseModel.HsBankName = item.HsBankName;
                        hSDetailResponseModel.HsBankBranch = item.HsBankBranch;
                        hSDetailResponseModel.HsAccountNo = item.HsAccountNo;
                        hSDetailResponseModel.HsAccountType = item.HsAccountType;
                        hSDetailResponseModel.HsIfsc = item.HsIfsc;
                        hSDetailResponseModel.HsMicr = item.HsMicr;
                        hSDetailResponseModel.IsActive = item.IsActive;
                        hSDetailResponseModel.ActiveSince = item.ActiveSince;
                        hSDetailResponseModel.UserName = item.User.UserName;

                        hSDetailResponseModelList.Add(hSDetailResponseModel);
                    }
                }
                apiResponse.Data = hSDetailResponseModelList;
                apiResponse.Msg = "Displaying Homestays List";
                apiResponse.Result = ResponseTypes.Success;

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
