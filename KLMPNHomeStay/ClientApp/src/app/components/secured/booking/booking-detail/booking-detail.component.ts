import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalsService } from '../../../../services/common/globals.service';
import { BookingService } from '../../../../services/secured/booking.service';

@Component({
  selector: 'app-booking-detail',
  templateUrl: './booking-detail.component.html',
  styleUrls: ['./booking-detail.component.css']
})
export class BookingDetailComponent implements OnInit {
  roomDetail: any = [];
  finalBookedRoomList: any = [];
  constructor(private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router, private globalservice: GlobalsService, private bookingService: BookingService) { }

  ngOnInit() {
    //this.activatedRoute.params.subscribe(params => {
    //  this.roomDetail = params.roomDetail
    //});
    //this.activatedRoute.params.subscribe(params => {
    //  //const ids = params.getAll('id');
    //  //console.log('ids', ids);
    //  //this.roomDetail = params.getAll('roomDetail');
    //  this.roomDetail = params.finalBookedRoomList
    //});
    this.roomDetail = window.sessionStorage.getItem('finalBookedRoomList');
    //this.finalBookedRoomList = this.activatedRoute.snapshot.params["finalBookedRoomList"];
    //this.finalBookedRoomList = 
    console.log(this.roomDetail);
  }

}
