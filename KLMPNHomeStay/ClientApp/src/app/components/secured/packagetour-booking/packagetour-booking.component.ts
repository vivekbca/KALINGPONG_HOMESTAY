import { Component, OnInit } from '@angular/core';
import { PackageTourService } from '../../../services/secured/package-tour.service';
import { GlobalsService } from '../../../services/common/globals.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-packagetour-booking',
  templateUrl: './packagetour-booking.component.html',
  styleUrls: ['./packagetour-booking.component.css']
})
export class PackagetourBookingComponent implements OnInit {
  tourList: any = [];
  tourDetail: any;
  routeSub: any = {};
  user: any;
  HideFirstHeader: boolean = false;
  HideSecondHeader: boolean = false;
  constructor(private activatedRoute: ActivatedRoute,private tourService: PackageTourService, private spinner: NgxSpinnerService, private globalservice: GlobalsService, private router: Router) { }

  ngOnInit() {
    this.routeSub = this.activatedRoute.queryParams
      .subscribe(params => {
          this.user = params.user;
      });
    this.GetAllTour();
    if (this.user == "user") {
      this.HideFirstHeader = true
      this.HideSecondHeader = false
    }
    else {
      this.HideFirstHeader = false
      this.HideSecondHeader=true
    }
  }
  GetAllTour() {
    this.spinner.show();
    this.tourService.getAllTour().subscribe(resp => {
      if (resp.result === 'Success') {
        this.tourList = resp.data;
        this.spinner.hide();
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    }
    )
  }
  back() {
    this.router.navigate(['']);
  }

  bookNow(tourId: any) {
    this.router.navigate(['tour-detail'], { queryParams: { tourId: tourId,user:"user" }, skipLocationChange: false });
  }
}
