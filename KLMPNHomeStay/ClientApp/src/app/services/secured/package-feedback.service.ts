import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';

@Injectable({
  providedIn: 'root'
})
export class PackageFeedbackService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  GetVisitedPackageList(data: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'packageFeedBack/Packagelist', data, { headers: reqHeader });
  }
  CancelBooking(bookingId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'packageFeedBack/cancelDetails/' + bookingId, { headers: reqHeader });
  }
  CancelBk(cancelModel: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'packageFeedBack/cancelBooking', cancelModel, { headers: reqHeader });
  }
  viewDetail(bid: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'packageFeedBack/viewDetail/' + bid, { headers: reqHeader });
  }
  addFeedback(feedbackModel: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'packageFeedBack/giveFeedback', feedbackModel, { headers: reqHeader });
  }
  Feedbackdetails(bookingId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'packageFeedBack/feedbackDetails/' + bookingId, { headers: reqHeader });
  }
}
