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
    public class StateController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public StateController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStateList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var stateList = await (from a in _context.TmState
                                        join b in _context.TmCountry on a.CountryId equals b.CountryId
                                        select new StateResponseModel
                                        {
                                            stateId = a.StateId,
                                            stateCode = a.StateCode,
                                            stateName = a.StateName,
                                            countryId = a.CountryId,
                                            countryName = b.CountryName,
                                            isActive = a.IsActive
                                        }).ToListAsync();

                apiResponse.Data = stateList;
                apiResponse.Msg = "Displaying State List";
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
        public async Task<IActionResult> GetStateById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var stateDet = await _context.TmState.Where(m => m.StateId == id).Include(m => m.Country).FirstOrDefaultAsync();
                if (stateDet == null)
                {
                    apiResponse.Msg = "State not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    StateResponseModel stateResponseModel = new StateResponseModel();
                    stateResponseModel.stateId = stateDet.StateId;
                    stateResponseModel.stateCode = stateDet.StateCode;
                    stateResponseModel.stateName = stateDet.StateName;
                    stateResponseModel.isActive = stateDet.IsActive;
                    stateResponseModel.countryId = stateDet.CountryId;
                    stateResponseModel.countryName = stateDet.Country.CountryName;

                    apiResponse.Data = stateResponseModel;
                    apiResponse.Msg = "Displaying State";
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
        public async Task<IActionResult> CreateState([FromBody] StateAddRequestModel stateAddRequest)//This method will insert the state into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };

            try
            {
                var duplicateStateCode = await _context.TmState.Where(m => m.StateCode == stateAddRequest.stateCode).CountAsync();
                var duplicateStateName = await _context.TmState.Where(m => m.StateName == stateAddRequest.stateName).CountAsync();

                if (duplicateStateCode > 0)
                {
                    apiResponse.Msg = "Duplicate State Code";
                    apiResponse.Result = ResponseTypes.Error;
                }
                else
                {
                    if (duplicateStateName > 0)
                    {
                        apiResponse.Msg = "Duplicate State Name";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            using (var tran = await _context.Database.BeginTransactionAsync())
                            {
                                var state = new TmState
                                {
                                    StateId = Guid.NewGuid().ToString(),
                                    StateCode = stateAddRequest.stateCode,
                                    StateName = stateAddRequest.stateName,
                                    CountryId = stateAddRequest.countryId,
                                    IsActive = stateAddRequest.isActive,
                                    //CreatedBy = userId.ToString(),
                                    CreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e",
                                    CreatedOn = DateTime.Now
                                };
                                _context.TmState.Add(state);
                                await _context.SaveChangesAsync();
                                await tran.CommitAsync();
                            }
                            apiResponse.Msg = "State added successfully";
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
        public async Task<IActionResult> UpdateState(StateAddRequestModel stateAddRequestModel)//Update method will update the state  
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var duplicateStateCode = await _context.TmState.Where(m => m.StateCode == stateAddRequestModel.stateCode && m.StateId != stateAddRequestModel.stateId).CountAsync();
                var duplicateStateName = await _context.TmState.Where(m => m.StateName == stateAddRequestModel.stateName && m.StateId != stateAddRequestModel.stateId).CountAsync();

                var stateDet = await _context.TmState.Where(m => m.StateId == stateAddRequestModel.stateId).FirstOrDefaultAsync();
                if (stateDet == null)
                {
                    apiResponse.Msg = "State not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    if (duplicateStateCode > 0)
                    {
                        apiResponse.Msg = "Duplicate State Code";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                    else
                    {
                        if (duplicateStateName > 0)
                        {
                            apiResponse.Msg = "Duplicate State Name";
                            apiResponse.Result = ResponseTypes.Error;
                        }
                        else
                        {
                            if (ModelState.IsValid)
                            {
                                using (var tran = await _context.Database.BeginTransactionAsync())
                                {
                                    stateDet.StateCode = stateAddRequestModel.stateCode;
                                    stateDet.StateName = stateAddRequestModel.stateName;
                                    stateDet.IsActive = 1;
                                    stateDet.CountryId = stateAddRequestModel.countryId;
                                    //stateDet.ModifiedBy = userId.ToString();
                                    stateDet.ModifiedBy = "f1fb5130-0192-11ec-8831-005056a4479e";
                                    stateDet.ModifiedOn = DateTime.Now;

                                    _context.TmState.Update(stateDet);
                                    await _context.SaveChangesAsync();
                                    await tran.CommitAsync();
                                }
                                apiResponse.Msg = "State updated successfully";
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
