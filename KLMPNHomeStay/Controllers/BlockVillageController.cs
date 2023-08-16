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
using System.Linq;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockVillageController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        public BlockVillageController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {

                var list = get();


                apiResponse.Data = list;
                apiResponse.Msg = "Displaying Block Village List";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpGet("{VillId}")]
        public async Task<IActionResult> GetById(string VillId)
        {
            var query = await get();
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {

                BlocakVillage obj = new BlocakVillage();
                var village = query.Where(m => m.VillId == VillId).FirstOrDefault();
                obj.VillId = village.VillId;
                obj.Country = village.Country;
                obj.State = village.State;
                obj.Block = village.Block;
                obj.Village = village.Village;
                obj.VillCode = village.VillCode;
                obj.District = village.District;

                apiResponse.Data = obj;
                apiResponse.Msg = "Displaying Block Village Detail";
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
        public async Task<IActionResult> Create([FromBody] BlocakVillage model)
        {
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {

                TmBlockVillage obj = new TmBlockVillage();

                obj.VillId = Guid.NewGuid().ToString();
                obj.StateId = model.State;
                obj.CountryId = model.Country;
                obj.VillCode = model.VillCode;
                obj.DistrictId = model.District;
                obj.BlockId = model.Block;
                obj.CreatedBy = "f1fb5130-0192-11ec-8831-005056a4479e";
                obj.CreatedOn = DateTime.Now;
                obj.ModifiedBy = "f1fb5130-0192-11ec-8831-005056a4479e";
                obj.ModifiedOn = DateTime.Now;
                obj.IsActive = model.isActive;

                await _context.TmBlockVillage.AddAsync(obj);
                await _context.SaveChangesAsync();

                apiResponse.Data = model;
                apiResponse.Msg = "Saved Sccessfully";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BlocakVillage obj)
        {
            
            ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
            try
            {

                //BlocakVillage obj = new BlocakVillage();
                var village =await  _context.TmBlockVillage.Where(m => m.VillId == obj.VillId).FirstOrDefaultAsync();
                village.VillId = obj.VillId;
                village.CountryId = obj.Country;
                village.StateId = obj.State;
                village.BlockId = obj.Block;
                village.VillId = obj.Village;
                village.VillCode = obj.VillCode;
                village.DistrictId = obj.District; 

                apiResponse.Data = obj;
                apiResponse.Msg = "Saved Successfully";
                apiResponse.Result = ResponseTypes.Success;
                ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
                return Ok(apiResponseFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        public async Task<List<BlocakVillage>> get()
        {
            var list = await (from a in _context.TmBlockVillage
                              join b in _context.TmBlock on a.BlockId equals b.BlockId
                              join c in _context.TmCountry on a.CountryId equals c.CountryId
                              join d in _context.TmDistrict on a.DistrictId equals d.DistrictId
                              join e in _context.TmState on a.StateId equals e.StateId
                              select new BlocakVillage
                              {
                                  Village = a.VillName,
                                  Block = b.BlockName,
                                  District = d.DistrictName,
                                  State = e.StateName,
                                  Country = c.CountryName,
                                  VillId = a.VillId,
                                  VillCode = a.VillCode

                              }).OrderBy(m=>m.Village).ToListAsync();



            return list;

        }
    }
}
