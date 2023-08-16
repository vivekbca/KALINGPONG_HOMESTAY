using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Services
{
    public interface IGlobalService
    {
        ApiResponseModelFinal GetFinalResponse(ApiResponseModel apiResponse);
        Guid GetLoggedInUserId(ClaimsPrincipal User);
        string GetBaseUrl();
    }

    public class GlobalService : IGlobalService
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public GlobalService(klmpnhomestay_dbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        public ApiResponseModelFinal GetFinalResponse(ApiResponseModel apiResponse)
        {
            return new ApiResponseModelFinal
            {
                Result = apiResponse.Result.ToString(),
                Msg = apiResponse.Msg,
                Data = apiResponse.Data
            };
        }
        public Guid GetLoggedInUserId(ClaimsPrincipal User)
        {
            Guid userId = Guid.Empty;
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userId1 = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                userId = Guid.Parse(userId1);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            return userId;
        }

        public string GetBaseUrl()
        {
            var request = _httpContext.HttpContext.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}
