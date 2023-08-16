import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { FormBuilder } from '@angular/forms';
import { BookingService } from '../../../services/secured/booking.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-hs-paymentapproval-bank',
  templateUrl: './hs-paymentapproval-bank.component.html',
  styleUrls: ['./hs-paymentapproval-bank.component.css']
})
export class HsPaymentapprovalBankComponent implements OnInit {
  hsList: any = [];
  hsDetail: any = [];
  p: number = 1;
  constructor(private router: Router, private apicall: GlobalsService, private globalservice: GlobalsService, private formBuilder: FormBuilder, private bookingService: BookingService) { }

  ngOnInit() {
    this.getAllPayHSList();
  }
  back() {
    this.router.navigate(['bankUserDashboard']);
  }
  getAllPayHSList() {
    this.bookingService.getAllPaymentApprovalHSBank().subscribe(resp => {
      if (resp.result === 'Success') {
        this.hsList = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });
  }
  accept(bookingId: any) {
    this.bookingService.getPaymentApprovalHSDetail(bookingId).subscribe(resp => {
      if (resp.result === 'Success') {
        this.hsDetail = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });
  }
  approveBookingPay(bookingId: any) {

    let packageComObj: any = {};
    packageComObj.bookingId = bookingId;
    Swal.fire({
      title: 'Are You Sure Want To Approve The Booking ?',
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
        this.bookingService.approveBookingPaymentBank(packageComObj).subscribe(resp => {

          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.msg == "Success") {
            this.apicall.swalSuccess("Booking Approved");
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
