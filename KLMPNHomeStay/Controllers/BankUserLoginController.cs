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
    public class BankUserLoginController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public BankUserLoginController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> BankUserLoginCheck([FromBody] GULoginResponseModel guLogin)
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
                        var bankRole = await _context.TmUserRole.Where(m => m.RoleName == String.Format("BankUser")).FirstOrDefaultAsync();
                        var logincheck = await _context.TmUser.Where(m => m.UserEmailId == guLogin.UserName && m.UserPassword == guLogin.Password).FirstOrDefaultAsync();
                        if (logincheck != null && logincheck.UserRoleId == bankRole.RoleId)
                        {
                            if (logincheck.UserIsActive == 1)
                            {
                                var authClaims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, logincheck.UserId),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                };

                                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                                var token = new JwtSecurityToken(
                                    issuer: _configuration["JWT:ValidIssuer"],
                                    audience: _configuration["JWT:ValidAudience"],
                                    expires: DateTime.Now.AddHours(3),
                                    claims: authClaims,
                                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                                    );
                                apiResponse.Data = new
                                {
                                    token = new JwtSecurityTokenHandler().WriteToken(token),
                                    accountDescription = logincheck.UserMobileNo,
                                    fullName = logincheck.UserName,
                                    expiration = token.ValidTo,
                                    email = logincheck.UserEmailId,
                                    userId = logincheck.UserId,
                                    roleName = bankRole.RoleName
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
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
            return Ok(apiResponseFinal);
        }
    }
}
