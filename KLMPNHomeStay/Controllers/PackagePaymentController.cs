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
    public class PackagePaymentController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public PackagePaymentController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        [Route("getPaymentPackageList")]
        public async Task<IActionResult> GetPaymentPackageList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                List<PackagePaymentApprovalListResponseModel> packagePaymentApprovalLists = new List<PackagePaymentApprovalListResponseModel>();
                
                var packPaymentList = from tourBooking in _context.TtTourBooking
                                      join tourDt in _context.TtTourDate
                                      on tourBooking.TourDateId equals tourDt.Id
                                      join tour in _context.TmTour
                                      on tourBooking.TourId equals tour.Id
                                      where (tourBooking.PaymentAmount != null && tourBooking.IsCancel == 0 && tourBooking.IsCheckedByAdmin == 0)
                                      select new PackagePaymentApprovalListResponseModel
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
                                          toDt = tourDt.ToDate.ToString("dd-MM-yyyy")
                                      };
                var paymentList = await packPaymentList.ToListAsync();
                if(paymentList.Count > 0)
                {
                    apiResponse.Data = paymentList;
                    apiResponse.Msg = "Displaying Tour List";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    apiResponse.Data = null;
                    apiResponse.Msg = "No Tour";
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

        [HttpGet]
        [Route("getPaymentApprovalTourDetail/{id}")]
        public async Task<IActionResult> GetPaymentApprovalTourDetail(string id)
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
                                 select new PackagePaymentDetailResponseModel
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
                                     guestUserName = guest.GuName
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
        [Route("approveBookingPayment")]
        public async Task<IActionResult> ApproveBookingPayment(ApprovePackagePaymentRequestModel paymentRequestModel)
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
                        bookingDet.IsCheckedByAdmin = 1;
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
        
        [HttpGet]
        [Route("getAllPaymentApprovalTourBank")]
        public async Task<IActionResult> GetAllPaymentApprovalTourBank()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                List<PackagePaymentApprovalListResponseModel> packagePaymentApprovalLists = new List<PackagePaymentApprovalListResponseModel>();

                var packPaymentList = from tourBooking in _context.TtTourBooking
                                      join tourDt in _context.TtTourDate
                                      on tourBooking.TourDateId equals tourDt.Id
                                      join tour in _context.TmTour
                                      on tourBooking.TourId equals tour.Id
                                      where (tourBooking.PaymentAmount != null && tourBooking.IsCheckedByAdmin == 1 && tourBooking.IsCancel == 0 && tourBooking.IsCheckedByBankUser == 0)
                                      select new PackagePaymentApprovalListResponseModel
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
                                          toDt = tourDt.ToDate.ToString("dd-MM-yyyy")
                                      };
                var paymentList = await packPaymentList.ToListAsync();
                if (paymentList.Count > 0)
                {
                    apiResponse.Data = paymentList;
                    apiResponse.Msg = "Displaying Tour List";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    apiResponse.Data = null;
                    apiResponse.Msg = "No Tour";
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

        [HttpPost]
        [Route("approveBookingPaymentBank")]
        public async Task<IActionResult> ApproveBookingPaymentBank(ApprovePackagePaymentRequestModel paymentRequestModel)
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
                        bookingDet.IsCheckedByBankUser = 1;
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
