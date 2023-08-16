using System;
using System.Collections.Generic;
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
    public class HomeStayController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public HomeStayController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetHomeStayList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                //var homeStayList1 = await _context.TmHomestay.ToListAsync();
                var homeStayList = await (from a in _context.TmHomestay
                                          join b in _context.TmBlockVillage on a.HsVillId equals b.VillId
                                          join c in _context.TmBlock on b.BlockId equals c.BlockId
                                          join d in _context.TmDistrict on c.DistrictId equals d.DistrictId
                                          join e in _context.TmState on d.StateId equals e.StateId
                                          join f in _context.TmCountry on e.CountryId equals f.CountryId
                                          select new HomeStayResponseModel
                                          {
                                              HsId = a.HsId,
                                              HsName = a.HsName,
                                              HsAddress1 = a.HsAddress1,
                                              HsAddress2 = a.HsAddress2,
                                              HsAddress3 = a.HsAddress3,
                                              HsVillId = b.VillId,
                                              HsVillName = b.VillName,
                                              HsBlockId = c.BlockId,
                                              HsBlockName = c.BlockName,
                                              HsDistrictId = d.DistrictId,
                                              HsDistrictName = d.DistrictName,
                                              HsStateId = e.StateId,
                                              HsStateName = e.StateName,
                                              HsCountryId = f.CountryId,
                                              HsCountryName = f.CountryName,
                                              HsContactMob1 = a.HsContactMob1,
                                              HsContactMob2 = a.HsContactMob2,
                                              HsContactEmail = a.HsContactEmail,
                                              HsNoOfRooms = a.HsNoOfRooms,
                                              HsBankName = a.HsBankName,
                                              HsBankBranch = a.HsBankBranch,
                                              HsAccountNo = a.HsAccountNo,
                                              HsAccountType = a.HsAccountType,
                                              HsIfsc = a.HsIfsc,
                                              HsMicr = a.HsMicr,
                                              IsActive = a.IsActive,
                                              ActiveSince = a.ActiveSince.ToString("dd-MM-yyyy")
                                          }).ToListAsync();

                apiResponse.Data = homeStayList;
                apiResponse.Msg = "Displaying Home Stay List";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeStayById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var homeStayDet = await _context.TmHomestay.Where(m => m.HsId == id).Include(m => m.HsVill).Include(m => m.HsBlock).Include(m => m.HsDistrict).Include(m => m.HsState).Include(m => m.HsCountry).FirstOrDefaultAsync();
                if (homeStayDet == null)
                {
                    apiResponse.Msg = "Home Stay not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    HomeStayResponseModel homeStayResponseModel = new HomeStayResponseModel();
                    homeStayResponseModel.HsId = homeStayDet.HsId;
                    homeStayResponseModel.HsName = homeStayDet.HsName;
                    homeStayResponseModel.HsAddress1 = homeStayDet.HsAddress1;
                    homeStayResponseModel.HsAddress2 = homeStayDet.HsAddress2;
                    homeStayResponseModel.HsAddress3 = homeStayDet.HsAddress3;
                    homeStayResponseModel.HsVillId = homeStayDet.HsVillId;
                    homeStayResponseModel.HsVillName = homeStayDet.HsVill.VillName;
                    homeStayResponseModel.HsBlockId = homeStayDet.HsBlockId;
                    homeStayResponseModel.HsBlockName = homeStayDet.HsBlock.BlockName;
                    homeStayResponseModel.HsDistrictId = homeStayDet.HsDistrictId;
                    homeStayResponseModel.HsDistrictName = homeStayDet.HsDistrict.DistrictName;
                    homeStayResponseModel.HsStateId = homeStayDet.HsStateId;
                    homeStayResponseModel.HsStateName = homeStayDet.HsState.StateName;
                    homeStayResponseModel.HsCountryId = homeStayDet.HsCountryId;
                    homeStayResponseModel.HsCountryName = homeStayDet.HsCountry.CountryName;
                    homeStayResponseModel.Pincode = homeStayDet.Pincode;
                    homeStayResponseModel.HsContactName = homeStayDet.HsContactName;
                    homeStayResponseModel.HsContactMob1 = homeStayDet.HsContactMob1;
                    homeStayResponseModel.HsContactMob2 = homeStayDet.HsContactMob2;
                    homeStayResponseModel.HsContactEmail = homeStayDet.HsContactEmail;
                    homeStayResponseModel.HsNoOfRooms = homeStayDet.HsNoOfRooms;
                    homeStayResponseModel.HsBankName = homeStayDet.HsBankName;
                    homeStayResponseModel.HsBankBranch = homeStayDet.HsBankBranch;
                    homeStayResponseModel.HsAccountNo = homeStayDet.HsAccountNo;
                    homeStayResponseModel.HsAccountType = homeStayDet.HsAccountType;
                    homeStayResponseModel.HsIfsc = homeStayDet.HsIfsc;
                    homeStayResponseModel.HsMicr = homeStayDet.HsMicr;
                    homeStayResponseModel.IsActive = homeStayDet.IsActive;
                    homeStayResponseModel.ActiveSince = homeStayDet.ActiveSince.ToString("dd-MM-yyyy");

                    apiResponse.Data = homeStayResponseModel;
                    apiResponse.Msg = "Displaying Home Stay";
                    apiResponse.Result = ResponseTypes.Success;
                }
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateHomeStay([FromBody] HomeStayAddRequestModel homeStayAddRequest)//This method will insert the state into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };

            try
            {
                if (ModelState.IsValid)
                {
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                        var homeStay = new TmHomestay
                        {
                            HsId = Guid.NewGuid().ToString(),
                            HsName = homeStayAddRequest.HsName,
                            HsAddress1 = homeStayAddRequest.HsAddress1,
                            HsAddress2 = homeStayAddRequest.HsAddress2,
                            HsAddress3 = homeStayAddRequest.HsAddress3,
                            HsVillId = homeStayAddRequest.HsVillId,
                            HsBlockId = homeStayAddRequest.HsBlockId,
                            HsDistrictId = homeStayAddRequest.HsDistrictId,
                            HsStateId = homeStayAddRequest.HsStateId,
                            HsCountryId = homeStayAddRequest.HsCountryId,
                            HsContactName = homeStayAddRequest.HsContactName,
                            HsContactMob1 = homeStayAddRequest.HsContactMob1,
                            HsContactMob2 = homeStayAddRequest.HsContactMob2,
                            HsContactEmail = homeStayAddRequest.HsContactEmail,
                            HsNoOfRooms = homeStayAddRequest.HsNoOfRooms,
                            HsBankName = homeStayAddRequest.HsBankName,
                            HsBankBranch = homeStayAddRequest.HsBankBranch,
                            HsAccountNo = homeStayAddRequest.HsAccountNo,
                            HsAccountType = homeStayAddRequest.HsAccountType,
                            HsIfsc = homeStayAddRequest.HsIfsc,
                            HsMicr = homeStayAddRequest.HsMicr,
                            //CreatedBy = userId.ToString(),
                            CreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e",
                            CreatedOn = DateTime.Now,
                            IsActive = homeStayAddRequest.IsActive,
                            ActiveSince = DateTime.Now
                        };

                        _context.TmHomestay.Add(homeStay);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }
                    apiResponse.Msg = "Home Stay added successfully";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    apiResponse.Msg = "Model State not valid";
                    apiResponse.Result = ResponseTypes.ModelErr;
                }

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHomeStay(HomeStayAddRequestModel homeStayAddRequest)//Update method will update the state  
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var homeStayDet = await _context.TmHomestay.Where(m => m.HsId == homeStayAddRequest.HsId).FirstOrDefaultAsync();
                if (homeStayDet == null)
                {
                    apiResponse.Msg = "Home Stay not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            homeStayDet.HsName = homeStayAddRequest.HsName;
                            homeStayDet.HsAddress1 = homeStayAddRequest.HsAddress1;
                            homeStayDet.HsAddress2 = homeStayAddRequest.HsAddress2;
                            homeStayDet.HsAddress3 = homeStayAddRequest.HsAddress3;
                            homeStayDet.HsVillId = homeStayAddRequest.HsVillId;
                            homeStayDet.HsBlockId = homeStayAddRequest.HsBlockId;
                            homeStayDet.HsDistrictId = homeStayAddRequest.HsDistrictId;
                            homeStayDet.HsStateId = homeStayAddRequest.HsStateId;
                            homeStayDet.HsCountryId = homeStayAddRequest.HsCountryId;
                            homeStayDet.HsContactName = homeStayAddRequest.HsContactName;
                            homeStayDet.HsContactMob1 = homeStayAddRequest.HsContactMob1;
                            homeStayDet.HsContactMob2 = homeStayAddRequest.HsContactMob2;
                            homeStayDet.HsContactEmail = homeStayAddRequest.HsContactEmail;
                            homeStayDet.HsNoOfRooms = homeStayAddRequest.HsNoOfRooms;
                            homeStayDet.HsBankName = homeStayAddRequest.HsBankName;
                            homeStayDet.HsBankBranch = homeStayAddRequest.HsBankBranch;
                            homeStayDet.HsAccountNo = homeStayAddRequest.HsAccountNo;
                            homeStayDet.HsAccountType = homeStayAddRequest.HsAccountType;
                            homeStayDet.HsIfsc = homeStayAddRequest.HsIfsc;
                            homeStayDet.HsMicr = homeStayAddRequest.HsMicr;
                            //homeStayDet.ModifiedBy = userId.ToString(),
                            homeStayDet.ModifiedBy = "f1fb5130-0192-11ec-8831-005056a4479e";
                            homeStayDet.ModifiedOn = DateTime.Now;
                            homeStayDet.IsActive = homeStayAddRequest.IsActive;

                            _context.TmHomestay.Update(homeStayDet);
                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                        }

                        apiResponse.Msg = "Home Stay updated successfully";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Msg = "Model State not valid";
                        apiResponse.Result = ResponseTypes.ModelErr;
                    }
                }
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
