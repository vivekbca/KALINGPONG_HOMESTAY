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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public HomePageController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet("popularHomestay")]
        public async Task<IActionResult> GetAllPopularImage()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var PopularData = from pplr in _context.TtHsPopularity.AsNoTracking()
                                  join gl in _context.TmHsGallery.AsNoTracking()
                                  on pplr.HsId equals gl.HsId
                                  join hs in _context.TmHomestay.AsNoTracking()
                                  on pplr.HsId equals hs.HsId
                                  join vill in _context.TmBlockVillage.AsNoTracking()
                                  on hs.HsVillId equals vill.VillId
                                  where pplr.HsId == gl.HsId
                                  select new
                                  {
                                      pplr.HsId,
                                      pplr.HsSearchCount,
                                      gl.HsLi1,
                                      vill.VillName,
                                      vill.VillId,
                                      vill.VillCode,
                                      hs.HsName
                                  };
                var popularList = await PopularData.ToListAsync();
                apiResponse.Data = popularList;
                apiResponse.Msg = "Display All Popular List";
                apiResponse.Result=ResponseTypes.Success;

                    
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }
        [HttpGet("offbeat")]
        public async Task<IActionResult> offbeat1()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var offbeat1 = (  from gl in _context.TmHsGallery.AsNoTracking()
                                  join hs in _context.TmHomestay.AsNoTracking()
                                  on gl.HsId equals hs.HsId
                                  join vill in _context.TmBlockVillage.AsNoTracking()
                                  on hs.HsVillId equals vill.VillId
                                  orderby hs.ActiveSince
                                  where hs.HsId == gl.HsId && hs.IsActive==1                                 
                                  select new
                                  {
                                      hs.HsId, 
                                      gl.HsLi1,
                                      vill.VillName,
                                      vill.VillId,
                                      vill.VillCode,
                                      hs.HsName,
                                      hs.IsActive,
                                      hs.ActiveSince,
                                      
                                  }).Take(2);               
                var offbeatList = await offbeat1.ToListAsync();
                apiResponse.Data = offbeatList;
                apiResponse.Msg = "Display Offbeat1";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("ourProperty")]
        public async Task<IActionResult> OwrProperty()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var owrproperty = await _context.TmVillageCategory.ToListAsync();
                apiResponse.Data = owrproperty;
                apiResponse.Msg = "Display Properties";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpPost("ourPropertybyid")]
        public async Task<IActionResult> OwrPropertyByID([FromBody] OurProperties ourProperties)
        {
            List<OurPropertiesResponseModel> ourPropertiesResponseModelFinal = new List<OurPropertiesResponseModel>();
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                
                List<HomestayAminitiestesting> aminitieslistTesting = new List<HomestayAminitiestesting>();
                List<HomestayAminitiestesting> aminitieslistFinal = new List<HomestayAminitiestesting>();
                var AllHomestays = await _context.TmHomestay.Where(m => m.VillageCategoryId == ourProperties.VillageCategoryId && m.IsActive==1).Take(10).ToListAsync();
                foreach (var rooms in AllHomestays)
                {
                    OurPropertiesResponseModel ourPropertiesResponseModel = new OurPropertiesResponseModel();
                    var owrpropertybtyid = (from hs in _context.TmHomestay.AsNoTracking()
                                            join rm in _context.TmHsRooms.AsNoTracking()
                                            on hs.HsId equals rm.HsId
                                            join fac in _context.TmHsFacilities.AsNoTracking()
                                            on rm.HsRoomFacility1 equals fac.HsFacilityId
                                            join gl in _context.TmHsGallery.AsNoTracking()
                                            on hs.HsId equals gl.HsId
                                            where hs.HsId == rooms.HsId && hs.IsActive == 1
                                            select new
                                            {
                                                hs.HomestayDescription,
                                                hs.HsName,
                                                rm.HsId,
                                                rm.HsRoomNo,
                                                rm.HsRoomCategoryId,
                                                rm.HsRoomRate,
                                                rm.HsRoomFloor,
                                                rm.HsRoomNoBeds,
                                                rm.HsRoomSize,
                                                gl.HsLi1,
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
                                                rm.HsRoomAvailable,
                                                rm.CreatedBy,
                                                rm.CreatedOn,
                                                rm.IsAvailable,
                                                rm.ModifiedBy,
                                                rm.ModifiedOn
                                            });
                    var objData = await owrpropertybtyid.FirstOrDefaultAsync();
                    var Data = await owrpropertybtyid.ToListAsync();
                    foreach (var item in Data)
                    {
                        HomestayAminitiestesting data = new HomestayAminitiestesting();
                        data.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility1).Select(m => m.FileName).FirstOrDefaultAsync();
                        data.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility1).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data);
                        HomestayAminitiestesting data2 = new HomestayAminitiestesting();
                        data2.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility2).Select(m => m.FileName).FirstOrDefaultAsync();
                        data2.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility2).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data2);
                        HomestayAminitiestesting data3 = new HomestayAminitiestesting();
                        data3.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility3).Select(m => m.FileName).FirstOrDefaultAsync();
                        data3.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility3).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data3);
                        HomestayAminitiestesting data4 = new HomestayAminitiestesting();
                        data4.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility4).Select(m => m.FileName).FirstOrDefaultAsync();
                        data4.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility4).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data4);
                        HomestayAminitiestesting data5 = new HomestayAminitiestesting();
                        data5.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                        data5.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data5);
                        HomestayAminitiestesting data6 = new HomestayAminitiestesting();
                        data6.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility6).Select(m => m.FileName).FirstOrDefaultAsync();
                        data6.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility6).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data6);
                        HomestayAminitiestesting data7 = new HomestayAminitiestesting();
                        data7.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility7).Select(m => m.FileName).FirstOrDefaultAsync();
                        data7.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility7).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data7);
                        HomestayAminitiestesting data8 = new HomestayAminitiestesting();
                        data8.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility8).Select(m => m.FileName).FirstOrDefaultAsync();
                        data8.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility8).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data8);
                        HomestayAminitiestesting data9 = new HomestayAminitiestesting();
                        data9.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility9).Select(m => m.FileName).FirstOrDefaultAsync();
                        data9.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility9).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data9);
                        HomestayAminitiestesting data10 = new HomestayAminitiestesting();
                        data10.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility10).Select(m => m.FileName).FirstOrDefaultAsync();
                        data10.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility10).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data10);
                        HomestayAminitiestesting data11 = new HomestayAminitiestesting();
                        data11.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility11).Select(m => m.FileName).FirstOrDefaultAsync();
                        data11.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility11).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data11);
                        HomestayAminitiestesting data12 = new HomestayAminitiestesting();
                        data12.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility12).Select(m => m.FileName).FirstOrDefaultAsync();
                        data12.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility12).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data12);
                        HomestayAminitiestesting data13 = new HomestayAminitiestesting();
                        data13.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility13).Select(m => m.FileName).FirstOrDefaultAsync();
                        data13.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility13).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data13);
                        HomestayAminitiestesting data14 = new HomestayAminitiestesting();
                        data14.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility14).Select(m => m.FileName).FirstOrDefaultAsync();
                        data14.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility14).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data14);
                        HomestayAminitiestesting data15 = new HomestayAminitiestesting();
                        data15.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                        data15.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
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
                    ourPropertiesResponseModel.HsName = objData.HsName;
                    ourPropertiesResponseModel.HsId = objData.HsId;
                    ourPropertiesResponseModel.HsRoomNo = objData.HsRoomNo;
                    ourPropertiesResponseModel.HsRoomCategoryId = objData.HsRoomCategoryId;
                    ourPropertiesResponseModel.HsRoomRate = objData.HsRoomRate;
                    ourPropertiesResponseModel.HsRoomFloor = objData.HsRoomFloor;
                    ourPropertiesResponseModel.HsRoomNoBeds = objData.HsRoomNoBeds;
                    ourPropertiesResponseModel.HsRoomSize = objData.HsRoomSize;
                    ourPropertiesResponseModel.HsRoomImage = objData.HsLi1;
                    ourPropertiesResponseModel.HsDescription = objData.HomestayDescription;
                    ourPropertiesResponseModelFinal.Add(ourPropertiesResponseModel);
                }
                apiResponse.Data = ourPropertiesResponseModelFinal.OrderBy(m=>m.HsName);
                apiResponse.Msg = "Display Properties";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("otherProperty")]
        public async Task<IActionResult> GetAllOtherProperty()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var PopularData = await _context.TmVillageCategory.Take(6).ToListAsync();         
                apiResponse.Data = PopularData;
                apiResponse.Msg = "Display All Popular List";
                apiResponse.Result = ResponseTypes.Success;


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("Villagelist/{id}")]
        public async Task<IActionResult> VillageList(string id)
        {
            List<Facility> facilitiesList = new List<Facility>();
            List<OurPropertiesResponseModel> ourPropertiesResponseModelFinal = new List<OurPropertiesResponseModel>();
            List<HomestayAminitiestesting> aminitieslistTesting = new List<HomestayAminitiestesting>();
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                int RoomRate = 0;
                var TotalHomestay = (from hs in _context.TmHomestay.AsNoTracking()
                                     join gl in _context.TmHsGallery.AsNoTracking()
                                        on hs.HsId equals gl.HsId
                                     where hs.HsVillId == id && hs.IsActive == 1
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
                    ourPropertiesResponseModel.facilities = facilitiesList;
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("blockList")]
        public async Task<IActionResult> GetAllBlock()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var BlockList = await _context.TmBlock.OrderBy(m=>m.BlockName).ToListAsync();
                apiResponse.Data = BlockList;
                apiResponse.Msg = "Display All Popular List";
                apiResponse.Result = ResponseTypes.Success;


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("VillagelistDDl/{id}")]
        public async Task<IActionResult> VillageListForDDL(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var VillageListList = await _context.TmBlockVillage.OrderBy(m => m.VillName).Where(m=>m.BlockId==id).ToListAsync();
                apiResponse.Data = VillageListList;
                apiResponse.Msg = "Display All Popular List";
                apiResponse.Result = ResponseTypes.Success;


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("HomestaylistDDl/{id}")]
        public async Task<IActionResult> HometsayListForDDL(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var HomestayListList = await _context.TmHomestay.OrderBy(m=>m.HsName).Where(m => m.HsVillId == id && m.IsActive==1).ToListAsync();
                apiResponse.Data = HomestayListList;
                apiResponse.Msg = "Display All Popular List";
                apiResponse.Result = ResponseTypes.Success;


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("Homestaydetail/{id}")]
        public async Task<IActionResult> GetHomestayDetails(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                HomestayDetailsResponseModel obj = new HomestayDetailsResponseModel();
                List<RoomForHomestayDetailsResponseModel> rooms = new List<RoomForHomestayDetailsResponseModel>();
                List<HomestayAminitiestesting> aminitieslistTesting = new List<HomestayAminitiestesting>();
                List<HomestayAminitiestesting> aminitieslistFinal = new List<HomestayAminitiestesting>();
                List<HomestayImages> imagelist = new List<HomestayImages>();
                //List<FacilityModel> facilityList = new List<FacilityModel>();
                var HomeStayData2 = (from hs in _context.TmHomestay.AsNoTracking()
                                    join gl in _context.TmHsGallery.AsNoTracking()
                                    on hs.HsId equals gl.HsId
                                    join vl in _context.TmBlockVillage.AsNoTracking()
                                    on hs.HsVillId equals vl.VillId
                                    where hs.HsId == id
                                    select new
                                    {
                                        hs.HsId, hs.HsName, hs.HomestayDescription, hs.LocalAttraction, hs.HwtReach, hs.HsAddress1, hs.HsAddress2, hs.HsAddress3,
                                        hs.AddonServices, hs.VillageCategoryId, hs.HsContactName, hs.HsContactMob1, hs.HsContactMob2, hs.HsContactEmail, hs.HsNoOfRooms,
                                        hs.IsActive, hs.ActiveSince, gl.HsLi1, gl.HsLi2, gl.HsLi3, gl.HsLi4, gl.HsLi5, gl.HsLi6, gl.HsLi7, gl.HsLi8, gl.HsLi9, gl.HsLi10, vl.VillName,
                                        gl.HsRi1, gl.HsRi2, gl.HsRi3, gl.HsRi4, gl.HsRi5, gl.HsRi6, gl.HsRi7, gl.HsRi8, gl.HsRi9, gl.HsRi10, gl.HsMapLat, gl.HsMapLong
                                    });
                var HomeStayData = await HomeStayData2.FirstOrDefaultAsync();
                var HomestayRoom = from hs in _context.TmHomestay.AsNoTracking()
                                   join rm in _context.TmHsRooms.AsNoTracking()
                                   on hs.HsId equals rm.HsId
                                   join rc in _context.TmHsRoomCategory.AsNoTracking()
                                   on rm.HsRoomCategoryId equals rc.HsCategoryId
                                   where hs.HsId == id
                                   select new
                                   {
                                       rc.HsCategoryName,hs.HsName,rm.HsId,rm.HsRoomNo,rm.HsRoomCategoryId,rm.HsRoomRate,rm.HsRoomFloor,rm.HsRoomNoBeds,rm.HsRoomSize,rm.HsRoomFacility1,rm.HsRoomFacility2,rm.HsRoomFacility3,
                                       rm.HsRoomFacility4,rm.HsRoomFacility5,rm.HsRoomFacility6,rm.HsRoomFacility7,rm.HsRoomFacility8,rm.HsRoomFacility9,rm.HsRoomFacility10,rm.HsRoomFacility11,rm.HsRoomFacility12,
                                       rm.HsRoomFacility13,rm.HsRoomFacility14,rm.HsRoomFacility15,rm.HsRoomAvailable,rm.CreatedBy,rm.CreatedOn,rm.IsAvailable,rm.ModifiedBy,rm.ModifiedOn,rm.HsRoomImage,
                                   };               
                var HomestayRoomList =await HomestayRoom.ToListAsync();

                //new Trying Aminities
                foreach(var item in HomestayRoomList)
                {
                    HomestayAminitiestesting data = new HomestayAminitiestesting();
                    data.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility1).Select(m => m.FileName).FirstOrDefaultAsync();
                    data.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility1).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data);
                    HomestayAminitiestesting data2 = new HomestayAminitiestesting();
                    data2.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility2).Select(m => m.FileName).FirstOrDefaultAsync();
                    data2.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility2).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data2);
                    HomestayAminitiestesting data3 = new HomestayAminitiestesting();
                    data3.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility3).Select(m => m.FileName).FirstOrDefaultAsync();
                    data3.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility3).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data3);
                    HomestayAminitiestesting data4 = new HomestayAminitiestesting();
                    data4.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility4).Select(m => m.FileName).FirstOrDefaultAsync();
                    data4.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility4).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data4);
                    HomestayAminitiestesting data5 = new HomestayAminitiestesting();
                    data5.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                    data5.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data5);
                    HomestayAminitiestesting data6 = new HomestayAminitiestesting();
                    data6.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility6).Select(m => m.FileName).FirstOrDefaultAsync();
                    data6.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility6).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data6);
                    HomestayAminitiestesting data7 = new HomestayAminitiestesting();
                    data7.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility7).Select(m => m.FileName).FirstOrDefaultAsync();
                    data7.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility7).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data7);
                    HomestayAminitiestesting data8 = new HomestayAminitiestesting();
                    data8.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility8).Select(m => m.FileName).FirstOrDefaultAsync();
                    data8.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility8).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data8);
                    HomestayAminitiestesting data9 = new HomestayAminitiestesting();
                    data9.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility9).Select(m => m.FileName).FirstOrDefaultAsync();
                    data9.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility9).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data9);
                    HomestayAminitiestesting data10 = new HomestayAminitiestesting();
                    data10.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility10).Select(m => m.FileName).FirstOrDefaultAsync();
                    data10.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility10).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data10);
                    HomestayAminitiestesting data11 = new HomestayAminitiestesting();
                    data11.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility11).Select(m => m.FileName).FirstOrDefaultAsync();
                    data11.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility11).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data11);
                    HomestayAminitiestesting data12 = new HomestayAminitiestesting();
                    data12.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility12).Select(m => m.FileName).FirstOrDefaultAsync();
                    data12.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility12).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data12);
                    HomestayAminitiestesting data13 = new HomestayAminitiestesting();
                    data13.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility13).Select(m => m.FileName).FirstOrDefaultAsync();
                    data13.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility13).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data13);
                    HomestayAminitiestesting data14 = new HomestayAminitiestesting();
                    data14.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility14).Select(m => m.FileName).FirstOrDefaultAsync();
                    data14.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility14).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data14);
                    HomestayAminitiestesting data15 = new HomestayAminitiestesting();
                    data15.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                    data15.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    aminitieslistTesting.Add(data15);
                }
                obj.HSAmImg = aminitieslistTesting;
                obj.VillageName = HomeStayData.VillName;
                obj.HsId = HomeStayData.HsId;
                obj.HsName = HomeStayData.HsName;
                obj.HomestayDescription = HomeStayData.HomestayDescription;
                obj.LocalAttraction = HomeStayData.LocalAttraction;
                obj.HwtReach = HomeStayData.HwtReach;
                obj.HsAddress1 = HomeStayData.HsAddress1;
                obj.HsAddress2 = HomeStayData.HsAddress2;
                obj.HsAddress3 = HomeStayData.HsAddress3;
                obj.AddonServices = HomeStayData.AddonServices;
                obj.HsNoOfRooms = HomeStayData.HsNoOfRooms;
                obj.HomestayImage1 = HomeStayData.HsLi1;
                obj.HomestayImage2 = HomeStayData.HsLi2;
                obj.HomestayImage3 = HomeStayData.HsLi3;
                obj.HomestayImage4 = HomeStayData.HsLi4;
                obj.HomestayImage5 = HomeStayData.HsLi5;
                obj.HomestayImage6 = HomeStayData.HsLi6;
                obj.HomestayImage7 = HomeStayData.HsLi7;
                obj.HomestayImage8 = HomeStayData.HsLi8;
                obj.HomestayImage9 = HomeStayData.HsLi9;
                obj.HomestayImage10 = HomeStayData.HsLi10;
                obj.HsRi1 = HomeStayData.HsRi1;
                obj.HsRi2 = HomeStayData.HsRi2;
                obj.HsRi3 = HomeStayData.HsRi3;
                obj.HsRi4 = HomeStayData.HsRi4;
                obj.HsRi5 = HomeStayData.HsRi5;
                obj.HsRi6 = HomeStayData.HsRi6;
                obj.HsRi7 = HomeStayData.HsRi7;
                obj.HsRi8 = HomeStayData.HsRi8;
                obj.HsRi9 = HomeStayData.HsRi9;
                obj.HsRi10 = HomeStayData.HsRi10;
                obj.HsMapLat = HomeStayData.HsMapLat;
                obj.HsMapLong = HomeStayData.HsMapLong;
                foreach (var item in HomestayRoomList)
                {
                    RoomForHomestayDetailsResponseModel room = new RoomForHomestayDetailsResponseModel();
                    FacilityModel facility1 = new FacilityModel();
                    List<FacilityModel> facilityList = new List<FacilityModel>();
                    facility1.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility1).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility1.facilityName= await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility1).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if(facility1.facilityId!=null)
                    {
                        facilityList.Add(facility1);
                    }
                   
                    FacilityModel facility2 = new FacilityModel();
                    facility2.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility2).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility2.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility2).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility2.facilityId != null)
                    {
                        facilityList.Add(facility2);
                    }
                    
                    FacilityModel facility3 = new FacilityModel();
                    facility3.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility3).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility3.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility3).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility3.facilityId != null)
                    {
                        facilityList.Add(facility3);
                    }
                    
                    FacilityModel facility4 = new FacilityModel();
                    facility4.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility4).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility4.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility4).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility4.facilityId != null)
                    {
                        facilityList.Add(facility4);
                    }
                    
                    FacilityModel facility5 = new FacilityModel();
                    facility5.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility5.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility5.facilityId != null)
                    {
                        facilityList.Add(facility5);
                    }
                    
                    FacilityModel facility6 = new FacilityModel();
                    facility6.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility6).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility6.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility6).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility6.facilityId != null)
                    {
                        facilityList.Add(facility6);
                    }
                    
                    FacilityModel facility7 = new FacilityModel();
                    facility7.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility7).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility7.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility7).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility7.facilityId != null)
                    {
                        facilityList.Add(facility7);
                    }
                    
                    FacilityModel facility8 = new FacilityModel();
                    facility8.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility8).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility8.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility8).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility8.facilityId != null)
                    {
                        facilityList.Add(facility8);
                    }
                    
                    FacilityModel facility9 = new FacilityModel();
                    facility9.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility9).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility9.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility9).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility9.facilityId != null)
                    {
                        facilityList.Add(facility9);
                    }
                    
                    FacilityModel facility10 = new FacilityModel();
                    facility10.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility10).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility10.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility10).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility10.facilityId != null)
                    {
                        facilityList.Add(facility10);
                    }
                    
                    FacilityModel facility11 = new FacilityModel();
                    facility11.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility11).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility11.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility11).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility11.facilityId != null)
                    {
                        facilityList.Add(facility11);
                    }
                   
                    FacilityModel facility12 = new FacilityModel();
                    facility12.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility12).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility12.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility12).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility12.facilityId != null)
                    {
                        facilityList.Add(facility12);
                    }
                    
                    FacilityModel facility13 = new FacilityModel();
                    facility13.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility13).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility13.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility13).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility13.facilityId != null)
                    {
                        facilityList.Add(facility13);
                    }
                    
                    FacilityModel facility14 = new FacilityModel();
                    facility14.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility14).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility14.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility14).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility14.facilityId != null)
                    {
                        facilityList.Add(facility14);
                    }
               
                    FacilityModel facility15 = new FacilityModel();
                    facility15.facilityId = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility15).Select(m => m.FileName).FirstOrDefaultAsync();
                    facility15.facilityName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == item.HsRoomFacility15).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                    if (facility15.facilityId != null)
                    {
                        facilityList.Add(facility15);
                    }
                    
                    room.HsCategoryName = item.HsCategoryName;
                    room.HsId = item.HsId;
                    room.HsRoomNo = item.HsRoomNo;
                    room.HsRoomCategoryId = item.HsRoomCategoryId;
                    room.HsRoomRate = item.HsRoomRate;
                    room.HsRoomFloor = item.HsRoomFloor;
                    room.HsRoomNoBeds = item.HsRoomNoBeds;
                    room.HsRoomSize = item.HsRoomSize;
                    room.HsRoomImage = item.HsRoomImage;
                    room.facilitymodel=facilityList;
                    rooms.Add(room);
                }                 
                //new HomestayImagesList
                //HomestayImages image = new HomestayImages();
                //image.image = HomeStayData.HsLi1;
                //imagelist.Add(image);
                //HomestayImages image2 = new HomestayImages();
                //image2.image = HomeStayData.HsLi2;
                //imagelist.Add(image2);
                //HomestayImages image3 = new HomestayImages();
                //image3.image = HomeStayData.HsLi3;
                //imagelist.Add(image3);
                //HomestayImages image4 = new HomestayImages();
                //image4.image = HomeStayData.HsLi4;
                //imagelist.Add(image4);
                //HomestayImages image5 = new HomestayImages();
                //image5.image = HomeStayData.HsLi5;
                //imagelist.Add(image5);
                //HomestayImages image6 = new HomestayImages();
                //image6.image = HomeStayData.HsLi6;
                //imagelist.Add(image6);
                //HomestayImages image7 = new HomestayImages();
                //image7.image = HomeStayData.HsLi7;
                //imagelist.Add(image7);
                //HomestayImages image8 = new HomestayImages();
                //image8.image = HomeStayData.HsLi8;
                //imagelist.Add(image8);
                //HomestayImages image9 = new HomestayImages();
                //image9.image = HomeStayData.HsLi9;
                //imagelist.Add(image9);
                //HomestayImages image10 = new HomestayImages();
                //image10.image = HomeStayData.HsLi10;
                //imagelist.Add(image10); 
                //obj.HSImages= imagelist;
                //new HomestayImagesList END
                obj.TmHsRooms = rooms;
                apiResponse.Data = obj;
                apiResponse.Msg = "Display Details For homestay";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("otherDestination")]
        public async Task<IActionResult> OtherDestination()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                Random rnd = new Random();
                var OtherDestinationList = await _context.TmBlockVillage.ToListAsync();
                var OtherDestination= OtherDestinationList.OrderBy(x => rnd.Next()).Take(6);
                apiResponse.Data = OtherDestination;
                apiResponse.Msg = "Display All Popular List";
                apiResponse.Result = ResponseTypes.Success;


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("populardestination")]
        public async Task<IActionResult> GetAllPopularDestination()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var PopularList = await _context.TmPopulardestination.OrderBy(m=>m.DestinationName).ToListAsync();
                apiResponse.Data = PopularList;
                apiResponse.Msg = "Display All Popular List";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("PopularDestinationHomestay/{id}")]
        public async Task<IActionResult> GetAllPopularDestinationHomsestays(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var PopularHomeStayList = await _context.TmHomestay.Where(m=>m.DestinationId==id).OrderBy(m=>m.HsName).ToListAsync();
                apiResponse.Data = PopularHomeStayList;
                apiResponse.Msg = "Display All Popular List";
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
