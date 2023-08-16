import { Injectable } from '@angular/core';
import { ApiResponse } from '../../model/common/api-response';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private router: Router, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  CheckAvailability(checkCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'booking/checkAvailabilty', checkCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
  BookedRooms(bookCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'booking/bookedRooms', bookCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }

  DiscountRate(discountCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'booking/discountRate', discountCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
  AssignMember(memberComObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'booking/bookingDetail', memberComObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
  LoginOn(loginComObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'gULogin', loginComObj, { headers: reqHeader });
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }

  Logout() {
    window.sessionStorage.removeItem('userToken');
    window.sessionStorage.removeItem('userAccDesc');
    window.sessionStorage.removeItem('userFullNm');
    window.sessionStorage.removeItem('userMenu');
    window.sessionStorage.removeItem('menuCollapsed');
    window.sessionStorage.removeItem('userId');
    //window.location.href = window.location.origin + '/gULogin';
    this.globalService.swalSuccess("You are logged out successfully");
    this.router.navigate(['']);
  }
  //AdultRate(adultCompObj: any) {
  //  //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
  //  return this.httpclient.post<ApiResponse>(this.ApiUrl + 'booking/adultRate', adultCompObj);
  //  //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  //}
  CalculatePricing(priceDetailObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'booking/calculatePricing', priceDetailObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
  ProceedPayBook(payComObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'booking/proceedPayBook', payComObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
  BookingPossible(bookCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'booking/bookingPossible', bookCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
  getAllHSApprovalTour(): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'hSPayment/getPaymentHSList');
  }
  getPaymentApprovalHSDetail(bookingId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'hSPayment/getPaymentApprovalHSDetail/' + bookingId, { headers: reqHeader });
  }
  approveBookingPayment(hsComObj: any) {
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'hSPayment/approveBookingPayment', hsComObj);
  }
  getAllPaymentApprovalHSBank(): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'hSPayment/getAllPaymentApprovalHSBank');
  }
  approveBookingPaymentBank(packageComObj: any) {
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'hSPayment/approveBookingPaymentBank', packageComObj);
  }
  //getAllHSRefund(): Observable<ApiResponse> {
  //  return this.httpclient.get<ApiResponse>(this.ApiUrl + 'hSBookingRefund/getRefundHSList');
  //}
  getAllHSRefund(data: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'hSBookingRefund/getRefundHSList', data, { headers: reqHeader });
  }
  getPaymentRefundHSDetail(bookingId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'hSBookingRefund/getPaymentRefundHSDetail/' + bookingId, { headers: reqHeader });
  }
  refundBookingAdmin(hsComObj: any) {
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'hSBookingRefund/refundBookingAdmin', hsComObj);
  }
  //getAllHSRefundBank(): Observable<ApiResponse> {
  //  return this.httpclient.get<ApiResponse>(this.ApiUrl + 'hSBookingRefund/getAllHSRefundBankList');
  //}
  getAllHSRefundBank(data: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'hSBookingRefund/getAllHSRefundBankList', data, { headers: reqHeader });
  }
  refundBookingBank(hsComObj: any) {
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'hSBookingRefund/refundBookingBank', hsComObj);
  }
} 
