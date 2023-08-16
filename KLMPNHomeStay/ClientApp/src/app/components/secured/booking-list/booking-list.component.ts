import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookinglistService } from '../../../services/secured/bookinglist.service';

@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {
  displayStyle: any;
  BookingList: any;
  BookingDetail: any;
  listFilter: any;
  HomestayName: any;
  p: number = 1;
  constructor(private bookingService: BookinglistService, private router: Router) { }

  ngOnInit() {
    this.listFilter = "Upcoming";
    this.bookingService.GetAllBookingList(this.listFilter).subscribe(resp => {
      this.BookingList = resp.data;
      console.log("Booking List",this.BookingList);
    })
  }
  onRadioChange(value:any) {
    this.listFilter = value;
    this.bookingService.GetAllBookingList(this.listFilter).subscribe(resp => {
      this.BookingList = resp.data;
      console.log("Booking List", this.BookingList);
    })
  }
  BookingDetails(value: any) {
    this.router.navigate(['/booking-details', value]);
  }
  back() {
    this.router.navigate(['admin-dashboard']);
  }
}
