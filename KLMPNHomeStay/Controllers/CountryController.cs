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

    public class CountryController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public CountryController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountryList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                //var countryList = await _context.TmCountry.AsNoTracking().ToListAsync();
                var countryList = await (from a in _context.TmCountry
                                         select new CountryResponseModel
                                         {
                                             countryId = a.CountryId,
                                             countryName = a.CountryName,
                                             countryCode = a.CountryCode,
                                             isActive = a.IsActive
                                         }).ToListAsync();
                apiResponse.Data = countryList;
                apiResponse.Msg = "Displaying Country List";
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
        public async Task<IActionResult> GetCountryById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var countryDet = await _context.TmCountry.Where(m => m.CountryId == id).FirstOrDefaultAsync();
               
                if (countryDet == null)
                {
                    apiResponse.Msg = "Country not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    CountryResponseModel countryResponse = new CountryResponseModel();
                    countryResponse.countryId = countryDet.CountryId;
                    countryResponse.countryName = countryDet.CountryName;
                    countryResponse.countryCode = countryDet.CountryCode;
                    countryResponse.isActive = countryDet.IsActive;

                    apiResponse.Data = countryResponse;
                    apiResponse.Msg = "Displaying Country";
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
        public async Task<IActionResult> CreateCountry([FromBody] CountryAddRequestModel countryAddRequest)//This method will insert the country into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };

            try
            {
                var duplicateCountryCode = await _context.TmCountry.Where(m => m.CountryCode == countryAddRequest.countryCode).CountAsync();
                var duplicateCountryName = await _context.TmCountry.Where(m => m.CountryName == countryAddRequest.countryName).CountAsync();

                if (duplicateCountryCode > 0)
                {
                    apiResponse.Msg = "Duplicate Country Code";
                    apiResponse.Result = ResponseTypes.Error;
                }
                else
                {
                    if (duplicateCountryName > 0)
                    {
                        apiResponse.Msg = "Duplicate Country Name";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            using (var tran = await _context.Database.BeginTransactionAsync())
                            {
                                var country = new TmCountry
                                {
                                    CountryId = Guid.NewGuid().ToString(),
                                    CountryName = countryAddRequest.countryName,
                                    CountryCode = countryAddRequest.countryCode,
                                    IsActive = countryAddRequest.isActive,
                                    //CreatedBy = userId.ToString(),
                                    CreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e",
                                    CreatedOn = DateTime.Now
                                };
                                _context.TmCountry.Add(country);
                                await _context.SaveChangesAsync();
                                await tran.CommitAsync();
                            }
                            apiResponse.Msg = "Country added successfully";
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

        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCountry(CountryAddRequestModel countryAddRequest)//Update method will update the country  
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var duplicateCountryCode = await _context.TmCountry.Where(m => m.CountryCode == countryAddRequest.countryCode && m.CountryId != countryAddRequest.countryId).CountAsync();
                var duplicateCountryName = await _context.TmCountry.Where(m => m.CountryName == countryAddRequest.countryName && m.CountryId != countryAddRequest.countryId).CountAsync();

                var countryDet = await _context.TmCountry.Where(m => m.CountryId == countryAddRequest.countryId).FirstOrDefaultAsync();
                if (countryDet == null)
                {
                    apiResponse.Msg = "Country not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {

                    if (duplicateCountryCode > 0)
                    {
                        apiResponse.Msg = "Duplicate Country Code";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                    else
                    {
                        if (duplicateCountryName > 0)
                        {
                            apiResponse.Msg = "Duplicate Country Name";
                            apiResponse.Result = ResponseTypes.Error;
                        }
                        else
                        {
                            if (ModelState.IsValid)
                            {
                                using (var tran = await _context.Database.BeginTransactionAsync())
                                {
                                    countryDet.CountryName = countryAddRequest.countryName;
                                    countryDet.CountryCode = countryAddRequest.countryCode;
                                    countryDet.IsActive = countryAddRequest.isActive;
                                    //countryDet.ModifiedBy = userId.ToString();
                                    countryDet.ModifiedBy = "f1fb5130-0192-11ec-8831-005056a4479e";
                                    countryDet.ModifiedOn = DateTime.Now;

                                    _context.TmCountry.Update(countryDet);
                                    await _context.SaveChangesAsync();
                                    await tran.CommitAsync();
                                }

                                apiResponse.Msg = "Country updated successfully";
                                apiResponse.Result = ResponseTypes.Success;
                            }
                            else
                            {
                                apiResponse.Msg = "Model State not valid";
                                apiResponse.Result = ResponseTypes.ModelErr;
                            }
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
