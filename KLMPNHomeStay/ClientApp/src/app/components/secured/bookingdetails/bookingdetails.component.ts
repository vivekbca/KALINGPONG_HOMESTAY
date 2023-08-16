import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingService } from '../../../services/secured/booking.service';
import { BookinglistService } from '../../../services/secured/bookinglist.service';
import { BookingListComponent } from '../booking-list/booking-list.component';

@Component({
  selector: 'app-bookingdetails',
  templateUrl: './bookingdetails.component.html',
  styleUrls: ['./bookingdetails.component.css']
})
export class BookingdetailsComponent implements OnInit {
  bookingId: any;
  BookingDetail: any;
  constructor(private route: ActivatedRoute, private bookingService: BookinglistService, private router: Router) { }

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.bookingId = params.get('id');
      this.bookingService.BookingDetail(this.bookingId).subscribe(resp => {
      this.BookingDetail = resp.data;
      console.log("Booking Detail", this.BookingDetail);
    })
    });
  }
  back() {
    this.router.navigate(['booking-list']);
  }
}
