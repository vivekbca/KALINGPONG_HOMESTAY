

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';
import { Feedback } from '../../model/secured/feedback';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  getAllFeedback(): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'feedback/FeedBackListList');
  }
  addFeedback(feedback: Feedback): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'Feedback/', JSON.stringify(feedback), { headers: reqHeader });
  } 
  viewDetail(bid: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'FeedBack/viewDetail/' + bid, { headers: reqHeader });
  }
}
