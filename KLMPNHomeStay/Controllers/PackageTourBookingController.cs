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
    public class PackageTourBookingController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        private readonly IEmailService _emailService;
        public PackageTourBookingController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService,IEmailService emailService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
            _emailService = emailService;
        }
        [HttpGet]
        [Route("getTourList")]
        public async Task<IActionResult> GetPackageTourList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                List<PackageTourListResponseModel> packageTourListResponseModels = new List<PackageTourListResponseModel>();
                var tourList = await _context.TmTour.Where(m => m.IsActive == 1).OrderByDescending(m => m.CreatedOn).ToListAsync();
                if(tourList.Count > 0)
                {
                    foreach(var item in tourList)
                    {
                        PackageTourListResponseModel packageTourListResponse = new PackageTourListResponseModel();
                        packageTourListResponse.tourId = item.Id;
                        packageTourListResponse.tourName = item.Name;
                        packageTourListResponse.destination = item.Description;
                        packageTourListResponse.destinationDay = item.DestinationDay;
                        packageTourListResponse.destinationNight = item.DestinationNight;
                        packageTourListResponse.cost = item.Cost;
                        packageTourListResponse.contactPersonName = item.ContactPersonName;
                        packageTourListResponse.contactPersonEmail = item.ContactPersonNameEmail;
                        packageTourListResponse.contactPersonMobile = item.ContactPersonMobile;
                        packageTourListResponse.img = item.Image1;
                        packageTourListResponseModels.Add(packageTourListResponse);
                    }
                    apiResponse.Data = packageTourListResponseModels;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                TourDetailResponseModel tourDetailResponse = new TourDetailResponseModel();
                List<HomestayAminitiestesting> aminitieslistTesting = new List<HomestayAminitiestesting>();
                var tourDet = await _context.TmTour.Where(m => m.Id == id).FirstOrDefaultAsync();
                if(tourDet != null)
                {
                    tourDetailResponse.tourId = tourDet.Id;
                    tourDetailResponse.tourName = tourDet.Name;
                    tourDetailResponse.destination = tourDet.Destination;
                    tourDetailResponse.destinationDay = tourDet.DestinationDay;
                    tourDetailResponse.destinationNight = tourDet.DestinationNight;
                    tourDetailResponse.cost = tourDet.Cost;
                    tourDetailResponse.subject = tourDet.Subject;
                    tourDetailResponse.description = tourDet.Description;
                    tourDetailResponse.contactPersonName = tourDet.ContactPersonName;
                    tourDetailResponse.contactPersonMobile = tourDet.ContactPersonMobile;
                    tourDetailResponse.contactPersonEmail = tourDet.ContactPersonNameEmail;
                    tourDetailResponse.img1 = tourDet.Image1;
                    tourDetailResponse.img2 = tourDet.Image2;
                    tourDetailResponse.img3 = tourDet.Image3;
                    tourDetailResponse.img4 = tourDet.Image4;
                    tourDetailResponse.img5 = tourDet.Image5;
                    tourDetailResponse.tourPDFFile = tourDet.TourPdfFile;


                    //My Aminities Code
                   
                   
                    
                   
                    
                    //My Aminities Code End
                    if (tourDet.FacilityId1 != null)
                    {
                        HomestayAminitiestesting data = new HomestayAminitiestesting();
                        data.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId1).Select(m => m.FileName).FirstOrDefaultAsync();
                        data.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId1).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data);
                        tourDetailResponse.facility1Id = tourDet.FacilityId1;
                        var facilityDet = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId1).FirstOrDefaultAsync();
                        tourDetailResponse.facility1Name = facilityDet.HsFacilityName;
                        tourDetailResponse.facility1Img = facilityDet.FileName;
                        var img = facilityDet.FileName;
                        tourDetailResponse.facilities.Add(img);
                    }
                    if (tourDet.FacilityId2 != null)
                    {
                        HomestayAminitiestesting data2 = new HomestayAminitiestesting();
                        data2.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId2).Select(m => m.FileName).FirstOrDefaultAsync();
                        data2.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId2).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data2);
                        tourDetailResponse.facility2Id = tourDet.FacilityId2;
                        var facilityDet = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId2).FirstOrDefaultAsync();
                        tourDetailResponse.facility2Name = facilityDet.HsFacilityName;
                        tourDetailResponse.facility2Img = facilityDet.FileName;
                        var img = facilityDet.FileName;
                        tourDetailResponse.facilities.Add(img);
                    }
                    if (tourDet.FacilityId3 != null)
                    {
                        HomestayAminitiestesting data3 = new HomestayAminitiestesting();
                        data3.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId3).Select(m => m.FileName).FirstOrDefaultAsync();
                        data3.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId3).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data3);
                        tourDetailResponse.facility3Id = tourDet.FacilityId3;
                        var facilityDet = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId3).FirstOrDefaultAsync();
                        tourDetailResponse.facility3Name = facilityDet.HsFacilityName;
                        tourDetailResponse.facility3Img = facilityDet.FileName;
                        var img = facilityDet.FileName;
                        tourDetailResponse.facilities.Add(img);
                    }
                    if (tourDet.FacilityId4 != null)
                    {
                        HomestayAminitiestesting data4 = new HomestayAminitiestesting();
                        data4.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId4).Select(m => m.FileName).FirstOrDefaultAsync();
                        data4.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId4).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data4);
                        tourDetailResponse.facility4Id = tourDet.FacilityId4;
                        var facilityDet = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId4).FirstOrDefaultAsync();
                        tourDetailResponse.facility4Name = facilityDet.HsFacilityName;
                        tourDetailResponse.facility4Img = facilityDet.FileName;
                        var img = facilityDet.FileName;
                        tourDetailResponse.facilities.Add(img);
                    }
                    if (tourDet.FacilityId5 != null)
                    {
                        HomestayAminitiestesting data5 = new HomestayAminitiestesting();
                        data5.amin = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId5).Select(m => m.FileName).FirstOrDefaultAsync();
                        data5.aminName = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId5).Select(m => m.HsFacilityName).FirstOrDefaultAsync();
                        aminitieslistTesting.Add(data5);
                        tourDetailResponse.facility1Id = tourDet.FacilityId5;
                        var facilityDet = await _context.TmHsFacilities.Where(m => m.HsFacilityId == tourDet.FacilityId5).FirstOrDefaultAsync();
                        tourDetailResponse.facility5Name = facilityDet.HsFacilityName;
                        tourDetailResponse.facility5Img = facilityDet.FileName;
                        var img = facilityDet.FileName;
                        tourDetailResponse.facilities.Add(img);

                    }
                    tourDetailResponse.HSAmImg = aminitieslistTesting;
                }
                apiResponse.Data = tourDetailResponse;
                apiResponse.Msg = "Displaying Tour";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("tourFile/{tourId}")]
        public IActionResult View(string tourId)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "");
            Guid siteId = Guid.Empty;
            FileStream stream = null;
            var tourDet = _context.TmTour.Where(m => m.Id == tourId).FirstOrDefault();

            var resourceBasePath = Directory.GetCurrentDirectory();
            resourceBasePath = Path.Combine(resourceBasePath, "Tour");

            string filePath = null;
            var fileResult = tourDet.TourPdfFile;

            try
            {
                if (fileResult != null)
                {
                    filePath = Path.Combine(resourceBasePath, fileResult);
                    stream = new FileStream(@filePath, FileMode.Open);

                    if (stream == null)
                        return NotFound();
                    else if (stream.Length == 0)
                        return NotFound();
                    else
                        return new FileStreamResult(stream, "application/pdf");
                }
            }
            catch (Exception)
            {
                filePath = "File Not Found!";
            }
            return null;
        }

        [HttpGet]
        [Route("getTourDateList/{tourId}")]
        public async Task<IActionResult> GetPackageDateList(string tourId)//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                List<PackageDateListResponseModel> packageDateListResponseModels = new List<PackageDateListResponseModel>();
                DateTime dateAfterFourDay = DateTime.Now.AddDays(4);
                var tourDtList = await _context.TtTourDate.Where(m => m.IsActive == 1 && m.TourId == tourId && m.FromDate > dateAfterFourDay).ToListAsync();
                if (tourDtList.Count > 0)
                {
                    foreach (var item in tourDtList)
                    {
                        PackageDateListResponseModel packageDateListResponse = new PackageDateListResponseModel();
                        packageDateListResponse.dateId = item.Id;
                        packageDateListResponse.tourId = item.TourId;
                        packageDateListResponse.fromDt = item.FromDate.ToString("dd-MM-yyyy");
                        packageDateListResponse.toDt = item.ToDate.ToString("dd-MM-yyyy");
                        packageDateListResponse.isActive = item.IsActive;
                        packageDateListResponseModels.Add(packageDateListResponse);
                    }
                    apiResponse.Data = packageDateListResponseModels;
                    apiResponse.Msg = "Displaying Tour Date List";
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
        [Route("getTourDateListforadmin/{tourId}")]
        public async Task<IActionResult> GetPackageDateListForAdmin(string tourId)//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                List<PackageDateListResponseModel> packageDateListResponseModels = new List<PackageDateListResponseModel>();
                var tourDtList = await _context.TtTourDate.Where(m => m.IsActive == 1 && m.TourId == tourId).ToListAsync();
                if (tourDtList.Count > 0)
                {
                    foreach (var item in tourDtList)
                    {
                        PackageDateListResponseModel packageDateListResponse = new PackageDateListResponseModel();
                        packageDateListResponse.dateId = item.Id;
                        packageDateListResponse.tourId = item.TourId;
                        packageDateListResponse.fromDt = item.FromDate.ToString("dd-MM-yyyy");
                        packageDateListResponse.toDt = item.ToDate.ToString("dd-MM-yyyy");
                        packageDateListResponse.isActive = item.IsActive;
                        packageDateListResponseModels.Add(packageDateListResponse);
                    }
                    apiResponse.Data = packageDateListResponseModels;
                    apiResponse.Msg = "Displaying Tour Date List";
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
        [Route("dateTourBooking")]
        public async Task<IActionResult> BookingDateList(TourBookingRequestModel tourBookingRequest)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                TourBookingDateResponseModel tourBookingDateResponse = new TourBookingDateResponseModel();
                var tourDateDet = await _context.TtTourDate.Where(m => m.TourId == tourBookingRequest.tourId && m.Id == tourBookingRequest.dateId).FirstOrDefaultAsync();
                if(tourDateDet != null)
                {
                    tourBookingDateResponse.dateId = tourDateDet.Id;
                    tourBookingDateResponse.fromDt = tourDateDet.FromDate.ToString("dd-MM-yyyy");
                    tourBookingDateResponse.toDt = tourDateDet.ToDate.ToString("dd-MM-yyyy");
                }
                apiResponse.Data = tourBookingDateResponse;
                apiResponse.Msg = "Displaying Tour Date List";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("totalCost")]
        public async Task<IActionResult> TotalCost(TourCostRequestModel tourCostRequestModel)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                TourCostResponseModel tourCostResponseModel  = new TourCostResponseModel();
                var tourDet = await _context.TmTour.Where(m => m.Id == tourCostRequestModel.tourId).FirstOrDefaultAsync();
                int totalCost = 0;
                if (tourDet != null)
                    totalCost = tourCostRequestModel.person * tourDet.Cost;

                tourCostResponseModel.totalCost = totalCost;
                apiResponse.Data = tourCostResponseModel;
                apiResponse.Msg = "Displaying Tour Date List";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("proceedPayment")]
        public async Task<IActionResult> ProceedPayment(ProceedPaymentRequestModel proceedPaymentRequests)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                TourBookingResponseModel tourBookingResponse = new TourBookingResponseModel();
                using (var tran = await _context.Database.BeginTransactionAsync())
                {
                    var tourBooking = new TtTourBooking
                    {
                        Id = Guid.NewGuid().ToString(),
                        TourId = proceedPaymentRequests.tourId,
                        TourDateId = proceedPaymentRequests.tourDateId,
                        GuId = proceedPaymentRequests.guId,
                        BookingDate = DateTime.Now,
                        TotalRate = proceedPaymentRequests.totalCost,
                        NoOfPerson = proceedPaymentRequests.person,
                        IsCompleted = 0,
                        IsCancel = 0
                    };
                    _context.TtTourBooking.Add(tourBooking);
                    List<TtTourBookingDetail> tourBookingDetails = new List<TtTourBookingDetail>();
                    foreach(var item in proceedPaymentRequests.guestDet)
                    {
                        var ttGuestDetail = new TtTourBookingDetail
                        {
                            Id = Guid.NewGuid().ToString(),
                            TourBookingId = tourBooking.Id,
                            FirstName = item.gFirstName,
                            LastName = item.gLastName,
                            Dob = DateTime.Parse(item.gDOB),
                            Sex = item.gGender
                        };
                        tourBookingDetails.Add(ttGuestDetail);
                    }
                    _context.TtTourBookingDetail.AddRange(tourBookingDetails);
                    tourBookingResponse.tourBookingId = tourBooking.Id;
                    await _context.SaveChangesAsync();
                    await tran.CommitAsync();
                    var adminId = "f1fb5130-0192-11ec-8831-005056a4479e";
                    var Societyemail = await _context.TmUser.Where(m => m.UserId == adminId).Select(m => m.UserEmailId).FirstOrDefaultAsync();
                    var GuData = await _context.TmGuestUser.Where(m => m.GuId == proceedPaymentRequests.guId).FirstOrDefaultAsync();
                    var PackageData = await _context.TtTourBooking.Where(m => m.Id == tourBooking.Id).FirstOrDefaultAsync();
                    var TourName = await _context.TmTour.Where(m => m.Id == PackageData.TourId).Select(m=>m.Name).FirstOrDefaultAsync();
                    var Tourdate = await _context.TtTourDate.Where(m => m.Id == proceedPaymentRequests.tourDateId).FirstOrDefaultAsync();
                    await SendBookingMail(PackageData.TotalRate, PackageData.BookingDate,TourName, PackageData.NoOfPerson, GuData.GuEmailId, GuData.GuName, Tourdate.FromDate, Tourdate.ToDate);
                    await SendBookingMailSociety(PackageData.PaymentAmount, TourName,PackageData.BookingDate, GuData.GuMobileNo,Societyemail,PackageData.NoOfPerson, GuData.GuEmailId, GuData.GuName, Tourdate.FromDate, Tourdate.ToDate);
                }
                apiResponse.Data = tourBookingResponse;
                apiResponse.Msg = "Tour Booking Detail Saved";
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
        private async Task SendBookingMail(int Amount,DateTime BookingDate,string PackageName,int person, string email, string name,DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var from = dateFrom.ToString("dd-MM-yyyy");
                var To = dateTo.ToString("dd-MM-yyyy");
                var Person = Convert.ToString(person);
                var path = Path.Combine(_env.ContentRootPath, "Template/TourBooking.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<Amount>>", Amount.ToString())
                        .Replace("<<BookingDate>>", BookingDate.ToString("dd-MM-yyyy"))
                        .Replace("<<PackageName>>", PackageName)
                        .Replace("<<Name>>", name)
                        .Replace("<<TourFrom>>", from)
                        .Replace("<<TourTo>>", To)
                        .Replace("<<TotalPerson>>", Person);
                //send password to email asynchronously
                await _emailService.Send(email, name, "Package Booking Details", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private async Task SendBookingMailSociety(int? PaymentAmt,string TourName,DateTime bookingDate,string mobile,string societymail,int person, string email, string name, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var from = dateFrom.ToString("dd-MM-yyyy");
                var To = dateTo.ToString("dd-MM-yyyy");
                var Person = Convert.ToString(person);
                var path = Path.Combine(_env.ContentRootPath, "Template/TourBookingSociety.html");
                string content = System.IO.File.ReadAllText(path);
                string content1
                        = content
                        .Replace("<<TourName>>", TourName)
                        .Replace("<<PaymentAmt>>", PaymentAmt.ToString())
                        .Replace("<<BookingDate>>", bookingDate.ToString("dd-MM-yyyy"))
                        .Replace("<<Mobile>>", mobile)
                        .Replace("<<Name>>", name)
                        .Replace("<<TourFrom>>", from)
                        .Replace("<<TourTo>>", To)
                        .Replace("<<TotalPerson>>", Person);
                //send password to email asynchronously
                await _emailService.Send(societymail, name, "Package Booking Details", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
