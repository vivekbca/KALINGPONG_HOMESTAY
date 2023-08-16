using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using KLMPNHomeStay.Models.Request_Model;
using KLMPNHomeStay.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        public GalleryController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GalleryAddModel model)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {
                if (model.HsRi1 == null)
                {
                    
                    return StatusCode(StatusCodes.Status400BadRequest, "Please select an image");
                }
                if (model.HsLi1 == null)
                {
                   
                    return StatusCode(StatusCodes.Status400BadRequest, "Please select an image");
                }


                TmHsGallery obj = new TmHsGallery();

                obj.HsId = model.HsId;
                obj.HsLi1 = model.HsLi1 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi1.FileName.Split('.')[1]);
                obj.HsLi2 = model.HsLi2 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi2.FileName.Split('.')[1]);
                obj.HsLi3 = model.HsLi3 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi3.FileName.Split('.')[1]);
                obj.HsLi4 = model.HsLi4 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi4.FileName.Split('.')[1]);
                obj.HsLi5 = model.HsLi5 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi5.FileName.Split('.')[1]);
                obj.HsLi6 = model.HsLi6 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi6.FileName.Split('.')[1]);
                obj.HsLi7 = model.HsLi7 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi7.FileName.Split('.')[1]);
                obj.HsLi8 = model.HsLi8 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi8.FileName.Split('.')[1]);
                obj.HsLi9 = model.HsLi9 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi9.FileName.Split('.')[1]);
                obj.HsLi10 = model.HsLi10 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsLi10.FileName.Split('.')[1]);
                obj.HsRi1 = model.HsRi1 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi1.FileName.Split('.')[1]);
                obj.HsRi2 = model.HsRi2 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi2.FileName.Split('.')[1]);
                obj.HsRi3 = model.HsRi3 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi3.FileName.Split('.')[1]);
                obj.HsRi4 = model.HsRi4 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi4.FileName.Split('.')[1]);
                obj.HsRi5 = model.HsRi5 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi5.FileName.Split('.')[1]);
                obj.HsRi6 = model.HsRi6 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi6.FileName.Split('.')[1]);
                obj.HsRi7 = model.HsRi7 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi7.FileName.Split('.')[1]);
                obj.HsRi8 = model.HsRi8 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi8.FileName.Split('.')[1]);
                obj.HsRi9 = model.HsRi9 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi9.FileName.Split('.')[1]);
                obj.HsRi10 = model.HsRi10 == null ? null : string.Format("{0}.{1}", Guid.NewGuid(), model.HsRi10.FileName.Split('.')[1]);
                obj.HsMapLat = model.HsMapLat == null ? null : model.HsMapLat;
                obj.HsMapLong = model.HsMapLat == null ? null : model.HsMapLong;


                await _context.TmHsGallery.AddAsync(obj);
                await _context.SaveChangesAsync();

                if (model.HsLi1 != null)
                {
                    SaveImage(obj.HsLi1, model.HsLi1);

                }
                if (model.HsLi2 != null)
                {
                    SaveImage(obj.HsLi2, model.HsLi2);

                }
                if (model.HsLi3 != null)
                {

                    SaveImage(obj.HsLi3, model.HsLi3);

                }
                if (model.HsLi4 != null)
                {

                    SaveImage(obj.HsLi4, model.HsLi4);

                }
                if (model.HsLi5 != null)
                {

                    SaveImage(obj.HsLi5, model.HsLi5);

                }
                if (model.HsLi6 != null)
                {

                    SaveImage(obj.HsLi6, model.HsLi6);

                }
                if (model.HsLi7 != null)
                {
                    SaveImage(obj.HsLi7, model.HsLi7);


                }
                if (model.HsLi8 != null)
                {
                    SaveImage(obj.HsLi8, model.HsLi8);

                }
                if (model.HsLi9 != null)
                {
                    SaveImage(obj.HsLi9, model.HsLi9);

                }
                if (model.HsLi10 != null)
                {
                    SaveImage(obj.HsLi10, model.HsLi10);
                }
                if (model.HsRi1 != null)
                {
                    SaveImage(obj.HsRi1, model.HsRi1);
                }
                if (model.HsRi2 != null)
                {
                    SaveImage(obj.HsRi2, model.HsRi2);
                }
                if (model.HsRi3 != null)
                {
                    SaveImage(obj.HsRi3, model.HsRi3);
                }
                if (model.HsRi4 != null)
                {
                    SaveImage(obj.HsRi4, model.HsRi4);
                }
                if (model.HsRi5 != null)
                {
                    SaveImage(obj.HsRi5, model.HsRi5);
                }
                if (model.HsRi6 != null)
                {
                    SaveImage(obj.HsRi6, model.HsRi6);
                }
                if (model.HsRi7 != null)
                {
                    SaveImage(obj.HsRi7, model.HsRi7);
                }
                if (model.HsRi8 != null)
                {
                    SaveImage(obj.HsRi8, model.HsRi8);
                }
                if (model.HsRi9 != null)
                {
                    SaveImage(obj.HsRi9, model.HsRi9);
                }
                if (model.HsRi10 != null)
                {
                    SaveImage(obj.HsRi10, model.HsRi10);
                }
                //apiResponse.Data = model;
                apiResponse.Msg = "File Uploaded SuccessFully";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            
        }
        public bool SaveImage(string filename, IFormFile file)
        {
            bool returnType = false;
            try
            {

                var FileDic = "Gallery";

                string FilePath = Path.Combine(_env.WebRootPath, FileDic);

                if (!Directory.Exists(filename))

                    Directory.CreateDirectory(filename);

                var fileName = filename;

                var filePath = Path.Combine(FilePath, fileName);



                using (FileStream fs = System.IO.File.Create(filePath))

                {

                    file.CopyTo(fs);
                    returnType = true;
                }
            }
            catch (Exception ex)
            {
                returnType = false;
            }

            return returnType;
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id,IFormFile file)
        {
            try
            {
                ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
                var tmGallery =await  _context.TmHsGallery.Where(m => m.HsId == id).FirstOrDefaultAsync();
                var FileDic = "Gallery";

                string FilePath = Path.Combine(_env.WebRootPath, FileDic);
                var SourcefilePath = Path.Combine(FilePath, id);
                

                System.IO.File.Delete(SourcefilePath);
                SaveImage(id, file);
                apiResponse.Msg = "Saved SuccessFully";
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
        public async Task<string> View(string id)
        {
            var tmGallery = await _context.TmHsGallery.Where(m => m.HsId == id).FirstOrDefaultAsync();
            var FileDic = "Gallery";

            string FilePath = Path.Combine(_env.WebRootPath, FileDic);
            var SourcefilePath = Path.Combine(FilePath, id);
            return SourcefilePath;

        }
    }
}
