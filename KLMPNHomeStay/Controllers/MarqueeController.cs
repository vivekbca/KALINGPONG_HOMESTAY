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
    public class MarqueeController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public MarqueeController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet("marqueeList")]
        public async Task<IActionResult> MarqueeList()
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                List<MarqueeResponseModel> marqueeResponseModels = new List<MarqueeResponseModel>();
                var marqueeList = await _context.TmMarquee.Where(m => m.IsActive == 1).ToListAsync();
                if(marqueeList.Count > 0)
                {
                    foreach(var item in marqueeList)
                    {
                        MarqueeResponseModel marqueeResponseModel = new MarqueeResponseModel();
                        marqueeResponseModel.marqueeId = item.MarqueeId;
                        marqueeResponseModel.heading = item.Heading;
                        marqueeResponseModel.desc = item.Desc;
                        marqueeResponseModel.isActive = item.IsActive;
                        marqueeResponseModels.Add(marqueeResponseModel);
                    }
                    apiResponse.Data = marqueeResponseModels;
                    apiResponse.Msg = "Displaying Marquee";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    
                    apiResponse.Msg = "No marquee";
                    apiResponse.Result = ResponseTypes.Error;
                }
                

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("homePageMarqueeList")]
        public async Task<IActionResult> HomePageMarquee()
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                MarqueeResponseModel marqueeResponseModel = new MarqueeResponseModel();
                string marquee = string.Empty;
                var marqueeList = await _context.TmMarquee.Where(m => m.IsActive == 1).ToListAsync();
                if (marqueeList.Count > 0)
                {
                    if (marqueeList.Count() == 0)
                    {
                        foreach (var item in marqueeList)
                        {
                            marquee = item.Heading;
                        }
                    }
                    else
                    {
                        foreach (var item in marqueeList)
                        {
                            marquee = marquee + " " + "|" + " " + item.Heading;
                        }
                        marquee = marquee.Substring(2);
                    }
                    marqueeResponseModel.heading = marquee;
                    apiResponse.Data = marqueeResponseModel;
                    apiResponse.Msg = "Displaying Marquee";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    apiResponse.Msg = "No marquee";
                    apiResponse.Result = ResponseTypes.Error;
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
