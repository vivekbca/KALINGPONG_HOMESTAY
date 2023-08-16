import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { FormBuilder } from '@angular/forms';
import { PackageTourService } from '../../../services/secured/package-tour.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-tour-booking-refund-admin',
  templateUrl: './tour-booking-refund-admin.component.html',
  styleUrls: ['./tour-booking-refund-admin.component.css']
})
export class TourBookingRefundAdminComponent implements OnInit {
  isRefunded: boolean = false;
  packageList: any = [];
  packageDetail: any = [];
  p: number = 1;
  constructor(private router: Router, private apicall: GlobalsService, private globalservice: GlobalsService, private formBuilder: FormBuilder, private tourService: PackageTourService) { }

  ngOnInit() {
    this.getAllRefundPackageList();
  }
  back() {
    this.router.navigate(['admin-dashboard']);
  }
  getAllRefundPackageList() {
    let data = {
      filter: "NeedToRefund"
    }
    this.tourService.getAllRefundTourAdmin(data).subscribe(resp => {
      if (resp.result === 'Success') {
        this.packageList = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });
  }
  refund(bookingId: any) {
    this.tourService.getRefundTourDetail(bookingId).subscribe(resp => {
      if (resp.result === 'Success') {
        this.packageDetail = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });
  }
 
  refundBooking(bookingId: any) {

    let packageComObj: any = {};
    packageComObj.bookingId = bookingId;
    Swal.fire({
      title: 'Are You Sure Want To Refund The Booking ?',
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: 'Yes',
      denyButtonText: 'No',
      customClass: {
        actions: 'my-actions',
        cancelButton: 'order-1 right-gap',
        confirmButton: 'order-2',
        denyButton: 'order-3',
      }
    }).then((result) => {
      if (result.isConfirmed) {
        this.tourService.refundBooking(packageComObj).subscribe(resp => {

          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.msg == "Success") {
            this.apicall.swalSuccess("Saved Successfully");
            window.location.reload();
          }
          else {
            this.apicall.swalSuccess("Something Went Wrong");
          }
        });
      } else if (result.isDenied) {
        Swal.fire('Changes are not saved', '', 'info')
      }
    })
  }
  onRadioChange(value: any) {
    this.packageList = [];
    if (value == "NeedToRefund") {
      this.isRefunded = false;
    }
    else {
      this.isRefunded = true;
    }
    let data = {
      filter: value
    }
    this.tourService.getAllRefundTourAdmin(data).subscribe(resp => {
      this.packageList = resp.data;
    })
  }
}
