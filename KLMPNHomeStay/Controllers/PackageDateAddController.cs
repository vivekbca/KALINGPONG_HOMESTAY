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
    public class PackageDateAddController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public PackageDateAddController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        [Route("getPackageList")]
        public async Task<IActionResult> GetPackageList()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var roomList = await (from a in _context.TmTour
                                      join b in _context.TtTourDate on a.Id equals b.TourId into tempTourTbl
                                      from temp in tempTourTbl.DefaultIfEmpty()
                                      select new PackageListViewModel
                                      {
                                          Id = a.Id,
                                          Name = a.Name,
                                          Destination = a.Destination,
                                          Description = a.Description,
                                          Subject = a.Subject,
                                          FromDate = temp.FromDate.ToString("dd-MMM-yyyy"),
                                          ToDate = temp.ToDate.ToString("dd-MMM-yyyy"),
                                          isActive = temp.IsActive

                                      }).ToListAsync();
                apiResponse.Data = roomList;
                apiResponse.Msg = "Displaying Tour Package List";
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
