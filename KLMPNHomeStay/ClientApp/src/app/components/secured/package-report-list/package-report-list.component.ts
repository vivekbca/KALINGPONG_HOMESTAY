import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Homestaycheck } from '../../../model/secured/homestaycheck';
import { GlobalsService } from '../../../services/common/globals.service';
import { BookingListReportService } from '../../../services/secured/booking-list-report.service';
import * as XLSX from 'xlsx';
import 'jspdf-autotable';
import { UserOptions } from 'jspdf-autotable';
import { jsPDF } from 'jspdf';
import { PackageCheck } from '../../../model/secured/package-check';
interface jsPDFWithPlugin extends jsPDF {
  autoTable: (options: UserOptions) => jsPDF;
}
@Component({
  selector: 'app-package-report-list',
  templateUrl: './package-report-list.component.html',
  styleUrls: ['./package-report-list.component.css']
})
export class PackageReportListComponent implements OnInit {

  bookingList: any;
  filter: any;
  p: number = 1;
  name = 'Angular';
  isMasterSel: boolean;
  checkedCategoryList: any;
  hideButton: boolean;
  constructor(private spinner: NgxSpinnerService, public datepipe: DatePipe, private globalservice: GlobalsService, private router: Router, private bookinglistreport: BookingListReportService) {
    this.isMasterSel = false;
  }

  ngOnInit() {
    this.bookingListCall();
  }
  onRadioChange(value: any) {
    if (value == "Checked") {
      this.hideButton = true
    }
    else {
      this.hideButton = false;
    }
    this.bookinglistreport.GetPreviousdayPackageList(value).subscribe(resp => {
      this.bookingList = resp.data;
      console.log(this.bookingList);
    })
  }
  bookingListCall() {
    this.filter = "Unchecked";
    this.bookinglistreport.GetPreviousdayPackageList(this.filter).subscribe(resp => {
      this.bookingList = resp.data;
      console.log(this.bookingList);
    })
  }
  ExportTOExcel() {
    let element = document.getElementById('htmlData')!;
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, 'report_' + new Date().getTime() + '.xlsx');
  }

  openPDF() {
    var prepare = [];
    this.bookingList.forEach(e => {
      var tempObj = [];
      tempObj.push(e.hsName);
      tempObj.push(e.guName);
      tempObj.push(this.datepipe.transform(e.bkDateFrom, 'dd-MM-yyyy'));
      tempObj.push(this.datepipe.transform(e.bkDateTo, 'dd-MM-yyyy'));
      tempObj.push(e.bkNoPers);
      tempObj.push(e.totalCost);
      tempObj.push(this.datepipe.transform(e.bkBookingDate, 'dd-MM-yyyy HH:mm'));
      prepare.push(tempObj);
    });
    const doc = new jsPDF('portrait', 'px', 'a4') as jsPDFWithPlugin;
    doc.text('Kalimpong District Homestay Tourism', 30, 20);
    doc.text('Homestay Booking List', 290, 20);
    //function addFooters() {
    //  const addFooters = doc => {
    //    const pageCount = doc.internal.getNumberOfPages();
    //    for (var i = 1; i <= pageCount; i++) {
    //      doc.text(String(i), 196, 285);
    //    }
    //  }
    //}
    const addFooters = doc => {
      const pageCount = doc.internal.getNumberOfPages()
      doc.setFont('helvetica', 'italic')
      doc.setFontSize(8)
      for (var i = 1; i <= pageCount; i++) {
        doc.setPage(i)
        doc.text('Page ' + String(i) + ' of ' + String(pageCount), 500, 285, {
          align: 'center'
        })
      }
    }
    doc.autoTable({
      head: [['Homestay Name', 'Booked By', 'Check In', 'Check out', 'Number of Person', 'Total Cost', 'Booked on']],
      body: prepare,
      styles: {
        cellPadding: 1.5,
        fontSize: 10
      },
      tableLineColor: [0, 0, 0], //choose RGB
      tableLineWidth: 0.5, //table border width
    });
    addFooters(doc);
    // below line for Open PDF document in new tab
    doc.output('dataurlnewwindow')
    // below line for Download PDF document  
    doc.save('HomestayBookedList.pdf');
  }
  back() {
    this.router.navigate(['admin-dashboard']);
  }
  checkUncheckAll() {
    for (var i = 0; i < this.bookingList.length; i++) {
      this.bookingList[i].isSelected = this.isMasterSel;
    }
    this.getCheckedItemList();
  }

  isAllSelected() {
    this.isMasterSel = this.bookingList.every(function (item: any) {
      return item.isSelected == true;
    })
    this.getCheckedItemList();
  }

  getCheckedItemList() {
    this.checkedCategoryList = [];
    for (var i = 0; i < this.bookingList.length; i++) {
      if (this.bookingList[i].isSelected)
        this.checkedCategoryList.push(this.bookingList[i]);

    }
    /*this.checkedCategoryList = JSON.stringify(this.checkedCategoryList);*/
    console.log("Checked List", this.checkedCategoryList)
  }
  Checked() {
    const pre = this.checkedCategoryList.value as PackageCheck;
    this.spinner.show();
    this.bookinglistreport.CheckedAllPkg(this.checkedCategoryList).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        console.log(resp.result)
        this.bookingList = resp;
        this.spinner.hide();
        if (resp.result == "Success") {
          this.globalservice.swalSuccess("Booking Checked");
        }
        window.location.reload();
      });
  }
  PackageBookingPdfReport() {

   // window.location.href = this.globalservice.getApiUrl() + 'PackageBookingPdfReport/getPdfReport';
    window.open(this.globalservice.getApiUrl() + 'PackageBookingPdfReport/getPdfReport', '_blank');
  }
  PackageCancellationPdfReport() {
   // window.location.href = this.globalservice.getApiUrl() + 'PackageCancelPdfReport/getPdfReport';
    window.open(this.globalservice.getApiUrl() + 'PackageCancelPdfReport/getPdfReport', '_blank');
  }
  DownloadPackageBookingReport() {
   // window.location.href = this.globalservice.getApiUrl() + 'report/getPkgBookingReport';
    window.open(this.globalservice.getApiUrl() + 'report/getPkgBookingReport', '_blank');
  }
  DownloadPackageCancelReport() {
   // window.location.href = this.globalservice.getApiUrl() + 'report/getPkgCancelReport';
    window.open(this.globalservice.getApiUrl() + 'report/getPkgCancelReport', '_blank');
  }
}
