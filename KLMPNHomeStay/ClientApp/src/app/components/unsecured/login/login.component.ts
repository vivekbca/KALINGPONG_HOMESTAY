import { Component, OnInit } from '@angular/core';
import { AuthGuardService } from '../../../services/unsecured/authentication/auth-guard.service';
import { LoginDTO } from '../../../model/unsecured/authentication/login-dto';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ValidationService } from 'src/app/services/common/validation.service';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/model/common/api-response';
import { GlobalsService } from '../../../services/common/globals.service';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  LoginForm: FormGroup;
  forgotPasswordForm: FormGroup;
  submitted = false;
  constructor(private authguardService: AuthGuardService, private router: Router,
    private globalservice: GlobalsService, private formBuilder: FormBuilder, private spinner: NgxSpinnerService) { }

  /*to set the form validation*/
  private buildForm() {
    this.LoginForm = this.formBuilder.group({
      UserName: ['', [Validators.required]],
      Password: ['', [Validators.required]],
    });
  }
  
  private initForm() {
    this.forgotPasswordForm = this.formBuilder.group({
      forgotEmail: ['', [Validators.required]],
    });
  }
  ngOnInit() {
    this.buildForm();
    this.initForm();
    if (this.authguardService.getToken()) {
      this.router.navigate(['home']);
    }
  }
  get f() { return this.LoginForm.controls; }
  get f1() { return this.forgotPasswordForm.controls; }
  OnGetToken() {
    this.submitted = true;
    this.LoginForm.markAllAsTouched();
    if (this.LoginForm.invalid) {
      return;
    }
    if (this.LoginForm.valid) {
      const loginModel = this.LoginForm.value as LoginDTO;
      this.spinner.show();
      this.authguardService.UserAuthentication(loginModel).subscribe(resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        if (resp.result === 'Success') {
          this.globalservice.swalSuccess("Login Success");

         

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
          this.spinner.hide();
          this.router.navigate(['user-profile']);
        }
        else {
          this.globalservice.swalError("Invalid Credential");
          this.spinner.hide();
        }

        /*
        this.authguardService.GetLoggedInUserData().subscribe((userInfoData: ApiResponse) => {
          if (userInfoData.result === 'Success') {
            let InfoData: any = userInfoData.data as any;
            window.sessionStorage.setItem('userFullName', InfoData.FullName);
          }
          this.router.navigate(['./home']);

        });
        */
      });
    }

  }
  submit() {
    this.submitted = true;
    this.spinner.show();
    this.forgotPasswordForm.markAllAsTouched();
    let forgotPasswordComObj: any = {};
    forgotPasswordComObj.emailId = this.f1.forgotEmail.value;
    this.authguardService.forgotPassword(forgotPasswordComObj).subscribe(resp => {
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
