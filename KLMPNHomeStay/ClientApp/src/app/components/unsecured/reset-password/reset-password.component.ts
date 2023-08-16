import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { GlobalsService } from '../../../services/common/globals.service';
import { AuthGuardService } from '../../../services/unsecured/authentication/auth-guard.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  token: string;
  routeSub: any;
  resetForm: FormGroup;
  submitted = false;
  error_messages = {
    'password': [
      { type: 'required', message: 'password is required.' },
      { type: 'minlength', message: 'password length minimum 6 character.' },
      { type: 'maxlength', message: 'password length maximum 30 character.' }
    ],
    'confirmpassword': [
      { type: 'required', message: 'password is required.' },
      { type: 'minlength', message: 'password length minimum 6 character.' },
      { type: 'maxlength', message: 'password length maximum 30 character.' }
    ],
  }
  constructor(private route: ActivatedRoute, private router: Router, private authguardService: AuthGuardService, private globalservice: GlobalsService, private formBuilder: FormBuilder, private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.routeSub = this.route.queryParams.subscribe(params => {
      if (params.token && params.hasOwnProperty('token')) {
        this.token = params.token;
      }
    });
    this.initForm();
  }
  get f() { return this.resetForm.controls; }

  private initForm() {
    this.resetForm = this.formBuilder.group({
      password: ['', [Validators.required]],
      confirmpassword: ['', [Validators.required]],
    });
    //this.resetForm = this.formBuilder.group({
    //  //password: ['', [Validators.required]],
    //  //confirmpassword: ['', [Validators.required]],this.loginForm = this.formBuilder.group({
    
    //  password: new FormControl('', Validators.compose([
    //    Validators.required,
    //    Validators.minLength(6),
    //    Validators.maxLength(30)
    //  ])),
    //  confirmpassword: new FormControl('', Validators.compose([
    //    Validators.required,
    //    Validators.minLength(6),
    //    Validators.maxLength(30)
    //  ])),
    //}, {
    //  validators: this.password.bind(this)
    //});
    //this.resetForm = this.formBuilder.group({
    //  password: ['', [Validators.required, Validators.minLength(6)]],
    //  confirmpassword: ['', [Validators.required, Validators.minLength(6)]],
    //},
    //  { validator: this.passwordMatchValidator },
    //);
  }
  //passwordMatchValidator(frm: FormGroup) {
  //  return frm.controls['password'].value ===
  //    frm.controls['confirmpassword'].value ? null : { 'mismatch': true };
  //}
 

  submit() {
    this.submitted = true;
    let resetPasswordObj: any = {};
    resetPasswordObj.password = this.f.password.value;
    resetPasswordObj.confirmpassword = this.f.confirmpassword.value;
    resetPasswordObj.token = this.token;
    if (resetPasswordObj.password != resetPasswordObj.confirmpassword) {
      this.spinner.hide();
      this.globalservice.swalError("Password not matched");
    }
    else {
      this.authguardService.resetPassword(resetPasswordObj).subscribe(resp => {
        this.spinner.show();
        this.globalservice.showMessage(resp.msg, resp.result);
        if (resp.result == "Success") {
          this.spinner.hide();
          this.submitted = false;
          this.globalservice.swalSuccess("Password change sucessfully");
          window.location.reload();
        }
        else {
          this.spinner.hide();
          this.globalservice.swalError(resp.msg);
        }
      });
    }
  }

  password(formGroup: FormGroup) {
    const { value: password } = formGroup.get('password');
    const { value: confirmPassword } = formGroup.get('confirmpassword');
    return password === confirmPassword ? "" : { passwordNotMatch: true };
  }
  //check() {
  //  alert("fdfds");
  //}
}
