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
    public class HSBookingRefundController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public HSBookingRefundController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpPost]
        [Route("getRefundHSList")]
        public async Task<IActionResult> GetRefundHSList([FromBody] RefundListRequestModel model)//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                List<HSRefundListResponseModel> hSRefundLists = new List<HSRefundListResponseModel>();
                if (model.filter == "NeedToRefund")
                {
                    var hsPayList = from booking in _context.TtBooking
                                    join hs in _context.TmHomestay
                                    on booking.HsId equals hs.HsId
                                    join guest in _context.TmGuestUser
                                    on booking.GuId equals guest.GuId
                                    join vill in _context.TmBlockVillage
                                    on hs.HsVillId equals vill.VillId
                                    where (booking.BkIsCancelled == 1 && booking.IsCancelCheckedByAdmin == 0 && booking.BkPaymentAmount != null)
                                    select new HSRefundListResponseModel
                                    {
                                        hsId = hs.HsId,
                                        bookingId = booking.HsBookingId,
                                        userId = booking.GuId,
                                        villId = hs.HsVillId,
                                        village = vill.VillName,
                                        hsName = hs.HsName,
                                        address = hs.HsAddress1,
                                        person = booking.BkNoPers,
                                        bookingDt = booking.BkBookingDate.ToString("dd-MM-yyyy"),
                                        fromDt = booking.BkDateFrom.ToString("dd-MM-yyyy"),
                                        toDt = booking.BkDateTo.ToString("dd-MM-yyyy"),
                                        cancelDt = booking.BkCancelledDate.ToString(),
                                        paymentAmount = booking.BkPaymentAmount,
                                        userName = guest.GuName
                                    };
                    var paymentList = await hsPayList.ToListAsync();

                    if (paymentList.Count > 0)
                    {
                        apiResponse.Data = paymentList;
                        apiResponse.Msg = "Displaying HS Booking List";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Data = null;
                        apiResponse.Msg = "No HS Booking";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                }
                if (model.filter == "Refunded")
                {
                    var hsPayList = from booking in _context.TtBooking
                                    join hs in _context.TmHomestay
                                    on booking.HsId equals hs.HsId
                                    join guest in _context.TmGuestUser
                                    on booking.GuId equals guest.GuId
                                    join vill in _context.TmBlockVillage
                                    on hs.HsVillId equals vill.VillId
                                    where (booking.BkIsCancelled == 1 && booking.IsCancelCheckedByAdmin == 1 && booking.BkPaymentAmount != null)
                                    select new HSRefundListResponseModel
                                    {
                                        hsId = hs.HsId,
                                        bookingId = booking.HsBookingId,
                                        userId = booking.GuId,
                                        villId = hs.HsVillId,
                                        village = vill.VillName,
                                        hsName = hs.HsName,
                                        address = hs.HsAddress1,
                                        person = booking.BkNoPers,
                                        bookingDt = booking.BkBookingDate.ToString("dd-MM-yyyy"),
                                        fromDt = booking.BkDateFrom.ToString("dd-MM-yyyy"),
                                        toDt = booking.BkDateTo.ToString("dd-MM-yyyy"),
                                        cancelDt = booking.BkCancelledDate.ToString(),
                                        paymentAmount = booking.BkPaymentAmount,
                                        userName = guest.GuName
                                    };
                    var paymentList = await hsPayList.ToListAsync();

                    if (paymentList.Count > 0)
                    {
                        apiResponse.Data = paymentList;
                        apiResponse.Msg = "Displaying HS Booking List";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Data = null;
                        apiResponse.Msg = "No HS Booking";
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
        [Route("getPaymentRefundHSDetail/{id}")]
        public async Task<IActionResult> GetPaymentRefundHSDetail(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var hsPayDetail = from booking in _context.TtBooking
                                  join hs in _context.TmHomestay
                                  on booking.HsId equals hs.HsId
                                  join guest in _context.TmGuestUser
                                  on booking.GuId equals guest.GuId
                                  join vill in _context.TmBlockVillage
                                  on hs.HsVillId equals vill.VillId
                                  where (booking.HsBookingId == id)
                                  select new HSRefundListResponseModel
                                  {
                                      hsId = hs.HsId,
                                      bookingId = booking.HsBookingId,
                                      userId = booking.GuId,
                                      villId = hs.HsVillId,
                                      village = vill.VillName,
                                      hsName = hs.HsName,
                                      address = hs.HsAddress1,
                                      person = booking.BkNoPers,
                                      bookingDt = booking.BkBookingDate.ToString("dd-MM-yyyy"),
                                      fromDt = booking.BkDateFrom.ToString("dd-MM-yyyy"),
                                      toDt = booking.BkDateTo.ToString("dd-MM-yyyy"),
                                      cancelDt = booking.BkCancelledDate.ToString(),
                                      paymentAmount = booking.BkPaymentAmount,
                                      userName = guest.GuName
                                  };

                var hsDet = await hsPayDetail.FirstOrDefaultAsync();
                if (hsDet != null)
                {
                    apiResponse.Data = hsDet;
                    apiResponse.Msg = "Display HomeStay";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    apiResponse.Data = null;
                    apiResponse.Msg = "No HomeStay Found";
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
        [Route("refundBookingAdmin")]
        public async Task<IActionResult> RefundBookingAdmin(ApproveHSPaymentRequestModel approveHSPaymentRequest)
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
                        var bookingDet = await _context.TtBooking.AsNoTracking().Where(m => m.HsBookingId == approveHSPaymentRequest.bookingId).FirstOrDefaultAsync();
                        bookingDet.IsCancelCheckedByAdmin = 1;
                        _context.TtBooking.Update(bookingDet);
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
        [Route("getAllHSRefundBankList")]
        public async Task<IActionResult> GetAllHSRefundBankList([FromBody] RefundListRequestModel model)//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                List<HSRefundListResponseModel> hSRefundLists = new List<HSRefundListResponseModel>();
                if (model.filter == "NeedToRefund")
                {
                    var hsPayList = from booking in _context.TtBooking
                                    join hs in _context.TmHomestay
                                    on booking.HsId equals hs.HsId
                                    join guest in _context.TmGuestUser
                                    on booking.GuId equals guest.GuId
                                    join vill in _context.TmBlockVillage
                                    on hs.HsVillId equals vill.VillId
                                    where (booking.BkIsCancelled == 1 && booking.IsCancelCheckedByAdmin == 1 && booking.BkPaymentAmount != null && booking.IsCancelCheckedByBankUser == 0)
                                    select new HSRefundListResponseModel
                                    {
                                        hsId = hs.HsId,
                                        bookingId = booking.HsBookingId,
                                        userId = booking.GuId,
                                        villId = hs.HsVillId,
                                        village = vill.VillName,
                                        hsName = hs.HsName,
                                        address = hs.HsAddress1,
                                        person = booking.BkNoPers,
                                        bookingDt = booking.BkBookingDate.ToString("dd-MM-yyyy"),
                                        fromDt = booking.BkDateFrom.ToString("dd-MM-yyyy"),
                                        toDt = booking.BkDateTo.ToString("dd-MM-yyyy"),
                                        cancelDt = booking.BkCancelledDate.ToString(),
                                        paymentAmount = booking.BkPaymentAmount,
                                        userName = guest.GuName
                                    };


                    var paymentList = await hsPayList.ToListAsync();

                    if (paymentList.Count > 0)
                    {
                        apiResponse.Data = paymentList;
                        apiResponse.Msg = "Displaying HS Booking List";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Data = null;
                        apiResponse.Msg = "No HS Booking";
                        apiResponse.Result = ResponseTypes.Error;
                    }
                }
                if (model.filter == "Refunded")
                {
                    var hsPayList = from booking in _context.TtBooking
                                    join hs in _context.TmHomestay
                                    on booking.HsId equals hs.HsId
                                    join guest in _context.TmGuestUser
                                    on booking.GuId equals guest.GuId
                                    join vill in _context.TmBlockVillage
                                    on hs.HsVillId equals vill.VillId
                                    where (booking.BkIsCancelled == 1 && booking.IsCancelCheckedByAdmin == 1 && booking.BkPaymentAmount != null && booking.IsCancelCheckedByBankUser == 1)
                                    select new HSRefundListResponseModel
                                    {
                                        hsId = hs.HsId,
                                        bookingId = booking.HsBookingId,
                                        userId = booking.GuId,
                                        villId = hs.HsVillId,
                                        village = vill.VillName,
                                        hsName = hs.HsName,
                                        address = hs.HsAddress1,
                                        person = booking.BkNoPers,
                                        bookingDt = booking.BkBookingDate.ToString("dd-MM-yyyy"),
                                        fromDt = booking.BkDateFrom.ToString("dd-MM-yyyy"),
                                        toDt = booking.BkDateTo.ToString("dd-MM-yyyy"),
                                        cancelDt = booking.BkCancelledDate.ToString(),
                                        paymentAmount = booking.BkPaymentAmount,
                                        userName = guest.GuName
                                    };


                    var paymentList = await hsPayList.ToListAsync();

                    if (paymentList.Count > 0)
                    {
                        apiResponse.Data = paymentList;
                        apiResponse.Msg = "Displaying HS Booking List";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Data = null;
                        apiResponse.Msg = "No HS Booking";
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
        [Route("refundBookingBank")]
        public async Task<IActionResult> RefundBookingBank(HSBookingRefundBankRequestModel hSBookingRefundBank)
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
                        var bookingDet = await _context.TtBooking.AsNoTracking().Where(m => m.HsBookingId == hSBookingRefundBank.bookingId).FirstOrDefaultAsync();
                        bookingDet.IsCancelCheckedByBankUser = 1;
                        bookingDet.BkIsRefundStatus = 1;
                        bookingDet.BkRefundOn = DateTime.Now;
                        bookingDet.BkRefundBy = hSBookingRefundBank.refundBy;
                        bookingDet.BkRefundMode = hSBookingRefundBank.voucherMode;
                        bookingDet.BkRefundStatus = hSBookingRefundBank.voucherStatus;
                        bookingDet.BkRefundAmount = bookingDet.BkPaymentAmount;
                        bookingDet.BkPmtVoucharDate = Convert.ToDateTime(hSBookingRefundBank.voucherDt);
                        bookingDet.BkRfdVoucharNo = hSBookingRefundBank.voucherNo; 
                        _context.TtBooking.Update(bookingDet);
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
