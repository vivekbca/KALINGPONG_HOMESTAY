import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { BookingService } from '../../../services/secured/booking.service';
import { PackageTourService } from '../../../services/secured/package-tour.service';
import { GuestregistrationService } from '../../../services/unsecured/GuestRegistration/guestregistration.service';

@Component({
  selector: 'app-admin-tour-detail',
  templateUrl: './admin-tour-detail.component.html',
  styleUrls: ['./admin-tour-detail.component.css']
})
export class AdminTourDetailComponent implements OnInit {
  routeSub: any = {};
  tourId: any;
  person: any;
  personNumber: number;
  tourDateId: any;
  totalCost: any;
  guestCounter: number = 0;
  userDetails: any = [];
  dateDetail: any = [];
  guestDetail: any = [];
  fromDate: any;
  toDate: any;
  public currentDate = new Date();
  loginUserId: any;
  tourCost: any;
  i: number;
  submitted = false;
  tourDetail: any;
  tourName: any;
  tourDateDetail: any = [];
  tourForm: FormGroup;
  assignMemberDetailForm: FormGroup;
  packgbookingLoginForm: FormGroup;
  invoiceDetailForm: FormGroup;
  gustDetailForm: FormGroup;
  tab1: boolean = false;
  tab2: boolean = false;
  tab3: boolean = false;
  tab4: boolean = false;
  loginNone: boolean = false;
  tourDateDetailCompObj: any = {};


  constructor(private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private guRegistrationService: GuestregistrationService, private router: Router, private globalservice: GlobalsService, private tourService: PackageTourService, private bookingService: BookingService) { }

  ngOnInit() {
    this.routeSub = this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.tourId && params.hasOwnProperty('tourId')) {
          this.tourId = params.tourId;
        }
      });
    //this.initForm();
    if (this.tourId) {
      this.initiateTourDetail(this.tourId);
      this.initiateTourDate(this.tourId);
    }
    this.initForm();
    this.initBookingForm();
    this.initLoginForm();
  }
  get f() { return this.tourForm.controls; }
  get f1() { return this.assignMemberDetailForm.controls; }
  get f2() { return this.packgbookingLoginForm.controls; }
  get f3() { return this.invoiceDetailForm.controls; }
  get f4() { return this.gustDetailForm.controls; }

  private initiateTourDetail(tourId: any) {
    this.tourService.getTourById(tourId).subscribe(resp => {
      this.tourDetail = resp.data;
      console.log("Tour Details",this.tourDetail);
      this.tourName = resp.data.tourName;
      this.tourCost = resp.data.cost;
    }
    )
  }

  tourFile(tourId: any) {
    this.tourService.viewTourFile(tourId).subscribe(res => {
      const fileURL = URL.createObjectURL(res);
      window.open(fileURL, '_blank');
    });
  }
  private initiateTourDate(tourId: any) {
    this.tourService.getTourDateByIdforadmin(tourId).subscribe(resp => {
      this.tourDateDetail = resp.data;
    }
    )
  }
  initForm() {
    this.tourForm = this.formBuilder.group({
      dateId: [''],
    });
  }
  initBookingForm() {
    this.assignMemberDetailForm = this.formBuilder.group({
      person: ['', Validators.required],
    });
  }
  initLoginForm() {
    this.packgbookingLoginForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
    });
  }
  get guestDet(): FormArray {
    return this.gustDetailForm.get("guestDet") as FormArray
  }
  back() {
    this.router.navigate(['view-package']);
  }
  logout() {
    this.bookingService.Logout();
  }

}
