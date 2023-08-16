import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminLoginService } from '../../../services/unsecured/adminLogin/admin-login.service';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent implements OnInit {
  loginForm: FormGroup;
  forgotPasswordForm: FormGroup;
  submitted = false;
  

  constructor(private formBuilder: FormBuilder, private spinner: NgxSpinnerService, private activatedRoute: ActivatedRoute, private router: Router, private globalservice: GlobalsService, private adminLoginService: AdminLoginService) { }

  ngOnInit() {
    this.initForm();
    this.buildForm();
  }
  get f() { return this.loginForm.controls; }
  get f1() { return this.forgotPasswordForm.controls; }

  initForm() {
    this.loginForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
    });
  }
  private buildForm() {
    this.forgotPasswordForm = this.formBuilder.group({
      forgotEmail: ['', [Validators.required]],
    });
  }
  onLogin() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    else {
      let loginComObj: any = {};
      loginComObj.UserName = this.f.userName.value;
      loginComObj.Password = this.f.password.value;
      this.spinner.show();

      this.adminLoginService.LoginOn(loginComObj).subscribe(
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
            const roleName = resp.data.roleName as string;
            window.sessionStorage.setItem('userToken', access_token);
            window.sessionStorage.setItem('userAccDesc', acc_desc);
            window.sessionStorage.setItem('userFullNm', full_nm);
            window.sessionStorage.setItem('userMenu', menu);
            window.sessionStorage.setItem('email', email);
            window.sessionStorage.setItem('userId', userId);
            window.sessionStorage.setItem('roleName', roleName);
            this.router.navigate(['admin-dashboard']);
          }
          if (resp.result === 'Info') {
            this.spinner.hide();
            this.submitted = false;
            this.globalservice.swalError(resp.msg);
          }
        });
    }
  }
  submit() {
    this.submitted = true;
    this.spinner.show();
    this.forgotPasswordForm.markAllAsTouched();
    let forgotPasswordComObj: any = {};
    forgotPasswordComObj.emailId = this.f1.forgotEmail.value;
    this.adminLoginService.forgotPassword(forgotPasswordComObj).subscribe(resp => {
      this.globalservice.showMessage(resp.msg, resp.result);
      if (resp.result == "Success") {
        this.submitted = false;
        this.spinner.hide();
        this.globalservice.swalSuccess("Please check your register Email Id");
        window.location.reload();
      }
      else {
        this.spinner.show();
        this.globalservice.swalError("Something Went Wrong");
      }
    });
  }
}
