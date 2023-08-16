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
    public class TenderController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public TenderController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        [Route("getTenderList")]
        public async Task<IActionResult> GetTenderList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var tenderList = await (from a in _context.TmTender.OrderByDescending(m => m.PublishingDate)
                                        join b in _context.TmFinancialYear on a.FinancialYearId equals b.FinancialYearId
                                        select new TenderResponseModel
                                        {
                                           tenderId = a.TenderId,
                                           subject = a.Subject,
                                           finYrId = a.FinancialYearId,
                                           finYr = b.FinancialYear,
                                           publishingDate = a.PublishingDate.ToString("yyyy-MM-dd"),
                                           memoNo = a.MemoNo,
                                           isPublished = a.IsPublished,
                                           closingDate = a.ClosingDate.ToString("yyyy-MM-dd"),
                                           fileName = a.FileName
                                        }).ToListAsync();
                apiResponse.Data = tenderList;
                apiResponse.Msg = "Displaying Tender List";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenderById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var tenderDet = await _context.TmTender.Where(m => m.TenderId == id).Include(m => m.FinancialYear).FirstOrDefaultAsync();

                if (tenderDet == null)
                {
                    apiResponse.Msg = "Tender not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    TenderResponseModel tenderResponseModel = new TenderResponseModel();
                    tenderResponseModel.tenderId = tenderDet.TenderId;
                    tenderResponseModel.subject = tenderDet.Subject;
                    tenderResponseModel.finYrId = tenderDet.FinancialYearId;
                    tenderResponseModel.finYr = tenderDet.FinancialYear.FinancialYear;
                    tenderResponseModel.publishingDate = tenderDet.PublishingDate.ToString("yyyy-MM-dd");
                    tenderResponseModel.memoNo = tenderDet.MemoNo;
                    tenderResponseModel.isPublished = tenderDet.IsPublished;
                    tenderResponseModel.closingDate = tenderDet.ClosingDate.ToString("yyyy-MM-dd");
                    tenderResponseModel.fileName = tenderDet.FileName;

                    apiResponse.Data = tenderResponseModel;
                    apiResponse.Msg = "Displaying Tender";
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
        //[Route("createTender/{tenderId}/{subject}/{finYrId}/{memoNo}/{closingDate}")]
        [Route("createTender")]
        public async Task<IActionResult> CreateTender([FromForm] TenderAddRequestModel tenderAddRequestModel)//This method will insert the tender into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            string tenderFileName = string.Empty;
            IFormFile file = null;
            //var tenderAddRequestModel = new TenderAddRequestModel
            //{
            //   tenderId = tenderId,
            //   subject = subject,
            //   finYrId = finYrId,
            //   memoNo = memoNo,
            //   closingDate = closingDate
            //};
            try
            {

                //if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                //    throw new ArgumentException("Invalid request");

                //if (Request.Form.Files.Count > 0)
                //    file = Request.Form.Files[0];
                file = tenderAddRequestModel.tenderFile;
                bool hasFile = false;
                if (file != null)
                {
                    if (file.Length > 0)
                        hasFile = true;
                }
                if (!hasFile)
                    throw new InvalidOperationException("File not selected");
                string extension = Path.GetExtension(file.FileName);
                tenderFileName = file == null ? null : string.Format("{0}{1}", Guid.NewGuid(), extension);

                if (ModelState.IsValid)
                {
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                        var tenderDet = new TmTender
                        {
                            TenderId = Guid.NewGuid().ToString(),
                            Subject = tenderAddRequestModel.subject,
                            MemoNo = tenderAddRequestModel.memoNo,
                            FinancialYearId = tenderAddRequestModel.finYrId,
                            ClosingDate = DateTime.Parse(tenderAddRequestModel.closingDate),
                            PublishingDate = DateTime.Now,
                            IsPublished = 1,
                            FileName = tenderFileName
                        };
                        _context.TmTender.Add(tenderDet);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }
                    if (file != null)
                    {
                        SaveTender(tenderFileName, file, hasFile);

                    }
                    apiResponse.Msg = "Tender added successfully";
                    apiResponse.Result = ResponseTypes.Success;
                }
                else
                {
                    apiResponse.Msg = "Model State not valid";
                    apiResponse.Result = ResponseTypes.ModelErr;
                }

                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        public bool SaveTender(string filename, IFormFile file, bool hasFile)
        {
            bool returnType = false;
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Tender");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string path1 = null;
                path1 = Path.Combine(path, filename);
                if (hasFile)
                {
                    using (var stream = new FileStream(path1, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                returnType = false;
            }

            return returnType;
        }

        [HttpPost]
        //[Route("updateTender/{tenderId}/{subject}/{finYrId}/{memoNo}/{closingDate}")]
        [Route("updateTender")]
        public async Task<IActionResult> UpdateTender([FromForm] TenderAddRequestModel tenderAddRequestModel)//This method will update the tender into db  {
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            string tenderFileName = string.Empty;
            IFormFile file = null;
            //var tenderAddRequestModel = new TenderAddRequestModel
            //{
            //    tenderId = tenderId,
            //    subject = subject,
            //    finYrId = finYrId,
            //    memoNo = memoNo,
            //    closingDate = closingDate
            //};
            try
            {
                var tenderDet = await _context.TmTender.Where(m => m.TenderId == tenderAddRequestModel.tenderId).FirstOrDefaultAsync();
                if (tenderDet != null)
                {

                    bool hasFile = false;
                    //if (MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                    //{
                        if (tenderAddRequestModel.tenderFile != null)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "Tender", tenderDet.FileName);
                            System.IO.File.Delete(path);
                            file = tenderAddRequestModel.tenderFile;
                        }
                        //if (!hasFile)
                        //    throw new InvalidOperationException("File not selected");
                        if (file != null)
                        {
                            if (file.Length > 0)
                                hasFile = true;
                        string extension = Path.GetExtension(file.FileName);
                        tenderFileName = file == null ? null : string.Format("{0}{1}", Guid.NewGuid(), extension);
                        }
                    //}

                    if (ModelState.IsValid)
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            tenderDet.Subject = tenderAddRequestModel.subject;
                            tenderDet.FinancialYearId = tenderAddRequestModel.finYrId;
                            tenderDet.ClosingDate = DateTime.Parse(tenderAddRequestModel.closingDate);
                            tenderDet.MemoNo = tenderAddRequestModel.memoNo;

                            if (tenderFileName != "")
                                tenderDet.FileName = tenderFileName;

                            _context.TmTender.Update(tenderDet);
                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                        }
                        if (file != null)
                        {
                            SaveTender(tenderFileName, file, hasFile);

                        }
                        apiResponse.Msg = "Tender update successfully";
                        apiResponse.Result = ResponseTypes.Success;
                    }
                    else
                    {
                        apiResponse.Msg = "Model State not valid";
                        apiResponse.Result = ResponseTypes.ModelErr;
                    }
                }
                else
                {
                    apiResponse.Msg = "Tender not found";
                    apiResponse.Result = ResponseTypes.Info;
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
        [Route("viewTender/{tenderId}")]
        public IActionResult View(string tenderId)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "");
            Guid siteId = Guid.Empty;
            FileStream stream = null;
            var tenderDet = _context.TmTender.Where(m => m.TenderId == tenderId).FirstOrDefault();

            var resourceBasePath = Directory.GetCurrentDirectory();
            resourceBasePath = Path.Combine(resourceBasePath, "Tender");

            string filePath = null;
            var fileResult = tenderDet.FileName;

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
    }
}
