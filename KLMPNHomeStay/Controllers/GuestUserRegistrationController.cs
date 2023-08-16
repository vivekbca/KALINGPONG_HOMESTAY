using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using KLMPNHomeStay.Models.Request_Model;
using KLMPNHomeStay.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestUserRegistrationController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public GuestUserRegistrationController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService,IEmailService emailService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> GuestUserRegistration([FromBody] GuestUserAddRequestModel guestuserAddRequest)//This method will insert the GuestUserRegistration into db  {
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
                        var allEmailIds =await _context.TmGuestUser.Where(m => m.GuEmailId == guestuserAddRequest.GuEmailId).ToListAsync();
                        if (allEmailIds.Count != 0)
                        {
                            apiResponse.Msg = "Email ID Already Exist!!";
                            ApiResponseModelFinal apiResponseError = _globalService.GetFinalResponse(apiResponse);
                            return Ok(apiResponseError);
                        }
                        var guestuser = new TmGuestUser
                        {
                            GuId = Guid.NewGuid().ToString(),
                            GuName = guestuserAddRequest.GuName,
                            GuAddress1 = guestuserAddRequest.GuAddress1,
                            GuAddress2 = guestuserAddRequest.GuAddress2,
                            GuAddress3 = guestuserAddRequest.GuAddress3,
                            GuCountryId = guestuserAddRequest.GuCountry,
                            GuDob = Convert.ToDateTime(guestuserAddRequest.GuDob),
                            GuSex = guestuserAddRequest.GuSex,
                            GuPassword = guestuserAddRequest.GuPassword,
                            GuMobileNo = guestuserAddRequest.GuMobileNo,
                            GuEmailId = guestuserAddRequest.GuEmailId,
                            GuCreatedOn = DateTime.Now,
                            GuLastActivity = DateTime.Now,
                            GuStateId = guestuserAddRequest.GuState,
                            GuCity= guestuserAddRequest.GuCity,
                            GuIdentityProof= guestuserAddRequest.GuIdentityProof,
                            GuIdentityNo= guestuserAddRequest.GuIdentityNo,
                            GuPincode= guestuserAddRequest.GuPincode,
                            GuIsActive = 1

                        };
                        _context.TmGuestUser.Add(guestuser);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                        await SendLoginEmail(guestuserAddRequest.GuEmailId, guestuserAddRequest.GuName,guestuserAddRequest.GuPassword);

                    }
                    apiResponse.Msg = "Guest User added successfully";
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
        [HttpGet("country")]
        
        public async Task<IActionResult> getAllCountry()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllCountry = await _context.TmCountry.OrderBy(m=>m.CountryName).ToListAsync();
                apiResponse.Data = AllCountry;
                apiResponse.Msg = "List of Country";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }
        [HttpGet("state/{id}")]

        public async Task<IActionResult> getAllState(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var AllState = await _context.TmState.Where(m=>m.CountryId==id).OrderBy(m=>m.StateName).ToListAsync();
                apiResponse.Data = AllState;
                apiResponse.Msg = "List of State";
                apiResponse.Result = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [NonAction]
        private async Task SendLoginEmail(string Email,string name,string password)
        {
            try
            {
                var names = name.Split(' ');
                string firstName = names[0];
                var path = Path.Combine(_env.ContentRootPath, "Template/UserMail.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Email>>", Email)
                        .Replace("<<Password>>",password)
                        .Replace("<<Name>>", firstName);
                //send password to email asynchronously
                await _emailService.Send(Email, name, "Welcome - New User", content1);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            }           
    }
}
