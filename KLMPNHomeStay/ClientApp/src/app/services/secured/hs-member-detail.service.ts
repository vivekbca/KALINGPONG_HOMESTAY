import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';
import { HsMemberDetails } from '../../model/secured/hs-member-details';

@Injectable({
  providedIn: 'root'
})
export class HsMemberDetailService {
  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  GetRoomCategory(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HSMemberDetails/category', { headers: reqHeader });
  }
  GetRoomFacility(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HSMemberDetails/service', { headers: reqHeader });
  }
  InsertHSmemberDtl(data: HsMemberDetails): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'HSMemberDetails/save/', JSON.stringify(data), { headers: reqHeader });
  }
  //Room Entry
  InsertHSRoomDtl(data: HsMemberDetails): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'HSMemberDetails/roomsave/', JSON.stringify(data), { headers: reqHeader });
  }
  GetHSDetails(hsId: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HSMemberDetails/getHSDtl/' + hsId, { headers: reqHeader });
  }
}
