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
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.Web;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;


        public SearchController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }

        [HttpPost]
        public async Task<IActionResult> GetHSbyPriceWise(SearchRequestByPriceModel searchObj)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var hsList = await (from a in _context.TmHomestay
                                      join b in _context.TmHsRooms on a.HsId equals b.HsId
                                      join c in _context.TmBlockVillage on a.HsVillId equals c.VillId
                                      join d in _context.TmBlock on a.HsBlockId equals d.BlockId
                                      join e in _context.TmDistrict on a.HsDistrictId equals e.DistrictId
                                      join f in _context.TmState on a.HsStateId equals f.StateId
                                      join g in _context.TmHsRoomCategory on b.HsRoomCategoryId equals g.HsCategoryId
                                      select new SearchResponseModel
                                      {
                                          HS_Id = a.HsId,
                                          HS_Name = a.HsName,
                                          HS_Address1 = a.HsAddress1,
                                          HS_Address2 = a.HsAddress2,
                                          HS_Address3 = a.HsAddress3,
                                          HS_VillId = c.VillId,
                                          HS_VillName = c.VillName,
                                          HS_BlockId = d.BlockId,
                                          HS_BlockName = d.BlockName,
                                          HS_DistId = e.DistrictId,
                                          HS_DistName = e.DistrictName,
                                          HS_StateId = f.StateId,
                                          HS_StateName = f.StateName,
                                          HS_ContactName = a.HsContactName,
                                          HS_ContactMob1 = a.HsContactMob1,
                                          HS_ContactMob2 = a.HsContactMob2,
                                          HS_ContactEmail = a.HsContactEmail,
                                          HS_NoOfRooms = a.HsNoOfRooms,
                                          ActiveSince = a.ActiveSince,
                                          RoomRate = b.HsRoomRate
                                      }).Where(m=>m.RoomRate>=searchObj.fromPrice && m.RoomRate<= searchObj.toPrice).ToListAsync();
                apiResponse.Data = hsList;
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
        [HttpGet]
        public async Task<IActionResult> LoadBlocks()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var blockList = await (from a in _context.TmBlock
                                    select new TMBlockResponseModel
                                    {
                                        BlockId = a.BlockId,
                                        BlockName = a.BlockName,
                                        IsActive = a.IsActive
                                    }).Where(m => m.IsActive ==1).OrderBy(m=>m.BlockName).ToListAsync();
                apiResponse.Data = blockList;
                apiResponse.Msg = "Displaying Block List";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpGet("LoadVillage/{id}")]
        public async Task<IActionResult> LoadVillage(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var villageList = await (from a in _context.TmBlockVillage
                                       select new BlockVillageResponseModel
                                       {
                                           VillId = a.VillId,
                                           VillCode = a.VillCode,
                                           Village = a.VillName,
                                           BlockId = a.BlockId,
                                           isActive = a.IsActive
                                       }).Where(m => m.isActive == 1 && m.BlockId== id).OrderBy(m=>m.Village).ToListAsync();
                apiResponse.Data = villageList;
                apiResponse.Msg = "Displaying Village List";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpGet("LoadHS/{id}")]
        public async Task<IActionResult> LoadHS(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var hsList = await (from a in _context.TmHomestay
                                         select new HomeStayAddRequestModel
                                         {
                                             HsId = a.HsId,
                                             HsName = a.HsName,
                                             IsActive = a.IsActive,
                                             HsVillId = a.HsVillId
                                         }).Where(m => m.IsActive == 1 && m.HsVillId == id).OrderBy(m=>m.HsName).ToListAsync();
                apiResponse.Data = hsList;
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
        [HttpGet("ViewDetails/{id}")]
        public async Task<IActionResult> ViewDetails(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var checkTbl = HttpContext.Session.GetString("SessionTbl");
                if (checkTbl == null)
                {
                    List<SessionTable> sessionTable = new List<SessionTable>();
                    SessionTable sessionTblObj = new SessionTable();
                    sessionTblObj.PropertyId = id;
                    sessionTable.Add(sessionTblObj);
                    HttpContext.Session.SetString("SessionTbl", JsonConvert.SerializeObject(sessionTable));
                }
                else
                {
                    var table = HttpContext.Session.GetString("SessionTbl");
                    List<SessionTable> propertyList = JsonConvert.DeserializeObject<List<SessionTable>>(table);
                    SessionTable sessionTblObj = new SessionTable();
                    sessionTblObj.PropertyId = id;
                    propertyList.Add(sessionTblObj);
                    HttpContext.Session.SetString("SessionTbl", JsonConvert.SerializeObject(propertyList));
                }

                var hsList = await (from a in _context.TmHomestay
                                    select new HomeStayAddRequestModel
                                    {
                                        HsId = a.HsId,
                                        HsName = a.HsName,
                                        IsActive = a.IsActive,
                                        HsVillId = a.HsVillId
                                    }).Where(m => m.IsActive == 1).ToListAsync();
                //apiResponse.Data = hsList;
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

        [HttpGet("LoadHSByName")]
        public async Task<IActionResult> LoadHSByName(string HSname)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var hsList = await (from a in _context.TmHomestay
                                    select new HomeStayAddRequestModel
                                    {
                                        HsId = a.HsId,
                                        HsName = a.HsName,
                                        IsActive = a.IsActive,
                                        HsVillId = a.HsVillId
                                    }).Where(m => m.IsActive == 1 && m.HsName.Contains(HSname)).ToListAsync();
                apiResponse.Data = hsList;
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

        [HttpPost("searchHomestay")]
        public async Task<IActionResult> VillageList([FromBody] HomepageSearchResponseModel searchModel)
        {
            List<OurPropertiesResponseModel> ourPropertiesResponseModelFinal = new List<OurPropertiesResponseModel>();
            List<HomestayAminitiestesting> aminitieslistTesting = new List<HomestayAminitiestesting>();
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if(searchModel.Name== "VillageID")
                {
                    int RoomRate = 0;
                    var TotalHomestay = (from hs in _context.TmHomestay.AsNoTracking()
                                         join gl in _context.TmHsGallery.AsNoTracking()
                                            on hs.HsId equals gl.HsId
                                         where hs.HsVillId == searchModel.ID && hs.IsActive == 1
                                         select new
                                         {
                                             hs.HomestayDescription,
                                             hs.HsName,
                                             hs.HsId,
                                             gl.HsLi1,

                                         }).ToList();
                    foreach (var item in TotalHomestay)
                    {
                        OurPropertiesResponseModel ourPropertiesResponseModel = new OurPropertiesResponseModel();
                        var Rooms = (from hs in _context.TmHomestay.AsNoTracking()
                                     join rm in _context.TmHsRooms.AsNoTracking()
                                     on hs.HsId equals rm.HsId
                                     where rm.HsId == item.HsId
                                     select new
                                     {
                                         rm.HsRoomRate,
                                         rm.HsRoomFacility1,
                                         rm.HsRoomFacility2,
                                         rm.HsRoomFacility3,
                                         rm.HsRoomFacility4,
                                         rm.HsRoomFacility5,
                                         rm.HsRoomFacility6,
                                         rm.HsRoomFacility7,
                                         rm.HsRoomFacility8,
                                         rm.HsRoomFacility9,
                                         rm.HsRoomFacility10,
                                         rm.HsRoomFacility11,
                                         rm.HsRoomFacility12,
                                         rm.HsRoomFacility13,
                                         rm.HsRoomFacility14,
                                         rm.HsRoomFacility15,
                                     }).ToList();                       
                        foreach(var itemaminities in Rooms)
                        {
                            HomestayAminitiestesting data = new HomestayAminitiestesting();
                            data.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility1).Select(m => m.FileName).FirstOrDefaultAsync();
                            data.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility1).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data);
                            HomestayAminitiestesting data2 = new HomestayAminitiestesting();
                            data2.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility2).Select(m => m.FileName).FirstOrDefaultAsync();
                            data2.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility2).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data2);
                            HomestayAminitiestesting data3 = new HomestayAminitiestesting();
                            data3.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility3).Select(m => m.FileName).FirstOrDefaultAsync();
                            data3.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility3).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data3);
                            HomestayAminitiestesting data4 = new HomestayAminitiestesting();
                            data4.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility4).Select(m => m.FileName).FirstOrDefaultAsync();
                            data4.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility4).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data4);
                            HomestayAminitiestesting data5 = new HomestayAminitiestesting();
                            data5.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                            data5.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data5);
                            HomestayAminitiestesting data6 = new HomestayAminitiestesting();
                            data6.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility6).Select(m => m.FileName).FirstOrDefaultAsync();
                            data6.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility6).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data6);
                            HomestayAminitiestesting data7 = new HomestayAminitiestesting();
                            data7.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility7).Select(m => m.FileName).FirstOrDefaultAsync();
                            data7.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility7).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data7);
                            HomestayAminitiestesting data8 = new HomestayAminitiestesting();
                            data8.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility8).Select(m => m.FileName).FirstOrDefaultAsync();
                            data8.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility8).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data8);
                            HomestayAminitiestesting data9 = new HomestayAminitiestesting();
                            data9.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility9).Select(m => m.FileName).FirstOrDefaultAsync();
                            data9.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility9).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data9);
                            HomestayAminitiestesting data10 = new HomestayAminitiestesting();
                            data10.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility10).Select(m => m.FileName).FirstOrDefaultAsync();
                            data10.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility10).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data10);
                            HomestayAminitiestesting data11 = new HomestayAminitiestesting();
                            data11.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility11).Select(m => m.FileName).FirstOrDefaultAsync();
                            data11.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility11).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data11);
                            HomestayAminitiestesting data12 = new HomestayAminitiestesting();
                            data12.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility12).Select(m => m.FileName).FirstOrDefaultAsync();
                            data12.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility12).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data12);
                            HomestayAminitiestesting data13 = new HomestayAminitiestesting();
                            data13.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility13).Select(m => m.FileName).FirstOrDefaultAsync();
                            data13.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility13).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data13);
                            HomestayAminitiestesting data14 = new HomestayAminitiestesting();
                            data14.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility14).Select(m => m.FileName).FirstOrDefaultAsync();
                            data14.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility14).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data14);
                            HomestayAminitiestesting data15 = new HomestayAminitiestesting();
                            data15.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                            data15.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data15);
                        }
                        List<HomestayAminitiestesting> finalaminities = new List<HomestayAminitiestesting>();
                        foreach (var u1 in aminitieslistTesting)
                        {
                            bool duplicatefound = false;
                            foreach (var u2 in finalaminities)
                                if (u1.amin == u2.amin)
                                    duplicatefound = true;

                            if (!duplicatefound)
                                finalaminities.Add(u1);
                        }
                        ourPropertiesResponseModel.HSAmImg = finalaminities;
                        ourPropertiesResponseModel.HsName = item.HsName;
                        ourPropertiesResponseModel.HsId = item.HsId;
                        ourPropertiesResponseModel.HsDescription = item.HomestayDescription;
                        ourPropertiesResponseModel.HsRoomImage = item.HsLi1;
                        ourPropertiesResponseModel.HsRoomRate = RoomRate;
                        ourPropertiesResponseModelFinal.Add(ourPropertiesResponseModel);
                    }

                    apiResponse.Data = ourPropertiesResponseModelFinal;
                    apiResponse.Msg = "Display Homestays";
                    apiResponse.Result = ResponseTypes.Success;
                }
                if(searchModel.Name== "HomestayID")
                {
                    int RoomRate = 0;
                    var TotalHomestay = (from hs in _context.TmHomestay.AsNoTracking()
                                         join gl in _context.TmHsGallery.AsNoTracking()
                                            on hs.HsId equals gl.HsId
                                         where hs.HsId == searchModel.ID && hs.IsActive == 1
                                         select new
                                         {
                                             hs.HomestayDescription,
                                             hs.HsName,
                                             hs.HsId,
                                             gl.HsLi1,

                                         }).ToList();
                    foreach (var item in TotalHomestay)
                    {
                        OurPropertiesResponseModel ourPropertiesResponseModel = new OurPropertiesResponseModel();
                        var Rooms = (from hs in _context.TmHomestay.AsNoTracking()
                                     join rm in _context.TmHsRooms.AsNoTracking()
                                     on hs.HsId equals rm.HsId
                                     where rm.HsId == item.HsId
                                     select new
                                     {
                                         rm.HsRoomRate,
                                         rm.HsRoomFacility1,
                                         rm.HsRoomFacility2,
                                         rm.HsRoomFacility3,
                                         rm.HsRoomFacility4,
                                         rm.HsRoomFacility5,
                                         rm.HsRoomFacility6,
                                         rm.HsRoomFacility7,
                                         rm.HsRoomFacility8,
                                         rm.HsRoomFacility9,
                                         rm.HsRoomFacility10,
                                         rm.HsRoomFacility11,
                                         rm.HsRoomFacility12,
                                         rm.HsRoomFacility13,
                                         rm.HsRoomFacility14,
                                         rm.HsRoomFacility15,
                                     }).ToList();                       
                        foreach (var itemaminities in Rooms)
                        {
                            HomestayAminitiestesting data = new HomestayAminitiestesting();
                            data.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility1).Select(m => m.FileName).FirstOrDefaultAsync();
                            data.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility1).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data);
                            HomestayAminitiestesting data2 = new HomestayAminitiestesting();
                            data2.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility2).Select(m => m.FileName).FirstOrDefaultAsync();
                            data2.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility2).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data2);
                            HomestayAminitiestesting data3 = new HomestayAminitiestesting();
                            data3.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility3).Select(m => m.FileName).FirstOrDefaultAsync();
                            data3.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility3).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data3);
                            HomestayAminitiestesting data4 = new HomestayAminitiestesting();
                            data4.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility4).Select(m => m.FileName).FirstOrDefaultAsync();
                            data4.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility4).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data4);
                            HomestayAminitiestesting data5 = new HomestayAminitiestesting();
                            data5.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                            data5.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data5);
                            HomestayAminitiestesting data6 = new HomestayAminitiestesting();
                            data6.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility6).Select(m => m.FileName).FirstOrDefaultAsync();
                            data6.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility6).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data6);
                            HomestayAminitiestesting data7 = new HomestayAminitiestesting();
                            data7.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility7).Select(m => m.FileName).FirstOrDefaultAsync();
                            data7.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility7).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data7);
                            HomestayAminitiestesting data8 = new HomestayAminitiestesting();
                            data8.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility8).Select(m => m.FileName).FirstOrDefaultAsync();
                            data8.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility8).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data8);
                            HomestayAminitiestesting data9 = new HomestayAminitiestesting();
                            data9.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility9).Select(m => m.FileName).FirstOrDefaultAsync();
                            data9.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility9).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data9);
                            HomestayAminitiestesting data10 = new HomestayAminitiestesting();
                            data10.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility10).Select(m => m.FileName).FirstOrDefaultAsync();
                            data10.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility10).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data10);
                            HomestayAminitiestesting data11 = new HomestayAminitiestesting();
                            data11.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility11).Select(m => m.FileName).FirstOrDefaultAsync();
                            data11.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility11).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data11);
                            HomestayAminitiestesting data12 = new HomestayAminitiestesting();
                            data12.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility12).Select(m => m.FileName).FirstOrDefaultAsync();
                            data12.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility12).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data12);
                            HomestayAminitiestesting data13 = new HomestayAminitiestesting();
                            data13.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility13).Select(m => m.FileName).FirstOrDefaultAsync();
                            data13.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility13).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data13);
                            HomestayAminitiestesting data14 = new HomestayAminitiestesting();
                            data14.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility14).Select(m => m.FileName).FirstOrDefaultAsync();
                            data14.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility14).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data14);
                            HomestayAminitiestesting data15 = new HomestayAminitiestesting();
                            data15.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                            data15.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data15);
                        }
                        List<HomestayAminitiestesting> finalaminities = new List<HomestayAminitiestesting>();
                        foreach (var u1 in aminitieslistTesting)
                        {
                            bool duplicatefound = false;
                            foreach (var u2 in finalaminities)
                                if (u1.amin == u2.amin)
                                    duplicatefound = true;

                            if (!duplicatefound)
                                finalaminities.Add(u1);
                        }
                        ourPropertiesResponseModel.HSAmImg = finalaminities;
                        ourPropertiesResponseModel.HsName = item.HsName;
                        ourPropertiesResponseModel.HsId = item.HsId;
                        ourPropertiesResponseModel.HsDescription = item.HomestayDescription;
                        ourPropertiesResponseModel.HsRoomImage = item.HsLi1;
                        ourPropertiesResponseModel.HsRoomRate = RoomRate;
                        ourPropertiesResponseModelFinal.Add(ourPropertiesResponseModel);
                    }

                    apiResponse.Data = ourPropertiesResponseModelFinal;
                    apiResponse.Msg = "Display Homestays";
                    apiResponse.Result = ResponseTypes.Success;
                } 
                if(searchModel.Name== "PopularArea")
                {
                    int RoomRate = 0;
                    var TotalHomestay = (from hs in _context.TmHomestay.AsNoTracking()
                                         join gl in _context.TmHsGallery.AsNoTracking()
                                            on hs.HsId equals gl.HsId
                                         where hs.DestinationId == searchModel.ID && hs.IsActive == 1
                                         select new
                                         {
                                             hs.HomestayDescription,
                                             hs.HsName,
                                             hs.HsId,
                                             gl.HsLi1,

                                         }).ToList();
                    foreach (var item in TotalHomestay)
                    {
                        OurPropertiesResponseModel ourPropertiesResponseModel = new OurPropertiesResponseModel();
                        var Rooms = (from hs in _context.TmHomestay.AsNoTracking()
                                     join rm in _context.TmHsRooms.AsNoTracking()
                                     on hs.HsId equals rm.HsId
                                     where rm.HsId == item.HsId
                                     select new
                                     {
                                         rm.HsRoomRate,
                                         rm.HsRoomFacility1,
                                         rm.HsRoomFacility2,
                                         rm.HsRoomFacility3,
                                         rm.HsRoomFacility4,
                                         rm.HsRoomFacility5,
                                         rm.HsRoomFacility6,
                                         rm.HsRoomFacility7,
                                         rm.HsRoomFacility8,
                                         rm.HsRoomFacility9,
                                         rm.HsRoomFacility10,
                                         rm.HsRoomFacility11,
                                         rm.HsRoomFacility12,
                                         rm.HsRoomFacility13,
                                         rm.HsRoomFacility14,
                                         rm.HsRoomFacility15,
                                     }).ToList();
                        foreach (var itemaminities in Rooms)
                        {
                            HomestayAminitiestesting data = new HomestayAminitiestesting();
                            data.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility1).Select(m => m.FileName).FirstOrDefaultAsync();
                            data.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility1).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data);
                            HomestayAminitiestesting data2 = new HomestayAminitiestesting();
                            data2.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility2).Select(m => m.FileName).FirstOrDefaultAsync();
                            data2.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility2).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data2);
                            HomestayAminitiestesting data3 = new HomestayAminitiestesting();
                            data3.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility3).Select(m => m.FileName).FirstOrDefaultAsync();
                            data3.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility3).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data3);
                            HomestayAminitiestesting data4 = new HomestayAminitiestesting();
                            data4.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility4).Select(m => m.FileName).FirstOrDefaultAsync();
                            data4.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility4).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data4);
                            HomestayAminitiestesting data5 = new HomestayAminitiestesting();
                            data5.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                            data5.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data5);
                            HomestayAminitiestesting data6 = new HomestayAminitiestesting();
                            data6.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility6).Select(m => m.FileName).FirstOrDefaultAsync();
                            data6.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility6).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data6);
                            HomestayAminitiestesting data7 = new HomestayAminitiestesting();
                            data7.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility7).Select(m => m.FileName).FirstOrDefaultAsync();
                            data7.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility7).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data7);
                            HomestayAminitiestesting data8 = new HomestayAminitiestesting();
                            data8.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility8).Select(m => m.FileName).FirstOrDefaultAsync();
                            data8.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility8).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data8);
                            HomestayAminitiestesting data9 = new HomestayAminitiestesting();
                            data9.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility9).Select(m => m.FileName).FirstOrDefaultAsync();
                            data9.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility9).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data9);
                            HomestayAminitiestesting data10 = new HomestayAminitiestesting();
                            data10.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility10).Select(m => m.FileName).FirstOrDefaultAsync();
                            data10.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility10).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data10);
                            HomestayAminitiestesting data11 = new HomestayAminitiestesting();
                            data11.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility11).Select(m => m.FileName).FirstOrDefaultAsync();
                            data11.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility11).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data11);
                            HomestayAminitiestesting data12 = new HomestayAminitiestesting();
                            data12.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility12).Select(m => m.FileName).FirstOrDefaultAsync();
                            data12.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility12).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data12);
                            HomestayAminitiestesting data13 = new HomestayAminitiestesting();
                            data13.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility13).Select(m => m.FileName).FirstOrDefaultAsync();
                            data13.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility13).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data13);
                            HomestayAminitiestesting data14 = new HomestayAminitiestesting();
                            data14.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility14).Select(m => m.FileName).FirstOrDefaultAsync();
                            data14.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility14).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data14);
                            HomestayAminitiestesting data15 = new HomestayAminitiestesting();
                            data15.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                            data15.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == itemaminities.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                            aminitieslistTesting.Add(data15);
                        }
                        List<HomestayAminitiestesting> finalaminities = new List<HomestayAminitiestesting>();
                        foreach (var u1 in aminitieslistTesting)
                        {
                            bool duplicatefound = false;
                            foreach (var u2 in finalaminities)
                                if (u1.amin == u2.amin)
                                    duplicatefound = true;

                            if (!duplicatefound)
                                finalaminities.Add(u1);
                        }
                        ourPropertiesResponseModel.HSAmImg = finalaminities;
                        ourPropertiesResponseModel.HsName = item.HsName;
                        ourPropertiesResponseModel.HsId = item.HsId;
                        ourPropertiesResponseModel.HsDescription = item.HomestayDescription;
                        ourPropertiesResponseModel.HsRoomImage = item.HsLi1;
                        ourPropertiesResponseModel.HsRoomRate = RoomRate;
                        ourPropertiesResponseModelFinal.Add(ourPropertiesResponseModel);
                    }

                    apiResponse.Data = ourPropertiesResponseModelFinal;
                    apiResponse.Msg = "Display Homestays";
                    apiResponse.Result = ResponseTypes.Success;
                }
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
