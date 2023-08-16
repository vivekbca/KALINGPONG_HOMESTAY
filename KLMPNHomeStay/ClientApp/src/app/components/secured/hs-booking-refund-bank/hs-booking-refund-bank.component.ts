import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookingService } from '../../../services/secured/booking.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-hs-booking-refund-bank',
  templateUrl: './hs-booking-refund-bank.component.html',
  styleUrls: ['./hs-booking-refund-bank.component.css']
})
export class HsBookingRefundBankComponent implements OnInit {
  isRefunded: boolean = false;
  submitted = false;
  hsList: any = [];
  hsDetail: any = [];
  bookingId: any;
  loginUserId: any;
  p: number = 1;
  refundForm: FormGroup;
  constructor(private router: Router, private apicall: GlobalsService, private globalservice: GlobalsService, private formBuilder: FormBuilder, private bookingService: BookingService) { }

  ngOnInit() {
    this.getAllRefundBankList();
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
    this.bookingService.getAllHSRefundBank(data).subscribe(resp => {
      this.hsList = resp.data;
    })
  }
  getAllRefundBankList() {
    let data = {
      filter: "NeedToRefund"
    }
    this.bookingService.getAllHSRefundBank(data).subscribe(resp => {
      if (resp.result === 'Success') {
        this.hsList = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });
  }
  refund(bookingId: any) {
    this.bookingId = bookingId;
    this.bookingService.getPaymentRefundHSDetail(bookingId).subscribe(resp => {
      if (resp.result === 'Success') {
        this.hsDetail = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });
  }
  submit() {
    let hsComObj: any = {};
    hsComObj.bookingId = this.bookingId;
    hsComObj.voucherMode = this.f.voucherMode.value;
    hsComObj.voucherStatus = this.f.voucherStatus.value;
    hsComObj.voucherNo = this.f.voucherNo.value;
    hsComObj.voucherDt = this.f.voucherDt.value;
    hsComObj.refundBy = this.loginUserId;
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
        this.bookingService.refundBookingBank(hsComObj).subscribe(resp => {

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
}
