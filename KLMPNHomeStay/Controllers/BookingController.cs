using System;
using System.Collections;
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
    public class BookingController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        private readonly IEmailService _emailService;

        public BookingController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService, IEmailService emailService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
            _emailService = emailService;
        }

        [HttpPost("fetchAvailabilty")]
        public async Task<IActionResult> FetchAvailabilty(FetchAvailabilityRequestModel fetchAvailabilityRequestModel)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };

                FetchAvailabilityResponseModel fetchAvailabilityResponseModel = new FetchAvailabilityResponseModel();
                fetchAvailabilityResponseModel.villId = fetchAvailabilityRequestModel.villId;
                fetchAvailabilityResponseModel.hsId = fetchAvailabilityRequestModel.hsId;

                apiResponse.Data = fetchAvailabilityResponseModel;
                apiResponse.Msg = "Displaying Fetch Availability";
                apiResponse.Result = ResponseTypes.Success;

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        [HttpPost("checkAvailabilty")]
        public async Task<IActionResult> CheckAvailabilty(CheckAvailabilityRequestModel availabilityRequestModel)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                List<CheckAvailabilityResponseModel> checkAvailabilityResponse = new List<CheckAvailabilityResponseModel>();
                var homeStayDet = await _context.TmHomestay.Where(m => m.HsId == availabilityRequestModel.hsId).FirstOrDefaultAsync();
                DateTime fromDt = DateTime.Parse(availabilityRequestModel.fromDt);
                DateTime toDt = DateTime.Parse(availabilityRequestModel.toDt);
                if(fromDt > toDt)
                {
                    apiResponse.Data = null;
                    apiResponse.Msg = "From Date is greater than To Date";
                    apiResponse.Result = ResponseTypes.Error;
                    ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                    return Ok(apiResponseFinal1);
                }
                else
                {
                    List<DateTime> allDates = new List<DateTime>();

                    //for all date
                    //for (DateTime date = fromDt; date <= toDt; date = date.AddDays(1))
                    //    allDates.Add(date);

                    //for all date except checkout date means the last date
                    for (DateTime date = fromDt; date < toDt; date = date.AddDays(1))
                        allDates.Add(date);

                    foreach (var item in allDates)
                    {
                        List<RoomAvailabilityModel> roomAvailabilityModels = new List<RoomAvailabilityModel>();
                        //var hsRoomList = await _context.TmHsRooms.Where(m => m.HsId == availabilityRequestModel.hsId).Include(m => m.Hs.TtBooking).Include(m => m.HsRoomCategory).ToListAsync();
                        var hsRoomList = await _context.TmHsRooms.Where(m => m.HsId == availabilityRequestModel.hsId).Include(m => m.HsRoomCategory).ToListAsync();
                        ArrayList arlist = new ArrayList();
                        if (hsRoomList.Count > 0)
                        {
                            if (hsRoomList.Count >= availabilityRequestModel.noOfRooms)
                            {
                                foreach (var item1 in hsRoomList)
                                {
                                    var roomBookedOrNot = await _context.TtBookingRoomDetail.Where(m => m.HsId == availabilityRequestModel.hsId && m.FromDt == item.Date && m.RoomNo == item1.HsRoomNo).FirstOrDefaultAsync();
                                    if (roomBookedOrNot != null)
                                    {
                                        RoomAvailabilityModel roomAvailabilityModel = new RoomAvailabilityModel();
                                        roomAvailabilityModel.HsId = homeStayDet.HsId;
                                        roomAvailabilityModel.HsRoomNo = item1.HsRoomNo;
                                        roomAvailabilityModel.HsRoomCategoryId = item1.HsRoomCategoryId;
                                        roomAvailabilityModel.HsRoomCategoryName = item1.HsRoomCategory.HsCategoryName;
                                        roomAvailabilityModel.HsRoomRate = item1.HsRoomRate;
                                        roomAvailabilityModel.HsRoomAvailable = item1.HsRoomAvailable;
                                        roomAvailabilityModel.isAvailable = 0;
                                        roomAvailabilityModels.Add(roomAvailabilityModel);
                                    }
                                    else
                                    {
                                        RoomAvailabilityModel roomAvailabilityModel = new RoomAvailabilityModel();
                                        roomAvailabilityModel.HsId = item1.HsId;
                                        roomAvailabilityModel.HsRoomNo = item1.HsRoomNo;
                                        roomAvailabilityModel.HsRoomCategoryId = item1.HsRoomCategoryId;
                                        roomAvailabilityModel.HsRoomCategoryName = item1.HsRoomCategory.HsCategoryName;
                                        roomAvailabilityModel.HsRoomRate = item1.HsRoomRate;
                                        roomAvailabilityModel.HsRoomAvailable = item1.HsRoomAvailable;
                                        roomAvailabilityModel.isAvailable = 1;
                                        roomAvailabilityModels.Add(roomAvailabilityModel);
                                    }
                                }
                                   
                                //foreach (var item1 in hsRoomList)
                                //{
                                //    var bookingList = await _context.TtBooking.Where(m => m.HsId == availabilityRequestModel.hsId && m.BkDateFrom.Date <= item.Date && item.Date <= m.BkDateTo.Date).ToListAsync();
                                //    if (bookingList.Count > 0)
                                //    {
                                //        var bookingRoomList = bookingList.Select(m => m.BkRoomNumber).ToList();

                                //        foreach (var item2 in bookingRoomList)
                                //        {
                                //            string[] split = null;
                                //            split = item2.Split(',');

                                //            for (int i = 0; i < split.Count(); i++)
                                //            {
                                //                arlist.Add(split[i]);
                                //            }
                                //        }
                                //    }
                                //    if (arlist.Contains(item1.HsRoomNo.ToString()))
                                //    {
                                //        RoomAvailabilityModel roomAvailabilityModel = new RoomAvailabilityModel();
                                //        roomAvailabilityModel.HsId = item1.HsId;
                                //        roomAvailabilityModel.HsRoomNo = item1.HsRoomNo;
                                //        roomAvailabilityModel.HsRoomCategoryId = item1.HsRoomCategoryId;
                                //        roomAvailabilityModel.HsRoomCategoryName = item1.HsRoomCategory.HsCategoryName;
                                //        roomAvailabilityModel.HsRoomRate = item1.HsRoomRate;
                                //        roomAvailabilityModel.HsRoomAvailable = item1.HsRoomAvailable;
                                //        roomAvailabilityModel.isAvailable = 0;
                                //        roomAvailabilityModels.Add(roomAvailabilityModel);
                                //    }
                                //    else
                                //    {
                                //        RoomAvailabilityModel roomAvailabilityModel = new RoomAvailabilityModel();
                                //        roomAvailabilityModel.HsId = item1.HsId;
                                //        roomAvailabilityModel.HsRoomNo = item1.HsRoomNo;
                                //        roomAvailabilityModel.HsRoomCategoryId = item1.HsRoomCategoryId;
                                //        roomAvailabilityModel.HsRoomCategoryName = item1.HsRoomCategory.HsCategoryName;
                                //        roomAvailabilityModel.HsRoomRate = item1.HsRoomRate;
                                //        roomAvailabilityModel.HsRoomAvailable = item1.HsRoomAvailable;
                                //        roomAvailabilityModel.isAvailable = 1;
                                //        roomAvailabilityModels.Add(roomAvailabilityModel);
                                //    }
                                //}
                            }
                            else
                            {
                                var roomCount = await _context.TmHsRooms.AsNoTracking().Where(m => m.HsId == availabilityRequestModel.hsId).CountAsync();
                                apiResponse.Msg = "No Room available for this Home Stay,You can't select more than " + roomCount + " rooms";
                                apiResponse.Result = ResponseTypes.Error;
                                ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                                return Ok(apiResponseFinal1);
                            }
                        }
                        else
                        {
                            apiResponse.Msg = "No Room available for this Home Stay";
                            apiResponse.Result = ResponseTypes.Error;
                            ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                            return Ok(apiResponseFinal1);
                        }
                        CheckAvailabilityResponseModel checkAvailabilityResponseModel = new CheckAvailabilityResponseModel();
                        checkAvailabilityResponseModel.date = item.ToString("yyyy-MM-dd");
                        checkAvailabilityResponseModel.roomAvailabilityModels = roomAvailabilityModels;
                        checkAvailabilityResponse.Add(checkAvailabilityResponseModel);
                    }

                    apiResponse.Data = checkAvailabilityResponse;
                    apiResponse.Msg = "Displaying Room Availability";
                    apiResponse.Result = ResponseTypes.Success;
                    ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                    return Ok(apiResponseFinal);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("bookedRooms")]
        public async Task<IActionResult> BookedRooms(List<BookedRoomsRequestModel> bookedRoomsRequestModel)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                BookedRoomsResponseModel bookedRoomsResponse = new BookedRoomsResponseModel();
                List<BookedRooms> bookedRooms = new List<BookedRooms>();
                float totalRate = 0;
                List<int> roomNos = new List<int>();
                if (bookedRoomsRequestModel.Count > 0)
                {
                    foreach(var item in bookedRoomsRequestModel)
                    {
                        foreach(var item1 in item.roomAvailabilityModels)
                        {
                            if(item1.isChecked == true)
                            {
                                BookedRooms booked = new BookedRooms();
                                //booked.bookedDate = item.date;
                                booked.fromDt = DateTime.Parse(item.date).ToString("yyyy-MM-dd");
                                DateTime toDate = DateTime.Parse(booked.fromDt).AddDays(1);
                                booked.toDt = toDate.ToString("yyyy-MM-dd");
                                if (!roomNos.Contains(item1.HsRoomNo))
                                {
                                    roomNos.Add(item1.HsRoomNo);
                                }
                                booked.roomNo = item1.HsRoomNo;
                                booked.rate = item1.HsRoomRate;
                                booked.categoryId = item1.HsRoomCategoryId;
                                booked.categoryName = item1.HsRoomCategoryName;

                                totalRate = totalRate + item1.HsRoomRate;

                                bookedRooms.Add(booked);
                            }
                        }
                    }
                    bookedRoomsResponse.bookedRoomsModels = bookedRooms;
                    bookedRoomsResponse.hsId = bookedRoomsRequestModel.FirstOrDefault().roomAvailabilityModels.FirstOrDefault().HsId;
                    var hsDet = await _context.TmHomestay.AsNoTracking().Where(m => m.HsId == bookedRoomsResponse.hsId).FirstOrDefaultAsync();
                    bookedRoomsResponse.hsName = hsDet.HsName;
                    bookedRoomsResponse.totalRate = totalRate;
                    bookedRoomsResponse.totalNights = bookedRoomsRequestModel.Count;
                    bookedRoomsResponse.totalRooms = roomNos.Count;
                }
                else
                {
                    apiResponse.Msg = "Please book rooms";
                    apiResponse.Result = ResponseTypes.Error;
                    ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                    return Ok(apiResponseFinal1);
                }
                apiResponse.Data = bookedRoomsResponse;
                apiResponse.Msg = "Displaying Booked Room Details";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("discountRate")]
        //Si - Single, S - Senior, C - Cancer
        public async Task<IActionResult> DiscountRate(DiscountRateRequestModel discountRateRequestModel)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                DiscountRateResponseModel discountRateResponseModel = new DiscountRateResponseModel();
                
                //if (discountRateRequestModel.adultNo > 1 && discountRateRequestModel.discountType == "Si")
                //{
                //    apiResponse.Msg = "Guest member should not be more than 1";
                //    apiResponse.Result = ResponseTypes.Error;
                //    ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                //    return Ok(apiResponseFinal1);
                //}
                //else
                //{
                    int discount = 0;

                    if (discountRateRequestModel.discountType == "Si")
                    {
                        discount = discountRateRequestModel.totalAmount - (discountRateRequestModel.totalAmount * discountRateRequestModel.discountRate / 100);
                    }
                    if (discountRateRequestModel.discountType == "S")
                    {
                        discount = discountRateRequestModel.totalAmount - (discountRateRequestModel.totalAmount * discountRateRequestModel.discountRate / 100);
                    }
                    if (discountRateRequestModel.discountType == "C")
                    {
                        discount = discountRateRequestModel.totalAmount - (discountRateRequestModel.totalAmount * discountRateRequestModel.discountRate / 100);
                    }

                    discountRateResponseModel.totalAmount = discountRateRequestModel.totalAmount;
                    discountRateResponseModel.discountAmount = discount;
                //}
                apiResponse.Data = discountRateResponseModel;
                apiResponse.Msg = "Displaying Discount Details";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("bookingDetail")]
        public async Task<IActionResult> BookingDetail(BookingDetailRequestModel bookingDetailRequestModel)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                BookingDetailResponseModel bookingDetailResponseModel = new BookingDetailResponseModel();
                List<BookingResponse> bookingResponses = new List<BookingResponse>();
                var hsDet = await _context.TmHomestay.Where(m => m.HsId == bookingDetailRequestModel.hsId).FirstOrDefaultAsync();
                
                if(bookingDetailRequestModel.bookingRoomDetails.Count > 0)
                {
                    foreach(var item in bookingDetailRequestModel.bookingRoomDetails)
                    {
                        BookingResponse bookingResponse = new BookingResponse();
                        var roomDet = await _context.TmHsRooms.Where(m => m.HsRoomNo == item.roomNo).Include(m => m.HsRoomCategory).FirstOrDefaultAsync();
                        bookingResponse.roomNo = item.roomNo;
                        bookingResponse.categoryId = roomDet.HsRoomCategoryId;
                        bookingResponse.categoryName = roomDet.HsRoomCategory.HsCategoryName;
                        bookingResponse.fromDt = item.fromDt;
                        DateTime todate = DateTime.Parse(bookingResponse.fromDt).AddDays(1);
                        bookingResponse.toDt = todate.ToString("yyyy-MM-dd");
                        bookingResponses.Add(bookingResponse);
                    }
                }
                bookingDetailResponseModel.hsId = bookingDetailRequestModel.hsId;
                bookingDetailResponseModel.adultNo = bookingDetailRequestModel.adultNo;
                bookingDetailResponseModel.childNo = bookingDetailRequestModel.childNo;
                bookingDetailResponseModel.totalRate = bookingDetailRequestModel.totalRate;
                bookingDetailResponseModel.discountRate = bookingDetailRequestModel.discountRate;
                bookingDetailResponseModel.discountType = bookingDetailRequestModel.discountType;
                bookingDetailResponseModel.bookingResponses = bookingResponses;

                apiResponse.Data = bookingDetailResponseModel;
                apiResponse.Msg = "Displaying Booking Room Details";
                apiResponse.Result = ResponseTypes.Success;

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        //[HttpPost("adultRate")]
        //public async Task<IActionResult> AdultRate(AdultRateRequestModel adultRateRequestModel)
        //{
        //    try
        //    {
        //        ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
        //        AdultRateResponseModel adultRateResponseModel  = new AdultRateResponseModel();
        //        adultRateResponseModel.totalAmount = adultRateRequestModel.totalRate;
        //        adultRateResponseModel.totalPerHeadAmount = (adultRateResponseModel.totalAmount * adultRateRequestModel.adultNumber);
        //        apiResponse.Data = adultRateResponseModel;
        //        apiResponse.Msg = "Displaying Adult Details";
        //        apiResponse.Result = ResponseTypes.Success;
        //        ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
        //        return Ok(apiResponseFinal);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //    }
        //}
        [HttpPost("calculatePricing")]
        public async Task<IActionResult> CalculatePricing(CalculatePricingRequestModel calculatePricingRequestModel)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                
                if((calculatePricingRequestModel.adultNo > 1 && calculatePricingRequestModel.discountRate == 25) || (calculatePricingRequestModel.adultNo == 1 && calculatePricingRequestModel.childNo > 0 && calculatePricingRequestModel.discountRate == 25))
                {
                    apiResponse.Msg = "Guest member should not be more than 1";
                    apiResponse.Result = ResponseTypes.Error;
                    ApiResponseModelFinal apiResponseFinal1 = _globalService.GetFinalResponse(apiResponse);
                    return Ok(apiResponseFinal1);
                }

                else
                {
                    CalculatePricingResponseModel calculatePricingResponseModel = new CalculatePricingResponseModel();
                    calculatePricingResponseModel.adultNo = calculatePricingRequestModel.adultNo;
                    calculatePricingResponseModel.childNo = calculatePricingRequestModel.childNo;
                    calculatePricingResponseModel.discountRate = calculatePricingRequestModel.discountRate;
                    calculatePricingResponseModel.totalRoomRate = calculatePricingRequestModel.totalRate;
                    int adultChildAmount = (calculatePricingRequestModel.totalRate * calculatePricingRequestModel.adultNo) + ((calculatePricingRequestModel.totalRate / 2) * calculatePricingRequestModel.childNo);
                    int totalAmount = adultChildAmount - (adultChildAmount * calculatePricingRequestModel.discountRate / 100);
                    calculatePricingResponseModel.totalBillRate = totalAmount;
                    calculatePricingResponseModel.withOutDiscountTotal = adultChildAmount;
                    calculatePricingResponseModel.discountAmount = (adultChildAmount * calculatePricingRequestModel.discountRate / 100);
                    apiResponse.Data = calculatePricingResponseModel;
                    apiResponse.Msg = "Displaying Total Details";
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
        
        [HttpPost("proceedPayBook")]
        public async Task<IActionResult> Booking(BookingRequestModel bookingRequestModel)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                var roomNos = bookingRequestModel.bookDetail.Select(m => m.roomNo).ToList();
                BookingResponseModel bookingResponseModel = new BookingResponseModel();
                string roomNo = string.Empty;
                if(roomNos.Count() == 0)
                {
                    foreach (var item in roomNos)
                    {
                        roomNo = item;
                    }
                }
                else
                {
                    foreach (var item in roomNos)
                    {
                        roomNo = roomNo + "," + item;
                    }
                    //roomNo = roomNo.Remove(roomNo.Length - 1, 1);
                    roomNo = roomNo.Substring(1);
                }
                using (var tran = await _context.Database.BeginTransactionAsync())
                {
                    var booking = new TtBooking
                    {
                        GuId = bookingRequestModel.guId,
                        HsId = bookingRequestModel.hsId,
                        HsBookingId = Guid.NewGuid().ToString(),
                        BkBookingDate = DateTime.Now,
                        TotalCost = bookingRequestModel.totalPrice,
                        BkDateFrom = DateTime.Parse(bookingRequestModel.fromDt),
                        BkDateTo = DateTime.Parse(bookingRequestModel.toDt),
                        BkIsAvailed = 0,
                        BkNoPers = Convert.ToByte(bookingRequestModel.adult + bookingRequestModel.child),
                        BkRoomNumber = roomNo
                    };
                   _context.TtBooking.Add(booking);
                   
                    //add booking room detail
                    foreach(var item in bookingRequestModel.bookDetail)
                    {
                        var bookedrommDet = new TtBookingRoomDetail
                        {
                            Id = Guid.NewGuid().ToString(),
                            BookingId = booking.HsBookingId,
                            HsId = booking.HsId,
                            FromDt = DateTime.Parse(item.fromDt),
                            ToDt = DateTime.Parse(item.fromDt),
                            RoomNo = Convert.ToByte(item.roomNo)
                        };
                        _context.TtBookingRoomDetail.Add(bookedrommDet);
                    }
                    //add 2 points for popularity
                    var hsPopularityDet = await _context.TtHsPopularity.Where(m => m.HsId == booking.HsId).FirstOrDefaultAsync();
                    if(hsPopularityDet != null)
                    {
                        hsPopularityDet.HsSearchCount = hsPopularityDet.HsSearchCount + 2;
                        _context.TtHsPopularity.Update(hsPopularityDet);
                    }
                    else
                    {
                        var hsPopular = new TtHsPopularity
                        {
                            HsId = booking.HsId,
                            HsSearchCount = 2
                        };
                        _context.TtHsPopularity.Add(hsPopular);
                    }

                    await _context.SaveChangesAsync();
                    await tran.CommitAsync();
                    var GuData = await _context.TmGuestUser.Where(m => m.GuId == booking.GuId).FirstOrDefaultAsync();
                    var HomestayData = await _context.TmHomestay.Where(m => m.HsId == booking.HsId).FirstOrDefaultAsync();
                    var bookingdata = await _context.TtBooking.Where(m => m.HsBookingId == booking.HsBookingId).FirstOrDefaultAsync();
                    var hsEmail = HomestayData.HsContactEmail;
                    var adminId = "f1fb5130-0192-11ec-8831-005056a4479e";
                    var Societyemail = await _context.TmUser.Where(m => m.UserId == adminId).Select(m => m.UserEmailId).FirstOrDefaultAsync();
                    await SendBookingMail(bookingdata.BkRoomNumber,bookingdata.BkBookingDate,bookingdata.TotalCost,bookingdata.BkNoPers, GuData.GuEmailId, GuData.GuName, HomestayData.HsName, bookingdata.BkDateFrom, bookingdata.BkDateTo);
                    await SendBookingMailToHomestay(bookingdata.BkBookingDate,bookingdata.TotalCost, GuData.GuMobileNo, hsEmail,bookingdata.BkNoPers, GuData.GuEmailId, GuData.GuName, HomestayData.HsName, bookingdata.BkDateFrom, bookingdata.BkDateTo);
                    await SendBookingMailToSociety(bookingdata.BkBookingDate,bookingdata.TotalCost, GuData.GuMobileNo,Societyemail, bookingdata.BkNoPers, GuData.GuEmailId, GuData.GuName, HomestayData.HsName, bookingdata.BkDateFrom, bookingdata.BkDateTo);
                    bookingResponseModel.bookingId = booking.HsBookingId;
                }
               
                apiResponse.Data = bookingResponseModel;
                apiResponse.Msg = "Booking Detail Saved";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("cancelBooking")]
        public async Task<IActionResult> CancelBooking([FromBody] BookingCancelRequestModel cancelmodel)//This method will insert the GuestUserRegistration into db  {
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

                        var updatedata = await _context.TtBooking.Where(m => m.HsBookingId == cancelmodel.BookingId).FirstOrDefaultAsync();
                        updatedata.BankName = cancelmodel.Bname;
                        updatedata.BankBranch = cancelmodel.BranchName;
                        updatedata.AccountNo = cancelmodel.AccNo;
                        updatedata.AccountType = cancelmodel.AccType;
                        updatedata.Ifsc = cancelmodel.ifsc;
                        updatedata.BkIsCancelled = 1;
                        updatedata.BkCancellationReason = cancelmodel.CancelReason;
                        updatedata.BkCancelledDate = DateTime.Now;
                        _context.TtBooking.Update(updatedata);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                        var homestayId = await _context.TtBooking.Where(m => m.HsBookingId == cancelmodel.BookingId).Select(m => m.HsId).FirstOrDefaultAsync();
                        var Gu_Id = await _context.TtBooking.Where(m => m.HsBookingId == cancelmodel.BookingId).Select(m => m.GuId).FirstOrDefaultAsync();
                        var GuData = await _context.TmGuestUser.Where(m => m.GuId == Gu_Id).FirstOrDefaultAsync();
                        var HomestayData = await _context.TmHomestay.Where(m => m.HsId == homestayId).FirstOrDefaultAsync();
                        var bookingdata = await _context.TtBooking.Where(m => m.HsBookingId == cancelmodel.BookingId).FirstOrDefaultAsync();
                        var hsEmail = HomestayData.HsContactEmail;
                        var adminId = "f1fb5130-0192-11ec-8831-005056a4479e";
                        var Societyemail = await _context.TmUser.Where(m => m.UserId == adminId).Select(m => m.UserEmailId).FirstOrDefaultAsync();
                        await SendCancelMail(bookingdata.BkCancelledDate,bookingdata.BkCancellationReason,bookingdata.BkPaymentAmount,bookingdata.BkRefundAmount,bookingdata.BkNoPers, GuData.GuEmailId, GuData.GuName, HomestayData.HsName, bookingdata.BkDateFrom, bookingdata.BkDateTo);
                        await SendCancelMailToHomestay(bookingdata.BkRefundAmount, bookingdata.BkCancelledDate, bookingdata.BkCancellationReason, bookingdata.TotalCost, GuData.GuMobileNo, hsEmail,bookingdata.BkNoPers, GuData.GuEmailId, GuData.GuName, HomestayData.HsName, bookingdata.BkDateFrom, bookingdata.BkDateTo);
                        await SendCancelMailToSociety(bookingdata.BkRefundAmount,bookingdata.BkCancelledDate, bookingdata.BkCancellationReason, bookingdata.TotalCost, GuData.GuMobileNo ,Societyemail,bookingdata.BkNoPers, GuData.GuEmailId, GuData.GuName, HomestayData.HsName, bookingdata.BkDateFrom, bookingdata.BkDateTo);


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

        [HttpPost("bookingPossible")]
        public async Task<IActionResult> BookingPossible(List<BookedRoomsRequestModel> bookedRoomsRequestModel)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                int bookingNotPossible = 0;
                foreach(var item in bookedRoomsRequestModel)
                {
                    var bookedByDateAtleastOneRoomOrNot = item.roomAvailabilityModels.Where(m => m.isChecked == true && m.isAvailable == 1).FirstOrDefault();
                    if(bookedByDateAtleastOneRoomOrNot != null)
                    {
                        bookingNotPossible = 0;
                    }
                    else
                    {
                        bookingNotPossible = 1;
                    }
                }
                apiResponse.Data = bookingNotPossible;
                apiResponse.Msg = "Displaying Booked Room Details";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [NonAction]
        private async Task SendBookingMail(string RoomNo,DateTime BookingDate,int? Amount,byte person,string email,string name,string HomestayName,DateTime dateFrom,DateTime dateTo)
        {
            try
            {
                var from = dateFrom.ToString("dd-MM-yyyy");
                var To = dateTo.ToString("dd-MM-yyyy");
                var Person = Convert.ToString(person);
                var path = Path.Combine(_env.ContentRootPath, "Template/HomestayBooking.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<HomestayName>>", HomestayName)
                        .Replace("<<RoomNo>>", RoomNo)
                        .Replace("<<BookingDate>>", BookingDate.ToString("dd-MM-yyyy"))
                        .Replace("<<Amount>>", Amount.ToString())
                        .Replace("<<Name>>", name)
                        .Replace("<<TourFrom>>", from)
                        .Replace("<<TourTo>>", To)
                        .Replace("<<TotalPerson>>", Person);
                //send password to email asynchronously
                await _emailService.Send(email, name, "Welcome", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private async Task SendCancelMail(DateTime? CancelDate,string CancelReason,int? Payment,int? Refund,byte person, string email, string name, string HomestayName, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var from = dateFrom.ToString("dd-MM-yyyy");
                var To = dateTo.ToString("dd-MM-yyyy");
                var Person = Convert.ToString(person);
                var path = Path.Combine(_env.ContentRootPath, "Template/CancelHomestay.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<HomestayName>>", HomestayName)
                        .Replace("<<CancelDate>>", Convert.ToDateTime(CancelDate).ToString("dd-MM-yyyy"))
                        .Replace("<<CancelReason>>", CancelReason)
                        .Replace("<<Payment>>", Payment.ToString())
                        .Replace("<<Refund>>", Refund.ToString())
                        .Replace("<<Name>>", name)
                        .Replace("<<TourFrom>>", from)
                        .Replace("<<TourTo>>", To)
                        .Replace("<<TotalPerson>>", Person);
                //send password to email asynchronously
                await _emailService.Send(email, name, "Welcome", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private async Task SendBookingMailToHomestay(DateTime bookingDate,int? cost,string mobile,string hsemail,byte person, string email, string name, string HomestayName, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var from = dateFrom.ToString("dd-MM-yyyy");
                var To = dateTo.ToString("dd-MM-yyyy");
                var Person = Convert.ToString(person);
                var path = Path.Combine(_env.ContentRootPath, "Template/HsBookingHomestay.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Total_Cost>>", Convert.ToString(cost))
                        .Replace("<<Booking_Date>>", bookingDate.ToString("dd-mm-yyyy"))
                        .Replace("<<Mobile>>", mobile)
                        .Replace("<<HomestayName>>", HomestayName)
                        .Replace("<<Name>>", name)
                        .Replace("<<TourFrom>>", from)
                        .Replace("<<TourTo>>", To)
                        .Replace("<<TotalPerson>>", Person);
                //send password to email asynchronously
                await _emailService.Send(hsemail, name, "Booking Details", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [NonAction]
        private async Task SendBookingMailToSociety(DateTime bookingDate,int? cost, string mobile,string Societyemail, byte person, string email, string name, string HomestayName, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var from = dateFrom.ToString("dd-MM-yyyy");
                var To = dateTo.ToString("dd-MM-yyyy");
                var Person = Convert.ToString(person);
                var path = Path.Combine(_env.ContentRootPath, "Template/HsBookingSociety.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Booking_Date>>", bookingDate.ToString("dd-mm-yyyy"))
                        .Replace("<<Total_Cost>>", Convert.ToString(cost))
                        .Replace("<<Mobile>>", mobile)
                        .Replace("<<HomestayName>>", HomestayName)
                        .Replace("<<Name>>", name)
                        .Replace("<<TourFrom>>", from)
                        .Replace("<<TourTo>>", To)
                        .Replace("<<TotalPerson>>", Person);
                //send password to email asynchronously
                await _emailService.Send(Societyemail, name, "Booking Details", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private async Task SendCancelMailToHomestay(int? refund,DateTime? cancelDate,string cancelReason,int? cost, string mobile, string hsemail, byte person, string email, string name, string HomestayName, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var from = dateFrom.ToString("dd-MM-yyyy");
                var To = dateTo.ToString("dd-MM-yyyy");
                var canceldate = Convert.ToString(cancelDate); 
                var Person = Convert.ToString(person);
                var path = Path.Combine(_env.ContentRootPath, "Template/HsCancelHomestay.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Reason>>", cancelReason)
                        .Replace("<<Total_Cost>>", Convert.ToString(cost))
                        .Replace("<<CancelDate>>", canceldate)
                        .Replace("<<Mobile>>", mobile)
                        .Replace("<<Refund>>", Convert.ToString(refund))
                        .Replace("<<HomestayName>>", HomestayName)
                        .Replace("<<Name>>", name)
                        .Replace("<<TourFrom>>", from)
                        .Replace("<<TourTo>>", To)
                        .Replace("<<TotalPerson>>", Person);
                //send password to email asynchronously
                await _emailService.Send(hsemail, name, "Cancellation Details", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private async Task SendCancelMailToSociety(int? refund, DateTime? cancelDate, string cancelReason,int? cost, string mobile, string Societyemail, byte person, string email, string name, string HomestayName, DateTime dateFrom, DateTime dateTo)

        {
            try
            {
                var from = dateFrom.ToString("dd-MM-yyyy");
                var To = dateTo.ToString("dd-MM-yyyy");
                var Person = Convert.ToString(person);
                var canceldate = Convert.ToString(cancelDate);
                var path = Path.Combine(_env.ContentRootPath, "Template/HsCancelSociety.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Reason>>", cancelReason)
                        .Replace("<<Total_Cost>>", Convert.ToString(cost))
                        .Replace("<<CancelDate>>", canceldate)
                        .Replace("<<Mobile>>", mobile)
                        .Replace("<<Refund>>", Convert.ToString(refund))
                        .Replace("<<HomestayName>>", HomestayName)
                        .Replace("<<Name>>", name)
                        .Replace("<<TourFrom>>", from)
                        .Replace("<<TourTo>>", To)
                        .Replace("<<TotalPerson>>", Person);
                //send password to email asynchronously
                await _emailService.Send(Societyemail, name, "Cancellation Details", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
