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
    public class RoomCategoryController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public RoomCategoryController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoomCategoryList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var roomCatList = await(from a in _context.TmHsRoomCategory
                                        select new RoomCategoryResponseModel
                                        {
                                            categoryId = a.HsCategoryId,
                                            categoryName = a.HsCategoryName
                                        }).ToListAsync();
                apiResponse.Data = roomCatList;
                apiResponse.Msg = "Displaying Room Category List";
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
        public async Task<IActionResult> GetRoomCategoryById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var roomCatDet = await _context.TmHsRoomCategory.Where(m => m.HsCategoryId == id).FirstOrDefaultAsync();
                if (roomCatDet == null)
                {
                    apiResponse.Msg = "Room Category not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    RoomCategoryResponseModel roomCategoryResponse = new RoomCategoryResponseModel();
                    roomCategoryResponse.categoryId = roomCatDet.HsCategoryId;
                    roomCategoryResponse.categoryName = roomCatDet.HsCategoryName;

                    apiResponse.Data = roomCategoryResponse;
                    apiResponse.Msg = "Displaying Room Category";
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
        public async Task<IActionResult> CreateRoomCategory([FromBody] RoomCategoryAddRequestModel roomCategoryAddRequest)//This method will insert the Room Category into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };

            try
            {
                var duplicateRoomCategory = await _context.TmHsRoomCategory.Where(m => m.HsCategoryName == roomCategoryAddRequest.categoryName).CountAsync();
                if (duplicateRoomCategory > 0)
                {
                    apiResponse.Msg = "Duplicate Room Category";
                    apiResponse.Result = ResponseTypes.Error;
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            var roomCategory = new TmHsRoomCategory
                            {
                                HsCategoryId = Guid.NewGuid().ToString(),
                                HsCategoryName = roomCategoryAddRequest.categoryName
                            };
                            _context.TmHsRoomCategory.Add(roomCategory);

                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                        }
                        apiResponse.Msg = "Room Category added successfully";
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

        [HttpPut]
        public async Task<IActionResult> UpdateRoomCategory(RoomCategoryAddRequestModel roomCategoryAddRequest)//Update method will update the country  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var roomCatDet = await _context.TmHsRoomCategory.Where(m => m.HsCategoryId == roomCategoryAddRequest.categoryId).FirstOrDefaultAsync();
                var duplicateRoomCat = await _context.TmHsRoomCategory.Where(m => m.HsCategoryName == roomCategoryAddRequest.categoryName && m.HsCategoryId != roomCategoryAddRequest.categoryId).CountAsync();

                if (roomCatDet == null)
                {
                    apiResponse.Msg = "Room Category not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    if (duplicateRoomCat > 0)
                    {
                        apiResponse.Msg = "Duplicate Room Category";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            using (var tran = await _context.Database.BeginTransactionAsync())
                            {
                                roomCatDet.HsCategoryName = roomCategoryAddRequest.categoryName;

                                _context.TmHsRoomCategory.Update(roomCatDet);
                                await _context.SaveChangesAsync();
                                await tran.CommitAsync();
                            }

                            apiResponse.Msg = "Room Category updated successfully";
                            apiResponse.Result = ResponseTypes.Success;
                        }
                        else
                        {
                            apiResponse.Msg = "Model State not valid";
                            apiResponse.Result = ResponseTypes.ModelErr;
                        }
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
