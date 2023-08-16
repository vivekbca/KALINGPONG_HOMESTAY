import { Component, OnInit } from '@angular/core';
import { GlobalsService } from '../../../services/common/globals.service';
import { BookingService } from '../../../services/secured/booking.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  loginUserId: any;
  userName: any;
  userEmail: any;
  userMobile: any;

  constructor(private globalservice: GlobalsService, private router: Router, private bookingService: BookingService) { }

  ngOnInit() {
    this.loginUserId = window.sessionStorage.getItem('userId');
    this.userName = window.sessionStorage.getItem('userFullNm');
    this.userEmail = window.sessionStorage.getItem('email');
    this.userMobile = window.sessionStorage.getItem('userAccDesc');
  }

  logout() {
    this.bookingService.Logout();
  }
  notice() {
    this.router.navigate(['notice-dashboard']);
  }
  home() {
    this.router.navigate(['admin-dashboard']);
  }
  tender() {
    this.router.navigate(['tender-dashboard']);
  }
  bookingHistory() {
    this.router.navigate(['booking-list']);
  }
  homeStayApproval() {
    this.router.navigate(['homestay-approval']);
  }
  AddPackage() {
    this.router.navigate(['view-package']);
  }
  PackagePaymentApproval() {
    this.router.navigate(['package-PaymentApproval']);
  }
  BookingReport() {
    this.router.navigate(['booking-report-list']);
  }
  HSPaymentApproval() {
    this.router.navigate(['hs-PaymentApproval']);
  }
  PackageRefund() {
    this.router.navigate(['package-RefundAdmin']);
  }
  HSRefund() {
    this.router.navigate(['hs-RefundAdmin']);
  }
  PackageReport() {
    this.router.navigate(['package-report-list']);
  }
}
