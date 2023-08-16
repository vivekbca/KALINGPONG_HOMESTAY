using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using KLMPNHomeStay.Models.Request_Model;
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
    public class UserController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public UserController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRegistration model)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var existemail =await  _context.TmUser.Where(m => m.UserEmailId == model.UserEmailId).ToListAsync();
                if (existemail.Count>0)
                {
                    apiResponse.Data = model;
                    apiResponse.Msg = "Email already exist.";
                    apiResponse.Result = ResponseTypes.Error;
                    //ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                    //return Ok(apiResponseFinal1);
                }
                if (model.UserEmailId==null)
                {
                    apiResponse.Data = model;
                    apiResponse.Msg = "Email Id is required.";
                    apiResponse.Result = ResponseTypes.Error;
                    //ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                    //return Ok(apiResponseFinal1);
                }
                if (model.UserMobileNo == null)
                {
                    apiResponse.Data = model;
                    apiResponse.Msg = "Mobile Number is required.";
                    apiResponse.Result = ResponseTypes.Error;
                    //ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                    //return Ok(apiResponseFinal1);
                }
                if (model.UserMobileNo.Length > 10|| model.UserMobileNo.Length<10)
                {
                    apiResponse.Data = model;
                    apiResponse.Msg = "Mobile Number is invalid.";
                    apiResponse.Result = ResponseTypes.Error;
                    //ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                    //return Ok(apiResponseFinal1);
                }
                if (ModelState.IsValid)
                {
                    TmUser obj = new TmUser();

                    obj.UserId = Guid.NewGuid().ToString();
                    obj.UserName = model.UserName;
                    obj.UserSex = model.UserSex;
                    obj.UserCreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e";
                    obj.UserCreatedOn = DateTime.Now;
                    obj.UserEmailId = model.UserEmailId;
                    obj.UserIsActive = model.UserIsActive;
                    obj.UserMobileNo = Convert.ToInt32(model.UserMobileNo);
                    obj.UserPassword = model.UserPassword;
                    obj.UserDob = model.UserDob;
                    obj.UserLastActivity = DateTime.Now;
                    obj.UserRoleId = model.UserRoleId;
                    obj.InvalidLoginAttempts = 0;
                    obj.PasswordLastChanged = DateTime.Now;
                    obj.LockedOutUntil = model.LockedOutUntil;
                    obj.ResetToken = model.ResetToken;
                    obj.ResetTokenExpires = model.ResetTokenExpires;
                    obj.IsSystemDefined = 1;


                    await _context.TmUser.AddAsync(obj);
                    await _context.SaveChangesAsync();

                    apiResponse.Data = model;
                    apiResponse.Msg = "Saved Sccessfully";
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
