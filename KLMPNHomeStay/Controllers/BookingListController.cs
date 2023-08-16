using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
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
    public class BookingListController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public BookingListController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet("getBookinglist/{filter}")]
        public async Task<IActionResult> GetAllBookingList(string filter)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if(filter== "Upcoming")
                {
                    var BookingData = from bk in _context.TtBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join hs in _context.TmHomestay.AsNoTracking()
                                      on bk.HsId equals hs.HsId
                                      join ctry in _context.TmCountry.AsNoTracking()
                                      on gu.GuCountryId equals ctry.CountryId
                                      join state in _context.TmState.AsNoTracking()
                                      on gu.GuStateId equals state.StateId
                                      where bk.GuId == gu.GuId && bk.BkIsCancelled == 0 && bk.BkIsAvailed==0
                                      select new
                                      {
                                          bk.GuId,
                                          bk.HsId,
                                          bk.HsBookingId,
                                          bk.BkDateFrom,
                                          bk.BkDateTo,
                                          hs.HsName,
                                          gu.GuName,
                                          state.StateName

                                      };
                    var BookingList = await BookingData.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Popular List";
                    apiResponse.Result = ResponseTypes.Success;
                }
                if (filter == "Availed")
                {
                    var BookingData = from bk in _context.TtBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join hs in _context.TmHomestay.AsNoTracking()
                                      on bk.HsId equals hs.HsId
                                      join ctry in _context.TmCountry.AsNoTracking()
                                      on gu.GuCountryId equals ctry.CountryId
                                      join state in _context.TmState.AsNoTracking()
                                      on gu.GuStateId equals state.StateId
                                      where bk.GuId == gu.GuId && bk.BkIsAvailed == 1
                                      select new
                                      {
                                          bk.GuId,
                                          bk.HsId,
                                          bk.HsBookingId,
                                          bk.BkDateFrom,
                                          bk.BkDateTo,
                                          hs.HsName,
                                          gu.GuName,
                                          state.StateName

                                      };
                    var BookingList = await BookingData.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Popular List";
                    apiResponse.Result = ResponseTypes.Success;
                }
                if (filter == "Cancelled")
                {
                    var BookingData = from bk in _context.TtBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join hs in _context.TmHomestay.AsNoTracking()
                                      on bk.HsId equals hs.HsId
                                      join ctry in _context.TmCountry.AsNoTracking()
                                      on gu.GuCountryId equals ctry.CountryId
                                      join state in _context.TmState.AsNoTracking()
                                      on gu.GuStateId equals state.StateId
                                      where bk.GuId == gu.GuId && bk.BkIsCancelled == 1
                                      select new
                                      {
                                          bk.GuId,
                                          bk.HsId,
                                          bk.HsBookingId,
                                          bk.BkDateFrom,
                                          bk.BkDateTo,
                                          hs.HsName,
                                          gu.GuName,
                                          state.StateName

                                      };
                    var BookingList = await BookingData.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Popular List";
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

        [HttpGet("bookingDetail/{bookingid}")]
        public async Task<IActionResult> GetBookingDetails(string bookingid)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                    var BookingData = from bk in _context.TtBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join hs in _context.TmHomestay.AsNoTracking()
                                      on bk.HsId equals hs.HsId
                                      where bk.HsBookingId == bookingid
                                      select new
                                      {
                                          bk.GuId,
                                          bk.HsId,
                                          bk.HsBookingId,
                                          bk.BkDateFrom,
                                          bk.BkDateTo,
                                          bk.BkNoPers,
                                          bk.BkRoomNumber,
                                          bk.BkPaymentMode,
                                          bk.BkPaymentStatus,
                                          bk.BkPaymentAmount,
                                          hs.HsAddress1,
                                          hs.HsName,
                                          gu.GuName,

                                      };
                    var BookingList = await BookingData.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Popular List";
                    apiResponse.Result = ResponseTypes.Success;
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
