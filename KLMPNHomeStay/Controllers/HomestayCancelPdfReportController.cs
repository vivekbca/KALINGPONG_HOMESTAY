using iTextSharp.text;
using iTextSharp.text.pdf;
using KLMPNHomeStay.Entities;
using KLMPNHomeStay.Models.Response_Model;
using KLMPNHomeStay.Services;
using Microsoft.AspNetCore.Hosting;
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
    public class HomestayCancelPdfReportController : Controller
    {
        private readonly klmpnhomestay_dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IGlobalService _globalService;

        #region Declaration
        int _totalColumn = 8;
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable = new PdfPTable(8);
        PdfPCell _pdfPCell;
        MemoryStream _memoryStream = new MemoryStream();
        #endregion

        public HomestayCancelPdfReportController(klmpnhomestay_dbContext context, IWebHostEnvironment env, IConfiguration configuration, IGlobalService globalService)
        {
            _context = context;
            _env = env;
            _globalService = globalService;
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
                var ToString = ReportDateToString + " " + "00:00";
                DateTime NewFromdate = Convert.ToDateTime(ToString);
                DateTime NewTodate = NewFromdate.AddHours(23).AddMinutes(59);
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
                var BookingList = await HomestayBoolingList.OrderByDescending(m=>m.BkCancelledDate).ToListAsync();
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

                    obj.BkCancelledDate = Convert.ToDateTime(item.BkCancelledDate);
                    model.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            byte[] abytes = PrepareReport(model);
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
            _pdfTable.SetWidths(new float[] { 90f, 50f, 35f, 35f, 50f,30f, 35f, 35f });
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
            _pdfPCell = new PdfPCell(new Phrase(" List Of Homestay Cancellation Details", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.White;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Period From : " + NewFromdate + " To : " + NewTodate, _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.White;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Total Refund : " + TotalCost, _fontStyle));
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

            _pdfPCell = new PdfPCell(new Phrase("Cancelled On", _fontStyle));
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
            foreach (HsBookingPdfModel model in hsBookingPdfModel)
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

                _pdfPCell = new PdfPCell(new Phrase(model.BkCancelledDate.ToString("dd-MM-yyyy"), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(model.BkNoPers.ToString(), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(model.BkBookingDate.ToString("dd-MM-yyyy"), _fontStyle));
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
            String footerText = "Page";
            _fontStyle = FontFactory.GetFont("arial", 8f);
            if (footerText.Substring(footerText.Length - 1) != " ") footerText += " ";

            Chunk chkFooter = new Chunk(footerText, _fontStyle);
            Phrase p2 = new Phrase(chkFooter);

            // Turn on numbering by setting the boolean to true
            HeaderFooter footer = new HeaderFooter(p2, true);
            footer.Border = Rectangle.NO_BORDER;
            footer.Alignment = 1;

            _document.Footer = footer;

            // Open the Document for writing and continue creating its content
            _document.Open();
            //HeaderFooter footer = new HeaderFooter(new Phrase("This is page: "), true);
            //footer.Border = Rectangle.NO_BORDER;
            //_document.Footer = footer;
            #endregion
        }
    }
}
