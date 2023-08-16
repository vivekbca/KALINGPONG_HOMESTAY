import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalsService } from '../../common/globals.service';
import { ApiResponse } from '../../../model/common/api-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  fetchAvail(fetchCompObj: any) {
    //return this.httpclient.post<ApiResponse>(this.ApiUrl + 'notice/createNotice/', + noticeId, fd);
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'booking/fetchAvailabilty', fetchCompObj);
    //`this.ApiUrl + 'notice/createNotice/'/${noticeId}/${heading}/${subject}`, fd
  }
  marqueeList(): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'marquee/homePageMarqueeList');
  }
}
