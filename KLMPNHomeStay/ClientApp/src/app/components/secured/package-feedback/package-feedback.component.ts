import { Component, OnInit } from '@angular/core';
import { GlobalsService } from '../../../services/common/globals.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { PackageFeedbackService } from '../../../services/secured/package-feedback.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-package-feedback',
  templateUrl: './package-feedback.component.html',
  styleUrls: ['./package-feedback.component.css']
})
export class PackageFeedbackComponent implements OnInit {
  feedbackhide: any = false;
  lodgeCount: any;
  cancelbooking: any = false;
  hideaction: any = false;
  canceldetail: any = [];
  bookingId: any;
  userId: any;
  cancelForm: FormGroup;
  submitted: any = false;
  AllVisitedPackageList: any = [];
  feedbackDetails: any = [];
  tourName: any;
  constructor(private apicall: GlobalsService, private router: Router, private spinner: NgxSpinnerService, private formBuilder: FormBuilder, private packageFeedbackService: PackageFeedbackService, private globalService: GlobalsService) { }

  ngOnInit() {
    this.initForm();
    this.userId = sessionStorage.getItem('userId');
    this.cancelbooking = true;
    let data = {
      userid: sessionStorage.getItem('userId'),
      filter: "Upcoming"
    }
    this.packageFeedbackService.GetVisitedPackageList(data).subscribe(resp => {
      this.AllVisitedPackageList = resp.data;
      console.log("List", this.AllVisitedPackageList);
    })
  }
  get f() { return this.cancelForm.controls; }

  initForm() {
    this.cancelForm = this.formBuilder.group({
      BookingId: [''],
      Bname: ['', Validators.required],
      BranchName: ['', [Validators.required]],
      AccNo: ['', [Validators.required]],
      AccType: ['', Validators.required],
      Ifsc: ['', Validators.required],
      CancelReason: ['', Validators.required],
    })
  }
      
  onRadioChange(value: any) {
    if (value == "Availed") {
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
      filter: value
    }
    this.packageFeedbackService.GetVisitedPackageList(data).subscribe(resp => {
      this.AllVisitedPackageList = resp.data;
    })
  }
  ViewFeedback(bookingId: any) {
    this.packageFeedbackService.Feedbackdetails(bookingId).subscribe(resp => {
      this.feedbackDetails = resp.data;
      //this.tourName = this.feedbackDetails.tourName;
      //console.log(this.tourName);
    });
  }
  Feedback(value: any) {
    this.router.navigate(['/package-addFeedback', { id: value }]);
  }
  CancelBookingDetail(value: any) {
    //alert(value);
    this.packageFeedbackService.CancelBooking(value).subscribe(resp => {
      this.canceldetail = resp.data;
      //this.bookingId = resp.data.tourBookingId;
      console.log("cancel detail", this.canceldetail)
    });
  }
  cancel(bookingId: any) {

    this.submitted = true;
    this.cancelForm.markAllAsTouched();
    ////const pre = this.cancelForm.value as Cancelmodel;
    //pre.BookingId = value;
    let cancelModel: any = {};
    cancelModel.bookingId = bookingId;
    cancelModel.bankName = this.f.Bname.value;
    cancelModel.branchName = this.f.BranchName.value;
    cancelModel.accNo = this.f.AccNo.value;
    cancelModel.accType = this.f.AccType.value;
    cancelModel.ifsc = this.f.Ifsc.value;
    cancelModel.cancelReason = this.f.CancelReason.value;
    cancelModel.userId = this.userId;
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
        this.packageFeedbackService.CancelBk(cancelModel).subscribe(
          resp => {
            this.globalService.showMessage(resp.msg, resp.result);
            if (resp.msg == "Success") {
              this.spinner.hide();
              this.apicall.swalSuccess("Your Booking Is Cancelled");
              window.location.reload();
            }
            else {
              this.spinner.hide();
              this.apicall.swalSuccess("Something Went Wrong");
            }
            this.spinner.hide();
          });
      } else if (result.isDenied) {
        Swal.fire('Changes are not saved', '', 'info')
      }
    })
   

  }
  back() {
    this.router.navigate(['user-profile']);
  }
}
