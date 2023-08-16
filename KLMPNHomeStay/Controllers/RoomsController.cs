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
    public class RoomsController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public RoomsController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomList()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var roomList = await (from a in _context.TmHsRooms.Include(m=>m.HsRoomCategory)
                                         select new RoomsResponseModel
                                         {
                                             HsId = a.HsId,
                                             HsRoomNo = a.HsRoomNo,
                                             HsRoomCategoryId = a.HsRoomCategoryId,
                                             CategoryName = a.HsRoomCategory.HsCategoryName,
                                             HsRoomRate = a.HsRoomRate,
                                             HsRoomFloor = a.HsRoomFloor,
                                             HsRoomNoBeds = a.HsRoomNoBeds,
                                             HsRoomSize = a.HsRoomSize,
                                             HsRoomImage = a.HsRoomImage,
                                             HsRoomFacility1 = a.HsRoomFacility1,
                                             HsRoomFacility2 = a.HsRoomFacility2,
                                             HsRoomFacility3 = a.HsRoomFacility3,
                                             HsRoomFacility4 = a.HsRoomFacility4,
                                             HsRoomFacility5 = a.HsRoomFacility5,
                                             HsRoomFacility6 = a.HsRoomFacility6,
                                             HsRoomFacility7 = a.HsRoomFacility7,
                                             HsRoomFacility8 = a.HsRoomFacility8,
                                             HsRoomFacility9 = a.HsRoomFacility9,
                                             HsRoomFacility10 = a.HsRoomFacility10,
                                             HsRoomFacility11 = a.HsRoomFacility11,
                                             HsRoomFacility12 = a.HsRoomFacility12,
                                             HsRoomFacility13 = a.HsRoomFacility13,
                                             HsRoomFacility14 = a.HsRoomFacility14,
                                             HsRoomFacility15 = a.HsRoomFacility15,
                                             HsRoomAvailable = a.HsRoomAvailable,
                                             IsAvailable = a.IsAvailable

                                         }).ToListAsync();
                apiResponse.Data = roomList;
                apiResponse.Msg = "Displaying Rooms List";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("GetRoomsById/{id}")]
        public async Task<IActionResult> GetRoomsById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var list = await (from roomsDet in _context.TmHsRooms.Include(m => m.HsRoomCategory).Where(m => m.HsId == id)
                               select new RoomsResponseModel
                               {
                                   HsId = roomsDet.HsId,
                                   HsRoomNo = roomsDet.HsRoomNo,
                                   HsRoomCategoryId = roomsDet.HsRoomCategoryId,
                                    CategoryName = roomsDet.HsRoomCategory.HsCategoryName,
                                    HsRoomRate = roomsDet.HsRoomRate,
                                    HsRoomFloor = roomsDet.HsRoomFloor,
                                    HsRoomNoBeds = roomsDet.HsRoomNoBeds,
                                    HsRoomSize = roomsDet.HsRoomSize,
                                    HsRoomImage = roomsDet.HsRoomImage,
                                    HsRoomFacility1 = roomsDet.HsRoomFacility1,
                                    HsRoomFacility2 = roomsDet.HsRoomFacility2,
                                    HsRoomFacility3 = roomsDet.HsRoomFacility3,
                                    HsRoomFacility4 = roomsDet.HsRoomFacility4,
                                    HsRoomFacility5 = roomsDet.HsRoomFacility5,
                                    HsRoomFacility6 = roomsDet.HsRoomFacility6,
                                    HsRoomFacility7 = roomsDet.HsRoomFacility7,
                                    HsRoomFacility8 = roomsDet.HsRoomFacility8,
                                    HsRoomFacility9 = roomsDet.HsRoomFacility9,
                                    HsRoomFacility10 = roomsDet.HsRoomFacility10,
                                    HsRoomFacility11 = roomsDet.HsRoomFacility11,
                                    HsRoomFacility12 = roomsDet.HsRoomFacility12,
                                    HsRoomFacility13 = roomsDet.HsRoomFacility13,
                                    HsRoomFacility14 = roomsDet.HsRoomFacility14,
                                    HsRoomFacility15 = roomsDet.HsRoomFacility15,
                                    HsRoomAvailable = roomsDet.HsRoomAvailable,
                                    IsAvailable = roomsDet.IsAvailable,
                             }).ToListAsync();
                if (list == null)
                {
                    apiResponse.Msg = "Room not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    apiResponse.Data = list;
                    apiResponse.Msg = "Displaying Rooms";
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


        [HttpGet("GetByHSandRoomid/{id}")]
        public async Task<IActionResult> GetByHSandRoomid(IdDtls obj)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var list = await (from roomsDet in _context.TmHsRooms.Include(m => m.HsRoomCategory).Where(m => m.HsId == obj.hsId && m.HsRoomNo.ToString()==obj.roomNo)
                                  select new RoomsResponseModel
                                  {
                                      HsId = roomsDet.HsId,
                                      HsRoomNo = roomsDet.HsRoomNo,
                                      HsRoomCategoryId = roomsDet.HsRoomCategoryId,
                                      CategoryName = roomsDet.HsRoomCategory.HsCategoryName,
                                      HsRoomRate = roomsDet.HsRoomRate,
                                      HsRoomFloor = roomsDet.HsRoomFloor,
                                      HsRoomNoBeds = roomsDet.HsRoomNoBeds,
                                      HsRoomSize = roomsDet.HsRoomSize,
                                      HsRoomImage = roomsDet.HsRoomImage,
                                      HsRoomFacility1 = roomsDet.HsRoomFacility1,
                                      HsRoomFacility2 = roomsDet.HsRoomFacility2,
                                      HsRoomFacility3 = roomsDet.HsRoomFacility3,
                                      HsRoomFacility4 = roomsDet.HsRoomFacility4,
                                      HsRoomFacility5 = roomsDet.HsRoomFacility5,
                                      HsRoomFacility6 = roomsDet.HsRoomFacility6,
                                      HsRoomFacility7 = roomsDet.HsRoomFacility7,
                                      HsRoomFacility8 = roomsDet.HsRoomFacility8,
                                      HsRoomFacility9 = roomsDet.HsRoomFacility9,
                                      HsRoomFacility10 = roomsDet.HsRoomFacility10,
                                      HsRoomFacility11 = roomsDet.HsRoomFacility11,
                                      HsRoomFacility12 = roomsDet.HsRoomFacility12,
                                      HsRoomFacility13 = roomsDet.HsRoomFacility13,
                                      HsRoomFacility14 = roomsDet.HsRoomFacility14,
                                      HsRoomFacility15 = roomsDet.HsRoomFacility15,
                                      HsRoomAvailable = roomsDet.HsRoomAvailable,
                                      IsAvailable = roomsDet.IsAvailable,
                                  }).ToListAsync();
                if (list == null)
                {
                    apiResponse.Msg = "Room not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    apiResponse.Data = list;
                    apiResponse.Msg = "Displaying Room";
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
        public async Task<IActionResult> CreateRooms([FromBody] RoomsRequestModel roomsAddRequest)//This method will insert the Room Category into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };

            try
            {
                using (var tran = await _context.Database.BeginTransactionAsync())
                {
                    var rooms = new TmHsRooms
                    {
                        HsId = roomsAddRequest.HsId,
                        HsRoomNo = roomsAddRequest.HsRoomNo,
                        HsRoomCategoryId = roomsAddRequest.HsRoomCategoryId,
                        HsRoomRate = roomsAddRequest.HsRoomRate,
                        HsRoomFloor = roomsAddRequest.HsRoomFloor,
                        HsRoomNoBeds = roomsAddRequest.HsRoomNoBeds,
                        HsRoomSize = roomsAddRequest.HsRoomSize,
                        HsRoomImage = roomsAddRequest.HsRoomImage,
                        HsRoomFacility1 = roomsAddRequest.HsRoomFacility1,
                        HsRoomFacility2 = roomsAddRequest.HsRoomFacility2,
                        HsRoomFacility3 = roomsAddRequest.HsRoomFacility3,
                        HsRoomFacility4 = roomsAddRequest.HsRoomFacility4,
                        HsRoomFacility5 = roomsAddRequest.HsRoomFacility5,
                        HsRoomFacility6 = roomsAddRequest.HsRoomFacility6,
                        HsRoomFacility7 = roomsAddRequest.HsRoomFacility7,
                        HsRoomFacility8 = roomsAddRequest.HsRoomFacility8,
                        HsRoomFacility9 = roomsAddRequest.HsRoomFacility9,
                        HsRoomFacility10 = roomsAddRequest.HsRoomFacility10,
                        HsRoomFacility11 = roomsAddRequest.HsRoomFacility11,
                        HsRoomFacility12 = roomsAddRequest.HsRoomFacility12,
                        HsRoomFacility13 = roomsAddRequest.HsRoomFacility13,
                        HsRoomFacility14 = roomsAddRequest.HsRoomFacility14,
                        HsRoomFacility15 = roomsAddRequest.HsRoomFacility15,
                        HsRoomAvailable = roomsAddRequest.HsRoomAvailable,
                        IsAvailable = roomsAddRequest.IsAvailable,
                        CreatedOn = DateTime.Now,
                        CreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e"
                    };
                    _context.TmHsRooms.Add(rooms);

                    await _context.SaveChangesAsync();
                    await tran.CommitAsync();
                }
                apiResponse.Msg = "Room added successfully";
                apiResponse.Result = ResponseTypes.Success;

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRooms(RoomsRequestModel roomUpdateRequest)//Update method will update the country  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var roomDet = await _context.TmHsRooms.Where(m => m.HsId == roomUpdateRequest.HsId).FirstOrDefaultAsync();
                if (roomDet == null)
                {
                    apiResponse.Msg = "Room not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                        roomDet.HsRoomNo = roomUpdateRequest.HsRoomNo;
                        roomDet.HsRoomCategoryId = roomUpdateRequest.HsRoomCategoryId;
                        roomDet.HsRoomRate = roomUpdateRequest.HsRoomRate;
                        roomDet.HsRoomFloor = roomUpdateRequest.HsRoomFloor;
                        roomDet.HsRoomNoBeds = roomUpdateRequest.HsRoomNoBeds;
                        roomDet.HsRoomSize = roomUpdateRequest.HsRoomSize;
                        roomDet.HsRoomImage = roomUpdateRequest.HsRoomImage;
                        roomDet.HsRoomFacility1 = roomUpdateRequest.HsRoomFacility1;
                        roomDet.HsRoomFacility2 = roomUpdateRequest.HsRoomFacility2;
                        roomDet.HsRoomFacility3 = roomUpdateRequest.HsRoomFacility3;
                        roomDet.HsRoomFacility4 = roomUpdateRequest.HsRoomFacility4;
                        roomDet.HsRoomFacility5 = roomUpdateRequest.HsRoomFacility5;
                        roomDet.HsRoomFacility6 = roomUpdateRequest.HsRoomFacility6;
                        roomDet.HsRoomFacility7 = roomUpdateRequest.HsRoomFacility7;
                        roomDet.HsRoomFacility8 = roomUpdateRequest.HsRoomFacility8;
                        roomDet.HsRoomFacility9 = roomUpdateRequest.HsRoomFacility9;
                        roomDet.HsRoomFacility10 = roomUpdateRequest.HsRoomFacility10;
                        roomDet.HsRoomFacility11 = roomUpdateRequest.HsRoomFacility11;
                        roomDet.HsRoomFacility12 = roomUpdateRequest.HsRoomFacility12;
                        roomDet.HsRoomFacility13 = roomUpdateRequest.HsRoomFacility13;
                        roomDet.HsRoomFacility14 = roomUpdateRequest.HsRoomFacility14;
                        roomDet.HsRoomFacility15 = roomUpdateRequest.HsRoomFacility15;
                        roomDet.HsRoomAvailable = roomUpdateRequest.HsRoomAvailable;
                        roomDet.IsAvailable = roomUpdateRequest.IsAvailable;
                        roomDet.ModifiedOn = DateTime.Now;
                        roomDet.ModifiedBy = "f1fb5130-0192-11ec-8831-005056a4479e";

                        _context.TmHsRooms.Update(roomDet);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }

                    apiResponse.Msg = "Room updated successfully";
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
    }
}
