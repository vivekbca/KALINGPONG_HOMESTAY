using System;
using System.Collections.Generic;
using System.IO;
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
    public class PackageFeedbackController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        private readonly IEmailService _emailService;
        public PackageFeedbackController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService, IEmailService emailService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
            _emailService = emailService;
        }

        [HttpPost("Packagelist")]
        public async Task<IActionResult> Packagelist([FromBody] PackageListRequestModel model)
        {
            List<PackageFeedbackListResponseModel> objlist = new List<PackageFeedbackListResponseModel>();
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if (model.filter == "Upcoming")
                {
                    var BookingData = from bk in _context.TtTourBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join bkDt in _context.TtTourDate.AsNoTracking()
                                      on bk.TourDateId equals bkDt.Id
                                      join tour in _context.TmTour.AsNoTracking()
                                      on bk.TourId equals tour.Id
                                      where bk.GuId == model.userid && bk.IsCancel == 0 && bk.IsCompleted == 0 && DateTime.Now <= bkDt.ToDate
                                      select new PackageFeedbackListResponseModel
                                      {
                                          GuId = bk.GuId,
                                          TourId = bk.TourId,
                                          Id = bk.Id,
                                          FromDate = bkDt.FromDate,
                                          ToDate = bkDt.ToDate,
                                          Name = tour.Name,
                                          GuName = gu.GuName,
                                          Destination = tour.Destination
                                      };
                    var BookingList = await BookingData.OrderByDescending(m => m.FromDate).ToListAsync();
                    apiResponse.Data = BookingList;
                    apiResponse.Msg = "Display All Booking List";
                    apiResponse.Result = ResponseTypes.Success;
                }
                if (model.filter == "Availed")
                {

                    var BookingData = from bk in _context.TtTourBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join bkDate in _context.TtTourDate.AsNoTracking()
                                      on bk.TourDateId equals bkDate.Id
                                      join pack in _context.TmTour.AsNoTracking()
                                      on bk.TourId equals pack.Id
                                      where bk.GuId == model.userid && bk.IsCompleted == 1
                                      select new
                                      {
                                          bk.GuId,
                                          bk.TourId,
                                          bk.Id,
                                          bkDate.FromDate,
                                          bkDate.ToDate,
                                          pack.Name,
                                          gu.GuName,
                                          pack.Destination
                                      };
                    var BookingList = await BookingData.OrderByDescending(m => m.FromDate).ToListAsync();
                    
                    foreach (var data in BookingList)
                    {
                        PackageFeedbackListResponseModel obj = new PackageFeedbackListResponseModel();
                        obj.GuId = data.GuId;
                        obj.TourId = data.TourId;
                        obj.Id = data.Id;
                        obj.FromDate = data.FromDate;
                        obj.ToDate = data.ToDate;
                        obj.GuName = data.GuName;
                        obj.Destination = data.Destination;
                        obj.Name = data.Name;
                        var checking = await _context.TtPackageFeedback.Where(m => m.TourBookingId == obj.Id).FirstOrDefaultAsync();
                        if (checking == null)
                        {
                            obj.IsFeedBackView = true;
                        }
                        else
                        {
                            obj.IsFeedBackView = false;
                            obj.IsGiveFeedBack = true;
                        }
                        objlist.Add(obj);
                    }
                    apiResponse.Data = objlist;
                    apiResponse.Msg = "Display All Booking List";
                    apiResponse.Result = ResponseTypes.Success;
                }
                if (model.filter == "Cancelled")
                {
                    var BookingData = from bk in _context.TtTourBooking.AsNoTracking()
                                      join gu in _context.TmGuestUser.AsNoTracking()
                                      on bk.GuId equals gu.GuId
                                      join bkDate in _context.TtTourDate.AsNoTracking()
                                      on bk.TourDateId equals bkDate.Id
                                      join pack in _context.TmTour.AsNoTracking()
                                      on bk.TourId equals pack.Id
                                      where bk.GuId == model.userid && bk.IsCancel == 1
                                      select new PackageFeedbackListResponseModel
                                      {
                                         GuId = bk.GuId,
                                         TourId = bk.TourId,
                                         Id = bk.Id,
                                         FromDate = bkDate.FromDate,
                                         ToDate = bkDate.ToDate,
                                         Name = pack.Name,
                                         GuName = gu.GuName,
                                         Destination = pack.Destination
                                      };
                    var BookingList = await BookingData.OrderByDescending(m => m.FromDate).ToListAsync();
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
        
        [HttpGet("cancelDetails/{id}")]
        public async Task<IActionResult> Canceldetails(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {

                var cancekbookData = from bk in _context.TtTourBooking.AsNoTracking()
                                     join tourDt in _context.TtTourDate
                                     on bk.TourDateId equals tourDt.Id
                                     join tour in _context.TmTour.AsNoTracking()
                                     on bk.TourId equals tour.Id
                                     where bk.Id == id
                                     select new PackageCancelResponseModel
                                     {
                                         tourName = tour.Name,
                                         destination = tour.Destination,
                                         tourId = tour.Id,
                                         tourDateId = bk.TourDateId,
                                         fromDt = tourDt.FromDate.ToString("dd-MM-yyyy"),
                                         toDt = tourDt.ToDate.ToString("dd-MM-yyyy"),
                                         tourBookingId = bk.Id,
                                         totalAmount = bk.TotalRate
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

        [HttpPost("cancelBooking")]
        public async Task<IActionResult> CancelBooking([FromBody] TourCancelRequestModel cancelmodel)
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

                        var tourDetail = await _context.TtTourBooking.Where(m => m.Id == cancelmodel.bookingId).FirstOrDefaultAsync();
                        tourDetail.BankName = cancelmodel.bankName;
                        tourDetail.BankBranch = cancelmodel.branchName;
                        tourDetail.AccountNo = cancelmodel.accNo;
                        tourDetail.AccountType = cancelmodel.accType;
                        tourDetail.Ifsc = cancelmodel.ifsc;
                        tourDetail.IsCancel = 1;
                        tourDetail.CancelledDate = DateTime.Now;
                        tourDetail.CancellationReason = cancelmodel.cancelReason;
                        tourDetail.CancelledBy = cancelmodel.userId;
                        _context.TtTourBooking.Update(tourDetail);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                        var adminId = "f1fb5130-0192-11ec-8831-005056a4479e";
                        var Societyemail = await _context.TmUser.Where(m => m.UserId == adminId).Select(m => m.UserEmailId).FirstOrDefaultAsync();
                        var GUser = await _context.TmGuestUser.Where(m => m.GuId == tourDetail.GuId).FirstOrDefaultAsync();
                        var PackageName = await _context.TmTour.Where(m => m.Id == tourDetail.TourId).Select(m => m.Name).FirstOrDefaultAsync();
                        var tourDate = await _context.TtTourDate.Where(m => m.Id == tourDetail.TourDateId).FirstOrDefaultAsync();
                        await SendTourCancelEmail(tourDetail.RfdVoucherAmount,tourDetail.PaymentAmount,tourDetail.CancelledDate,tourDetail.CancellationReason,PackageName,GUser.GuEmailId, GUser.GuName,tourDate.FromDate,tourDate.ToDate,tourDetail.NoOfPerson);
                        await SendTourCancelEmailSociety(tourDetail.CancelledDate,tourDetail.RfdVoucherAmount, tourDetail.PaymentAmount,tourDetail.CancellationReason, GUser.GuMobileNo, PackageName, Societyemail, GUser.GuEmailId, GUser.GuName, tourDate.FromDate, tourDate.ToDate, tourDetail.NoOfPerson);
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

        [HttpGet("viewDetail/{bid}")]
        public async Task<IActionResult> viewDetail(string bid)///to be displayed last tranction
        {
            try
            { 
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                var cancekbookData = from bk in _context.TtTourBooking.AsNoTracking()
                                     join tourDt in _context.TtTourDate
                                     on bk.TourDateId equals tourDt.Id
                                     join tour in _context.TmTour
                                     on bk.TourId equals tour.Id
                                     where bk.Id == bid
                                     select new PackageCancelResponseModel
                                     {
                                         tourName = tour.Name,
                                         destination = tour.Destination,
                                         tourId = tour.Id,
                                         tourDateId = bk.TourDateId,
                                         fromDt = tourDt.FromDate.ToString("dd-MM-yyyy"),
                                         toDt = tourDt.ToDate.ToString("dd-MM-yyyy"),
                                         tourBookingId = bk.Id,
                                         totalAmount = bk.TotalRate
                                     };

                apiResponse.Data = await cancekbookData.ToListAsync();
                apiResponse.Msg = "Display All Popular List";
                apiResponse.Result = ResponseTypes.Success;

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

        [HttpPost("giveFeedback")]
        public async Task<IActionResult> GiveFeedback([FromBody] TourFeedbackAddRequestModel requestModel)
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
                        var packageFeedback = new TtPackageFeedback
                        {
                            FeedbackId = Guid.NewGuid().ToString(),
                            TourBookingId = requestModel.tourBookingId,
                            HsRatings = requestModel.tourRatings,
                            HsFeedback = requestModel.tourFeedback
                        };
                        _context.TtPackageFeedback.Add(packageFeedback);
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

        [HttpGet("feedbackDetails/{id}")]
        public async Task<IActionResult> Feedbackdetails(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var feedbackDet = await _context.TtPackageFeedback.Where(m => m.TourBookingId == id).Include(m => m.TourBooking).FirstOrDefaultAsync();
                var tourDet = await _context.TtTourDate.Where(m => m.Id == feedbackDet.TourBooking.TourDateId).Include(m => m.Tour).FirstOrDefaultAsync();
                TourFeedbackViewResponseModel viewResponseModel = new TourFeedbackViewResponseModel();
                viewResponseModel.tourId = tourDet.TourId;
                viewResponseModel.tourName = tourDet.Tour.Name;
                viewResponseModel.destination = tourDet.Tour.Destination;
                viewResponseModel.tourDateId = tourDet.Id;
                viewResponseModel.fromDt = tourDet.FromDate.ToString("dd-MM-yyy");
                viewResponseModel.toDt = tourDet.ToDate.ToString("dd-MM-yyy");
                viewResponseModel.feedbackId = feedbackDet.FeedbackId;
                viewResponseModel.feedback = feedbackDet.HsFeedback;
                viewResponseModel.feedbackRating = feedbackDet.HsRatings;

                apiResponse.Data = viewResponseModel;
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

        [NonAction]
        private async Task SendTourCancelEmail(int? Refund,int? Payment,DateTime? CancelDate,string CancelReason,string PackageName,string Email, string name, DateTime from,DateTime to,int person)
        {
            try
            {
                var names = name.Split(' ');
                string firstName = names[0];
                var datefrom = from.ToString("dd-MM-yyyy");
                var dateto = to.ToString("dd-MM-yyyy");
                var totalperson = Convert.ToString(person);
                var path = Path.Combine(_env.ContentRootPath, "Template/TourCancel.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Refund>>", Refund.ToString())
                        .Replace("<<Payment>>", Payment.ToString())
                        .Replace("<<CancelDate>>", Convert.ToDateTime(CancelDate).ToString("dd-MM-yyyy"))
                        .Replace("<<CancelReason>>", CancelReason)
                        .Replace("<<PackageName>>", PackageName)
                        .Replace("<<Name>>", firstName)
                        .Replace("<<TourFrom>>", datefrom)
                        .Replace("<<TotalPerson>>", totalperson)
                        .Replace("<<TourTo>>", dateto);
                //send password to email asynchronously
                await _emailService.Send(Email, name, "Welcome", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private async Task SendTourCancelEmailSociety(DateTime? CancelDate,int? refund,int? payment,string cancelReason,string mobile,string PackageName, string Societyemail,string Email, string name, DateTime from, DateTime to, int person)
        {
            try
            {
                var names = name.Split(' ');
                string firstName = names[0];
                var datefrom = from.ToString("dd-MM-yyyy");
                var dateto = to.ToString("dd-MM-yyyy");
                var totalperson = Convert.ToString(person);
                string CancelDateTime = Convert.ToDateTime(CancelDate).ToString("dd-MM-yyyy");
                var path = Path.Combine(_env.ContentRootPath, "Template/TourCancelSociety.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Refund>>", refund.ToString())
                        .Replace("<<Canceldate>>", CancelDateTime)
                        .Replace("<<Payment>>", payment.ToString())
                        .Replace("<<CancelReason>>", cancelReason)
                        .Replace("<<Mobile>>", mobile)
                        .Replace("<<TourName>>", PackageName)
                        .Replace("<<Name>>", firstName)
                        .Replace("<<TourFrom>>", datefrom)
                        .Replace("<<TotalPerson>>", totalperson)
                        .Replace("<<TourTo>>", dateto);
                //send password to email asynchronously
                await _emailService.Send(Societyemail, name, "Package Cancellation Details", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
