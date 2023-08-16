import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';

@Injectable({
  providedIn: 'root'
})
export class NoticeService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  getAllNotice(): Observable<ApiResponse> {
    //const queryStr = this.globalService.getSearchQueryString();
    return this.httpclient.get<ApiResponse>(this.ApiUrl +  'notice/getNoticeList');
  }
  
  viewNoticeFile(noticeId: string) {
    let headers = new HttpHeaders({ "Content-Type": "application/pdf" });
    let options = { headers: headers };
    return this.httpclient.get(this.ApiUrl + 'notice/viewNotice/' + noticeId, { headers: new HttpHeaders({ 'Content-Type': 'application/pdf' }), responseType: 'blob' });
  }
  InsertNotice(noticeCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice',noticeCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }

  GetNoticeById(noticeId: string): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'notice/' + noticeId);
  }

  UpdateNotice(noticeCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/updateNotice', noticeCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
}
