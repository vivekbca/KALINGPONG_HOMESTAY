using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Common;
using KLMPNHomeStay.Models.Request_Model;
using KLMPNHomeStay.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : Controller
    {
        private SmtpClient client;
        private IConfiguration config;
        public IConfiguration appConfig { get => config; }
        private readonly IEmailService _emailService;
        public PackageController(IConfiguration config,IEmailService emailService)
        {
            _emailService = emailService;
            this.config = config;
            client = new SmtpClient(config["SMTPServer:Address"]);
            client.UseDefaultCredentials = false;
            client.Port = Convert.ToInt32(config["SMTPServer:Port"]);
            client.Credentials = new NetworkCredential(config["SMTPServer:Username"], config["SMTPServer:Pasword"]);
            //client.EnableSsl = false;
            client.EnableSsl = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;  
        }
        //[HttpPost("package")]
        //public async Task<IActionResult> AddPackage([FromBody] Package obj)
        //{
        //    ApiResponseModel apiResponse = new ApiResponseModel { Result = ResponseTypes.Error, Msg = "Some problems occurred", Data = null };
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            apiResponse.Data = ModelState;
        //            apiResponse.Result = ResponseTypes.ModelErr;
        //        }
        //        else
        //        {
        //            using (var tran = await _context.Database.BeginTransactionAsync())
        //            {
        //                var webRoot = _env.ContentRootPath;
        //                string uniqueFileName2 = null;
        //                string uniqueFileName3 = null;
        //                string uniqueFileName4 = null;
        //                string uniqueFileName5 = null;
        //                //Image 1
        //                string[] splitdata1 = obj.Image1.Split(",");
        //                var content1 = splitdata1[1];
        //                string uniqueFileName1 = Guid.NewGuid().ToString();
        //                String path1 = Path.Combine($"{_env.ContentRootPath}\\ClientApp\\src\\assets\\tour\\"); //Path
        //                System.IO.File.WriteAllBytes(path1 + uniqueFileName1 + ".jpeg", Convert.FromBase64String(content1));
        //                //Image 2
        //                if (obj.Image2 != null) { 
        //                string[] splitdata2 = obj.Image2.Split(",");
        //                var content2 = splitdata2[1];
        //                uniqueFileName2 = Guid.NewGuid().ToString();
        //                String path2 = Path.Combine($"{_env.ContentRootPath}\\ClientApp\\src\\assets\\tour\\"); //Path
        //                System.IO.File.WriteAllBytes(path2 + uniqueFileName2 + ".jpeg", Convert.FromBase64String(content2));
        //                }
        //                //Image 3
        //                if(obj.Image3!=null)
        //                {                       
        //                string[] splitdata3 = obj.Image3.Split(",");
        //                var content3 = splitdata3[1];
        //                uniqueFileName3 = Guid.NewGuid().ToString();
        //                String path3 = Path.Combine($"{_env.ContentRootPath}\\ClientApp\\src\\assets\\tour\\"); //Path
        //                System.IO.File.WriteAllBytes(path3 + uniqueFileName3 + ".jpeg", Convert.FromBase64String(content3));
        //                }
        //                //Image 4
        //                if(obj.Image4!=null)
        //                { 
        //                string[] splitdata4 = obj.Image4.Split(",");
        //                var content4 = splitdata4[1];
        //                uniqueFileName4 = Guid.NewGuid().ToString();
        //                String path4 = Path.Combine($"{_env.ContentRootPath}\\ClientApp\\src\\assets\\tour\\"); //Path
        //                System.IO.File.WriteAllBytes(path4 + uniqueFileName4 + ".jpeg", Convert.FromBase64String(content4));
        //                }
        //                //Image 5
        //                if(obj.Image5!=null)
        //                { 
        //                string[] splitdata5 = obj.Image5.Split(",");
        //                var content5 = splitdata5[1];
        //                uniqueFileName5 = Guid.NewGuid().ToString();
        //                String path5 = Path.Combine($"{_env.ContentRootPath}\\ClientApp\\src\\assets\\tour\\"); //Path
        //                System.IO.File.WriteAllBytes(path5 + uniqueFileName5 + ".jpeg", Convert.FromBase64String(content5));
        //                }
        //                //PDF FILE
        //                string[] splitdata6 = obj.PackagePdf.Split(",");
        //                var content6 = splitdata6[1];
        //                string uniqueFileName6 = Guid.NewGuid().ToString();
        //                String path6 = Path.Combine($"{_env.ContentRootPath}\\Tour\\"); //Path
        //                System.IO.File.WriteAllBytes(path6 + uniqueFileName6 + ".pdf", Convert.FromBase64String(content6));
        //                foreach(var item in obj.roomFacility)
        //                {
        //                    if (obj.FacilityId1 == null)
        //                    {
        //                        obj.FacilityId1 = item.hsFacilityId;
        //                    }
        //                    else
        //                    {
        //                        if (obj.FacilityId2 == null)
        //                        {
        //                            obj.FacilityId2 = item.hsFacilityId;
        //                        }
        //                        else
        //                        {
        //                            if (obj.FacilityId3 == null)
        //                            {
        //                                obj.FacilityId3 = item.hsFacilityId;
        //                            }
        //                            else
        //                            {
        //                                if (obj.FacilityId4 == null)
        //                                {
        //                                    obj.FacilityId4 = item.hsFacilityId;
        //                                }
        //                                else 
        //                                     {
        //                                         if (obj.FacilityId5 == null)
        //                                         {
        //                                              obj.FacilityId5 = item.hsFacilityId;
        //                                         }
        //                                     }
        //                            }

        //                        }

        //                    }

        //                }
        //                var tour = new TmTour();
        //                tour.Id = Guid.NewGuid().ToString();
        //                tour.Name = obj.PName;
        //                tour.Destination = obj.DName;
        //                tour.DestinationDay = Convert.ToInt32(obj.Day);
        //                tour.DestinationNight = Convert.ToInt32(obj.Night);
        //                tour.Subject = obj.Subject;
        //                tour.Description = obj.Description;
        //                tour.Cost = Convert.ToInt32(obj.Cost);
        //                tour.ContactPersonName = obj.ContactPerson;
        //                tour.ContactPersonNameEmail = obj.ContactEmail;
        //                //tour.ContactPersonMobile = (int?)Convert.ToUInt32(obj.ContactMobile);
        //                tour.FacilityId1 = obj.FacilityId1;
        //                tour.FacilityId2 = obj.FacilityId2;
        //                tour.FacilityId3 = obj.FacilityId3;
        //                tour.FacilityId4 = obj.FacilityId4;
        //                tour.FacilityId5 = obj.FacilityId5;
        //                tour.Image1 = uniqueFileName1 + ".jpeg";
        //                if(uniqueFileName2!=null)
        //                { 
        //                tour.Image2 = uniqueFileName2 + ".jpeg";
        //                }
        //                if(uniqueFileName3!=null)
        //                { 
        //                tour.Image3 = uniqueFileName3 + ".jpeg";
        //                }
        //                if(uniqueFileName4!=null)
        //                { 
        //                tour.Image4 = uniqueFileName4 + ".jpeg";
        //                }
        //                if(uniqueFileName5!=null)
        //                { 
        //                tour.Image5 = uniqueFileName5 + ".jpeg";
        //                }
        //                tour.TourPdfFile = uniqueFileName6 + ".pdf";
        //                tour.IsActive = 0;
        //                tour.CreatedBy = obj.CreatedBy;
        //                tour.CreatedOn = DateTime.Now;    
        //                _context.TmTour.Add(tour);
        //                await _context.SaveChangesAsync();
        //                await tran.CommitAsync();
        //            }
        //            apiResponse.Msg = "Success";
        //            apiResponse.Result = ResponseTypes.Success;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //    }
        //    ApiResponseModelFinal apiResponseFinal = _globalService.GetFinalResponse(apiResponse);
        //    return Ok(apiResponseFinal);
        //}

        [HttpGet("trigger")]
        public async Task<IActionResult> trigger()
        {
            var body= @"<html>
                      <body>
                      <p>Dear Ms. Susan,</p>
                      <p>Thank you for your letter of yesterday inviting me to come for an interview on Friday afternoon, 5th July, at 2:30.
                              I shall be happy to be there as requested and will bring my diploma and other papers with me.</p>
                      <p>Sincerely,<br>-Jack</br></p>
                      </body>
                      </html>
                     ";
           // SendEmail("Exception Alert", body, "saikatbasu97@gmail.com");
            mail();
            //SendAlertMail("2516516");
            return Ok();
        }

        public void SendEmail(string subject, string body , string receivers)
        {
            try
            {
                Disable_CertificateValidation();
                
                if (receivers == null)
                    throw new ArgumentException();

                using (MailMessage msg = new MailMessage())
                {
                    //foreach (var r in receivers)
                        msg.To.Add(receivers);

                    

                    msg.Subject = subject;
                    msg.IsBodyHtml = true;
                    msg.Body = body;
                    msg.From = new MailAddress(config["SMTPServer:Email"], config["SMTPServer:DisplayName"]);
                    client.Send(msg);
                }
            }
            catch (Exception ex)
            {
                //Logging.WriteLine(ex.Message);
                //Logging.WriteLine(ex.StackTrace);
            }
        }
        static void Disable_CertificateValidation()
        {
            // Disabling certificate validation because I can't find a way to make this work at the moment.
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
        }

        [NonAction]
        private async Task SendAlertMail(string VehicleNumber)
        {
            try
            {
                var name = "Saikat Basu";
                var email = "saikatbasu97@gmail.com";
                //var Todaysdate = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                //var path = Path.Combine(_env.ContentRootPath, "Template\\Alert.html");
                //string content = System.IO.File.ReadAllText(path);
                string content1= @"<html>
                      <body>
                      <p>Dear Ms. Susan,</p>
                      <p>Thank you for your letter of yesterday inviting me to come for an interview on Friday afternoon, 5th July, at 2:30.
                              I shall be happy to be there as requested and will bring my diploma and other papers with me.</p>
                      <p>Sincerely,<br>-Jack</br></p>
                      </body>
                      </html>
                     ";
                //        = content
                //        .Replace("<<VehicleNumber>>", VehicleNumber)
                //        .Replace("<<Todaysdate>>", Todaysdate);
                //send password to email asynchronously
                await _emailService.Send(email, name, "Welcome", content1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void mail()
        {
            Disable_CertificateValidation();
            MailAddress to = new MailAddress("srutakirti@live.com");
            MailAddress from = new MailAddress("crl@echoltech.com.sg");

            MailMessage message = new MailMessage(from, to);
            message.Subject = "Regarding our meeting";
            message.Body = "Elizabeth, as requested, sending you the invoice for Harry and Meghan's wedding. Any questions? Let me know.;";

            SmtpClient client = new SmtpClient("mail.echoltech.com.sg", 587)
            {
                Credentials = new NetworkCredential("crl", "ARQ)o5"),
                EnableSsl = true
            };
            // code in brackets above needed if authentication required   

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
       
    }
}
