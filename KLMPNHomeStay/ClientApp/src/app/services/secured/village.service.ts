import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';

@Injectable({
  providedIn: 'root'
})
export class VillageService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  getAllVillage(): Observable<ApiResponse> {
    //const queryStr = this.globalService.getSearchQueryString();
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'blockVillage');
  }
  getAllHSByVillId(villId: string): Observable<ApiResponse> {
    //const queryStr = this.globalService.getSearchQueryString();
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'search/LoadHS/' + villId);
  }
}
