using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using KLMPNHomeStay.Models.Request_Model;
using KLMPNHomeStay.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HSMemberDetailsController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public HSMemberDetailsController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet("category")]
        public async Task<IActionResult> GetRoomCategory()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllRoomCategory = await _context.TmHsRoomCategory.OrderBy(m => m.HsCategoryName).ToListAsync();
                apiResponse.Data = AllRoomCategory;
                apiResponse.Msg = "List of Room Category";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("service")]
        public async Task<IActionResult> GetRoomService()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var RoomFacility = await _context.TmHsFacilities.OrderBy(m => m.HsFacilityName).ToListAsync();
                apiResponse.Data = RoomFacility;
                apiResponse.Msg = "List of Room Facility";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        public string UploadHSImages(string base64Images)
        {
            string uniqueFileName = "";
            if (base64Images!=null)
            {
                string[] splitdata1 = base64Images.Split(",");
                var content = splitdata1[1];
                uniqueFileName = Guid.NewGuid().ToString();
                String path1 = Path.Combine($"{_env.ContentRootPath}\\ClientApp\\src\\assets\\");
                System.IO.File.WriteAllBytes(path1 + uniqueFileName + ".jpeg", Convert.FromBase64String(content));
                uniqueFileName = uniqueFileName + ".jpeg";
            }
            return uniqueFileName;
        }

        [HttpPost("save")]
        public async Task<IActionResult> HSMemberdetailsSave([FromBody] HSMemberDetailsRequestModel memberDetails)
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

                        var galleryAdd = new List<TmHsGallery>();
                        var galleryUpdate = new List<TmHsGallery>();

                        if (memberDetails.roomImageStrings == null)
                            memberDetails.roomImageStrings = "";

                        var base64Images = memberDetails.roomImageStrings.Split("@@@");

                        //Room & HS Images
                        var checkHSimage = _context.TmHsGallery.Where(m => m.HsId == memberDetails.hsId).FirstOrDefault();
                        if (checkHSimage != null)
                        {
                            checkHSimage.HsLi1 = UploadHSImages(memberDetails.hsImage1);
                            checkHSimage.HsLi2 = UploadHSImages(memberDetails.hsImage2);
                            checkHSimage.HsLi3 = UploadHSImages(memberDetails.hsImage3);
                            checkHSimage.HsLi4 = UploadHSImages(memberDetails.hsImage4);
                            checkHSimage.HsLi5 = UploadHSImages(memberDetails.hsImage5);
                            checkHSimage.HsLi6 = UploadHSImages(memberDetails.hsImage6);
                            checkHSimage.HsLi7 = UploadHSImages(memberDetails.hsImage7);
                            checkHSimage.HsLi8 = UploadHSImages(memberDetails.hsImage8);
                            checkHSimage.HsLi9 = UploadHSImages(memberDetails.hsImage9);
                            checkHSimage.HsLi10 = UploadHSImages(memberDetails.hsImage10);
                            checkHSimage.HsRi1 = UploadHSImages(memberDetails.roomImage1);
                            checkHSimage.HsRi2 = UploadHSImages(memberDetails.roomImage2);
                            checkHSimage.HsRi3 = UploadHSImages(memberDetails.roomImage3);
                            checkHSimage.HsRi4 = UploadHSImages(memberDetails.roomImage4);
                            checkHSimage.HsRi5 = UploadHSImages(memberDetails.roomImage5);
                            checkHSimage.HsRi6 = UploadHSImages(memberDetails.roomImage6);
                            checkHSimage.HsRi7 = UploadHSImages(memberDetails.roomImage7);
                            checkHSimage.HsRi8 = UploadHSImages(memberDetails.roomImage8);
                            checkHSimage.HsRi9 = UploadHSImages(memberDetails.roomImage9);
                            checkHSimage.HsRi10 = UploadHSImages(memberDetails.roomImage10);
                            checkHSimage.HsMapLat = memberDetails.latitude == null || memberDetails.latitude=="" ? 0 : decimal.Parse(memberDetails.latitude);
                            checkHSimage.HsMapLong = memberDetails.longitude == null || memberDetails.longitude == "" ? 0 : decimal.Parse(memberDetails.longitude);
                            galleryUpdate.Add(checkHSimage);
                        }
                        else
                        {
                            var gallery = new TmHsGallery
                            {
                                HsId = Guid.NewGuid().ToString(),
                                HsLi1 = UploadHSImages(memberDetails.hsImage1),
                                HsLi2 = UploadHSImages(memberDetails.hsImage2),
                                HsLi3 = UploadHSImages(memberDetails.hsImage3),
                                HsLi4 = UploadHSImages(memberDetails.hsImage4),
                                HsLi5 = UploadHSImages(memberDetails.hsImage5),
                                HsLi6 = UploadHSImages(memberDetails.hsImage6),
                                HsLi7 = UploadHSImages(memberDetails.hsImage7),
                                HsLi8 = UploadHSImages(memberDetails.hsImage8),
                                HsLi9 = UploadHSImages(memberDetails.hsImage9),
                                HsLi10 = UploadHSImages(memberDetails.hsImage10),
                                HsRi1 = UploadHSImages(memberDetails.roomImage1),
                                HsRi2 = UploadHSImages(memberDetails.roomImage2),
                                HsRi3 = UploadHSImages(memberDetails.roomImage3),
                                HsRi4 = UploadHSImages(memberDetails.roomImage4),
                                HsRi5 = UploadHSImages(memberDetails.roomImage5),
                                HsRi6 = UploadHSImages(memberDetails.roomImage6),
                                HsRi7 = UploadHSImages(memberDetails.roomImage7),
                                HsRi8 = UploadHSImages(memberDetails.roomImage8),
                                HsRi9 = UploadHSImages(memberDetails.roomImage9),
                                HsRi10 = UploadHSImages(memberDetails.roomImage10),

                                HsMapLat = memberDetails.latitude == null || memberDetails.latitude == "" ? 0 : decimal.Parse(memberDetails.latitude),
                                HsMapLong = memberDetails.longitude == null || memberDetails.longitude == "" ? 0 : decimal.Parse(memberDetails.longitude),
                            };
                            galleryAdd.Add(gallery);
                        }

                        if (galleryUpdate.Count > 0)
                            _context.TmHsGallery.UpdateRange(galleryUpdate);
                        if (galleryAdd.Count > 0)
                            _context.TmHsGallery.AddRange(galleryAdd);

                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }
                    apiResponse.Msg = "HS Details Save successfully";
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
        //Room Details Save
        [HttpPost("roomsave")]
        public async Task<IActionResult> HSRoomSave([FromBody] HSMemberDetailsRequestModel memberDetails)
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
                        RoomDetails[] roomDtlList = JsonConvert.DeserializeObject<RoomDetails[]>(memberDetails.roomDetailsList.ToString());
                        var roomListAdd = new List<TmHsRooms>();
                        var roomListUpdate = new List<TmHsRooms>();


                        int i = 0;
                        if (memberDetails.roomImageStrings == null)
                            memberDetails.roomImageStrings = "";

                        var base64Images = memberDetails.roomImageStrings.Split("@@@");
                        string uniqueFileName = "";

                        foreach (var item in roomDtlList)
                        {
                            if (item.roomNo != null && item.roomNo != "")
                            {
                                RoomFacility[] facilities = JsonConvert.DeserializeObject<RoomFacility[]>(item.roomFacility.ToString());
                                var checkRoom = _context.TmHsRooms.Where(m => m.HsId == memberDetails.hsId && m.HsRoomNo.ToString() == item.roomNo.ToString()).FirstOrDefault();
                                if (checkRoom != null)
                                {
                                    //Room Image
                                    if (base64Images.Length - 1 >= i)
                                    {
                                        if (base64Images[i] != "")
                                        {
                                            string[] splitdata1 = base64Images[i].Split(",");
                                            var content = splitdata1[1];
                                            uniqueFileName = Guid.NewGuid().ToString();
                                            String path1 = Path.Combine($"{_env.ContentRootPath}\\ClientApp\\src\\assets\\");
                                            System.IO.File.WriteAllBytes(path1 + uniqueFileName + ".jpeg", Convert.FromBase64String(content));
                                            uniqueFileName = uniqueFileName + ".jpeg";
                                        }

                                    }
                                    else
                                    {
                                        uniqueFileName = "";
                                    }

                                    checkRoom.HsRoomNo = byte.Parse(item.roomNo);
                                    checkRoom.HsRoomCategoryId = item.roomCategory;
                                    checkRoom.HsRoomRate = Int32.Parse(item.roomRate);
                                    checkRoom.HsRoomFloor = item.roomFloor;
                                    checkRoom.HsRoomNoBeds = byte.Parse(item.noOfBeds);
                                    checkRoom.HsRoomSize = item.roomSize;
                                    checkRoom.HsRoomImage = uniqueFileName;
                                    checkRoom.HsRoomAvailable = memberDetails.isAvailable == "Yes" ? (short)1 : (short)0;
                                    checkRoom.ModifiedBy = "f1fb5130-0192-11ec-8831-005056a4479e";
                                    checkRoom.ModifiedOn = DateTime.Now;
                                    checkRoom.IsAvailable = item.isAvailable == "Yes" ? (short)1 : (short)0;
                                    var totalFacility = facilities.Count();
                                    if (1 <= totalFacility)
                                        checkRoom.HsRoomFacility1 = facilities[0].hsFacilityId;
                                    if (2 <= totalFacility)
                                        checkRoom.HsRoomFacility2 = facilities[1].hsFacilityId;
                                    if (3 <= totalFacility)
                                        checkRoom.HsRoomFacility3 = facilities[2].hsFacilityId;
                                    if (4 <= totalFacility)
                                        checkRoom.HsRoomFacility4 = facilities[3].hsFacilityId;
                                    if (5 <= totalFacility)
                                        checkRoom.HsRoomFacility5 = facilities[4].hsFacilityId;
                                    if (6 <= totalFacility)
                                        checkRoom.HsRoomFacility6 = facilities[5].hsFacilityId;
                                    if (7 <= totalFacility)
                                        checkRoom.HsRoomFacility7 = facilities[6].hsFacilityId;
                                    if (8 <= totalFacility)
                                        checkRoom.HsRoomFacility8 = facilities[7].hsFacilityId;
                                    if (9 <= totalFacility)
                                        checkRoom.HsRoomFacility9 = facilities[8].hsFacilityId;
                                    if (10 <= totalFacility)
                                        checkRoom.HsRoomFacility10 = facilities[9].hsFacilityId;
                                    if (11 <= totalFacility)
                                        checkRoom.HsRoomFacility11 = facilities[10].hsFacilityId;
                                    if (12 <= totalFacility)
                                        checkRoom.HsRoomFacility12 = facilities[11].hsFacilityId;
                                    if (13 <= totalFacility)
                                        checkRoom.HsRoomFacility13 = facilities[12].hsFacilityId;
                                    if (14 <= totalFacility)
                                        checkRoom.HsRoomFacility14 = facilities[13].hsFacilityId;
                                    if (15 <= totalFacility)
                                        checkRoom.HsRoomFacility15 = facilities[14].hsFacilityId;
                                    roomListUpdate.Add(checkRoom);
                                }
                                else
                                {
                                    var webRoot = _env.ContentRootPath;
                                    //Room Image
                                    if (base64Images.Length - 1 >= i)
                                    {
                                        if (base64Images[i] != "")
                                        {
                                            string[] splitdata1 = base64Images[i].Split(",");
                                            var content = splitdata1[1];
                                            uniqueFileName = Guid.NewGuid().ToString();
                                            String path1 = Path.Combine($"{_env.ContentRootPath}\\ClientApp\\src\\assets\\");
                                            System.IO.File.WriteAllBytes(path1 + uniqueFileName + ".jpeg", Convert.FromBase64String(content));
                                            uniqueFileName = uniqueFileName + ".jpeg";
                                        }

                                    }
                                    else
                                    {
                                        uniqueFileName = "";
                                    }

                                    var room = new TmHsRooms
                                    {
                                        HsId = memberDetails.hsId,
                                        HsRoomNo = byte.Parse(item.roomNo),
                                        HsRoomCategoryId = item.roomCategory,
                                        HsRoomRate = Int32.Parse(item.roomRate),
                                        HsRoomFloor = item.roomFloor,
                                        HsRoomNoBeds = byte.Parse(item.noOfBeds),
                                        HsRoomSize = item.roomSize,
                                        HsRoomImage = uniqueFileName,
                                        HsRoomAvailable = memberDetails.isAvailable == "Yes" ? (short)1 : (short)0,
                                        CreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e",
                                        CreatedOn = DateTime.Now,
                                        IsAvailable = 1,

                                    };
                                    var totalFacility = facilities.Count();
                                    if (1 <= totalFacility)
                                        room.HsRoomFacility1 = facilities[0].hsFacilityId;
                                    if (2 <= totalFacility)
                                        room.HsRoomFacility2 = facilities[1].hsFacilityId;
                                    if (3 <= totalFacility)
                                        room.HsRoomFacility3 = facilities[2].hsFacilityId;
                                    if (4 <= totalFacility)
                                        room.HsRoomFacility4 = facilities[3].hsFacilityId;
                                    if (5 <= totalFacility)
                                        room.HsRoomFacility5 = facilities[4].hsFacilityId;
                                    if (6 <= totalFacility)
                                        room.HsRoomFacility6 = facilities[5].hsFacilityId;
                                    if (7 <= totalFacility)
                                        room.HsRoomFacility7 = facilities[6].hsFacilityId;
                                    if (8 <= totalFacility)
                                        room.HsRoomFacility8 = facilities[7].hsFacilityId;
                                    if (9 <= totalFacility)
                                        room.HsRoomFacility9 = facilities[8].hsFacilityId;
                                    if (10 <= totalFacility)
                                        room.HsRoomFacility10 = facilities[9].hsFacilityId;
                                    if (11 <= totalFacility)
                                        room.HsRoomFacility11 = facilities[10].hsFacilityId;
                                    if (12 <= totalFacility)
                                        room.HsRoomFacility12 = facilities[11].hsFacilityId;
                                    if (13 <= totalFacility)
                                        room.HsRoomFacility13 = facilities[12].hsFacilityId;
                                    if (14 <= totalFacility)
                                        room.HsRoomFacility14 = facilities[13].hsFacilityId;
                                    if (15 <= totalFacility)
                                        room.HsRoomFacility15 = facilities[14].hsFacilityId;

                                    roomListAdd.Add(room);
                                }
                            }

                            i = i + 1;
                        }
                        if (roomListAdd.Count > 0)
                            _context.TmHsRooms.AddRange(roomListAdd);
                        if (roomListUpdate.Count > 0)
                            _context.TmHsRooms.UpdateRange(roomListUpdate);

                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }
                    apiResponse.Msg = "HS Details Save successfully";
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

        [HttpGet("getHSDtl/{id}")]
        public async Task<IActionResult> GetHSDtl(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var GetHSDtl = await _context.TmHomestay.Where(m => m.UserId == id).ToListAsync();
                apiResponse.Data = GetHSDtl;
                apiResponse.Msg = "HomeStay Details";
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
