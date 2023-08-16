import { Injectable } from '@angular/core';
import { GlobalsService } from '../../common/globals.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiResponse } from '../../../model/common/api-response';

@Injectable({
  providedIn: 'root'
})
export class AdminLoginService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private router: Router, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  LoginOn(loginComObj: any) {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'userLogin', loginComObj, { headers: reqHeader });
  }
  forgotPassword(forgotPassComObj: any) {
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'forgotPassword/userForgotPassword', forgotPassComObj);
  }
  resetPassword(resetPasswordObj: any) {
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'forgotPassword/userResetPassword', resetPasswordObj);
  }
}
