using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Response_Model;
using KLMPNHomeStay.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
namespace KLMPNHomeStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;
        private readonly IEmailService _emailService;

        #region Declaration
        int _totalColumn = 7;
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable = new PdfPTable(7);
        PdfPCell _pdfPCell;
        MemoryStream _memoryStream = new MemoryStream();
        #endregion
        public ReportController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService, IEmailService emailService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
            _emailService = emailService;
        }
        [HttpGet]
        [Route("getBookReport")]
        public async Task<IActionResult> GetTransactionReportDataExcel()
        {
            var stream = new MemoryStream();
            try
            {
               /// var allData = await _context.TtBooking.ToListAsync();
                var HomestayBoolingList = from bk in _context.TtBooking.AsNoTracking()
                                          join gu in _context.TmGuestUser.AsNoTracking()
                                          on bk.GuId equals gu.GuId
                                          join hs in _context.TmHomestay.AsNoTracking()
                                          on bk.HsId equals hs.HsId
                                          where bk.BkPaymentStatus != null && bk.BkIsCancelled == 0
                                          select new
                                          {
                                              bk.GuId,
                                              bk.HsId,
                                              bk.HsBookingId,
                                              bk.BkDateFrom,
                                              bk.BkDateTo,
                                              hs.HsName,
                                              gu.GuName,
                                              bk.BkNoPers,
                                              bk.BkBookingDate,
                                              bk.TotalCost
                                          };
                var BookingList = await HomestayBoolingList.ToListAsync();
                int? TotalCost = 0;
                foreach (var item in BookingList)
                {
                    TotalCost = TotalCost + item.TotalCost;
                }
                if (BookingList != null)
                {
                    int row = 7;
                    //new DateTime range
                    
                    string from = DateTime.Now.ToString("dd-MM-yyyy");
                    DateTime newfrom = DateTime.Parse(from);
                    string From = newfrom.AddDays(-1).ToString("dd-MM-yyyy hh:mm");
                    DateTime Originalfrom = DateTime.Parse(From);

                    string to = DateTime.Now.ToString("dd-MM-yyyy");
                    string newto = DateTime.Parse(to).AddDays(-1).AddHours(23).AddMinutes(59).ToString("dd-MM-yyyy hh:mm");
                    DateTime OriginalTo = DateTime.Parse(newto);
                    //new DateTime range end
                    string Today = DateTime.Now.ToString("dd-MM-yyyy");
                    string fromDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                    string toDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                    using (var packg = new ExcelPackage(stream))
                    {
                        ExcelWorksheet Sheet = packg.Workbook.Worksheets.Add("Report");

                        Sheet.TabColor = System.Drawing.Color.Black;
                        Sheet.Cells["A1:B1"].Merge = true;
                        Sheet.Cells["A1:B1"].Style.Font.Size=15;
                        Sheet.Cells["A2:B2"].Merge = true;
                        Sheet.Cells["A2:B2"].Style.Font.Size = 15;
                        Sheet.Cells["A3:B3"].Merge = true;
                        Sheet.Cells["B4:D4"].Merge = true;
                        Sheet.Cells["B4:D4"].Style.Font.Size = 13;
                        Sheet.Cells["B5:E5"].Merge = true;
                        Sheet.SelectedRange["A1"].Value = "Kalimpong District Homestay Tourism";
                        Sheet.Row(1).Style.Font.Bold = true;
                        Sheet.Row(2).Style.Font.Bold = true;
                        Sheet.Row(3).Style.Font.Bold = true;
                        Sheet.Row(4).Style.Font.Bold = true;
                        Sheet.Row(5).Style.Font.Bold = true;
                        Sheet.Cells[1, 6].Value = "Run Date : " + Today;
                        Sheet.Cells[2,1].Value = "Co-operative Society Ltd.";
                        Sheet.Cells[4,2].Value = "            List Of Homestay Booking Details";
                        Sheet.Cells[4,2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Cells[4,6].Value = "TOTAL AMOUNT : " +TotalCost;
                        Sheet.Cells[5,2].Value = "Period from : "+fromDate+"  To : "+toDate;
                        Sheet.Cells[6,1].Value = "HOMESTAY NAME";
                        Sheet.Cells[6,2].Value = "BOOKED BY";
                        Sheet.Cells[6,3].Value = "BOOKED ON";
                        Sheet.Cells[6,4].Value = "CHECK IN";
                        Sheet.Cells[6,5].Value = "CHECK OUT";
                        Sheet.Cells[6,6].Value = "PERSON";
                        Sheet.Cells[6,7].Value = "TOTAL PAID";
                        Sheet.Row(6).Style.Font.Bold = true;
                        Sheet.Column(1).Width = 30;
                        Sheet.Column(2).Width = 18;
                        Sheet.Column(3).Width = 17;
                        Sheet.Column(4).Width = 11;
                        Sheet.Column(5).Width = 11;
                        Sheet.Column(6).Width = 10;
                        Sheet.Column(7).Width = 15;
                        Sheet.Row(1).Height = 25;
                        //Sheet.Row(2).Height = 25;
                        Sheet.Row(3).Height = 25;
                        Sheet.Row(4).Height = 25;
                        Sheet.Row(5).Height = 25;
                        Sheet.Row(6).Height = 25;
                        Sheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(4).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(5).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        //Sheet.Row(6).Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //Sheet.Row(6).Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        for (int i = 0; i < BookingList.Count; i++)
                        {
                            Sheet.Cells[string.Format("A{0}", row)].Value = BookingList[i].HsName;
                            Sheet.Cells[string.Format("B{0}", row)].Value = BookingList[i].GuName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = Convert.ToDateTime(BookingList[i].BkBookingDate).ToString("dd-MM-yyyy HH:mm");
                            Sheet.Cells[string.Format("D{0}", row)].Value = Convert.ToDateTime(BookingList[i].BkDateFrom).ToString("dd-MM-yyyy");
                            Sheet.Cells[string.Format("E{0}", row)].Value = Convert.ToDateTime(BookingList[i].BkDateTo).ToString("dd-MM-yyyy");
                            Sheet.Cells[string.Format("F{0}", row)].Value = BookingList[i].BkNoPers;
                            Sheet.Cells[string.Format("G{0}", row)].Value = BookingList[i].TotalCost;
                            row++;
                        }
                        Sheet.Cells["A:AZ"].Style.Font.Size = 11;
                        Sheet.Cells["A:AZ"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        packg.Save();
                    }
                }
                else
                {
                    using (var packg = new ExcelPackage(stream))
                    {
                        ExcelWorksheet Sheet = packg.Workbook.Worksheets.Add("Report");
                        Sheet.Cells["A1"].Value = "NO DATA FOUND";
                        packg.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            stream.Position = 0;
            string excelName = $"HomeStayData-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpGet]
        [Route("getCancelReport")]
        public async Task<IActionResult> GetTransactionCancelReportDataExcel()
        {
            var stream = new MemoryStream();
            try
            {
                /// var allData = await _context.TtBooking.ToListAsync();
                var HomestayBoolingList = from bk in _context.TtBooking.AsNoTracking()
                                          join gu in _context.TmGuestUser.AsNoTracking()
                                          on bk.GuId equals gu.GuId
                                          join hs in _context.TmHomestay.AsNoTracking()
                                          on bk.HsId equals hs.HsId
                                          where bk.BkPaymentStatus != null && bk.BkIsCancelled == 1
                                          select new
                                          {
                                              bk.GuId,
                                              bk.HsId,
                                              bk.HsBookingId,
                                              bk.BkDateFrom,
                                              bk.BkDateTo,
                                              hs.HsName,
                                              gu.GuName,
                                              bk.BkNoPers,
                                              bk.BkBookingDate,
                                              bk.TotalCost,
                                              bk.CancelledBy,
                                              bk.BkCancelledDate
                                             
                                          };
                var BookingList = await HomestayBoolingList.ToListAsync();
                int? TotalCost = 0;
                foreach(var item in BookingList)
                {
                    TotalCost = TotalCost + item.TotalCost;
                }
                if (BookingList != null)
                {
                    int row = 7;
                    string Today = DateTime.Now.ToString("dd-MM-yyyy");
                    string fromDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                    string toDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                    using (var packg = new ExcelPackage(stream))
                    {
                        ExcelWorksheet Sheet = packg.Workbook.Worksheets.Add("Report");
                        Sheet.Cells["A1:B1"].Merge = true;
                        Sheet.Cells["A1:B1"].Style.Font.Size = 15;
                        Sheet.Cells["A2:B2"].Merge = true;
                        Sheet.Cells["A2:B2"].Style.Font.Size = 15;
                        Sheet.Cells["A3:B3"].Merge = true;
                        Sheet.Cells["B4:D4"].Merge = true;
                        Sheet.Cells["B4:D4"].Style.Font.Size = 13;
                        Sheet.Cells["B5:E5"].Merge = true;
                        Sheet.SelectedRange["A1"].Value = "Kalimpong District Homestay Tourism";
                        Sheet.Row(1).Style.Font.Bold = true;
                        Sheet.Row(2).Style.Font.Bold = true;
                        Sheet.Row(3).Style.Font.Bold = true;
                        Sheet.Row(4).Style.Font.Bold = true;
                        Sheet.Row(5).Style.Font.Bold = true;
                        Sheet.Cells[1, 6].Value = "Run Date : " + Today;
                        Sheet.Cells[2, 1].Value = "Co-operative Society Ltd.";
                        Sheet.Cells[4, 2].Value = "              List Of Homestay Cancellation Details";
                        Sheet.Cells[4, 6].Value = "TOTAL REFUND : " + TotalCost;
                        Sheet.Cells[5, 2].Value = "Period from : " + fromDate + " " + "  To : " + "" + toDate;
                        Sheet.Cells[6, 1].Value = "HOMESTAY NAME";
                        Sheet.Cells[6, 2].Value = "BOOKED BY";
                        Sheet.Cells[6, 3].Value = "BOOKED ON";
                        Sheet.Cells[6, 4].Value = "CHECK IN";
                        Sheet.Cells[6, 5].Value = "CHECK OUT";
                        Sheet.Cells[6, 6].Value = "CANCELLED ON";
                        Sheet.Cells[6, 7].Value = "PERSON";
                        Sheet.Cells[6, 8].Value = "REFUND AMT";
                        
                        Sheet.Row(6).Style.Font.Bold = true;


                        Sheet.Column(1).Width = 25;
                        Sheet.Column(2).Width = 16;
                        Sheet.Column(3).Width = 15;
                        Sheet.Column(4).Width = 10;
                        Sheet.Column(5).Width = 10;
                        Sheet.Column(6).Width = 13;
                        Sheet.Column(7).Width = 8;
                        Sheet.Column(8).Width = 10;
                        Sheet.Row(1).Height = 25;
                        //Sheet.Row(2).Height = 25;
                        Sheet.Row(3).Height = 25;
                        Sheet.Row(4).Height = 25;
                        Sheet.Row(5).Height = 25;
                        Sheet.Row(6).Height = 25;
                        Sheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(4).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(5).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        //Sheet.Row(6).Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //Sheet.Row(6).Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                      
                        for (int i = 0; i < BookingList.Count; i++)
                        {
                            Sheet.Cells[string.Format("A{0}", row)].Value = BookingList[i].HsName;
                            Sheet.Cells[string.Format("B{0}", row)].Value = BookingList[i].GuName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = Convert.ToDateTime(BookingList[i].BkBookingDate).ToString("dd-MM-yyyy HH:mm");
                            Sheet.Cells[string.Format("D{0}", row)].Value = Convert.ToDateTime(BookingList[i].BkDateFrom).ToString("dd-MM-yyyy");
                            Sheet.Cells[string.Format("E{0}", row)].Value = Convert.ToDateTime(BookingList[i].BkDateTo).ToString("dd-MM-yyyy");
                            Sheet.Cells[string.Format("F{0}", row)].Value = Convert.ToDateTime(BookingList[i].BkCancelledDate).ToString("dd-MM-yyyy");
                            Sheet.Cells[string.Format("G{0}", row)].Value = BookingList[i].BkNoPers;
                            Sheet.Cells[string.Format("H{0}", row)].Value = BookingList[i].TotalCost;
                           
                            row++;
                        }

                        Sheet.Cells["A:AZ"].Style.Font.Size = 10;
                        Sheet.Cells["A:AZ"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        packg.Save();
                    }
                }
                else
                {
                    using (var packg = new ExcelPackage(stream))
                    {
                        ExcelWorksheet Sheet = packg.Workbook.Worksheets.Add("Report");
                        Sheet.Cells["A1"].Value = "NO DATA FOUND";
                        packg.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            stream.Position = 0;
            string excelName = $"HomeStayData-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpGet]
        [Route("getPkgBookingReport")]
        public async Task<IActionResult> GetTransactionPkgReportDataExcel()
        {
            var stream = new MemoryStream();
            try
            {
                /// var allData = await _context.TtBooking.ToListAsync();
                var PackageBoolingList = from bk in _context.TtTourBooking.AsNoTracking()
                                          join gu in _context.TmGuestUser.AsNoTracking()
                                          on bk.GuId equals gu.GuId
                                          join date in _context.TtTourDate.AsNoTracking()
                                          on bk.TourDateId equals date.Id
                                          join tour in _context.TmTour.AsNoTracking()
                                          on bk.TourId equals tour.Id
                                          where bk.PaymentStatus != null && bk.IsCancel == 0
                                          select new
                                          {
                                             bk.GuId,
                                             bk.BookingDate,
                                             bk.TotalRate,
                                             bk.NoOfPerson,
                                             gu.GuName,
                                             date.FromDate,
                                             date.ToDate,
                                             tour.Name
                                          };
                var BookingList = await PackageBoolingList.ToListAsync();
                int? TotalCost = 0;
                foreach (var item in BookingList)
                {
                    TotalCost = TotalCost + item.TotalRate;
                }
                if (BookingList != null)
                {
                    int row = 7;
                    string Today = DateTime.Now.ToString("dd-MM-yyyy");
                    string fromDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                    string toDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                    using (var packg = new ExcelPackage(stream))
                    {
                        ExcelWorksheet Sheet = packg.Workbook.Worksheets.Add("Report");
                        Sheet.Cells["A1:B1"].Merge = true;
                        Sheet.Cells["A1:B1"].Style.Font.Size = 15;
                        Sheet.Cells["A2:B2"].Merge = true;
                        Sheet.Cells["A2:B2"].Style.Font.Size = 15;
                        Sheet.Cells["A3:B3"].Merge = true;
                        Sheet.Cells["B4:D4"].Merge = true;
                        Sheet.Cells["B4:D4"].Style.Font.Size = 13;
                        Sheet.Cells["B5:E5"].Merge = true;
                        Sheet.SelectedRange["A1"].Value = "Kalimpong District Homestay Tourism";
                        Sheet.Row(1).Style.Font.Bold = true;
                        Sheet.Row(2).Style.Font.Bold = true;
                        Sheet.Row(3).Style.Font.Bold = true;
                        Sheet.Row(4).Style.Font.Bold = true;
                        Sheet.Row(5).Style.Font.Bold = true;
                        Sheet.Cells[1, 6].Value = "Run Date : " + Today;
                        Sheet.Cells[2, 1].Value = "Co-operative Society Ltd.";
                        Sheet.Cells[4, 2].Value = "              List Of Package Booking Details";
                        Sheet.Cells[4, 6].Value = "TOTAL AMOUNT : " + TotalCost;
                        Sheet.Cells[5, 2].Value = "Period from : " + fromDate + " " + "  To : " + "" + toDate;
                        Sheet.Cells[6, 1].Value = "PACKAGE NAME";
                        Sheet.Cells[6, 2].Value = "BOOKED BY";
                        Sheet.Cells[6, 3].Value = "BOOKED ON";
                        Sheet.Cells[6, 4].Value = "PACKAGE FROM";
                        Sheet.Cells[6, 5].Value = "PACKAGE TO";
                        Sheet.Cells[6, 6].Value = "PERSON";
                        Sheet.Cells[6, 7].Value = "TOTAL PAID";

                        Sheet.Row(6).Style.Font.Bold = true;


                        Sheet.Column(1).Width = 30;
                        Sheet.Column(2).Width = 18;
                        Sheet.Column(3).Width = 17;
                        Sheet.Column(4).Width = 15;
                        Sheet.Column(5).Width = 15;
                        Sheet.Column(6).Width = 9;
                        Sheet.Column(7).Width = 12;
                        Sheet.Column(8).Width = 25;
                        Sheet.Row(1).Height = 25;
                        //Sheet.Row(2).Height = 25;
                        Sheet.Row(3).Height = 25;
                        Sheet.Row(4).Height = 25;
                        Sheet.Row(5).Height = 25;
                        Sheet.Row(6).Height = 25;
                        Sheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(4).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(5).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        //Sheet.Row(6).Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //Sheet.Row(6).Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                        for (int i = 0; i < BookingList.Count; i++)
                        {
                            Sheet.Cells[string.Format("A{0}", row)].Value = BookingList[i].Name;
                            Sheet.Cells[string.Format("B{0}", row)].Value = BookingList[i].GuName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = Convert.ToDateTime(BookingList[i].BookingDate).ToString("dd-MM-yyyy HH:mm");
                            Sheet.Cells[string.Format("D{0}", row)].Value = Convert.ToDateTime(BookingList[i].FromDate).ToString("dd-MM-yyyy");
                            Sheet.Cells[string.Format("E{0}", row)].Value = Convert.ToDateTime(BookingList[i].ToDate).ToString("dd-MM-yyyy");
                            //Sheet.Cells[string.Format("F{0}", row)].Value = Convert.ToDateTime(BookingList[i].BkCancelledDate).ToString("dd-MM-yyyy HH:mm");
                            Sheet.Cells[string.Format("F{0}", row)].Value = BookingList[i].NoOfPerson;
                            Sheet.Cells[string.Format("G{0}", row)].Value = BookingList[i].TotalRate;
                            row++;
                        }

                        Sheet.Cells["A:AZ"].Style.Font.Size = 11;
                        Sheet.Cells["A:AZ"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        packg.Save();
                    }
                }
                else
                {
                    using (var packg = new ExcelPackage(stream))
                    {
                        ExcelWorksheet Sheet = packg.Workbook.Worksheets.Add("Report");
                        Sheet.Cells["A1"].Value = "NO DATA FOUND";
                        packg.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            stream.Position = 0;
            string excelName = $"PackageBookingData-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpGet]
        [Route("getPkgCancelReport")]
        public async Task<IActionResult> GetTransactionPkgCancelReportDataExcel()
        {
            var stream = new MemoryStream();
            try
            {
                /// var allData = await _context.TtBooking.ToListAsync();
                var PackageBoolingList = from bk in _context.TtTourBooking.AsNoTracking()
                                         join gu in _context.TmGuestUser.AsNoTracking()
                                         on bk.GuId equals gu.GuId
                                         join date in _context.TtTourDate.AsNoTracking()
                                         on bk.TourDateId equals date.Id
                                         join tour in _context.TmTour.AsNoTracking()
                                         on bk.TourId equals tour.Id
                                         where bk.IsCancel == 1
                                         select new
                                         {
                                             bk.GuId,
                                             bk.BookingDate,
                                             bk.TotalRate,
                                             bk.NoOfPerson,
                                             gu.GuName,
                                             date.FromDate,
                                             date.ToDate,
                                             tour.Name,
                                             bk.CancelledDate
                                         };
                var BookingList = await PackageBoolingList.ToListAsync();
                int? TotalCost = 0;
                foreach (var item in BookingList)
                {
                    TotalCost = TotalCost + item.TotalRate;
                }
                if (BookingList != null)
                {
                    int row = 7;
                    string Today = DateTime.Now.ToString("dd-MM-yyyy");
                    string fromDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                    string toDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                    using (var packg = new ExcelPackage(stream))
                    {
                        ExcelWorksheet Sheet = packg.Workbook.Worksheets.Add("Report");
                        Sheet.Cells["A1:B1"].Merge = true;
                        Sheet.Cells["A1:B1"].Style.Font.Size = 15;
                        Sheet.Cells["A2:B2"].Merge = true;
                        Sheet.Cells["A2:B2"].Style.Font.Size = 15;
                        Sheet.Cells["A3:B3"].Merge = true;
                        Sheet.Cells["B4:D4"].Merge = true;
                        Sheet.Cells["B4:D4"].Style.Font.Size = 13;
                        Sheet.Cells["B5:E5"].Merge = true;
                        Sheet.SelectedRange["A1"].Value = "Kalimpong District Homestay Tourism";
                        Sheet.Row(1).Style.Font.Bold = true;
                        Sheet.Row(2).Style.Font.Bold = true;
                        Sheet.Row(3).Style.Font.Bold = true;
                        Sheet.Row(4).Style.Font.Bold = true;
                        Sheet.Row(5).Style.Font.Bold = true;
                        Sheet.Cells[1, 6].Value = "Run Date : " + Today;
                        Sheet.Cells[2, 1].Value = "Co-operative Society Ltd.";
                        Sheet.Cells[4, 2].Value = "              List Of Package Cancellation Details";
                        Sheet.Cells[4, 6].Value = "TOTAL REFUND : " + TotalCost;
                        Sheet.Cells[5, 2].Value = "Period from : " + fromDate + " " + "  To : " + "" + toDate;
                        Sheet.Cells[6, 1].Value = "PACKAGE NAME";
                        Sheet.Cells[6, 2].Value = "BOOKED BY";
                        Sheet.Cells[6, 3].Value = "BOOKED ON";
                        Sheet.Cells[6, 4].Value = "PACKAGE FROM";
                        Sheet.Cells[6, 5].Value = "PACKAGE TO";
                        Sheet.Cells[6, 6].Value = "CANCELLED ON";
                        Sheet.Cells[6, 7].Value = "PERSON";
                        Sheet.Cells[6, 8].Value = "REFUND AMT";

                        Sheet.Row(6).Style.Font.Bold = true;


                        Sheet.Column(1).Width = 25;
                        Sheet.Column(2).Width = 16;
                        Sheet.Column(3).Width = 15;
                        Sheet.Column(4).Width = 13;
                        Sheet.Column(5).Width = 11;
                        Sheet.Column(6).Width = 13;
                        Sheet.Column(7).Width = 7;
                        Sheet.Column(8).Width = 10;
                        Sheet.Row(1).Height = 25;
                        //Sheet.Row(2).Height = 25;
                        Sheet.Row(3).Height = 25;
                        Sheet.Row(4).Height = 25;
                        Sheet.Row(5).Height = 25;
                        Sheet.Row(6).Height = 25;
                        Sheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(4).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet.Row(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet.Row(5).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        //Sheet.Row(6).Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //Sheet.Row(6).Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                        for (int i = 0; i < BookingList.Count; i++)
                        {
                            Sheet.Cells[string.Format("A{0}", row)].Value = BookingList[i].Name;
                            Sheet.Cells[string.Format("B{0}", row)].Value = BookingList[i].GuName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = Convert.ToDateTime(BookingList[i].BookingDate).ToString("dd-MM-yyyy HH:mm");
                            Sheet.Cells[string.Format("D{0}", row)].Value = Convert.ToDateTime(BookingList[i].FromDate).ToString("dd-MM-yyyy");
                            Sheet.Cells[string.Format("E{0}", row)].Value = Convert.ToDateTime(BookingList[i].ToDate).ToString("dd-MM-yyyy");
                            Sheet.Cells[string.Format("F{0}", row)].Value = Convert.ToDateTime(BookingList[i].CancelledDate).ToString("dd-MM-yyyy");
                            Sheet.Cells[string.Format("G{0}", row)].Value = BookingList[i].NoOfPerson;
                            Sheet.Cells[string.Format("H{0}", row)].Value = BookingList[i].TotalRate;
                            row++;
                        }
                       
                        Sheet.Cells["A:AZ"].Style.Font.Size = 10;
                        Sheet.Cells["A:AZ"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        packg.Save();
                    }
                }
                else
                {
                    using (var packg = new ExcelPackage(stream))
                    {
                        ExcelWorksheet Sheet = packg.Workbook.Worksheets.Add("Report");
                        Sheet.Cells["A1"].Value = "NO DATA FOUND";
                        packg.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            stream.Position = 0;
            string excelName = $"PackageCancel-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpGet]
        [Route("getPdfReport")]
        public async Task<IActionResult> pdfExport()
        {
            List<HsBookingPdfModel> model = new List<HsBookingPdfModel>();
            // var stream = new MemoryStream();
            try
            {
                DateTime ReportDate = DateTime.Now.AddDays(-1);
                var ReportDateToString = ReportDate.ToString("dd-MM-yyyy");
                var ToString = ReportDateToString+" " + "00:00";
                DateTime NewFromdate = Convert.ToDateTime(ToString);
                DateTime NewTodate = NewFromdate.AddHours(23).AddMinutes(59);
                /// var allData = await _context.TtBooking.ToListAsync();
                var HomestayBoolingList = from bk in _context.TtBooking.AsNoTracking()
                                          join gu in _context.TmGuestUser.AsNoTracking()
                                          on bk.GuId equals gu.GuId
                                          join hs in _context.TmHomestay.AsNoTracking()
                                          on bk.HsId equals hs.HsId
                                          where bk.BkPaymentStatus != null && bk.BkIsCancelled == 0
                                          select new
                                          {
                                              bk.GuId,
                                              bk.HsId,
                                              bk.HsBookingId,
                                              bk.BkDateFrom,
                                              bk.BkDateTo,
                                              hs.HsName,
                                              gu.GuName,
                                              bk.BkNoPers,
                                              bk.BkBookingDate,
                                              bk.TotalCost
                                          };
                var BookingList = await HomestayBoolingList.OrderByDescending(m=>m.BkBookingDate).ToListAsync();
                foreach (var item in BookingList)
                {
                    HsBookingPdfModel obj = new HsBookingPdfModel();
                    obj.GuId = item.GuId;
                    obj.HsId = item.HsId;
                    obj.BkDateFrom = item.BkDateFrom;
                    obj.GuName = item.GuName;
                    obj.BkDateTo = item.BkDateTo;
                    obj.BkNoPers = item.BkNoPers;
                    obj.HsName = item.HsName;
                    obj.BkBookingDate = item.BkBookingDate;
                    obj.TotalCost = item.TotalCost;
                    model.Add(obj);
                }       
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            byte[] abytes = PrepareReport(model);
            var adminId = "f1fb5130-0192-11ec-8831-005056a4479e";
            var Societyemail = await _context.TmUser.Where(m => m.UserId == adminId).Select(m => m.UserEmailId).FirstOrDefaultAsync();
            await SendHsBookingPdf(Societyemail,abytes);
            return File(abytes, "application/pdf");
            
            
        }

        public byte[] PrepareReport(List<HsBookingPdfModel> hsBookingPdfModel)
        {
            #region Total Cost
            int? TotalCost = 0;
            foreach (var item in hsBookingPdfModel)
            {
                TotalCost = TotalCost + item.TotalCost;
            }
            #endregion

            #region Prepare Report
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            _pdfTable.SetWidths(new float[] { 90f, 50f, 35f,35f,30f,55f,35f });
            #endregion
            this.ReportHeader(TotalCost);
            this.ReportBody(hsBookingPdfModel);
            this.ReportFooter();
            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);
            _document.Close();                
            return _memoryStream.ToArray();
        }
        

        private void ReportHeader(int? TotalCost)
        {
            DateTime ReportDate = DateTime.Now.AddDays(-1);
            var ReportDateToString = ReportDate.ToString("dd-MM-yyyy");
            var ToString = ReportDateToString + " " + "00:00";
            DateTime NewFromdate = Convert.ToDateTime(ToString);
            var RangeFrom = NewFromdate.ToString("dd-mm-yyyy hh:MM");
            DateTime NewTodate = NewFromdate.AddHours(23).AddMinutes(59);
            var RangeTo = NewTodate.ToString("dd-mm-yyyy hh:MM");
            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Kalimpong District Homestay Tourism", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.White;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Co-operative Society Ltd.", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.White;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Run Date : " + DateTime.Now.ToString("dd-MM-yyyy"), _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.White;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase(" List Of Homestay Booking Details", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.White;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Period From : "+ NewFromdate +  " To : "+ NewTodate, _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.White;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Total Amount : "+ TotalCost, _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.White;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();
        }

        private void ReportBody(List<HsBookingPdfModel> hsBookingPdfModel)
        {
            #region Table Header
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Homestay Name", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Booked By", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Check In", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Check Out", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Person", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Booked On", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Total Paid", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LightGray;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();
            #endregion

            #region Table Body
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
            foreach(HsBookingPdfModel model in hsBookingPdfModel)
            {
                _pdfPCell = new PdfPCell(new Phrase(model.HsName, _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(model.GuName, _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(model.BkDateFrom.ToString("dd-MM-yyyy"), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(model.BkDateTo.ToString("dd-MM-yyyy"), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(model.BkNoPers.ToString(), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(model.BkBookingDate.ToString("dd-MM-yyyy HH:mm"), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(model.TotalCost.ToString(), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfPCell);
                _pdfTable.CompleteRow();
                
            }
      
            #endregion
        }

        private void ReportFooter()
        {
            #region Footer
            //String footerText = "Page";
            _fontStyle = FontFactory.GetFont("arial", 8f);
            //if (footerText.Substring(footerText.Length - 1) != " ") footerText += " ";
            //Chunk chkFooter = new Chunk(footerText, _fontStyle);
            //Phrase p2 = new Phrase(chkFooter);
            //// Turn on numbering by setting the boolean to true
            //Boolean pagenumber = Convert.ToBoolean(_document.PageNumber);
            //HeaderFooter footer = new HeaderFooter();
            //footer.PageNumber=
            //footer.Border = Rectangle.NO_BORDER;
            //footer.Alignment = 1;
            //_document.Footer = footer;
            //_document.Open();
            // Footer
            HeaderFooter footer = new HeaderFooter(new Phrase(new Chunk("Page ", _fontStyle)),
                new Phrase(new Chunk(".", _fontStyle)));
            footer.Alignment = 1;
            _document.Footer = footer;
            _document.Open();
            #endregion
        }

        [NonAction]
        private async Task SendHsBookingPdf(string Mail,byte[] pdf)
        {
            //var path = Path.Combine(_env.ContentRootPath, "Template/HsCutOffTimePdf.html");
            //string content = System.IO.File.ReadAllText(path);
            //string content1
            //           = content
                       
            //          // .Replace();
            //await _emailService.Send(Mail,"Society","Booking Details", content1);
        }
    }
}
