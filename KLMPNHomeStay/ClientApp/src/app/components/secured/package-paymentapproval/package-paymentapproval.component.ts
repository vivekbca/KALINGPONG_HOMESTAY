import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { GlobalsService } from '../../../services/common/globals.service';
import { Router } from '@angular/router';
import { PackageTourService } from '../../../services/secured/package-tour.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-package-paymentapproval',
  templateUrl: './package-paymentapproval.component.html',
  styleUrls: ['./package-paymentapproval.component.css']
})
export class PackagePaymentapprovalComponent implements OnInit {
  packageList: any = [];
  packageDetail: any = [];
  p: number = 1;
  constructor(private router: Router, private apicall: GlobalsService, private globalservice: GlobalsService, private formBuilder: FormBuilder, private tourService: PackageTourService) { }

  ngOnInit() {
    this.getAllPayPackageList();
  }
  back() {
    this.router.navigate(['admin-dashboard']);
  }
  getAllPayPackageList() {
    this.tourService.getAllPaymentApprovalTour().subscribe(resp => {
      if (resp.result === 'Success') {
        this.packageList = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });
  }
  accept(bookingId: any) {
    this.tourService.getPaymentApprovalTourDetail(bookingId).subscribe(resp => {
      if (resp.result === 'Success') {
        this.packageDetail = resp.data;
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
        this.tourService.approveBookingPayment(packageComObj).subscribe(resp => {

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
