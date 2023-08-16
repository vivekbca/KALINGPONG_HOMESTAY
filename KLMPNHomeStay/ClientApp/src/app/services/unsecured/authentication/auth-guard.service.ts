import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiResponse } from '../../../model/common/api-response';
import { Observable } from 'rxjs';
import { MenuModel } from '../../../model/secured/common/menu-model';
import { GlobalsService } from '../../common/globals.service';
import { Router } from '@angular/router';
import { RoutePermissionModel } from '../../../model/secured/common/route-permission-model';
import { LoginDTO } from '../../../model/unsecured/authentication/login-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {

  ApiUrl = '';

  constructor
    (
      private httpclient: HttpClient,
      private globalService: GlobalsService,
      private router: Router

    ) { this.ApiUrl = globalService.getApiUrl(); }

  getToken() {
    return !!window.sessionStorage.getItem("userToken");
  }

  UserAuthentication(loginModel: LoginDTO): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'gULogin', loginModel, { headers: reqHeader });
  }
  UserAuthenticationForBankUser(loginModel: LoginDTO): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'bankUserLogin', loginModel, { headers: reqHeader });
  }
  Logout() {
    window.sessionStorage.removeItem('userToken');
    window.sessionStorage.removeItem('userAccDesc');
    window.sessionStorage.removeItem('userFullNm');
    window.sessionStorage.removeItem('userMenu');
    window.sessionStorage.removeItem('menuCollapsed');

    window.location.href = window.location.origin + '/login';
    this.globalService.showMessage("You are logged out successfully", "Success");
  }
  getUsrToken() {
    return window.sessionStorage.getItem("userToken");
  }
  geUsrAccDesc() {
    return window.sessionStorage.getItem("userAccDesc");
  }
  getUsrFullNm() {
    return window.sessionStorage.getItem("userFullNm");
  }

  getUsrMenu(): MenuModel[] {
    const menuStr = window.sessionStorage.getItem("userMenu");
    const menuArr = JSON.parse(menuStr) as MenuModel[];
    return menuArr;
  }

  getRoutePermissions(): RoutePermissionModel {
    const menuArr = this.getUsrMenu();
    const permission = { hasViewRights: false, hasModificationRights: false } as RoutePermissionModel;
    const testUrl = '.' + this.router.url;
    let currentPerm = menuArr.find(t => t.url === testUrl);

    if (currentPerm) {
      if (currentPerm.permission === 'A') {
        permission.hasViewRights = true;
        permission.hasModificationRights = true;
      }
      else if (currentPerm.permission === 'V') {
        permission.hasViewRights = true;
      }
    }
    return permission;
  }

  GetLoggedInUserData(): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'userLogin/UserLoginCheck');
  }

  forgotPassword(forgotPassComObj: any) {
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'forgotPassword/guestForgotPassword', forgotPassComObj);
  }
  resetPassword(resetPasswordObj: any) {
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'forgotPassword/resetPassword', resetPasswordObj);
  }
}
