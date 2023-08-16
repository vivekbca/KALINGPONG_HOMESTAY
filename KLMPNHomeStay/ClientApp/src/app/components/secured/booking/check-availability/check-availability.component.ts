import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { GlobalsService } from '../../../../services/common/globals.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingService } from '../../../../services/secured/booking.service';
import { GuestregistrationService } from '../../../../services/unsecured/GuestRegistration/guestregistration.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { DatePipe } from '@angular/common';
declare var $: any;
@Component({
  selector: 'app-check-availability',
  templateUrl: './check-availability.component.html',
  styleUrls: ['./check-availability.component.css']
})
export class CheckAvailabilityComponent implements OnInit {
  @ViewChild("flexslider", { static: true }) flexslider: ElementRef;
  @ViewChild("parentHorizontalTab", { static: true }) parentHorizontalTab: ElementRef;
  @ViewChild("owlcarousel", { static: true }) owlcarousel: ElementRef;
  @ViewChild("loop", { static: true }) loop: ElementRef;
  public currentDate = new Date();
  submitted = false;
  checkavailForm: FormGroup;
  roomavailForm: FormGroup;
  bookingDetailForm: FormGroup;
  bookingLoginForm: FormGroup;
  invoiceDetailForm: FormGroup;
  hsId: any;
  hsName: any;
  totalRate: any;
  discountVal: any;
  discountRate: any;
  bookingNotPossible: any = 0;
  beforeDiscountTotal: any;
  fromDt: any;
  toDt: any;
  adultNumber: any;
  childNumber: any;
  discountDet: any;
  bookingId: any;
  totalNight: any;
  totalRooms: any;
  withOutDiscountTotal: any;
  discountAmount: any;
  loginUserId: any;
  userDetails: any;
  countryList: any = [];
  //currentDt = new Date();
  calculatePrice: boolean = false;
  checkRoom: boolean = false;
  bookRoom: boolean = false;
  tab1: boolean = false;
  tab2: boolean = false;
  tab3: boolean = false;
  tab4: boolean = false;
  loginNone: boolean = false;
  invoice: boolean = false;
  logoutButton: boolean = false;
  availRoomList: any = [];
  roomList: any = [];
  dateList: any = [];
  checkedroomList: any = [];
  roomModels: any = [];
  finalBookedRoomList: any;
  bookedRoomsModel: any = {
    date: null,
    //roomNo: null,
    roomNo: [],
  };
  requestData: any = {
    hsId: null,
    bookedRoomsModels: this.bookedRoomsModel,
  };

  constructor(private formBuilder: FormBuilder,private spinner: NgxSpinnerService, private activatedRoute: ActivatedRoute, private router: Router, private globalservice: GlobalsService, private bookingService: BookingService, private guRegistrationServi: GuestregistrationService) { }

  ngOnInit() {
    //$(document).ready(function () {
    //  //Horizontal Tab
    //  //$('#parentHorizontalTab-two').easyResponsiveTabs({
    //  //  type: 'default', //Types: default, vertical, accordion
    //  //  width: 'auto', //auto or any width like 600px
    //  //  fit: true, // 100% fit in a container
    //  //  tabidentify: 'hor_1', // The tab groups identifier
    //  //  activate: function (event) { // Callback function if tab is switched
    //  //    var $tab = $(this);
    //  //    var $info = $('#nested-tabInfo');
    //  //    var $name = $('span', $info);
    //  //    $name.text($tab.text());
    //  //    $info.show();
    //  //  }
    //  //});


    //  //});
    //  this.activatedRoute.params.subscribe(params => {
    //    this.hsId = params.hsId
    //  });
    //  this.initForm();
    //  this.initBookingForm();
    //});

    this.activatedRoute.params.subscribe(params => {
      this.hsId = params.hsId;
    });
    this.initForm();
    this.initBookingForm();
    this.initLoginForm();
    this.initInvoiceForm();
    this.loginUserId = window.sessionStorage.getItem('userId');
    if (this.loginUserId) {
      this.logoutButton = true;
    }
  }
   
  get f() { return this.checkavailForm.controls; }
  get f1() { return this.bookingDetailForm.controls; }
  get f2() { return this.bookingLoginForm.controls; }
  get f3() { return this.invoiceDetailForm.controls; }


  initForm() {
    this.checkavailForm = this.formBuilder.group({
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required],
      noOfRooms: ['', Validators.required],
    });
  }
  initBookingForm() {
    this.bookingDetailForm = this.formBuilder.group({
      adult: ['', Validators.required],
      child: [''],
      discountType: [''],
    });
  }
  initLoginForm() {
    this.bookingLoginForm = this.formBuilder.group({
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
  OnCheck() {
    this.submitted = true;
    if (this.checkavailForm.invalid) {
      return;
    } else {
      this.spinner.show();
      this.fromDt = this.f.fromDate.value;
      this.toDt = this.f.toDate.value;
      this.checkRoom = false;
      let checkCompObj: any = {};
      checkCompObj.fromDt = this.f.fromDate.value;
      checkCompObj.toDt = this.f.toDate.value;
      checkCompObj.hsId = this.hsId;
      checkCompObj.noOfRooms = this.f.noOfRooms.value;
      
      this.bookingService.CheckAvailability(checkCompObj).subscribe(
        resp => {
          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.result === 'Success') {
            this.submitted = false;
            this.checkRoom = true;
            this.availRoomList = resp.data;
            this.spinner.hide();
            //this.roomList = resp.data.roomAvailabilityModels;
            //console.log(resp.data.roomAvailabilityModels);
            //alert("Saved Successfully");
            //this.globalservice.swalSuccess("Tender Add Successfully");
            //this.router.navigate(['check-availability']);
          }
          if (resp.result === 'Error') {
            this.globalservice.swalError(resp.msg);
            this.spinner.hide();
          }
        });
    }
  }

  back() {
    this.router.navigate(['']);
  }

  bookedRoomNext() {
    this.bookingNotPossible = 0;
    this.spinner.show();
    console.log(this.availRoomList);
    this.bookingService.BookingPossible(this.availRoomList).subscribe(resp => {
      if (resp.data == 1) {
        this.bookingNotPossible = 1;
        this.globalservice.swalError("You can't book this homestay during this date range as rooms not available in all dates or please check atleast one room for every date");
        this.spinner.hide();
      }
      else {
        //if (this.bookingNotPossible == 0) {
          this.bookingService.BookedRooms(this.availRoomList).subscribe(
            resp => {
              this.globalservice.showMessage(resp.msg, resp.result);
              if (resp.result === 'Success') {
                this.spinner.hide();
                this.checkRoom = false;
                this.bookRoom = true;
                this.tab2 = true;
                this.availRoomList = [];
                this.finalBookedRoomList = resp.data;
                this.roomModels = resp.data.bookedRoomsModels;
                this.hsName = resp.data.hsName;
                this.totalRate = resp.data.totalRate;
                this.beforeDiscountTotal = resp.data.totalRate;
                this.discountRate = resp.data.totalRate;
                this.totalNight = resp.data.totalNights;
                this.totalRooms = resp.data.totalRooms;
              }
            });
        //}
      }
    });
    //for (let entry of this.availRoomList) {
    //  for (let entry1 of entry.roomAvailabilityModels)
    //    if (entry1.isChecked == 0 && entry.isAvailable == 0) {
    //      this.bookingNotPossible = 1;
    //  }
    //}
    
   
  }
  adultNo(adultNo: any) {
    this.calculatePrice = false;
    this.adultNumber = adultNo;
  }
  childNo(childNo: any) {
    this.childNumber = childNo;
    this.calculatePrice = false;
  }
  discountChange(discountVal: any) {

    if (discountVal === "20% - Senior Citizen") {
      this.discountDet = discountVal;
      this.discountVal = "S";
      this.discountRate = 20;
    }
    else if (discountVal === "50% - Cancer Patient") {
      this.discountDet = discountVal;
      this.discountVal = "C";
      this.discountRate = 50;
    }
    else if (discountVal === "25% - Single Occupancy") {
      this.discountDet = discountVal;
      this.discountVal = "Si";
      this.discountRate = 25;
    }
    else
    {
      this.discountDet = "Not Applicable";
      this.discountRate = 0;
    }
    this.calculatePrice = false;
    //let discountCompObj: any = {};
    //discountCompObj.totalAmount = this.beforeDiscountTotal; 
    ////discountCompObj.totalAmount = this.totalRate; 
    //discountCompObj.discountType = this.discountVal;
    //discountCompObj.discountRate = this.discountRate;
    //this.bookingService.DiscountRate(discountCompObj).subscribe(
    //  resp => {
    //    this.globalservice.showMessage(resp.msg, resp.result);
    //    if (resp.result === 'Success') {
    //      //this.beforeDiscountTotal = resp.data.totalAmount;
    //      //this.totalRate = resp.data.discountAmount;
    //    }
    //  });
  }
  bookingDetailNext() {
    console.log(this.roomModels);
    this.submitted = true;
    let bookingDetailObj: any = {};
    bookingDetailObj.hsId = this.hsId;
    bookingDetailObj.adultNo = this.f1.adult.value;
    bookingDetailObj.childNo = this.f1.child.value; 
    bookingDetailObj.discountRate = this.discountRate;
    bookingDetailObj.totalRate = this.beforeDiscountTotal;
    bookingDetailObj.discountType = this.discountVal;
    bookingDetailObj.bookingRoomDetails = this.roomModels;
    
    this.bookingService.AssignMember(bookingDetailObj).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        if (resp.result === 'Success') {
          let userName = window.sessionStorage.getItem('userFullNm');
          if (userName == null) {
            this.loginNone = true;
            this.submitted = false;
            this.GetAllCountry();
          }
          else {
            this.submitted = false;
            this.tab3 = true;
            this.invoice = true;
            this.bookRoom = false;
            this.GetAllCountry();
            this.loginUserId = window.sessionStorage.getItem('userId');
            this.initiateUpdateBooking();
          }
        }
        if (resp.result === 'Error') {
          this.globalservice.swalError(resp.msg);
        }
      });
  }

  back2() {
    this.tab2 = false;
    this.checkRoom = false;
    this.bookRoom = false;
    this.initForm();
  }

  onLogin() {
    this.submitted = true;
    if (this.bookingLoginForm.invalid) {
      return;
    }
    else {
      let loginComObj: any = {};
      loginComObj.UserName = this.f2.userName.value;
      loginComObj.Password = this.f2.password.value;
      this.spinner.show();

      this.bookingService.LoginOn(loginComObj).subscribe(
        resp => {
          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.result === 'Success') {
            this.spinner.hide();
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

            this.loginNone = false;
            this.tab3 = true;
            this.invoice = true;
            this.bookRoom = false;
            this.loginUserId = window.sessionStorage.getItem('userId');

            this.initiateUpdateBooking();
          }
          if (resp.result === 'Info') {
            this.spinner.hide();
            this.globalservice.swalError(resp.msg);
          }
        });
    }
    
  }

  logout() {
    this.logoutButton = false;
    this.bookingService.Logout();
  }

  calculatePricing() {
    this.submitted = true;
    if (this.discountRate > 50)
    {
      this.discountRate = 0;
    }
    let priceDetailObj: any = {};
    priceDetailObj.adultNo = this.f1.adult.value;
    priceDetailObj.childNo = this.f1.child.value == "" ? 0 : this.f1.child.value;
    priceDetailObj.discountRate = this.discountRate;
    priceDetailObj.totalRate = this.beforeDiscountTotal;
    this.bookingService.CalculatePricing(priceDetailObj).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        if (resp.result === 'Success') {
          this.submitted = false;
          this.totalRate = resp.data.totalBillRate;
          this.calculatePrice = true;
          this.withOutDiscountTotal = resp.data.withOutDiscountTotal;
          this.discountAmount = resp.data.discountAmount;
        }
        if (resp.result === 'Error') {
          this.globalservice.swalError(resp.msg);
        }
      });
  }

  GetAllCountry() {
    this.guRegistrationServi.GetAllCountry().subscribe(resp => {
      if (resp.result === 'Success') {
        this.countryList = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }

    });
  }

  private initiateUpdateBooking()
  {
    if (this.loginUserId) {
         this.guRegistrationServi.GetUserById(this.loginUserId).subscribe(data => {
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
    this.submitted = true;
    let payComObj: any = {};
    //payComObj.userName = this.f3.userName.value;
    //payComObj.password = this.f3.password.value;
    payComObj.totalPrice = this.totalRate;
    payComObj.hsId = this.hsId;
    payComObj.adult = this.adultNumber;
    payComObj.child = this.childNumber;
    payComObj.bookDetail = this.roomModels;
    payComObj.fromDt = this.fromDt;
    payComObj.toDt = this.toDt;
    payComObj.guId = this.loginUserId;
    this.spinner.show();
    this.bookingService.ProceedPayBook(payComObj).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        if (resp.result === 'Success') {
          this.spinner.hide();
          this.submitted = false;
          //this.totalRate = resp.data.totalBillRate;
          //this.withOutDiscountTotal = resp.data.withOutDiscountTotal;
          //this.discountAmount = resp.data.discountAmount;
          this.calculatePrice = true;
          this.bookingId = resp.data.bookingId;
          this.tab4 = true;
          this.invoice = false;
        }
        if (resp.result === 'Error') {
          this.spinner.hide();
          this.globalservice.swalError(resp.msg);
          
        }
      });
  }
  backMember() {
    this.tab3 = false;
    this.bookRoom = true;
  }
}
