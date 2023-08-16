import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { GuestregistrationDTO } from '../../../model/unsecured/GuestRegistration/guestregistration-dto';
import { ConfirmedValidator } from '../../../services/common/confirmed.validator';
import { GlobalsService } from '../../../services/common/globals.service';
import { GuestregistrationService } from '../../../services/unsecured/GuestRegistration/guestregistration.service';
/*import { MustMatch } from './_helpers/must-match.validator';*/
@Component({
  selector: 'app-guestregistration',
  templateUrl: './guestregistration.component.html',
  styleUrls: ['./guestregistration.component.css']
})
export class GuestregistrationComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  countries: any;
  states: any;
  repassworderrmsg: any;
  captchacode: any;
  HideIdNum: any=true;
  constructor(private spinner: NgxSpinnerService,private router: Router, private apicall: GlobalsService, private formBuilder: FormBuilder, private guestregistrationservice: GuestregistrationService, private globalservice: GlobalsService) { }

  ngOnInit() {
    this.captchacode = "GR" + Math.floor(Math.random() * 899999 + 100000);
    this.registerForm = this.formBuilder.group({ 
      GuName: ['', Validators.required],
      GuMobileNo: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
      GuEmailId: ['', [Validators.required, Validators.email]],
      GuAddress1: ['', Validators.required],
      GuAddress2: [''],
      GuAddress3: [''],
      GuState: ['', Validators.required],
      GuCountry: ['', Validators.required],
      GuSex: ['', Validators.required],
      GuDob: ['', [Validators.required, Validators.pattern(/^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$/)]],     
      GuPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
      repassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
      GuIdentityProof: ['', Validators.required],
      GuPincode: ['',Validators.required],
      GuIdentityNo: ['', Validators.required],
      GuCity: ['', Validators.required],
      Captcha: ['', Validators.required]
      /*recaptcha: ['', Validators.required]*/
    })
    
    this.guestregistrationservice.GetAllCountry().subscribe(response => {
      this.countries = response.data;
      console.log(response.data)
    });
  }
  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }

  reCaptha() {
    this.captchacode = "GR" + Math.floor(Math.random() * 899999 + 100000);
  }
  onSubmit() {
    this.submitted = true;
    this.registerForm.markAllAsTouched();
    const pre = this.registerForm.value as GuestregistrationDTO;
    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }
    if (pre.GuPassword != pre.repassword) {
      this.apicall.swalError("Password And Confirm Password Not matched.");
      return;
    }
    if (pre.Captcha != this.captchacode) {
      this.apicall.swalError("Captcha Not matched.");
      return;
    }
    this.spinner.show();
    pre.GuId = '';
    this.guestregistrationservice.InsertProfile(pre).subscribe(
        resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        console.log(resp.msg)
        if (resp.msg == 'Email ID Already Exist!!') {
          this.apicall.swalError("Email ID Already Exist!!");
          this.spinner.hide();
        }
        else {
          this.apicall.swalSuccess("Registration Successfull");
          this.spinner.hide();
          this.router.navigate(['']);
        }                     
        });
  }

  onChangeProof(value: any) {
    if (value == "AADHAAR" || value == "PAN" || value == "DRIVING LISCENCE" || value =="VOTER ID") {
      this.HideIdNum = false;
    }
    else {
      this.HideIdNum = true;
    }
  }

  onChangeCountry(value: any) {
    this.guestregistrationservice.GetAllState(value).subscribe(response => {
      this.states = response.data;
      console.log(response.data)
    });
  }
  login() {
    this.router.navigate(['login']);
  }
}
