import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';
import { HttpClient } from '@angular/common/http';
import { GlobalsService } from '../common/globals.service';

@Injectable({
  providedIn: 'root'
})
export class FinancialyearService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  getAllFinYr(): Observable<ApiResponse> {
    //const queryStr = this.globalService.getSearchQueryString();
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'financialYear/getFinYrList');
  }
}
