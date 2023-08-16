import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PackageTourService } from '../../../services/secured/package-tour.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-tour-booking-refund-bank',
  templateUrl: './tour-booking-refund-bank.component.html',
  styleUrls: ['./tour-booking-refund-bank.component.css']
})
export class TourBookingRefundBankComponent implements OnInit {
  isRefunded: boolean = false;
  submitted = false;
  packageList: any = [];
  packageDetail: any = [];
  p: number = 1;
  bookingId: any;
  loginUserId: any;
  refundForm: FormGroup;
  constructor(private router: Router, private apicall: GlobalsService, private globalservice: GlobalsService, private formBuilder: FormBuilder, private tourService: PackageTourService) { }

  ngOnInit() {
    this.getAllRefundPackageList();
    this.initForm();
    this.loginUserId = window.sessionStorage.getItem('userId');
  }
  back() {
    this.router.navigate(['bankUserDashboard']);
  }
  get f() { return this.refundForm.controls; }
  initForm() {
    this.refundForm = this.formBuilder.group({
      voucherMode: ['', Validators.required],
      voucherStatus: ['', Validators.required],
      voucherNo: ['', Validators.required],
      voucherDt: ['', Validators.required],
    });
  }
  getAllRefundPackageList() {
    let data = {
      filter: "NeedToRefund"
    }
    this.tourService.getAllRefundTourBank(data).subscribe(resp => {
      if (resp.result === 'Success') {
        this.packageList = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });
  }

  refund(bookingId: any) {
    this.bookingId = bookingId;
    this.tourService.getRefundTourDetail(bookingId).subscribe(resp => {
      if (resp.result === 'Success') {
        this.packageDetail = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });
  }

  submit() {
    
    let packageComObj: any = {};
    packageComObj.bookingId = this.bookingId;
    packageComObj.voucherMode = this.f.voucherMode.value;
    packageComObj.voucherStatus = this.f.voucherStatus.value;
    packageComObj.voucherNo = this.f.voucherNo.value;
    packageComObj.voucherDt = this.f.voucherDt.value;
    packageComObj.refundBy = this.loginUserId;
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
        this.submitted = true;
        this.tourService.refundBookingBank(packageComObj).subscribe(resp => {

          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.msg == "Success") {
            this.submitted = false;
            this.apicall.swalSuccess("Refund Successfully");
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
    if (value == "NeedToRefund") {
      this.isRefunded = false;
    }
    else {
      this.isRefunded = true;
    }
    let data = {
      filter: value
    }
    this.tourService.getAllRefundTourBank(data).subscribe(resp => {
      this.packageList = resp.data;
    })
  }
}
