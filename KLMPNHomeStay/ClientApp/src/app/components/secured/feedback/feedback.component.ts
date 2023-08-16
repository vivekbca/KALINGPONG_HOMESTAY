
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FeedbackService } from '../../../services/secured/feedback.service';
import { Router, ActivatedRoute } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { Feedback } from '../../../model/secured/feedback';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
  
})
export class FeedbackComponent implements OnInit {
  feedbackForm: FormGroup;
  Viewdetail: any = [];
  constructor(private route: ActivatedRoute,private router: Router, private apicall: GlobalsService, private formBuilder: FormBuilder, private feedbackService: FeedbackService, private globalService: GlobalsService) { }
  HsBookingId: string;
  currentRate = 0;
  ngOnInit(): void {
    this.feedbackForm = this.formBuilder.group({
      
      HsBookingId: [''],
      HsRatings: ['', Validators.required],
      HsFeedback: ['']
    })
    
    this.route.paramMap.subscribe((params) => {
      this.HsBookingId = params.get('id');
    });
    this.feedbackService.viewDetail(this.HsBookingId).subscribe(resp => {
      this.Viewdetail = resp.data;
      console.log("details", this.Viewdetail)
    });


  }
  get f() { return this.feedbackForm.controls; }
  initForm() {
    this.feedbackForm = this.formBuilder.group({
      HsBookingId: [''],
      HsFeedback: ['', Validators.required],
      HsRatings: ['']

    });
  }

  back() {
    this.router.navigate(['user-profile']);
  }
  addFeedback(value:any) {

    //this.feedbackForm.markAllAsTouched();
    const pre = this.feedbackForm.value as Feedback;
    // stop here if form is invalid
    if (this.feedbackForm.invalid) {
      return;
    }
    pre.HsBookingId = this.Viewdetail.bookingId;


    this.feedbackService.addFeedback(pre).subscribe(
      resp => {
        this.globalService.showMessage(resp.msg, resp.result);
        console.log(resp.msg)
        if (resp.msg == 'saved') {
          this.apicall.swalError("some error occured");
        }
        else {
          this.apicall.swalSuccess("FeedBack Added Successfully");
          this.router.navigate(['user-profile']);
        }



      });
  }
  stars: number[] = [1, 2, 3, 4, 5];
  selectedValue: number;

  
  countStar(star) {
    this.selectedValue = star;
    this.feedbackForm.controls['HsRatings'].setValue(star);
    console.log('Value of star', star);
  }

  

}

