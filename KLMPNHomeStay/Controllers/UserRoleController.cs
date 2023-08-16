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
    public class UserRoleController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public UserRoleController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserRoleList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var roleList = await (from a in _context.TmUserRole
                                      select new UserRoleResponseModel
                                      {
                                          roleId = a.RoleId,
                                          roleName = a.RoleName,
                                          roleIsActive = a.RoleIsActive,
                                          isSystemDefined = a.IsSystemDefined,
                                          isDeleted = a.IsDeleted
                                      }).ToListAsync();
                apiResponse.Data = roleList;
                apiResponse.Msg = "Displaying User Role List";
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
        public async Task<IActionResult> GetUserRoleById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var roleDet = await _context.TmUserRole.Where(m => m.RoleId == id).FirstOrDefaultAsync();

                if (roleDet == null)
                {
                    apiResponse.Msg = "User Role not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    UserRoleResponseModel userRoleResponse = new UserRoleResponseModel();
                    userRoleResponse.roleId = roleDet.RoleId;
                    userRoleResponse.roleName = roleDet.RoleName;
                    userRoleResponse.roleIsActive = roleDet.RoleIsActive;
                    userRoleResponse.isSystemDefined = roleDet.IsSystemDefined;
                    userRoleResponse.isDeleted = roleDet.IsDeleted;

                    apiResponse.Data = userRoleResponse;
                    apiResponse.Msg = "Displaying User Role";
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
        public async Task<IActionResult> CreateUserRole([FromBody] UserRoleAddRequestModel roleAddRequestModel)//This method will insert the role into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };

            try
            {
                var duplicateUserRole = await _context.TmUserRole.Where(m => m.RoleName == roleAddRequestModel.roleName).CountAsync();

                if (duplicateUserRole > 0)
                {
                    apiResponse.Msg = "Duplicate User Role";
                    apiResponse.Result = ResponseTypes.Error;
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            var role = new TmUserRole
                            {
                                RoleId = Guid.NewGuid().ToString(),
                                RoleName = roleAddRequestModel.roleName,
                                RoleIsActive = 1,
                                IsSystemDefined = 0,
                                IsDeleted = 0
                            };
                            _context.TmUserRole.Add(role);
                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                        }
                        apiResponse.Msg = "User Role added successfully";
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

        [HttpPut]
        public async Task<IActionResult> UpdateUserRole(UserRoleAddRequestModel roleAddRequestModel)//Update method will update the role  
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var duplicateUserRole = await _context.TmUserRole.Where(m => m.RoleName == roleAddRequestModel.roleName && m.RoleId != roleAddRequestModel.roleId).CountAsync();

                var roleDet = await _context.TmUserRole.Where(m => m.RoleId == roleAddRequestModel.roleId).FirstOrDefaultAsync();
                if (roleDet == null)
                {
                    apiResponse.Msg = "User Role not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    if (duplicateUserRole > 0)
                    {
                        apiResponse.Msg = "Duplicate User Role";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            using (var tran = await _context.Database.BeginTransactionAsync())
                            {
                                roleDet.RoleName = roleAddRequestModel.roleName;

                                _context.TmUserRole.Update(roleDet);
                                await _context.SaveChangesAsync();
                                await tran.CommitAsync();
                            }

                            apiResponse.Msg = "User Role updated successfully";
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
