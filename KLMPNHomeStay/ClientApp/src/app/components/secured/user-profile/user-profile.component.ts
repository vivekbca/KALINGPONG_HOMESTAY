import { Component, OnInit } from '@angular/core';
import { AuthGuardService } from '../../../services/unsecured/authentication/auth-guard.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { UserProfileService } from '../../../services/secured/user-profile.service';
import { BookingService } from '../../../services/secured/booking.service';
import { FeedbackService } from '../../../services/secured/feedback.service';
import { Cancelmodel } from '../../../model/secured/cancelmodel';
import { NgxSpinnerService } from 'ngx-spinner';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  displayStyle: any;
  userName: any;
  mobileNo: any;
  email: any;
  bookingId: any;
  userId: string;
  bookingList: any = [];
  AllVisitedHomestayList: any = [];
  feedDetail: any = [];
  listFilter: any;
  p: number = 1;
  feedbackhide: any = false;
  lodgeCount: any;
  cancelbooking: any = false;
  hideaction: any = false;
  FeedbackDetails: any;
  canceldetail: any;
  cancelForm: FormGroup;
  submitted: any = false;
  PackageCount: any;
  routeSub: any = {};
  constructor(private apicall: GlobalsService,private spinner: NgxSpinnerService, private feedbackService: FeedbackService, private bookingService: BookingService, private router: Router, private globalservice: GlobalsService, private formBuilder: FormBuilder, private userProfileService: UserProfileService) { }

  ngOnInit() {
   
    this.cancelForm = this.formBuilder.group({
      BookingId:[''],
      Bname: ['', Validators.required],
      BranchName: ['', [Validators.required]],
      AccNo: ['', [Validators.required]],
      AccType: ['', Validators.required],
      ifsc: ['', Validators.required],
      CancelReason: ['', Validators.required],
    })
    this.cancelbooking = true;
    this.setUserProfile();
    let data = {
      userid: sessionStorage.getItem('userId'),
      filter: "Upcoming"
    }
    this.userProfileService.GeVisitedHomestayList(data).subscribe(resp => {
      this.AllVisitedHomestayList = resp.data;
      console.log("List", this.AllVisitedHomestayList);
    })
    let datacount = {
      userid: sessionStorage.getItem('userId'),
      filter: "ALL"
    }
    this.userProfileService.GeVisitedHomestayCount(datacount).subscribe(resp => {
      this.lodgeCount = resp.data;
    })
    let datacountforpkg = {
      userid: sessionStorage.getItem('userId'),
      filter: "ALL"
    }
    this.userProfileService.GePackageCount(datacountforpkg).subscribe(resp => {
      this.PackageCount = resp.data;
    })
    //this.userProfileService.GeVisitedHomestayList(sessionStorage.getItem('userId')).subscribe(resp => {
    //  this.AllVisitedHomestayList = resp.data;
    //  console.log("All visitedList", this.AllVisitedHomestayList)
    //});
    //this.GetBookingHistory(profileId: any);
  }
  get f() { return this.cancelForm.controls; }
  cancel(value:any)
  {
   
    this.submitted = true;
    this.cancelForm.markAllAsTouched();
    const pre = this.cancelForm.value as Cancelmodel;
    pre.BookingId = value;
    if (this.cancelForm.invalid) {
      return;
    }
    Swal.fire({
      title: 'Are You Sure Want To Cancel The Booking ?',
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: 'Yes',
      denyButtonText: 'No',
      customClass: {
        actions: 'my-actions',
        cancelButton: 'order-1 right-gap',
        confirmButton: 'order-2',
        denyButton: 'order-3',
      }
    }).then((result) => {
      if (result.isConfirmed) {
        this.spinner.show();
        this.userProfileService.CancelBk(pre).subscribe(
          resp => {
            this.globalservice.showMessage(resp.msg, resp.result);
            if (resp.msg == "Success") {
              this.spinner.hide();
              this.apicall.swalSuccess("Your Booking Is Cancelled");
              window.location.reload();
            }
            else {
              this.spinner.hide();
              this.apicall.swalSuccess("Something Went Wrong");
            }
            
          });
      } else if (result.isDenied) {
        Swal.fire('Changes are not saved', '', 'info')
      }
    })
  
   
  }
  setUserProfile() {
    this.userName = sessionStorage.getItem('userFullNm');
    this.mobileNo = sessionStorage.getItem('userAccDesc');
    this.email = sessionStorage.getItem('email');
    this.userId = sessionStorage.getItem('userId');
    if (this.userName == null) {
      this.router.navigate(['login']);
    }
  }
  onRadioChange(value: any) {
    if (value == "Availed" ) {
      this.feedbackhide = false;
      this.cancelbooking = false;
      this.hideaction = false;
    }
    if (value == "Upcoming" || value == "Cancelled") {
      if (value == "Upcoming") {
        this.cancelbooking = true;
        this.hideaction = false;
      }
      else {
        this.feedbackhide = false;
        this.cancelbooking = false;
        this.hideaction = true;
      }     
    }
    let data = {
      userid: sessionStorage.getItem('userId'),
      filter:value
    }
    this.userProfileService.GeVisitedHomestayList(data).subscribe(resp => {
      this.AllVisitedHomestayList = resp.data;
      console.log(this.AllVisitedHomestayList)
    })
  }
  getFeedbackdetail() {
    this.userProfileService.GetFeedBackById(sessionStorage.getItem('userId')).subscribe(resp => {
      this.AllVisitedHomestayList = resp.data;
    });
  }
  Feedback(value:any) {
    this.router.navigate(['/Feedback', { id: value }]);
  }
  logout() {
    this.bookingService.Logout();
  }
  searchHomestay() {
    this.router.navigate(['Gu-Search']);
  }
  ViewFeedback(bookingId: any) {
    this.userProfileService.Feedbackdetails(bookingId).subscribe(resp => {
      this.FeedbackDetails = resp.data;
    })
  }
  CancelBookingDetail(value:any)
  {
    this.userProfileService.CancelBooking(value).subscribe(resp => {
      this.canceldetail = resp.data;
      this.bookingId = resp.data.hsBookingId;
      console.log("cancel detail", this.canceldetail)
    });
  }
  BookPackage() {
    this.router.navigate(['packagetour-booking'], { queryParams: { user: "user" } });
  }
}
