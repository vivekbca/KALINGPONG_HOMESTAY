using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
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
    public class FinancialYearController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public FinancialYearController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
       
        [HttpGet]
        [Route("getFinYrList")]
        public async Task<IActionResult> GetFinYrList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                //var countryList = await _context.TmCountry.AsNoTracking().ToListAsync();
                var finYrList = await (from a in _context.TmFinancialYear
                                        select new FinancialYearResponseModel
                                        {
                                            finYrId = a.FinancialYearId,
                                            finYr = a.FinancialYear
                                        }).ToListAsync();

                apiResponse.Data = finYrList;
                apiResponse.Msg = "Displaying Fin Yr List";
                apiResponse.Result = ResponseTypes.Success;
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
