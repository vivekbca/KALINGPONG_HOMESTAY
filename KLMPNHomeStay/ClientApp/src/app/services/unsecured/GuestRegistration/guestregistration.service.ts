import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../model/common/api-response';
import { GuestregistrationDTO } from '../../../model/unsecured/GuestRegistration/guestregistration-dto';
import { GlobalsService } from '../../common/globals.service';

@Injectable({
  providedIn: 'root'
})
export class GuestregistrationService {
  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  InsertProfile(profile: GuestregistrationDTO): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'GuestUserRegistration', JSON.stringify(profile), { headers: reqHeader });
  }
  GetAllCountry(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'GuestUserRegistration/country', { headers: reqHeader });
  }
  GetAllState(CountryId:string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'GuestUserRegistration/state/' +CountryId, { headers: reqHeader });
  }
  GetUserById(userId: string): Observable<ApiResponse> {
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'gULogin/' + userId);
  }
}
