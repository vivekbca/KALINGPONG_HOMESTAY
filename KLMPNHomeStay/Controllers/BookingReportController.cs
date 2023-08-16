using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using KLMPNHomeStay.Models.Request_Model;
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
    public class BookingReportController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public BookingReportController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet("HomestayListReport/{filter}")]
        public async Task<IActionResult> GetPreviousDayHsList(string filter)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if (filter == "Unchecked")
                {
                    var HomestayBoolingList = from bk in _context.TtBooking.AsNoTracking()
                                              join gu in _context.TmGuestUser.AsNoTracking()
                                              on bk.GuId equals gu.GuId
                                              join hs in _context.TmHomestay.AsNoTracking()
                                              on bk.HsId equals hs.HsId
                                              where bk.BkPaymentStatus != null && bk.BkIsCancelled==0 && bk.IsReportChecked==0
                                              select new
                                              {
                                                  bk.GuId,
                                                  bk.HsId,
                                                  bk.HsBookingId,
                                                  bk.BkDateFrom,
                                                  bk.BkDateTo,
                                                  hs.HsName,
                                                  gu.GuName,
                                                  bk.BkNoPers,
                                                  bk.BkBookingDate,
                                                  bk.TotalCost
                                              };
                    var BookingList = await HomestayBoolingList.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Popular List";
                    apiResponse.Result = ResponseTypes.Success;
                }
                if(filter=="Checked")
                {
                    var HomestayBoolingList = from bk in _context.TtBooking.AsNoTracking()
                                              join gu in _context.TmGuestUser.AsNoTracking()
                                              on bk.GuId equals gu.GuId
                                              join hs in _context.TmHomestay.AsNoTracking()
                                              on bk.HsId equals hs.HsId
                                              where bk.BkPaymentStatus != null && bk.BkIsCancelled == 0 && bk.IsReportChecked==1
                                              select new
                                              {
                                                  bk.GuId,
                                                  bk.HsId,
                                                  bk.HsBookingId,
                                                  bk.BkDateFrom,
                                                  bk.BkDateTo,
                                                  hs.HsName,
                                                  gu.GuName,
                                                  bk.BkNoPers,
                                                  bk.BkBookingDate,
                                                  bk.TotalCost
                                              };
                    var BookingList = await HomestayBoolingList.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Booking List";
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

        [HttpPost("checkall")]
        public async Task<IActionResult> AllChecked([FromBody] List<HomestayBookingRequestModel> model)
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
                    List<TtBooking> ttBookings = new List<TtBooking>();
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                       foreach(var item in model)
                        {
                            var CheckedData = await _context.TtBooking.Where(m => m.HsBookingId == item.hsBookingId).FirstOrDefaultAsync();
                            if(CheckedData!=null)
                            {
                                CheckedData.IsReportChecked = 1;
                                ttBookings.Add(CheckedData);
                               //_context.TtBooking.UpdateRange(CheckedData);
                              
                            }
                        }
                        _context.TtBooking.UpdateRange(ttBookings);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();

                    }
                    var HomestayBoolingList = from bk in _context.TtBooking.AsNoTracking()
                                              join gu in _context.TmGuestUser.AsNoTracking()
                                              on bk.GuId equals gu.GuId
                                              join hs in _context.TmHomestay.AsNoTracking()
                                              on bk.HsId equals hs.HsId
                                              where bk.BkPaymentStatus != null && bk.BkIsCancelled == 0 && bk.IsReportChecked == 0
                                              select new
                                              {
                                                  bk.GuId,
                                                  bk.HsId,
                                                  bk.HsBookingId,
                                                  bk.BkDateFrom,
                                                  bk.BkDateTo,
                                                  hs.HsName,
                                                  gu.GuName,
                                                  bk.BkNoPers,
                                                  bk.BkBookingDate,
                                                  bk.TotalCost
                                              };
                    var BookingList = await HomestayBoolingList.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Booking Checked";
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

        [HttpPost("checkallpkg")]
        public async Task<IActionResult> AllCheckedPkg ([FromBody] List<PackageBookingRequestModel> model)
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
                    List<TtTourBooking> ttBookings = new List<TtTourBooking>();
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                        foreach (var item in model)
                        {
                            var CheckedData = await _context.TtTourBooking.Where(m => m.Id == item.id).FirstOrDefaultAsync();
                            if (CheckedData != null)
                            {
                                CheckedData.IsReportChecked = 1;
                                ttBookings.Add(CheckedData);
                                //_context.TtBooking.UpdateRange(CheckedData);

                            }
                        }
                        _context.TtTourBooking.UpdateRange(ttBookings);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();

                    }
                    var HomestayBoolingList = from bk in _context.TtTourBooking.AsNoTracking()
                                              join gu in _context.TmGuestUser.AsNoTracking()
                                              on bk.GuId equals gu.GuId
                                              join hs in _context.TmTour.AsNoTracking()
                                              on bk.TourId equals hs.Id
                                              join date in _context.TtTourDate.AsNoTracking()
                                              on bk.TourDateId equals date.Id
                                              where bk.PaymentStatus != null && bk.IsCancel == 0 && bk.IsReportChecked == 0
                                              select new
                                              {
                                                  bk.Id,
                                                  hs.Name,
                                                  bk.GuId,
                                                  bk.BookingDate,
                                                  date.FromDate,
                                                  date.ToDate,
                                                  gu.GuName,
                                                  bk.NoOfPerson,
                                                  bk.TotalRate
                                              };
                    var BookingList = await HomestayBoolingList.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Booking Checked";
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

        [HttpGet("PackageListReport/{filter}")]
        public async Task<IActionResult> GetPreviousDayPkgList(string filter)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if (filter == "Unchecked")
                {
                    var HomestayBoolingList = from bk in _context.TtTourBooking.AsNoTracking()
                                              join gu in _context.TmGuestUser.AsNoTracking()
                                              on bk.GuId equals gu.GuId
                                              join hs in _context.TmTour.AsNoTracking()
                                              on bk.TourId equals hs.Id
                                              join date in _context.TtTourDate.AsNoTracking()
                                              on bk.TourDateId equals date.Id
                                              where bk.PaymentStatus != null && bk.IsCancel == 0 && bk.IsReportChecked == 0
                                              select new
                                              {
                                                  bk.Id,
                                                  hs.Name,
                                                  bk.GuId,
                                                  bk.BookingDate,
                                                  date.FromDate,
                                                  date.ToDate,
                                                  gu.GuName,
                                                  bk.NoOfPerson,                                                  
                                                  bk.TotalRate
                                              };
                    var BookingList = await HomestayBoolingList.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Package Report";
                    apiResponse.Result = ResponseTypes.Success;
                }
                if (filter == "Checked")
                {
                    var HomestayBoolingList = from bk in _context.TtTourBooking.AsNoTracking()
                                              join gu in _context.TmGuestUser.AsNoTracking()
                                              on bk.GuId equals gu.GuId
                                              join hs in _context.TmTour.AsNoTracking()
                                              on bk.TourId equals hs.Id
                                              join date in _context.TtTourDate.AsNoTracking()
                                              on bk.TourDateId equals date.Id
                                              where bk.PaymentStatus != null && bk.IsCancel == 0 && bk.IsReportChecked == 1
                                              select new
                                              {
                                                  bk.Id,
                                                  hs.Name,
                                                  bk.GuId,
                                                  bk.BookingDate,
                                                  date.FromDate,
                                                  date.ToDate,
                                                  gu.GuName,
                                                  bk.NoOfPerson,
                                                  bk.TotalRate
                                              };
                    var BookingList = await HomestayBoolingList.ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Booking List";
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

    }
}
