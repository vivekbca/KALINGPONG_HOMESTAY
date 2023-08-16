import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminLoginService } from '../../../services/unsecured/adminLogin/admin-login.service';

@Component({
  selector: 'app-reset-password-user',
  templateUrl: './reset-password-user.component.html',
  styleUrls: ['./reset-password-user.component.css']
})
export class ResetPasswordUserComponent implements OnInit {
  token: string;
  routeSub: any;
  resetForm: FormGroup;
  submitted = false;

  constructor(private route: ActivatedRoute, private router: Router, private globalservice: GlobalsService, private adminLoginService: AdminLoginService, private formBuilder: FormBuilder, private spinner: NgxSpinnerService) { }

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
   
  }
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
      this.adminLoginService.resetPassword(resetPasswordObj).subscribe(resp => {
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
}
