import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';
import { GlobalsService } from '../common/globals.service';

@Injectable({
  providedIn: 'root'
})
export class PackageDateAdd {
  ApiUrl = '';
  constructor(private httpClient: HttpClient,private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  getAllTour(): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(this.ApiUrl + 'packageDateAdd/getPackageList');
  }
}
