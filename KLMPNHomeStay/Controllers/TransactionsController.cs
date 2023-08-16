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

    public class TransactionsController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public TransactionsController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTransactionList()//This is the get method for list
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var SessionId = "70bfdd12-43de-42eb-90f1-a008a400b046";
                var PreviousTranstFirstQuery = from prevtrans in _context.TtBooking.AsNoTracking()
                                               join hmsty in _context.TmHomestay.AsNoTracking()
                                               on prevtrans.HsId equals hmsty.HsId
                                               where (prevtrans.HsId== hmsty.HsId && prevtrans.BkIsCancelled==1 && prevtrans.GuId==SessionId)
                                               select new
                                               {
                                                   hmsty.HsName,
                                                   prevtrans.GuId,
                                                   prevtrans.HsId,
                                                   prevtrans.HsBookingId,
                                                   prevtrans.BkBookingDate,
                                                   prevtrans.BkDateFrom,
                                                   prevtrans.BkDateTo,
                                                   prevtrans.BkNoPers,
                                                   prevtrans.BkRoomNumber,
                                                   prevtrans.BkIsAvailed,
                                                   prevtrans.BkPaymentMode,
                                                   prevtrans.BkPaymentStatus,
                                                   prevtrans.BkPaymentAmount,
                                                   prevtrans.BkPmtVoucharNo,
                                                   prevtrans.BkPmtVoucharDate,
                                                   prevtrans.BkIsCancelled,
                                                   prevtrans.BkCancelledDate,
                                                   prevtrans.BkRefundMode,
                                                   prevtrans.BkRefundStatus,
                                                   prevtrans.BkRefundAmount,
                                                   prevtrans.BkRfdVoucharNo,
                                                   prevtrans.BkRfdVoucharDate
                                               };
                
                var PreviousTranstSecondQuery = from prevtrans in _context.TtBooking.AsNoTracking()
                                                join hmsty in _context.TmHomestay.AsNoTracking()
                                                on prevtrans.HsId equals hmsty.HsId
                                                where (prevtrans.HsId == hmsty.HsId && prevtrans.GuId == SessionId && prevtrans.BkIsCancelled == 0 && prevtrans.BkDateTo < DateTime.Now 
                                                        && prevtrans.BkRefundStatus==null)
                                                select new
                                                {
                                                    hmsty.HsName,
                                                    prevtrans.GuId,
                                                    prevtrans.HsId,
                                                    prevtrans.HsBookingId,
                                                    prevtrans.BkBookingDate,
                                                    prevtrans.BkDateFrom,
                                                    prevtrans.BkDateTo,
                                                    prevtrans.BkNoPers,
                                                    prevtrans.BkRoomNumber,
                                                    prevtrans.BkIsAvailed,
                                                    prevtrans.BkPaymentMode,
                                                    prevtrans.BkPaymentStatus,
                                                    prevtrans.BkPaymentAmount,
                                                    prevtrans.BkPmtVoucharNo,
                                                    prevtrans.BkPmtVoucharDate,
                                                    prevtrans.BkIsCancelled,
                                                    prevtrans.BkCancelledDate,
                                                    prevtrans.BkRefundMode,
                                                    prevtrans.BkRefundStatus,
                                                    prevtrans.BkRefundAmount,
                                                    prevtrans.BkRfdVoucharNo,
                                                    prevtrans.BkRfdVoucharDate
                                                };
                var PreviousTranstList = await PreviousTranstFirstQuery.ToListAsync();
                var PreviousTranstListsecond = await PreviousTranstSecondQuery.ToListAsync();
                PreviousTranstList.AddRange(PreviousTranstListsecond);
                if(PreviousTranstList.Count == 0)
                {
                    apiResponse.Data = PreviousTranstList;
                    apiResponse.Msg = "No Data Found";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    apiResponse.Data = PreviousTranstList;
                    apiResponse.Msg = "Displaying Transaction List";
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
        
    }
}
