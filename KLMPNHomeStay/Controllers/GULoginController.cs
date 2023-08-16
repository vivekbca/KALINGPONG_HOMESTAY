using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using KLMPNHomeStay.Models.Response_Model;
using KLMPNHomeStay.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GULoginController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public GULoginController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> GULoginCheck([FromBody] GULoginResponseModel guLogin)
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
                        var logincheck = await _context.TmGuestUser.Where(m => m.GuEmailId == guLogin.UserName && m.GuPassword== guLogin.Password).FirstOrDefaultAsync();
                        if (logincheck != null)
                        {
                            if (logincheck.GuIsActive == 1)
                            {
                                var authClaims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, logincheck.GuId),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                };

                                //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("54wN0QSwHBW6GLZgrGIVZBwpBnWvEBR21H7upfk/LVE="));
                                //var token = new JwtSecurityToken(
                                //    issuer: _configuration["JWT:ValidIssuer"],
                                //    audience: _configuration["JWT:ValidAudience"],
                                //    expires: DateTime.Now.AddHours(3),
                                //    claims: authClaims,
                                //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                                //    );
                                var token = new JwtSecurityToken(
                                    issuer: "http://103.107.66.140:8092",
                                    audience: "http://103.107.66.140:8092",
                                    expires: DateTime.Now.AddHours(3),
                                    claims: authClaims,
                                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                                    );
                                apiResponse.Data = new
                                {
                                    token = new JwtSecurityTokenHandler().WriteToken(token),
                                    accountDescription = logincheck.GuMobileNo,
                                    fullName = logincheck.GuName,
                                    expiration = token.ValidTo,
                                    email = logincheck.GuEmailId,
                                    userId = logincheck.GuId,
                                    roleName = "Guest"
                                };
                                apiResponse.Msg = "Log in success";
                                apiResponse.Result = ResponseTypes.Success;
                            }
                            else
                            {
                                apiResponse.Msg = "Your Account is Inactive Please Contact Admin";
                                apiResponse.Result = ResponseTypes.Info;
                            }
                        }
                        else
                        {
                            apiResponse.Msg = "Please Provide a Valid Username and Password";
                            apiResponse.Result = ResponseTypes.Info;
                        }                       
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var userDet = await _context.TmGuestUser.Where(m => m.GuId == id).Include(m => m.GuCountry).Include(m => m.GuState).FirstOrDefaultAsync();

                if (userDet == null)
                {
                    apiResponse.Msg = "User not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    GuestDetailResponseModel guestDetailResponseModel  = new GuestDetailResponseModel();
                    guestDetailResponseModel.guId = userDet.GuId;
                    if(userDet.GuName.Contains(" "))
                    {
                        var userName = userDet.GuName.Split(" ");
                        guestDetailResponseModel.guFirstName = userName[0];
                        guestDetailResponseModel.guLastName = userName[1];
                    }
                    else
                    {
                        
                        guestDetailResponseModel.guFirstName = userDet.GuName;
                        guestDetailResponseModel.guLastName = "";
                    }
                   
                    guestDetailResponseModel.address = userDet.GuAddress1;
                    guestDetailResponseModel.dob = userDet.GuDob.ToString("dd-MM-yyyy");
                    guestDetailResponseModel.gender = userDet.GuSex;
                    guestDetailResponseModel.emailId = userDet.GuEmailId;
                    guestDetailResponseModel.mobileNo = userDet.GuMobileNo;
                    guestDetailResponseModel.stateId = userDet.GuStateId;
                    guestDetailResponseModel.stateName = userDet.GuState.StateName;
                    guestDetailResponseModel.countryId = userDet.GuCountryId;
                    guestDetailResponseModel.countryName = userDet.GuCountry.CountryName;
                    guestDetailResponseModel.pinCode = userDet.GuPincode;
                    guestDetailResponseModel.city = userDet.GuCity;
                    guestDetailResponseModel.identityType = userDet.GuIdentityProof;
                    guestDetailResponseModel.identityNo = userDet.GuIdentityNo;

                    apiResponse.Data = guestDetailResponseModel;
                    apiResponse.Msg = "Displaying User Detail";
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
