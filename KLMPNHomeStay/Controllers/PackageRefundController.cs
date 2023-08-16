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
    public class PackageRefundController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public PackageRefundController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpPost]
        [Route("getRefundPackageList")]
        public async Task<IActionResult> GetPaymentPackageList([FromBody] RefundListRequestModel model)//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                List<PackagRefundListResponseModel> packagRefundLists = new List<PackagRefundListResponseModel>();
                if (model.filter == "NeedToRefund")
                {
                    var packRefundList = from tourBooking in _context.TtTourBooking
                                         join tourDt in _context.TtTourDate
                                         on tourBooking.TourDateId equals tourDt.Id
                                         join tour in _context.TmTour
                                         on tourBooking.TourId equals tour.Id
                                         where (tourBooking.IsCancel == 1 && tourBooking.IsCancelCheckedByAdmin == 0 && tourBooking.PaymentAmount != null)
                                         select new PackagRefundListResponseModel
                                         {
                                             bookingId = tourBooking.Id,
                                             tourDtId = tourDt.Id,
                                             tourId = tour.Id,
                                             tourName = tour.Name,
                                             paymentAmount = tourBooking.PaymentAmount,
                                             bookingDate = tourBooking.BookingDate.ToString("dd-MM-yyyy"),
                                             destination = tour.Destination,
                                             person = tourBooking.NoOfPerson,
                                             fromDt = tourDt.FromDate.ToString("dd-MM-yyyy"),
                                             toDt = tourDt.ToDate.ToString("dd-MM-yyyy"),
                                             cancelDate = tourBooking.CancelledDate == null ? "" : tourBooking.CancelledDate.ToString()
                                         };
                    var refundList = await packRefundList.ToListAsync();
                    if (refundList.Count > 0)
                    {
                        apiResponse.Data = refundList;
                        apiResponse.Msg = "Displaying Tour List";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Data = null;
                        apiResponse.Msg = "No Tour";
                        apiResponse.Result = ResponseTypes.Error;
                    }

                }
                if (model.filter == "Refunded")
                {
                    var packRefundList = from tourBooking in _context.TtTourBooking
                                         join tourDt in _context.TtTourDate
                                         on tourBooking.TourDateId equals tourDt.Id
                                         join tour in _context.TmTour
                                         on tourBooking.TourId equals tour.Id
                                         where (tourBooking.IsCancel == 1 && tourBooking.IsCancelCheckedByAdmin == 1 && tourBooking.PaymentAmount != null)
                                         select new PackagRefundListResponseModel
                                         {
                                             bookingId = tourBooking.Id,
                                             tourDtId = tourDt.Id,
                                             tourId = tour.Id,
                                             tourName = tour.Name,
                                             paymentAmount = tourBooking.PaymentAmount,
                                             bookingDate = tourBooking.BookingDate.ToString("dd-MM-yyyy"),
                                             destination = tour.Destination,
                                             person = tourBooking.NoOfPerson,
                                             fromDt = tourDt.FromDate.ToString("dd-MM-yyyy"),
                                             toDt = tourDt.ToDate.ToString("dd-MM-yyyy"),
                                             cancelDate = tourBooking.CancelledDate == null ? "" : tourBooking.CancelledDate.ToString()
                                         };
                    var refundList = await packRefundList.ToListAsync();
                    if (refundList.Count > 0)
                    {
                        apiResponse.Data = refundList;
                        apiResponse.Msg = "Displaying Tour List";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Data = null;
                        apiResponse.Msg = "No Tour";
                        apiResponse.Result = ResponseTypes.Error;
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

        [HttpGet]
        [Route("getRefundTourDetail/{id}")]
        public async Task<IActionResult> GetRefundTourDetail(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var tourDetail = from booking in _context.TtTourBooking
                                 join guest in _context.TmGuestUser
                                 on booking.GuId equals guest.GuId
                                 join tourDt in _context.TtTourDate
                                 on booking.TourDateId equals tourDt.Id
                                 join tour in _context.TmTour
                                 on booking.TourId equals tour.Id
                                 where (booking.Id == id)
                                 select new PackageRefundDetailResponseModel
                                 {
                                     bookingId = booking.Id,
                                     tourDtId = tourDt.Id,
                                     tourId = tour.Id,
                                     tourName = tour.Name,
                                     paymentAmount = booking.PaymentAmount,
                                     bookingDate = booking.BookingDate.ToString("dd-MM-yyyy"),
                                     destination = tour.Destination,
                                     person = booking.NoOfPerson,
                                     fromDt = tourDt.FromDate.ToString("dd-MM-yyyy"),
                                     toDt = tourDt.ToDate.ToString("dd-MM-yyyy"),
                                     guestUserName = guest.GuName,
                                     cancelDt = booking.CancelledDate.ToString()
                                 };
                var tourDet = await tourDetail.FirstOrDefaultAsync();
                if (tourDet != null)
                {
                    apiResponse.Data = tourDet;
                    apiResponse.Msg = "Display Tour";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    apiResponse.Data = null;
                    apiResponse.Msg = "No Tour Found";
                    apiResponse.Result = ResponseTypes.Error;
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
        [Route("refundTourBooking")]
        public async Task<IActionResult> RefundTourBooking(ApprovePackagePaymentRequestModel paymentRequestModel)
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
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                        var bookingDet = await _context.TtTourBooking.AsNoTracking().Where(m => m.Id == paymentRequestModel.bookingId).FirstOrDefaultAsync();
                        bookingDet.IsCancelCheckedByAdmin = 1;
                        _context.TtTourBooking.Update(bookingDet);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }
                    apiResponse.Msg = "Success";
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
        
        [HttpPost]
        [Route("getAllRefundTourBank")]
        public async Task<IActionResult> GetAllRefundTourBank([FromBody] RefundListRequestModel model)//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                List<PackagRefundListResponseModel> packagRefundLists = new List<PackagRefundListResponseModel>();
                if (model.filter == "NeedToRefund")
                {
                    var packRefundList = from tourBooking in _context.TtTourBooking
                                         join tourDt in _context.TtTourDate
                                         on tourBooking.TourDateId equals tourDt.Id
                                         join tour in _context.TmTour
                                         on tourBooking.TourId equals tour.Id
                                         where (tourBooking.IsCancel == 1 && tourBooking.IsCancelCheckedByAdmin == 1 && tourBooking.IsCancelCheckedByBankUser == 0 && tourBooking.PaymentAmount != null)
                                         select new PackagRefundListResponseModel
                                         {
                                             bookingId = tourBooking.Id,
                                             tourDtId = tourDt.Id,
                                             tourId = tour.Id,
                                             tourName = tour.Name,
                                             paymentAmount = tourBooking.PaymentAmount,
                                             bookingDate = tourBooking.BookingDate.ToString("dd-MM-yyyy"),
                                             destination = tour.Destination,
                                             person = tourBooking.NoOfPerson,
                                             fromDt = tourDt.FromDate.ToString("dd-MM-yyyy"),
                                             toDt = tourDt.ToDate.ToString("dd-MM-yyyy"),
                                             cancelDate = tourBooking.CancelledDate == null ? "" : tourBooking.CancelledDate.ToString()
                                         };
                    var refundList = await packRefundList.ToListAsync();
                    if (refundList.Count > 0)
                    {
                        apiResponse.Data = refundList;
                        apiResponse.Msg = "Displaying Tour List";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Data = null;
                        apiResponse.Msg = "No Tour";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                }
                if (model.filter == "Refunded")
                {
                    var packRefundList = from tourBooking in _context.TtTourBooking
                                         join tourDt in _context.TtTourDate
                                         on tourBooking.TourDateId equals tourDt.Id
                                         join tour in _context.TmTour
                                         on tourBooking.TourId equals tour.Id
                                         where (tourBooking.IsCancel == 1 && tourBooking.IsCancelCheckedByAdmin == 1 && tourBooking.IsCancelCheckedByBankUser == 1 && tourBooking.PaymentAmount != null)
                                         select new PackagRefundListResponseModel
                                         {
                                             bookingId = tourBooking.Id,
                                             tourDtId = tourDt.Id,
                                             tourId = tour.Id,
                                             tourName = tour.Name,
                                             paymentAmount = tourBooking.PaymentAmount,
                                             bookingDate = tourBooking.BookingDate.ToString("dd-MM-yyyy"),
                                             destination = tour.Destination,
                                             person = tourBooking.NoOfPerson,
                                             fromDt = tourDt.FromDate.ToString("dd-MM-yyyy"),
                                             toDt = tourDt.ToDate.ToString("dd-MM-yyyy"),
                                             cancelDate = tourBooking.CancelledDate == null ? "" : tourBooking.CancelledDate.ToString()
                                         };
                    var refundList = await packRefundList.ToListAsync();
                    if (refundList.Count > 0)
                    {
                        apiResponse.Data = refundList;
                        apiResponse.Msg = "Displaying Tour List";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Data = null;
                        apiResponse.Msg = "No Tour";
                        apiResponse.Result = ResponseTypes.Error;
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
        
        [HttpPost]
        [Route("refundTourBookingBank")]
        public async Task<IActionResult> RefundTourBookingBank(TourRefundBankRequestModel tourRefundBankModel)
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
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                        var bookingDet = await _context.TtTourBooking.AsNoTracking().Where(m => m.Id == tourRefundBankModel.bookingId).FirstOrDefaultAsync();
                        bookingDet.IsCancelCheckedByBankUser = 1;
                        bookingDet.RefundStatus = 1;
                        bookingDet.RefundOn = DateTime.Now;
                        bookingDet.RefundBy = tourRefundBankModel.refundBy;
                        bookingDet.RfdVoucherMode = tourRefundBankModel.voucherMode;
                        bookingDet.RfdVoucherStatus = tourRefundBankModel.voucherStatus;
                        bookingDet.RfdVoucherAmount = bookingDet.PaymentAmount;
                        bookingDet.RfdVoucherDate = Convert.ToDateTime(tourRefundBankModel.voucherDt);
                        bookingDet.RfdVoucherNo = tourRefundBankModel.voucherNo;
                        _context.TtTourBooking.Update(bookingDet);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }
                    apiResponse.Msg = "Success";
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

