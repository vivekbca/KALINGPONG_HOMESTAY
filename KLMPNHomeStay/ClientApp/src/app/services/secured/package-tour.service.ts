import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';

@Injectable({
  providedIn: 'root'
})
export class PackageTourService {
  ApiUrl = '';
  constructor(private httpClient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  getAllTour(): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packageTourBooking/getTourList');
  }
  getTourById(tourId: any): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packageTourBooking/' + tourId);
  }
  viewTourFile(tourId: string) {
    let headers = new HttpHeaders({ "Content-Type": "application/pdf" });
    let options = { headers: headers };
    return this.httpClient.get(this.ApiUrl + 'packageTourBooking/tourFile/' + tourId, { headers: new HttpHeaders({ 'Content-Type': 'application/pdf' }), responseType: 'blob' });
  }
  getTourDateById(tourId: any): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packageTourBooking/getTourDateList/' + tourId);
  }
  getTourDateByIdforadmin(tourId: any): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packageTourBooking/getTourDateListforadmin/' + tourId);
  }
  bookingDate(tourDateCompObj: any) {
    return this.httpClient.post<ApiResponse>(this.ApiUrl + 'packageTourBooking/dateTourBooking', tourDateCompObj);
  }
  totalCost(costCompObj: any) {
    return this.httpClient.post<ApiResponse>(this.ApiUrl + 'packageTourBooking/totalCost', costCompObj);
  }
  proceedPay(payCompObj: any) {
    return this.httpClient.post<ApiResponse>(this.ApiUrl + 'packageTourBooking/proceedPayment', payCompObj);
  }
  getAllPaymentApprovalTour(): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packagePayment/getPaymentPackageList');
  }
  getPaymentApprovalTourDetail(bookingId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packagePayment/getPaymentApprovalTourDetail/' + bookingId, { headers: reqHeader });
  }
  approveBookingPayment(packageComObj: any) {
    return this.httpClient.post<ApiResponse>(this.ApiUrl + 'packagePayment/approveBookingPayment', packageComObj);
  }
  getAllPaymentApprovalTourBank(): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packagePayment/getAllPaymentApprovalTourBank');
  }
  approveBookingPaymentBank(packageComObj: any) {
    return this.httpClient.post<ApiResponse>(this.ApiUrl + 'packagePayment/approveBookingPaymentBank', packageComObj);
  }
  //getAllRefundTourAdmin(): Observable<ApiResponse> {
  //  return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packageRefund/getRefundPackageList');
  //}
  getAllRefundTourAdmin(data: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpClient.post<ApiResponse>(this.ApiUrl + 'packageRefund/getRefundPackageList', data, { headers: reqHeader });
  }
  getRefundTourDetail(bookingId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packageRefund/getRefundTourDetail/' + bookingId, { headers: reqHeader });
  }
  refundBooking(packageComObj: any) {
    return this.httpClient.post<ApiResponse>(this.ApiUrl + 'packageRefund/refundTourBooking', packageComObj);
  }
  //getAllRefundTourBank(): Observable<ApiResponse> {
  //  return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packageRefund/getAllRefundTourBank');
  //}
  getAllRefundTourBank(data: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpClient.post<ApiResponse>(this.ApiUrl + 'packageRefund/getAllRefundTourBank', data, { headers: reqHeader });
  }
  refundBookingBank(packageComObj: any) {
    return this.httpClient.post<ApiResponse>(this.ApiUrl + 'packageRefund/refundTourBookingBank', packageComObj);
  }
}
