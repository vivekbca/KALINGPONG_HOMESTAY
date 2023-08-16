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
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.Web;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public UserProfileController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingList(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var bookingList = (from a in _context.TtBooking
                                   join b in _context.TmHomestay on a.HsId equals b.HsId
                                   select new
                                   {
                                       GUid = a.GuId,
                                       HSid = a.HsId,
                                       HSbookingId = a.HsBookingId,
                                       BookingDate = a.BkBookingDate,
                                       BookingFromDate = a.BkDateFrom,
                                       BookingToDate = a.BkDateTo,
                                       NoOfPerson = a.BkNoPers,
                                       RoomNumber = a.BkRoomNumber,
                                       BookingIsAviled = a.BkIsAvailed,
                                       PaymantMode = a.BkPaymentMode,
                                       Amount = a.BkPaymentAmount,
                                       IsCanceled = a.BkIsCancelled
                                   }).Where(m=>m.GUid==id).ToList();

                apiResponse.Data = bookingList;
                apiResponse.Msg = "Displaying Booking List";
                apiResponse.Result = ResponseTypes.Success;

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("HomeStaylist/{profileId}")]
        public async Task<IActionResult> HomeStaylistByUserId(string profileId)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                // HomeStayFeedbackModel bookedRoomsResponse = new HomeStayFeedbackModel();

                List<HomeStayFeedbackModel> HomeStayRoomBooklist = new List<HomeStayFeedbackModel>();
                var bookedRoomss = await _context.TtBooking.Where(m => m.GuId == profileId).ToListAsync();

                if (bookedRoomss.Count > 0)
                {
                    foreach (var item in bookedRoomss)
                    {
                        var homestay = await _context.TmHomestay.Where(m => m.HsId == item.HsId).FirstOrDefaultAsync();
                        HomeStayFeedbackModel obj = new HomeStayFeedbackModel();
                        obj.bookedfrom = item.BkDateFrom.ToString("dd-MM-yyyy");
                        obj.bookedTo = item.BkDateTo.ToString("dd-MM-yyyy");
                        obj.HomestayName = homestay.HsName;
                        obj.RoomBooked = item.BkRoomNumber;
                        HomeStayRoomBooklist.Add(obj);

                    }

                }

                else
                {
                    apiResponse.Msg = "No Home Stay Records Found";
                    apiResponse.Result = ResponseTypes.Error;
                    ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                    return Ok(apiResponseFinal1);
                }
                apiResponse.Data = HomeStayRoomBooklist;
                apiResponse.Msg = "Displaying Booked Home Stay Details";
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
