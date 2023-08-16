import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../model/common/api-response';
import { GlobalsService } from '../../common/globals.service';
import { MemberWithUsDto } from '../../../model/unsecured/mamberwithus/member-with-us-dto';

@Injectable({
  providedIn: 'root'
})
export class MemberWithUSService {
  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  GetAllCountry(): Observable<ApiResponse>   {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'MemberWithUs/country', { headers: reqHeader });
  }
  GetAllDistrictByDefault(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'MemberWithUs/allDistrict', { headers: reqHeader });
  }
  GetVillCat(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'MemberWithUs/villCat', { headers: reqHeader });
  }
  GetAllState(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'MemberWithUs/state', { headers: reqHeader });
  }
  GetAllDistrict(StateId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'MemberWithUs/district/' + StateId, { headers: reqHeader});
  }
  GetAllBlock(DistrictId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'MemberWithUs/block/' + DistrictId, { headers: reqHeader });
  }
  GetAllVillage(BlockId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'MemberWithUs/village/' + BlockId, { headers: reqHeader });
  }
  InsertProfile(profile: MemberWithUsDto): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'MemberWithUs', JSON.stringify(profile), { headers: reqHeader });
  }
  GetBlockByDefault(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'MemberWithUs/blockByDefault', { headers: reqHeader });
  }
  GetVillageByDefault(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'MemberWithUs/villageByDefault', { headers: reqHeader });
  }
}
