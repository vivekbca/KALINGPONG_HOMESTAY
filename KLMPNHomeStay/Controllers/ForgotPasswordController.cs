using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    public class ForgotPasswordController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        private readonly IEmailService _emailService;
        public ForgotPasswordController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService, IEmailService emailService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
            _emailService = emailService;
        }
        [HttpPost]
        [Route("guestForgotPassword")]
        public async Task<IActionResult> GuestForgotPassword(ForgotPasswordRequestModel forgotPasswordRequest)
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
                    var host = Request.Headers["origin"];
                    var userDet = await _context.TmGuestUser.Where(m => m.GuEmailId == forgotPasswordRequest.emailId).FirstOrDefaultAsync();
                    if(userDet != null)
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            var resetToken = RandomTokenString();
                            userDet.ResetToken = resetToken;
                            userDet.ResetTokenExpires = DateTime.Now.AddDays(1);
                            _context.TmGuestUser.Update(userDet);
                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                            var resetUrl = $"{host}/reset-password?token=" + userDet.ResetToken;
                            await SendResetPasswordEmail(userDet.GuEmailId, userDet.GuName, resetUrl);
                            apiResponse.Msg = "Success";
                            apiResponse.Result = ResponseTypes.Success;
                        }
                    }
                    else
                    {
                        apiResponse.Msg = "Please give valid email id";
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

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestModel passwordRequest)
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
                    var userDet = await _context.TmGuestUser.Where(m => m.ResetToken == passwordRequest.token).FirstOrDefaultAsync();
                    if(userDet != null)
                    {
                        if (DateTime.Now <= userDet.ResetTokenExpires)
                        {
                            using (var tran = await _context.Database.BeginTransactionAsync())
                            {
                                userDet.GuPassword = passwordRequest.password;
                                userDet.ResetToken = null;
                                userDet.ResetTokenExpires = null;
                                _context.TmGuestUser.Update(userDet);
                                await _context.SaveChangesAsync();
                                await tran.CommitAsync();
                                apiResponse.Msg = "Success";
                                apiResponse.Result = ResponseTypes.Success;
                            }
                        }
                        else
                        {
                            apiResponse.Msg = "Reset Token Expire";
                            apiResponse.Result = ResponseTypes.Error;
                        }
                    }
                    else
                    {
                        apiResponse.Msg = "Invalid Token";
                        apiResponse.Result = ResponseTypes.Error;
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

        [HttpPost]
        [Route("userForgotPassword")]
        public async Task<IActionResult> UserForgotPassword(ForgotPasswordRequestModel forgotPasswordRequest)
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
                    var host = Request.Headers["origin"];
                    var userDet = await _context.TmUser.Where(m => m.UserEmailId == forgotPasswordRequest.emailId).FirstOrDefaultAsync();
                    if (userDet != null)
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            var resetToken = RandomTokenString();
                            userDet.ResetToken = resetToken;
                            userDet.ResetTokenExpires = DateTime.Now.AddDays(1);
                            _context.TmUser.Update(userDet);
                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                            var resetUrl = $"{host}/reset-password-user?token=" + userDet.ResetToken;
                            await SendUserResetPasswordEmail(userDet.UserEmailId, userDet.UserName, resetUrl);
                            apiResponse.Msg = "Success";
                            apiResponse.Result = ResponseTypes.Success;
                        }
                    }
                    else
                    {
                        apiResponse.Msg = "Please give valid email id";
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

        [HttpPost]
        [Route("userResetPassword")]
        public async Task<IActionResult> UserResetPassword(ResetPasswordRequestModel passwordRequest)
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
                    var userDet = await _context.TmUser.Where(m => m.ResetToken == passwordRequest.token).FirstOrDefaultAsync();
                    if (userDet != null)
                    {
                        if (DateTime.Now <= userDet.ResetTokenExpires)
                        {
                            using (var tran = await _context.Database.BeginTransactionAsync())
                            {
                                userDet.UserPassword = passwordRequest.password;
                                userDet.ResetToken = null;
                                userDet.ResetTokenExpires = null;
                                _context.TmUser.Update(userDet);
                                await _context.SaveChangesAsync();
                                await tran.CommitAsync();
                                apiResponse.Msg = "Success";
                                apiResponse.Result = ResponseTypes.Success;
                            }
                        }
                        else
                        {
                            apiResponse.Msg = "Reset Token Expire";
                            apiResponse.Result = ResponseTypes.Error;
                        }
                    }
                    else
                    {
                        apiResponse.Msg = "Invalid Token";
                        apiResponse.Result = ResponseTypes.Error;
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


        [NonAction]
        private async Task SendResetPasswordEmail(string Email, string Name,string resetUrl)
        {
            try
            {
                //var names = name.Split(' ');
                //string firstName = names[0];
                var path = Path.Combine(_env.ContentRootPath, "Template/ResetPassword.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Email>>", Email)
                        .Replace("<<Name>>", Name)
                        .Replace("<<ResetUrl>>", resetUrl);
                       
                //send password to email asynchronously
                await _emailService.Send(Email, Name, "Reset Password", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [NonAction]
        private async Task SendUserResetPasswordEmail(string Email, string Name, string resetUrl)
        {
            try
            {
                //var names = name.Split(' ');
                //string firstName = names[0];
                var path = Path.Combine(_env.ContentRootPath, "Template/ResetPassword.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Email>>", Email)
                        .Replace("<<Name>>", Name)
                        .Replace("<<ResetUrl>>", resetUrl);

                //send password to email asynchronously
                await _emailService.Send(Email, Name, "Reset Password", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }


        //For Mobile App
        [HttpPost]
        [Route("guestForgotPasswordMob")]
        public async Task<IActionResult> GuestForgotPasswordMob(ForgotPasswordRequestModel forgotPasswordRequest)
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
                    var userDet = await _context.TmGuestUser.Where(m => m.GuEmailId == forgotPasswordRequest.emailId).FirstOrDefaultAsync();
                    if (userDet != null)
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            var resetOTP = RandomOTP();
                            userDet.ResetToken = resetOTP;
                            userDet.ResetTokenExpires = DateTime.Now.AddDays(1);
                            _context.TmGuestUser.Update(userDet);
                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                            await SendResetPasswordEmailMob(userDet.GuEmailId, userDet.GuName, resetOTP);
                            apiResponse.Msg = "Success";
                            apiResponse.Result = ResponseTypes.Success;
                        }
                    }
                    else
                    {
                        apiResponse.Msg = "Please give valid email id";
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
        public static string RandomOTP()
        {
            int iOTPLength = 8;
            string sOTP = String.Empty;
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0","A","a","B","b","C","c","D","d","E","e","F","f","G","g","H","h","I","i","J","j","K","k","L","l","M","m","N","n","O","o","P","p","Q","q","R","r","S","s","T","t","U","u","V","v","W","w","X","x","Y","y","Z","z" };

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        [NonAction]
        private async Task SendResetPasswordEmailMob(string Email, string Name, string resetOTP)
        {
            try
            {
                //var names = name.Split(' ');
                //string firstName = names[0];
                var path = Path.Combine(_env.ContentRootPath, "Template/ResetPasswordApp.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Email>>", Email)
                        .Replace("<<Name>>", Name)
                        .Replace("<<ResetOTP>>", resetOTP);

                //send password to email asynchronously
                await _emailService.Send(Email, Name, "Reset Password", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("verifyOTPGuest")]
        public async Task<IActionResult> VerifyOTPGuest(OTPVerifyRequestModel oTPVerifyRequest)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            OTPVerifyResponseModel oTPVerify = new OTPVerifyResponseModel(); 
            try
            {
                if (!ModelState.IsValid)
                {
                    apiResponse.Data = ModelState;
                    apiResponse.Result = ResponseTypes.ModelErr;
                }
                else
                {
                    var otpDet = await _context.TmGuestUser.Where(m => m.ResetToken == oTPVerifyRequest.verifyOTP).FirstOrDefaultAsync();
                    if (otpDet != null)
                    {
                        oTPVerify.token = oTPVerifyRequest.verifyOTP;

                        apiResponse.Data = oTPVerify.token;
                        apiResponse.Msg = "Verified";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Msg = "Please give valid OTP";
                        apiResponse.Result = ResponseTypes.Error;
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
