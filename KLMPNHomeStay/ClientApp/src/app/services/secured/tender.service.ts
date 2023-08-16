import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';

@Injectable({
  providedIn: 'root'
})
export class TenderService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  getAllTender(): Observable<ApiResponse> {
    //const queryStr = this.globalService.getSearchQueryString();
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'tender/getTenderList');
  }
  viewTenderFile(tenderId: string) {
    let headers = new HttpHeaders({ "Content-Type": "application/pdf" });
    let options = { headers: headers };
    return this.httpclient.get(this.ApiUrl + 'tender/viewTender/' + tenderId, { headers: new HttpHeaders({ 'Content-Type': 'application/pdf' }), responseType: 'blob' });
  }
  InsertTender(tenderCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'tender/createTender', tenderCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
  GetTenderById(tenderId: string): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'tender/' + tenderId);
  }
  UpdateTender(tenderCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'tender/updateTender', tenderCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
}
