import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';
import { GlobalsService } from '../common/globals.service';

@Injectable({
  providedIn: 'root'
})
export class BookingListReportService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  GetPreviousdayBookingList(filtervalue: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'BookingReport/HomestayListReport/' + filtervalue, { headers: reqHeader });
  }
  GetPreviousdayPackageList(filtervalue: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'BookingReport/PackageListReport/' + filtervalue, { headers: reqHeader });
  }
  CheckedAll(profile: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'BookingReport/checkall', JSON.stringify(profile), { headers: reqHeader });
  }
  CheckedAllPkg(profile: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'BookingReport/checkallpkg', JSON.stringify(profile), { headers: reqHeader });
  }
  HSBookingReport(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/vnd.openxmlformats-ficedocument.spreadsheetml.sheet', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'Report/getReposrt', { headers: reqHeader });
  }
}
