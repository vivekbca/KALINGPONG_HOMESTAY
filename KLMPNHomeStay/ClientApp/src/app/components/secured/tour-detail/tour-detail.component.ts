import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { PackageTourService } from '../../../services/secured/package-tour.service';
import { BookingService } from '../../../services/secured/booking.service';
import { GuestregistrationService } from '../../../services/unsecured/GuestRegistration/guestregistration.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { param } from 'jquery';

@Component({
  selector: 'app-tour-detail',
  templateUrl: './tour-detail.component.html',
  styleUrls: ['./tour-detail.component.css']
})
export class TourDetailComponent implements OnInit {
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
  noDate: any;
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
  user:any
  tourDateDetailCompObj: any = {};
  HideFirstHeader: boolean = false;
  HideSecondHeader: boolean = false;


  constructor(private spinner: NgxSpinnerService, private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private guRegistrationService: GuestregistrationService, private router: Router, private globalservice: GlobalsService, private tourService: PackageTourService, private bookingService: BookingService) { }

  ngOnInit() {
    this.routeSub = this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.tourId && params.hasOwnProperty('tourId')) {
          this.tourId = params.tourId;
          this.user = params.user;
        }
      });
    if (this.user == "user") {
      this.HideFirstHeader = true
      this.HideSecondHeader = false
    }
    else {
      this.HideFirstHeader = false
      this.HideSecondHeader = true
    }
    //this.initForm();
    if (this.tourId) {
      this.initiateTourDetail(this.tourId);
      this.initiateTourDate(this.tourId);
    }
    this.initForm();
    this.initBookingForm();
    this.initLoginForm();
    this.initInvoiceForm();
    this.initguestDetForm();
  }
  get f() { return this.tourForm.controls; }
  get f1() { return this.assignMemberDetailForm.controls; }
  get f2() { return this.packgbookingLoginForm.controls; }
  get f3() { return this.invoiceDetailForm.controls; }
  get f4() { return this.gustDetailForm.controls; }

  private initiateTourDetail(tourId: any) {
    this.tourService.getTourById(tourId).subscribe(resp => {
      this.tourDetail = resp.data;
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
    this.tourService.getTourDateById(tourId).subscribe(resp => {
      this.tourDateDetail = resp.data;
      if (this.tourDateDetail == null) {
        this.noDate = true;
      }
    }
    )
  }
  OnBooking() {
    
    if (this.tourForm.invalid) {
      return;
    } else {
      let tourDtCompObj: any = {};
      this.tourDateId = this.f.dateId.value;
      tourDtCompObj.dateId = this.f.dateId.value;
      tourDtCompObj.tourId = this.tourId;
      this.tab1 = true;
      this.tab2 = true;
      this.loginNone = false;
      this.tourDateDetailCompObj = tourDtCompObj;
     
      //console.log(this.tourDateDetailCompObj);
      this.tourService.bookingDate(tourDtCompObj).subscribe(
        resp => {
          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.result === 'Success') {
            this.dateDetail = resp.data;
            this.fromDate = resp.data.fromDt;
            this.toDate = resp.data.toDt;
          }
          if (resp.result === 'Error') {
            this.globalservice.swalError(resp.msg);
          }
        });
    }
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
  initInvoiceForm() {
    this.invoiceDetailForm = this.formBuilder.group({
      firstName: [''],
      lastName: [''],
      mobileNo: [''],
      email: [''],
      address: [''],
      city: [''],
      country: [''],
      pinCode: [''],
      gender: [''],
      dob: [''],
      identityType: [''],
      identityNo: [''],
      //userName: ['', Validators.required],
      //password: ['', Validators.required],
    });
  }
  initguestDetForm() {
    //this.gustDetailForm = this.formBuilder.group({
    //  //gFirstName: [''],
    //  //gLastName: [''],
    //  //gDOB: [''],
    //  //gGender: [''],
    //  'guestDet': this.formBuilder.array([
    //    this.getGuestDet()
    //  ])
    //});
    this.gustDetailForm = this.formBuilder.group({
     
      guestDet: this.formBuilder.array([]),
    });
  }
  get guestDet(): FormArray {
    return this.gustDetailForm.get("guestDet") as FormArray
  }
  newGuestDet(): FormGroup {
    return this.formBuilder.group({
      gFirstName: '',
      gLastName: '',
      gDOB: '',
      gGender: '',
    })
  }
  addGuests() {
    this.guestCounter = this.guestCounter + 1;
    if (this.guestCounter > this.personNumber) {
      this.globalservice.swalError("You can't add more than " + this.personNumber + " guest detail");
    }
    if (this.guestCounter <= this.personNumber) {
      this.guestDet.push(this.newGuestDet());
    }
  }
  AssignMember() {
    
    this.submitted = true;
    this.person = this.f1.person.value;
    this.personNumber = this.f1.person.value;
    let costCompObj: any = {};
    costCompObj.person = this.person;
    costCompObj.tourId = this.tourId;
    
    this.tourService.totalCost(costCompObj).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        if (resp.result === 'Success') {
          this.totalCost = resp.data.totalCost;
        }
        if (resp.result === 'Error') {
          this.globalservice.swalError(resp.msg);
        }
      });

    let userName = window.sessionStorage.getItem('userFullNm');
    if (userName == null) {
      this.loginNone = true;
      this.tab1 = true;
      this.tab2 = false;
      this.submitted = false;
    }
    else {
      this.submitted = false;
      this.tab1 = true;
      this.tab2 = false;
      this.tab3 = true;
      this.loginNone = false;
      this.loginUserId = window.sessionStorage.getItem('userId');
      this.initiateUpdateBooking();
    }
  }
  onLogin() {
   
    if (this.packgbookingLoginForm.invalid) {
      return;
    }
    else {
      let loginComObj: any = {};
      loginComObj.UserName = this.f2.userName.value;
      loginComObj.Password = this.f2.password.value;

      this.bookingService.LoginOn(loginComObj).subscribe(
        resp => {
          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.result === 'Success') {
            this.submitted = false;
            const access_token = resp.data.token as string;
            const acc_desc = resp.data.accountDescription as string;
            const full_nm = resp.data.fullName as string;
            const menu = JSON.stringify(resp.data.menu) as string;
            const email = resp.data.email as string;
            const userId = resp.data.userId as string;

            window.sessionStorage.setItem('userToken', access_token);
            window.sessionStorage.setItem('userAccDesc', acc_desc);
            window.sessionStorage.setItem('userFullNm', full_nm);
            window.sessionStorage.setItem('userMenu', menu);
            window.sessionStorage.setItem('email', email);
            window.sessionStorage.setItem('userId', userId);
            //this.tab3 = true;
            //this.loginNone = false;
            this.tab1 = true;
            this.tab2 = false;
            this.loginNone = false;
            this.tab3 = true;
            this.loginUserId = window.sessionStorage.getItem('userId');
            this.initiateUpdateBooking();
          }
          if (resp.result === 'Info') {
            this.globalservice.swalError(resp.msg);
          }
        });
    }
  }
  back() {
    this.router.navigate(['packagetour-booking']);
  }
  back1() {
    //this.tab1 = true;
    this.router.navigate(['packagetour-booking']);
  }
  private initiateUpdateBooking() {
    if (this.loginUserId) {
      this.guRegistrationService.GetUserById(this.loginUserId).subscribe(data => {
        let res: any = data;
        if (res.result === 'Success') {
          this.userDetails = res.data;
          this.onModifyUserForm();
        } else {
        }
      });
    }
  }
  private onModifyUserForm() {
    this.f3.firstName.setValue(this.userDetails.guFirstName);
    this.f3.lastName.setValue(this.userDetails.guLastName);
    this.f3.mobileNo.setValue(this.userDetails.mobileNo);
    this.f3.email.setValue(this.userDetails.emailId);
    this.f3.address.setValue(this.userDetails.address);
    this.f3.dob.setValue(this.userDetails.dob);
    this.f3.gender.setValue(this.userDetails.gender);
    this.f3.country.setValue(this.userDetails.countryName);
    this.f3.city.setValue(this.userDetails.city);
    this.f3.pinCode.setValue(this.userDetails.pinCode);
    this.f3.identityType.setValue(this.userDetails.identityType);
    this.f3.identityNo.setValue(this.userDetails.identityNo);

  }
  proceedPay() {
    console.log(this.guestDet.value);
    if (this.guestCounter == 0) {
      this.globalservice.swalError("Please add guest details");
      return;
    }
    if (this.guestCounter < this.personNumber) {
      this.globalservice.swalError("Please add " + this.personNumber + " guest detail");
      return;
    }
    
    let payCompObj: any = {};
    payCompObj.person = this.personNumber;
    payCompObj.tourId = this.tourId;
    payCompObj.tourDateId = this.tourDateId;
    payCompObj.guestDet = this.guestDet.value;
    payCompObj.guId = this.loginUserId;
    payCompObj.totalCost = this.totalCost;
    this.spinner.show();
    this.tourService.proceedPay(payCompObj).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        if (resp.result === 'Success') {
          this.spinner.hide();
          this.tab3 = false;
          this.tab4 = true;
        }
        if (resp.result === 'Error') {
          this.spinner.hide();
          this.globalservice.swalError(resp.msg);
        }
      });
    
  }
  logout() {
    //this.logoutButton = false;
    this.bookingService.Logout();
  }
  //counter(i) {
  //  this.i = this.personNumber;
  //  for (i = 0; i < this.i; i++) {
  //    this.guestDetail.push(i);
  //    console.log(this.guestDetail);
  //  }
  //}
}
