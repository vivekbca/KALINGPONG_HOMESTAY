import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { HsLoginService } from '../../../services/unsecured/hsLogin/hs-login.service';
import { AdminLoginService } from '../../../services/unsecured/adminLogin/admin-login.service';

@Component({
  selector: 'app-hs-login',
  templateUrl: './hs-login.component.html',
  styleUrls: ['./hs-login.component.css']
})
export class HsLoginComponent implements OnInit {
  forgotPasswordForm: FormGroup;
  loginForm: FormGroup;
  submitted = false;


  constructor(private formBuilder: FormBuilder, private spinner: NgxSpinnerService, private activatedRoute: ActivatedRoute, private router: Router, private globalservice: GlobalsService, private hsLoginService: HsLoginService, private adminLoginService: AdminLoginService) { }

  ngOnInit() {
    this.initForm();
    this.buildForm();
  }
  get f() { return this.loginForm.controls; }
  get f1() { return this.forgotPasswordForm.controls; }

  private buildForm() {
    this.forgotPasswordForm = this.formBuilder.group({
      forgotEmail: ['', [Validators.required]],
    });
  }
  initForm() {
    this.loginForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
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

      this.hsLoginService.LoginOn(loginComObj).subscribe(
        resp => {
          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.result === 'Success') {
            console.log(resp)
            this.spinner.hide();
            this.submitted = false;
            const access_token = resp.data.token as string;
            const acc_desc = resp.data.accountDescription as string;
            const full_nm = resp.data.fullName as string;
            const menu = JSON.stringify(resp.data.menu) as string;
            const email = resp.data.email as string;
            const userId = resp.data.userId as string;

            window.sessionStorage.setItem('userToken', access_token);
            //window.sessionStorage.setItem('userAccDesc', acc_desc);
            window.sessionStorage.setItem('userFullNm', full_nm);
            //window.sessionStorage.setItem('userMenu', menu);
            //window.sessionStorage.setItem('email', email);
            window.sessionStorage.setItem('userId', userId);

            //this.router.navigate(['hs-member-details']);
            this.router.navigate(['mamber-with-us']);
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
