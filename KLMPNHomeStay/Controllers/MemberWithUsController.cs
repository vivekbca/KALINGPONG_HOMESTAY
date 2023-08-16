using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using KLMPNHomeStay.Models.Request_Model;
using KLMPNHomeStay.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberWithUsController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public MemberWithUsController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet("country")]
        public async Task<IActionResult> getAllCountry()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllCountry = await _context.TmCountry.OrderBy(m => m.CountryName == "India").ToListAsync();
                apiResponse.Data = AllCountry;
                apiResponse.Msg = "List of Country";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("villCat")]
        public async Task<IActionResult> getVillCategory()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var response = await _context.TmVillageCategory.OrderBy(m => m.VillageCategoryName == "None").ToListAsync();
                apiResponse.Data = response;
                apiResponse.Msg = "List of Village Category";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }
        [HttpGet("state")]
        public async Task<IActionResult> getAllState()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllState = await _context.TmState.OrderBy(m => m.StateName == "West Bengal").ToListAsync();
                apiResponse.Data = AllState;
                apiResponse.Msg = "List of State";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("district/{id}")]
        public async Task<IActionResult> getAllDistrict(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllDistrict = await _context.TmDistrict.Where(m => m.StateId == id).ToListAsync();
                apiResponse.Data = AllDistrict;
                apiResponse.Msg = "List of District";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }
        [HttpGet("allDistrict")]
        public async Task<IActionResult> GetAllDistrictByDefault()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllDistrict = await _context.TmDistrict.OrderBy(m => m.DistrictName).ToListAsync();
                apiResponse.Data = AllDistrict;
                apiResponse.Msg = "List of District";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("block/{id}")]
        public async Task<IActionResult> getAllBlock(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllBlock = await _context.TmBlock.Where(m => m.DistrictId == id).ToListAsync();
                apiResponse.Data = AllBlock;
                apiResponse.Msg = "List of District";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }
        [HttpGet("village/{id}")]
        public async Task<IActionResult> getAllVillage(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllVillage = await _context.TmBlockVillage.Where(m => m.BlockId == id).ToListAsync();
                apiResponse.Data = AllVillage;
                apiResponse.Msg = "List of Village";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }
        [HttpPost]
        public async Task<IActionResult> MemberRegistration([FromBody] MemberWithUs memberWithUs)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if (!ModelState.IsValid)
                {
                    apiResponse.Data = ModelState;
                    apiResponse.Result = ResponseTypes.ModelErr;
                }
                else
                {
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                        //checking
                        var isUpdate = _context.TmHomestay.Where(m => m.HsId == memberWithUs.HsId).FirstOrDefault();
                        if (isUpdate != null)
                        {
                            if (memberWithUs.GuCountry == "" || memberWithUs.GuCountry == null)
                            {
                                memberWithUs.GuCountry = "f2082e3c-0193-11ec-8831-005056a4479e";
                            }
                            if (memberWithUs.ddlState == "" || memberWithUs.ddlState == null)
                            {
                                memberWithUs.ddlState = "e4eb5370-ebcf-4ff6-8e75-6eb3299918cc";
                            }
                            if (memberWithUs.HsBankName == "" || memberWithUs.HsBankName == null)
                            {
                                memberWithUs.HsBankName = "Darjeeling District Central Co-operative Bank Ltd";
                            }
                            //user id
                            var getUserId = _context.TmUser.Where(m => m.UserName == memberWithUs.UserName).Select(m => m.UserId).FirstOrDefault();

                            isUpdate.HsName = memberWithUs.hsName;
                            isUpdate.HomestayDescription = memberWithUs.homeStayDesc;
                            isUpdate.LocalAttraction = memberWithUs.localAttraction;
                            isUpdate.HwtReach = memberWithUs.hwtReach;
                            isUpdate.HsAddress1 = memberWithUs.address1;
                            isUpdate.HsAddress2 = memberWithUs.address2;
                            isUpdate.HsAddress3 = memberWithUs.address3;
                            isUpdate.AddonServices = memberWithUs.addonService;
                            isUpdate.VillageCategoryId = memberWithUs.ddlVillageCat;
                            isUpdate.HsVillId = memberWithUs.ddlVillageName;
                            isUpdate.HsBlockId = memberWithUs.ddlBlock;
                            isUpdate.HsDistrictId = memberWithUs.ddlDistrict;
                            isUpdate.HsStateId = memberWithUs.ddlState;
                            isUpdate.HsCountryId = memberWithUs.GuCountry;
                            isUpdate.HsContactName = memberWithUs.txtContactPerson;
                            isUpdate.HsContactMob1 = memberWithUs.txtContactNo1;
                            isUpdate.HsContactMob2 = memberWithUs.txtContactNo2;
                            isUpdate.HsContactEmail = memberWithUs.txtEmailId;
                            isUpdate.DestinationId = "0a7eada9-8a7b-4c8c-aead-c75840e17381";
                            isUpdate.Pincode = memberWithUs.txtPinCode;

                            isUpdate.HsNoOfRooms = memberWithUs.HsNoOfRooms;
                            isUpdate.HsBankName = memberWithUs.HsBankName;
                            isUpdate.HsBankBranch = memberWithUs.HsBankBranch;
                            isUpdate.HsAccountNo = memberWithUs.HsAccountNo;
                            isUpdate.HsAccountType = memberWithUs.HsAccountType;
                            isUpdate.HsIfsc = memberWithUs.HsIfsc;
                            isUpdate.HsMicr = memberWithUs.HsMicr;
                            isUpdate.IsActive = memberWithUs.IsActive;
                            isUpdate.ActiveSince = DateTime.Now;
                            isUpdate.UserId = getUserId;

                            _context.TmHomestay.Update(isUpdate);
                        }
                        else
                        {
                            var allEmailIds = await _context.TmHomestay.Where(m => m.HsContactEmail == memberWithUs.txtEmailId).ToListAsync();
                            if (allEmailIds.Count != 0)
                            {
                                apiResponse.Msg = "Email ID Already Exist!!";
                                ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                                return Ok(apiResponseError);
                            }
                            var checkUser = await _context.TmUser.Where(m => m.UserName == memberWithUs.UserName).ToListAsync();
                            if (checkUser.Count != 0)
                            {
                                apiResponse.Msg = "User ID Already Exist!!";
                                ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                                return Ok(apiResponseError);
                            }

                            var userId = Guid.NewGuid().ToString();
                            if (memberWithUs.GuCountry == "" || memberWithUs.GuCountry == null)
                            {
                                memberWithUs.GuCountry = "f2082e3c-0193-11ec-8831-005056a4479e";
                            }
                            if (memberWithUs.ddlState == "" || memberWithUs.ddlState == null)
                            {
                                memberWithUs.ddlState = "e4eb5370-ebcf-4ff6-8e75-6eb3299918cc";
                            }
                            if (memberWithUs.HsBankName == "" || memberWithUs.HsBankName == null)
                            {
                                memberWithUs.HsBankName = "Darjeeling District Central Co-operative Bank Ltd";
                            }

                            var userDtl = new TmUser
                            {
                                UserId = userId,
                                UserName = memberWithUs.UserName,
                                UserRoleId = "0ca44d4c-22fe-11ec-be13-005056a4479e",
                                UserDob = DateTime.MinValue,
                                UserSex = "M",
                                UserPassword = "password",
                                UserMobileNo = 123456789,
                                UserEmailId = memberWithUs.txtEmailId,
                                UserLastActivity = DateTime.Now,
                                UserIsActive = 1,
                                IsSystemDefined = 1,
                                LockoutEnabled = 1,
                                UserCreatedOn = DateTime.Now,
                                UserCreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e"
                            };
                            _context.TmUser.Add(userDtl);

                            var member = new TmHomestay
                            {
                                HsId = Guid.NewGuid().ToString(),
                                HsName = memberWithUs.hsName,
                                HomestayDescription = memberWithUs.homeStayDesc,
                                LocalAttraction = memberWithUs.localAttraction,
                                HwtReach = memberWithUs.hwtReach,
                                HsAddress1 = memberWithUs.address1,
                                HsAddress2 = memberWithUs.address2,
                                HsAddress3 = memberWithUs.address3,
                                AddonServices = memberWithUs.addonService,
                                VillageCategoryId = memberWithUs.ddlVillageCat,
                                HsVillId = memberWithUs.ddlVillageName,
                                HsBlockId = memberWithUs.ddlBlock,
                                HsDistrictId = memberWithUs.ddlDistrict,
                                HsStateId = memberWithUs.ddlState,
                                HsCountryId = memberWithUs.GuCountry,
                                HsContactName = memberWithUs.txtContactPerson,
                                HsContactMob1 = memberWithUs.txtContactNo1,
                                HsContactMob2 = memberWithUs.txtContactNo2,
                                HsContactEmail = memberWithUs.txtEmailId,
                                DestinationId = "0a7eada9-8a7b-4c8c-aead-c75840e17381",
                                Pincode = memberWithUs.txtPinCode,

                                HsNoOfRooms = 0,
                                HsBankName = memberWithUs.HsBankName,
                                HsBankBranch = memberWithUs.HsBankBranch,
                                HsAccountNo = memberWithUs.HsAccountNo,
                                HsAccountType = memberWithUs.HsAccountType,
                                HsIfsc = memberWithUs.HsIfsc,
                                HsMicr = memberWithUs.HsMicr,
                                CreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e",
                                CreatedOn = DateTime.Now,
                                IsActive = 0,
                                ActiveSince = DateTime.Now,
                                UserId = userId
                            };
                            _context.TmHomestay.Add(member);
                        }
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }
                    apiResponse.Msg = "Guest User added successfully";
                    apiResponse.Result = ResponseTypes.Success;


                }
            }
            catch (Exception ex)
            {
                apiResponse.Msg = "Error in Saving Data";
                apiResponse.Result = ResponseTypes.Error;
                apiResponse.Data = ex.ToString();
                ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseError);
                //return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("blockByDefault")]
        public async Task<IActionResult> getAllBlockByDefault()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllBlock = await _context.TmBlock.Where(m => m.IsActive==1).ToListAsync();
                apiResponse.Data = AllBlock;
                apiResponse.Msg = "List of District";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }
        [HttpGet("villageByDefault")]
        public async Task<IActionResult> getAllVillageDefault()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllVillage = await _context.TmBlockVillage.Where(m => m.IsActive == 1).ToListAsync();
                apiResponse.Data = AllVillage;
                apiResponse.Msg = "List of Village";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }
    }
}
