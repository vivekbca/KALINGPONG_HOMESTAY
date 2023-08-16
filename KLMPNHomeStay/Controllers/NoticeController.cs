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
    public class NoticeController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public NoticeController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }
        [HttpGet]
        [Route("getNoticeList")]
        public async Task<IActionResult> GetNoticeList()//This is the get method for list  
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                //var countryList = await _context.TmCountry.AsNoTracking().ToListAsync();
                var noticeList = await (from a in _context.TmNotice.OrderByDescending(m => m.PublishingDate)
                                        select new NoticeResponseModel
                                         {
                                             noticeId = a.NoticeId,
                                             subject = a.Subject,
                                             heading = a.Heading,
                                             publishingDate = a.PublishingDate.ToString("yyyy-MM-dd"),
                                             fileName = a.FileName,
                                             isDeleted = a.IsDeleted
                                         }).ToListAsync();
                apiResponse.Data = noticeList;
                apiResponse.Msg = "Displaying Notice List";
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
        public async Task<IActionResult> GetNoticeById(string id)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                var noticeDet = await _context.TmNotice.Where(m => m.NoticeId == id).FirstOrDefaultAsync();

                if (noticeDet == null)
                {
                    apiResponse.Msg = "Notice not found";
                    apiResponse.Result = ResponseTypes.Info;
                }
                else
                {
                    NoticeResponseModel noticeResponseModel = new NoticeResponseModel();
                    noticeResponseModel.noticeId = noticeDet.NoticeId;
                    noticeResponseModel.heading = noticeDet.Heading;
                    noticeResponseModel.subject = noticeDet.Subject;
                    noticeResponseModel.publishingDate = noticeDet.PublishingDate.ToString("yyyy-MM-dd");
                    noticeResponseModel.fileName = noticeDet.FileName;
                    noticeResponseModel.isDeleted = noticeDet.IsDeleted;
                 
                    apiResponse.Data = noticeResponseModel;
                    apiResponse.Msg = "Displaying Notice";
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
        //[Route("createNotice/{heading}/{subject}")]
        [Route("createNotice")]
        public async Task<IActionResult> CreateNotice([FromForm] NoticeAddRequestModel noticeAddRequestModel)//This method will insert the notice into db  {
        {
            //Guid userId = _globalService.GetLoggedInUserId(this.User);
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            string noticeFileName = string.Empty;
            IFormFile file = null;
           
            try
            {

                //if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                //    throw new ArgumentException("Invalid request");

                //if (Request.Form.Files.Count > 0)
                //    file = Request.Form.Files[0];
                file = noticeAddRequestModel.noticeFile;
                bool hasFile = false;
                if (file != null)
                {
                    if (file.Length > 0)
                        hasFile = true;
                }
                if (!hasFile)
                    throw new InvalidOperationException("File not selected");
                string extension = Path.GetExtension(file.FileName);
                noticeFileName = file == null ? null : string.Format("{0}{1}", Guid.NewGuid(), extension);
                
                if (ModelState.IsValid)
                {
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                        var noticeDet = new TmNotice
                        {
                            NoticeId = Guid.NewGuid().ToString(),
                            Heading = noticeAddRequestModel.heading,
                            Subject = noticeAddRequestModel.subject,
                            PublishingDate = DateTime.Now,
                            FileName = noticeFileName,
                            IsDeleted = 0
                        };
                        _context.TmNotice.Add(noticeDet);
                        await _context.SaveChangesAsync();
                        await tran.CommitAsync();
                    }
                    if (file != null)
                    {
                        SaveNotice(noticeFileName, file,hasFile);

                    }
                    apiResponse.Msg = "Notice added successfully";
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
        public bool SaveNotice(string filename, IFormFile file,bool hasFile)
        {
            bool returnType = false;
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Notice");
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
        [Route("updateNotice")]
        public async Task<IActionResult> UpdateNotice([FromForm] NoticeAddRequestModel noticeAddRequestModel)//This method will update the notice into db  {
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            string noticeFileName = string.Empty;
            IFormFile file = null;
            //var noticeAddRequestModel = new NoticeAddRequestModel
            //{
            //    noticeId = noticeId,
            //    heading = heading,
            //    subject = subject
            //};
            try
            {
                var noticeDet = await _context.TmNotice.Where(m => m.NoticeId == noticeAddRequestModel.noticeId).FirstOrDefaultAsync();
                if (noticeDet != null)
                {

                    bool hasFile = false;
                    //if (MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                    //{
                        if (noticeAddRequestModel.noticeFile != null)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "Notice", noticeDet.FileName);
                            System.IO.File.Delete(path);
                            file = noticeAddRequestModel.noticeFile;
                        }
                        //if (!hasFile)
                        //    throw new InvalidOperationException("File not selected");
                        if (file != null)
                        {
                            if (file.Length > 0)
                                hasFile = true;
                        string extension = Path.GetExtension(file.FileName);
                        noticeFileName = file == null ? null : string.Format("{0}{1}", Guid.NewGuid(), extension);
                        }
                        
                    //}

                    if (ModelState.IsValid)
                    {
                        using (var tran = await _context.Database.BeginTransactionAsync())
                        {
                            noticeDet.Heading = noticeAddRequestModel.heading;
                            noticeDet.Subject = noticeAddRequestModel.subject;
                            if(noticeFileName != "")
                                noticeDet.FileName = noticeFileName;

                            _context.TmNotice.Update(noticeDet);
                            await _context.SaveChangesAsync();
                            await tran.CommitAsync();
                        }
                        if (file != null)
                        {
                            SaveNotice(noticeFileName, file, hasFile);

                        }
                        apiResponse.Msg = "Notice update successfully";
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
                    apiResponse.Msg = "Notice not found";
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
        [Route("viewNotice/{noticeId}")]
        public IActionResult View(string noticeId)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "");
            Guid siteId = Guid.Empty;
            FileStream stream = null;
            var noticeDet =  _context.TmNotice.Where(m => m.NoticeId == noticeId).FirstOrDefault();

            var resourceBasePath = Directory.GetCurrentDirectory();
            resourceBasePath = Path.Combine(resourceBasePath, "Notice");

            string filePath = null;
            var fileResult = noticeDet.FileName;

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
