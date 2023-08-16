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
    public class FacilityController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public FacilityController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetFacilityList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var facilityList = await _context.TmHsFacilities.AsNoTracking().ToListAsync();
                apiResponse.Data = facilityList;
                apiResponse.Msg = "Displaying Facility List";
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
        public async Task<IActionResult> GetFacilityById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var facilityDet = await _context.TmHsFacilities.Where(m => m.HsFacilityId == id).FirstOrDefaultAsync();
                if (facilityDet == null)
                {
                    apiResponse.Msg = "Facility not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    FacilityResponseModel facilityResponseModel = new FacilityResponseModel();
                    facilityResponseModel.facilityId = facilityDet.HsFacilityId;
                    facilityResponseModel.facilityName = facilityDet.HsFacilityName;

                    apiResponse.Data = facilityResponseModel;
                    apiResponse.Msg = "Displaying Facility";
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
        public async Task<IActionResult> CreateFacility([FromBody] FacilityAddRequestModel facilityAddRequestModel)//This method will insert the facility into db  {
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };

            try
            {
                var duplicateFacility = await _context.TmHsFacilities.Where(m => m.HsFacilityName == facilityAddRequestModel.facilityName).CountAsync();
                if (duplicateFacility > 0)
                {
                    apiResponse.Msg = "Duplicate Facility";
                    apiResponse.Result = ResponseTypes.Error;
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            var facility = new TmHsFacilities
                            {
                                HsFacilityId = Guid.NewGuid().ToString(),
                                HsFacilityName = facilityAddRequestModel.facilityName
                            };
                            _context.TmHsFacilities.Add(facility);
                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                        }
                        apiResponse.Msg = "Facility added successfully";
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

        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateFacility(FacilityAddRequestModel facilityAddRequestModel)//Update method will update the state  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var facilityDet = await _context.TmHsFacilities.Where(m => m.HsFacilityId == facilityAddRequestModel.facilityId).FirstOrDefaultAsync();
                var duplicateFacility = await _context.TmHsFacilities.Where(m => m.HsFacilityName == facilityAddRequestModel.facilityName && m.HsFacilityId != facilityAddRequestModel.facilityId).CountAsync();

                if (facilityDet == null)
                {
                    apiResponse.Msg = "Facility not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    if (duplicateFacility > 0)
                    {
                        apiResponse.Msg = "Duplicate Facility";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            using (var tran = await _context.Database.BeginTransactionAsync())
                            {
                                facilityDet.HsFacilityName = facilityAddRequestModel.facilityName;

                                _context.TmHsFacilities.Update(facilityDet);
                                await _context.SaveChangesAsync();
                                await tran.CommitAsync();
                            }
                            apiResponse.Msg = "Facility updated successfully";
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
