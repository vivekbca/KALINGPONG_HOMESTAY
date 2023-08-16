import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../model/common/api-response';
import { SearchModel } from '../../../model/unsecured/Search/search-model';
import { GlobalsService } from '../../common/globals.service';

@Injectable({
  providedIn: 'root'
})
export class PopularityService {
  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  GetAllPopularity(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/popularHomestay', { headers: reqHeader });
  }
  offbeat1(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/offbeat', { headers: reqHeader });
  }
  OurProperty(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/ourProperty', { headers: reqHeader });
  }
  InserOurPropertyByID(model: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'HomePage/ourPropertybyid', JSON.stringify(model), { headers: reqHeader });
  }
  OtherProperties(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/otherProperty', { headers: reqHeader });
  }
  Villagelist(VillId:string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/Villagelist/' + VillId, { headers: reqHeader });
  }
  DDLBlockList(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/blockList', { headers: reqHeader });
  }
  DDLVillageList(BlockId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/VillagelistDDl/' + BlockId, { headers: reqHeader });
  }
  DDLHomastayList(VillageId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/HomestaylistDDl/' + VillageId, { headers: reqHeader });
  }
  HomestayDetails(HomestayId: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/Homestaydetail/' + HomestayId, { headers: reqHeader });
  }

  HomestaySearch(Data:any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'Search/searchHomestay', JSON.stringify(Data), { headers: reqHeader });
  }
  OtherDestination(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/otherDestination', { headers: reqHeader });
  }
  allPopularDestination(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/populardestination', { headers: reqHeader });
  }
  PopularHomestay(DestinationID: string): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/PopularDestinationHomestay/' + DestinationID, { headers: reqHeader });
  }
  popularHomestayGU(): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'HomePage/populardestination', { headers: reqHeader });
  }
}
