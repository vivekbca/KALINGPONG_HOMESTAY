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
   
    public class DistrictController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public DistrictController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDistrictList()//This is the get method for list
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var districtList = await _context.TmDistrict.AsNoTracking().ToListAsync();
                apiResponse.Data = districtList;
                apiResponse.Msg = "Displaying District List";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistrictById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var districtDet = await _context.TmDistrict.Where(m => m.DistrictId == id).FirstOrDefaultAsync();
                if (districtDet == null)
                {
                    apiResponse.Msg = "District not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    DistrictResponseModel districtResponse = new DistrictResponseModel();
                    districtResponse.DistrictId = districtDet.DistrictId;
                    districtResponse.DistrictName = districtDet.DistrictName;
                    districtResponse.DistrictCode = districtDet.DistrictCode;
                    districtResponse.IsActive = districtDet.IsActive;
                    apiResponse.Data = districtResponse;
                    apiResponse.Msg = "Displaying District";
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
        public async Task<IActionResult> CreateDistrict([FromBody] DistrictAddRequestModel districtAddRequest)//This method will insert the District into db  {
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
                    var allDistrictcodes = await _context.TmDistrict.Where(m => m.DistrictCode == districtAddRequest.DistrictCode && m.DistrictId!= districtAddRequest.DistrictId).ToListAsync();
                    var allDistrictNames = await _context.TmDistrict.Where(m => m.DistrictName == districtAddRequest.DistrictName && m.DistrictId != districtAddRequest.DistrictId).ToListAsync();
                    if (allDistrictcodes.Count != 0)
                    {
                        apiResponse.Msg = "District Code Already Exist!!";
                        ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                        return Ok(apiResponseError);
                    }
                    if (allDistrictNames.Count != 0)
                    {
                        apiResponse.Msg = "District Name Already Exist!!";
                        ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                        return Ok(apiResponseError);
                    }
                    var district = new TmDistrict
                    {
                        DistrictId = Guid.NewGuid().ToString(),
                        DistrictCode = districtAddRequest.DistrictCode,
                        DistrictName = districtAddRequest.DistrictName,
                        IsActive = districtAddRequest.IsActive,
                        CountryId = districtAddRequest.CountryId,
                        StateId = districtAddRequest.StateId,
                        //CreatedBy = userId.ToString(),
                        CreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e",
                        CreatedOn = DateTime.Now
                    };
                    _context.TmDistrict.Add(district);
                    await _context.SaveChangesAsync();
                    await tran.CommitAsync();
                }
                apiResponse.Msg = "District added successfully";
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
        public async Task<IActionResult> UpdateDistrict(DistrictAddRequestModel districtAddRequest)//Update method will update the District  
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
                    var districtDet = await _context.TmDistrict.Where(m => m.DistrictId == districtAddRequest.DistrictId).FirstOrDefaultAsync();
                if (districtDet == null)
                {
                    apiResponse.Msg = "District not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                            var allDistrictcodes = await _context.TmDistrict.Where(m => m.DistrictCode == districtAddRequest.DistrictCode).ToListAsync();
                            var allDistrictNames = await _context.TmDistrict.Where(m => m.DistrictName == districtAddRequest.DistrictName).ToListAsync();
                            if (allDistrictcodes.Count != 0)
                            {
                                apiResponse.Msg = "District Code Already Exist!!";
                                ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                                return Ok(apiResponseError);
                            }
                            if (allDistrictNames.Count != 0)
                            {
                                apiResponse.Msg = "District Name Already Exist!!";
                                ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                                return Ok(apiResponseError);
                            }
                            districtDet.DistrictName = districtAddRequest.DistrictName;
                        districtDet.DistrictCode = districtAddRequest.DistrictCode;
                        districtDet.IsActive = districtAddRequest.IsActive;
                        //countryDet.ModifiedBy = userId.ToString();
                        districtDet.ModifiedBy = "f1fb5130-0192-11ec-8831-005056a4479e";
                        districtDet.ModifiedOn = DateTime.Now;

                        _context.TmDistrict.Update(districtDet);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }

                    apiResponse.Msg = "District updated successfully";
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
