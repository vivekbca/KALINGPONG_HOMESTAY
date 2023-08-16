import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FeedbackService } from '../../../services/secured/feedback.service';
import { PackageFeedbackService } from '../../../services/secured/package-feedback.service';

@Component({
  selector: 'app-tour-feedback-add',
  templateUrl: './tour-feedback-add.component.html',
  styleUrls: ['./tour-feedback-add.component.css']
})
export class TourFeedbackAddComponent implements OnInit {
  tourBookingId: any;
  Viewdetail: any = [];
  feedbackForm: FormGroup;
  constructor(private route: ActivatedRoute, private router: Router, private apicall: GlobalsService, private formBuilder: FormBuilder, private packageFeedbackService: PackageFeedbackService, private globalService: GlobalsService) { }

  ngOnInit() {
    this.initForm();
    this.route.paramMap.subscribe((params) => {
      this.tourBookingId = params.get('id');
    });
    this.packageFeedbackService.viewDetail(this.tourBookingId).subscribe(resp => {
      this.Viewdetail = resp.data;
      console.log("details", this.Viewdetail)
    });
  }
  initForm() {
    this.feedbackForm = this.formBuilder.group({
      tourBookingId: [''],
      tourFeedback: ['', Validators.required],
      tourRatings: ['']
    });
  }
  get f() { return this.feedbackForm.controls; }

  stars: number[] = [1, 2, 3, 4, 5];
  selectedValue: number;


  countStar(star) {
    this.selectedValue = star;
    this.feedbackForm.controls['tourRatings'].setValue(star);
    console.log('Value of star', star);
  }
  back() {
    this.router.navigate(['package-feedback']);
  }
  addFeedback(value: any) {
    //alert(value);
    this.feedbackForm.markAllAsTouched();
    //const pre = this.feedbackForm.value as Feedback;
    //// stop here if form is invalid
    if (this.feedbackForm.invalid) {
      return;
    }
    let tourFeedObj: any = {};
    tourFeedObj.tourBookingId = this.Viewdetail.tourBookingId;
    tourFeedObj.tourFeedback = this.f.tourFeedback.value;
    tourFeedObj.tourRatings = this.f.tourRatings.value;
    this.packageFeedbackService.addFeedback(tourFeedObj).subscribe(
      resp => {
        this.globalService.showMessage(resp.msg, resp.result);
        console.log(resp.msg)
        if (resp.msg == 'saved') {
          this.apicall.swalError("some error occured");
        }
        else {
          this.apicall.swalSuccess("FeedBack Added Successfully");
          this.router.navigate(['package-feedback']);
        }
      });
  }
}
