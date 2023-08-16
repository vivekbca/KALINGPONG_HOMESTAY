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
    public class TMBlockController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public TMBlockController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTMBlockList()//This is the get method for list 
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var tmblockList = await _context.TmBlock.AsNoTracking().ToListAsync();
                apiResponse.Data = tmblockList;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTMBlockById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var tmblockDet = await _context.TmBlock.Where(m => m.BlockId == id).FirstOrDefaultAsync();
                if (tmblockDet == null)
                {
                    apiResponse.Msg = "Block not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    TMBlockResponseModel tmblockResponse = new TMBlockResponseModel();
                    tmblockResponse.BlockId = tmblockDet.BlockId;
                    tmblockResponse.BlockName = tmblockDet.BlockName;
                    tmblockResponse.BlockCode = tmblockDet.BlockCode;
                    tmblockResponse.IsActive = tmblockDet.IsActive;

                    apiResponse.Data = tmblockResponse;
                    apiResponse.Msg = "Displaying Block";
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
        public async Task<IActionResult> CreateTMBlock([FromBody] TMBlockRequestModel tmblockAddRequest)//This method will insert the Block into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
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
                        var allBlockcodes = await _context.TmBlock.Where(m => m.BlockCode == tmblockAddRequest.BlockCode).ToListAsync();
                        var allBlockNames = await _context.TmBlock.Where(m => m.BlockName == tmblockAddRequest.BlockName).ToListAsync();
                        if (allBlockcodes.Count != 0)
                        {
                            apiResponse.Msg = "Block Code Already Exist!!";
                            ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                            return Ok(apiResponseError);
                        }
                        if (allBlockNames.Count != 0)
                        {
                            apiResponse.Msg = "Block Name Already Exist!!";
                            ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                            return Ok(apiResponseError);
                        }
                        var tmblock = new TmBlock
                        {
                            BlockId = Guid.NewGuid().ToString(),
                            BlockCode = tmblockAddRequest.BlockCode,
                            BlockName = tmblockAddRequest.BlockName,
                            IsActive = tmblockAddRequest.IsActive,
                            CountryId = tmblockAddRequest.CountryId,
                            StateId = tmblockAddRequest.StateId,
                            DistrictId = tmblockAddRequest.DistrictId,
                            //CreatedBy = userId.ToString(),
                            CreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e",
                            CreatedOn = DateTime.Now
                        };
                        _context.TmBlock.Add(tmblock);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }
                    apiResponse.Msg = "Block added successfully";
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
        [HttpPut]
        public async Task<IActionResult> UpdateTMBlock(TMBlockRequestModel tmblockAddRequest)//Update method will update the country  
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
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
                    var tmblockDet = await _context.TmBlock.Where(m => m.BlockId == tmblockAddRequest.BlockId).FirstOrDefaultAsync();
                    if (tmblockDet == null)
                    {
                        apiResponse.Msg = "Block not found";
                        apiResponse.Result = ResponseTypes.Info;
                    }
                    else
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            var allBlockcodes = await _context.TmBlock.Where(m => m.BlockCode == tmblockAddRequest.BlockCode && m.BlockId!=tmblockAddRequest.BlockId).ToListAsync();
                            var allBlockNames = await _context.TmBlock.Where(m => m.BlockName == tmblockAddRequest.BlockName && m.BlockId != tmblockAddRequest.BlockId).ToListAsync();
                            if (allBlockcodes.Count != 0)
                            {
                                apiResponse.Msg = "Block Code Already Exist!!";
                                ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                                return Ok(apiResponseError);
                            }
                            if (allBlockNames.Count != 0)
                            {
                                apiResponse.Msg = "Block Name Already Exist!!";
                                ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                                return Ok(apiResponseError);
                            }
                            tmblockDet.BlockName = tmblockAddRequest.BlockName;
                            tmblockDet.BlockCode = tmblockAddRequest.BlockCode;
                            tmblockDet.IsActive = tmblockAddRequest.IsActive;
                            //countryDet.ModifiedBy = userId.ToString();
                            tmblockDet.ModifiedBy = "f1fb5130-0192-11ec-8831-005056a4479e";
                            tmblockDet.ModifiedOn = DateTime.Now;

                            _context.TmBlock.Update(tmblockDet);
                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                        }

                        apiResponse.Msg = "Block updated successfully";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                   
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
