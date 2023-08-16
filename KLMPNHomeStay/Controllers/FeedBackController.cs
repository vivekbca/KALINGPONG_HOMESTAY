
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public FeedBackController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }

        [HttpGet]
        public async Task<IActionResult> FeedBackListList()//This is the get method for list 
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var feedbackQuery = from fdbk in _context.TtHsFeedback.AsNoTracking()
                                    join bk in _context.TtBooking.AsNoTracking()
                                    on fdbk.HsBookingId equals bk.HsBookingId where fdbk.HsBookingId == bk.HsBookingId
                                    select new { fdbk.FeedbackId, fdbk.HsBookingId, fdbk.HsRatings, fdbk.HsFeedback,
                                        fdbk.IsViewed, fdbk.IsActionTaken, fdbk.ActionDescription, fdbk.ActionTakenBy, fdbk.ActionDate, bk.HsId };
                var feedbackList = await feedbackQuery.ToListAsync();
                apiResponse.Data = feedbackList;
                apiResponse.Msg = "Displaying FeedBack";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("GetFeedBackById/{id}")]
        public async Task<IActionResult> GetFeedBackById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var feedbackQuery = await _context.TtHsFeedback.Where(m => m.FeedbackId == id).FirstOrDefaultAsync();
                if (feedbackQuery == null)
                {
                    apiResponse.Msg = "feedback not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    var feedbackQuery1 = await (from fdbk in _context.TtHsFeedback.AsNoTracking()
                                                join bk in _context.TtBooking.AsNoTracking()
                                                on fdbk.HsBookingId equals bk.HsBookingId
                                                join hs in _context.TmHomestay.AsNoTracking()
                                                on bk.HsId equals hs.HsId
                                                where fdbk.FeedbackId == id
                                                select new
                                                {
                                                    homestayname = hs.HsName,
                                                    feedback = fdbk.HsFeedback,
                                                    room = bk.BkRoomNumber,
                                                    rating = fdbk.HsRatings,
                                                    Datefrom = bk.BkDateFrom.ToString("dd-MM-yyyy"),
                                                    Dateto = bk.BkDateTo.ToString("dd-MM-yyyy"),

                                                }).FirstOrDefaultAsync();



                    apiResponse.Data = feedbackQuery1;
                    apiResponse.Msg = "Displaying FeedBack";
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

        [HttpPost]
        public async Task<IActionResult> FeedBackRegister([FromBody] FeedbackAddRequestModel feedbackAddRequest)//This method will insert the Feedback into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
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

                        var feedback = new TtHsFeedback
                        {
                            FeedbackId = Guid.NewGuid().ToString(),
                            HsBookingId = feedbackAddRequest.HsBookingId,
                            HsRatings = Convert.ToInt32(feedbackAddRequest.HsRatings),
                            HsFeedback = feedbackAddRequest.HsFeedback,
                            IsViewed = 0,
                            IsActionTaken = 0,
                            ActionDescription = null,
                            ActionTakenBy = null,

                        };
                        _context.TtHsFeedback.Add(feedback);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();


                    }
                    apiResponse.Msg = "FeedBack added successfully";
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

        //[HttpGet("HomeStaylist/{id}")]
        //public async Task<IActionResult> HomeStaylistByUserId(string id)///to be displayed last tranction
        //{
        //    try
        //    {
        //        ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
        //        // HomeStayFeedbackModel bookedRoomsResponse = new HomeStayFeedbackModel();

        //        List<HomeStayFeedbackModel> HomeStayRoomBooklist = new List<HomeStayFeedbackModel>();
        //        var bookedRoomss = await _context.TtBooking.Where(m => m.GuId == id).ToListAsync();
        //        var ids = bookedRoomss.Select(m => m.HsBookingId).ToList();
        //        var tran = await _context.TtBankTransaction.Where(m => ids.Contains(m.BookingId)).ToListAsync();

        //        if (bookedRoomss.Count > 0)
        //        {
        //            foreach (var item in bookedRoomss)
        //            {
        //                DateTime today = DateTime.Now;
        //                var homestay = await _context.TmHomestay.Where(m => m.HsId == item.HsId).FirstOrDefaultAsync();
        //                var feed = await _context.TtHsFeedback.Where(m => m.HsBookingId == item.HsBookingId).FirstOrDefaultAsync();
        //                HomeStayFeedbackModel obj = new HomeStayFeedbackModel();
        //                obj.bookedfrom = item.BkDateFrom.ToString("dd-MM-yyyy");
        //                obj.bookedTo = item.BkDateTo.ToString("dd-MM-yyyy");
        //                obj.HomestayName = homestay.HsName;
        //                obj.RoomBooked = item.BkRoomNumber;
        //                obj.BookingId = item.HsBookingId;
        //                obj.isCanceled = item.BkIsCancelled.Value;
        //                obj.isCompleted = item.BkIsAvailed;
        //                obj.isUpcoming = today <= item.BkDateFrom &&item.BkPaymentStatus!="Pending"?false:true;
        //                obj.Address = homestay.HsAddress1;
        //                obj.Amonunt = item.BkPaymentAmount.ToString();
        //                obj.isFeedbackGiven = feed != null ?true:false;
        //                obj.feedbackid = feed!=null? feed.FeedbackId:null;
        //                if (obj.isCompleted==1)
        //                {
        //                    obj.status = "Completed";
        //                }
        //                if (obj.isCanceled==1)
        //                {
        //                    obj.status = "Canceled";
        //                }
        //                if (obj.isUpcoming==true&& obj.isCanceled == 0&& obj.isCompleted == 0)
        //                {
        //                    obj.status = "Upcoming";
        //                }
        //                //if (obj.isCompleted != 1&& obj.isCanceled == 0&& obj.isUpcoming == false)
        //                //{
        //                //    obj.status = "Pending";
        //                //}
        //                HomeStayRoomBooklist.Add(obj);

        //            }

        //        }

        //        else
        //        {
        //            apiResponse.Msg = "No Home Stay Records Found";
        //            apiResponse.Result = ResponseTypes.Error;
        //            ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
        //            return Ok(apiResponseFinal1);
        //        }
        //        apiResponse.Data = HomeStayRoomBooklist;
        //        apiResponse.Msg = "Displaying Booked Home Stay Details";
        //        apiResponse.Result = ResponseTypes.Success;
        //        ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
        //        return Ok(apiResponseFinal);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //    }
        //}

        [HttpGet("viewDetail/{bid}")]
        public async Task<IActionResult> viewDetail(string bid)///to be displayed last tranction
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                // HomeStayFeedbackModel bookedRoomsResponse = new HomeStayFeedbackModel();


                var bookedRoomss = await _context.TtBooking.Where(m => m.HsBookingId == bid).FirstOrDefaultAsync();
                HomeStayFeedbackModel obj = new HomeStayFeedbackModel();
                if (bookedRoomss != null)
                {

                    DateTime today = DateTime.Now;
                    var homestay = await _context.TmHomestay.Where(m => m.HsId == bookedRoomss.HsId).FirstOrDefaultAsync();
                    var feed = await _context.TtHsFeedback.Where(m => m.HsBookingId == bookedRoomss.HsBookingId).FirstOrDefaultAsync();

                    obj.bookedfrom = bookedRoomss.BkDateFrom.ToString("dd-MM-yyyy");
                    obj.bookedTo = bookedRoomss.BkDateTo.ToString("dd-MM-yyyy");
                    obj.HomestayName = homestay.HsName;
                    obj.RoomBooked = bookedRoomss.BkRoomNumber;
                    obj.BookingId = bookedRoomss.HsBookingId;
                    obj.isCanceled = bookedRoomss.BkIsCancelled.Value;
                    obj.isCompleted = bookedRoomss.BkIsAvailed;
                    obj.isUpcoming = today <= bookedRoomss.BkDateFrom ? false : true;
                    obj.Address = homestay.HsAddress1;
                    obj.Amonunt = bookedRoomss.BkPaymentAmount.ToString();
                    obj.isFeedbackGiven = feed != null ? true : false;




                }

                else
                {
                    apiResponse.Msg = "No Home Stay Records Found";
                    apiResponse.Result = ResponseTypes.Error;
                    ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                    return Ok(apiResponseFinal1);
                }
                apiResponse.Data = obj;
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

        [HttpPost("HomeStaylist")]
        public async Task<IActionResult> HomeStaylistByUserId([FromBody] GUBookingHistoryResponseModel model)
        {
            List<FeedBackListResponseModel> objlist = new List<FeedBackListResponseModel>();
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if (model.filter == "Upcoming")
                {
                    var BookingData = from bk in _context.TtBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join hs in _context.TmHomestay.AsNoTracking()
                                      on bk.HsId equals hs.HsId
                                      where bk.GuId == model.userid && bk.BkIsCancelled == 0 && bk.BkIsAvailed == 0 && DateTime.Now <= bk.BkDateTo && bk.BkPaymentAmount!=null
                                      select new
                                      {
                                          bk.GuId,
                                          bk.HsId,
                                          bk.HsBookingId,
                                          bk.BkDateFrom,
                                          bk.BkDateTo,
                                          hs.HsName,
                                          gu.GuName,
                                          hs.HsAddress1
                                      };
                    var BookingList = await BookingData.OrderByDescending(m=>m.BkDateFrom).ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Popular List";
                    apiResponse.Result = ResponseTypes.Success;
                }
                if (model.filter == "Availed")
                {
                    var BookingData = from bk in _context.TtBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join hs in _context.TmHomestay.AsNoTracking()
                                      on bk.HsId equals hs.HsId
                                      where bk.GuId == model.userid && bk.BkIsAvailed == 1
                                      select new
                                      {
                                          bk.GuId,
                                          bk.HsId,
                                          bk.HsBookingId,
                                          bk.BkDateFrom,
                                          bk.BkDateTo,
                                          hs.HsName,
                                          gu.GuName,
                                          hs.HsAddress1
                                      };
                    var BookingList = await BookingData.OrderByDescending(m => m.BkDateFrom).ToListAsync();
                    foreach (var data in BookingList)
                    {
                        FeedBackListResponseModel obj = new FeedBackListResponseModel();
                        obj.GuId = data.GuId;
                        obj.HsId = data.HsId;
                        obj.HsBookingId = data.HsBookingId;
                        obj.BkDateFrom = data.BkDateFrom;
                        obj.BkDateTo = data.BkDateTo;
                        obj.GuName = data.GuName;
                        obj.HsName = data.HsName;
                        obj.HsAddress1 = data.HsAddress1;
                        var checking = await _context.TtHsFeedback.Where(m => m.HsBookingId == obj.HsBookingId).FirstOrDefaultAsync();
                        if (checking == null)
                        {
                            obj.IsFeedBackView = true;
                        }
                        else
                        {
                            obj.IsFeedBackView = false;
                        }
                        objlist.Add(obj);
                    }
                    apiResponse.Data = objlist;
                    apiResponse.Msg = "Display All Popular List";
                    apiResponse.Result = ResponseTypes.Success;
                }
                if (model.filter == "Cancelled")
                {
                    var BookingData = from bk in _context.TtBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join hs in _context.TmHomestay.AsNoTracking()
                                      on bk.HsId equals hs.HsId
                                      where bk.GuId == model.userid && bk.BkIsCancelled == 1
                                      select new
                                      {
                                          bk.GuId,
                                          bk.HsId,
                                          bk.HsBookingId,
                                          bk.BkDateFrom,
                                          bk.BkDateTo,
                                          hs.HsName,
                                          gu.GuName,
                                          hs.HsAddress1,
                                          bk.BkCancelledDate
                                      };
                    var BookingList = await BookingData.OrderByDescending(m => m.BkDateFrom).ToListAsync();
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

        [HttpPost("HomeStaylistCount")]
        public async Task<IActionResult> HomeStaylistByUserIdCount([FromBody] GUBookingHistoryResponseModel model)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if (model.filter == "ALL")
                {
                    int UpcomungCount = await _context.TtBooking.Where(m => m.GuId == model.userid && m.BkIsCancelled == 0 && DateTime.Now <= m.BkDateTo).CountAsync();
                    int AvailedCount = await _context.TtBooking.Where(m => m.GuId == model.userid && m.BkIsAvailed == 1).CountAsync();
                    int BookingData = UpcomungCount + AvailedCount;
                    apiResponse.Data = BookingData;
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

        [HttpPost("PackagelistCount")]
        public async Task<IActionResult> PackagelistByUserIdCount([FromBody] GUBookingHistoryResponseModel model)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if (model.filter == "ALL")
                {
                    var TourData = await _context.TtTourBooking.Where(m => m.GuId == model.userid).FirstOrDefaultAsync();
                    int UpcomingCount = await _context.TtTourBooking.Where(m => m.GuId == model.userid && m.IsCancel == 0).CountAsync();
                    int AvailedCount = await _context.TtTourBooking.Where(m => m.GuId == model.userid && m.IsCompleted == 1).CountAsync();
                    int BookingData = UpcomingCount + AvailedCount;
                    apiResponse.Data = BookingData;
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

        [HttpGet("feedbackDetails/{id}")]
        public async Task<IActionResult> Feedbackdetails(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {

                var FeedbackData = from fbk in _context.TtHsFeedback.AsNoTracking()
                                  join bk in _context.TtBooking.AsNoTracking()
                                  on fbk.HsBookingId equals bk.HsBookingId
                                  join hs in _context.TmHomestay.AsNoTracking()
                                  on bk.HsId equals hs.HsId
                                  where fbk.HsBookingId == id
                                  select new
                                  {
                                      hs.HsName,
                                      hs.HsAddress1,
                                      hs.Pincode,
                                      hs.HsContactEmail,
                                      fbk.FeedbackId,
                                      fbk.HsRatings,
                                      fbk.HsFeedback
                                  };

                    apiResponse.Data =await FeedbackData.ToListAsync();
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

        [HttpGet("cancelDetails/{id}")]
        public async Task<IActionResult> Canceldetails(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {

                var cancekbookData = from bk in _context.TtBooking.AsNoTracking()
                                   join hs in _context.TmHomestay.AsNoTracking()
                                   on bk.HsId equals hs.HsId
                                   where bk.HsBookingId == id
                                   select new
                                   {
                                       hs.HsName,
                                       hs.HsAddress1,
                                       hs.Pincode,
                                       hs.HsContactEmail,
                                       bk.HsBookingId,
                                       bk.HsId,
                                       bk.BkBookingDate,
                                       bk.BkDateFrom,
                                       bk.BkDateTo,
                                       bk.BkNoPers,
                                       bk.BkPaymentAmount
                                   };

                apiResponse.Data = await cancekbookData.ToListAsync();
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
