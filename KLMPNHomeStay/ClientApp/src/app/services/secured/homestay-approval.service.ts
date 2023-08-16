import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GlobalsService } from '../common/globals.service';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../../model/common/api-response';

@Injectable({
  providedIn: 'root'
})
export class HomestayApprovalService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  getAllHomeStay(): Observable<ApiResponse> {
    //const queryStr = this.globalService.getSearchQueryString();
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'homestayApproval/hsApprovalList');
  }
  ApproveRejectHS(hsCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'homestayApproval/approveHS', hsCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
  GetHSById(hsId: string): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'homeStay/' + hsId);
  }
  GetHSByIdNew(hsId: string): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'homestayApproval/hsListById/' + hsId);
  }
}
