import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';
import { Cancelmodel } from '../../model/secured/cancelmodel';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  GetUserProfileById(profileId: string): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'user-profile/' + profileId);
  }
  
  GeVisitedHomestayList(data: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'FeedBack/HomeStaylist' , data, { headers: reqHeader });
  }
  GeVisitedHomestayCount(data: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'FeedBack/HomeStaylistCount', data, { headers: reqHeader });
  }
  GePackageCount(data: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'FeedBack/PackagelistCount', data, { headers: reqHeader });
  }
  GetFeedBackById(id: string): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'Feedback/GetFeedBackById/' + id);
  }
  Feedbackdetails(bookingId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'Feedback/feedbackDetails/' + bookingId, { headers: reqHeader });
  }
  CancelBooking(bookingId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'Feedback/cancelDetails/' + bookingId, { headers: reqHeader });
  }
  CancelBk(data: Cancelmodel): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'Booking/cancelBooking', JSON.stringify(data), { headers: reqHeader });
  }
}
