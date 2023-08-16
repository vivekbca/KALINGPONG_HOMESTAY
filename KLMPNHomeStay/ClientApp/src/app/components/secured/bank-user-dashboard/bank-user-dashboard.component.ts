import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { BookingService } from '../../../services/secured/booking.service';

@Component({
  selector: 'app-bank-user-dashboard',
  templateUrl: './bank-user-dashboard.component.html',
  styleUrls: ['./bank-user-dashboard.component.css']
})
export class BankUserDashboardComponent implements OnInit {
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
  PackagePaymentApprovalBank() {
    this.router.navigate(['package-PaymentApprovalBank']);
  }
  HSPaymentApprovalBank() {
    this.router.navigate(['hs-PaymentApprovalBank']);
  }
  PackageRefundBank() {
    this.router.navigate(['package-RefundBank']);
  }
  HomeStayRefundBank() {
    this.router.navigate(['hs-RefundBank']);
  }
}
