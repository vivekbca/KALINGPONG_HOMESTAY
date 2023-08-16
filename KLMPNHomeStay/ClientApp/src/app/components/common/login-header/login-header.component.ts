import { Component, OnInit } from '@angular/core';
import { BookingService } from '../../../services/secured/booking.service';

@Component({
  selector: 'app-login-header',
  templateUrl: './login-header.component.html',
  styleUrls: ['./login-header.component.css']
})
export class LoginHeaderComponent implements OnInit {
  userName: any;
  roleName: any;
  constructor(private bookingService: BookingService) { }

  ngOnInit() {
    this.userName = window.sessionStorage.getItem('userFullNm');
    this.roleName = window.sessionStorage.getItem('roleName');
  }
  logout() {
    this.bookingService.Logout();
  }
}
