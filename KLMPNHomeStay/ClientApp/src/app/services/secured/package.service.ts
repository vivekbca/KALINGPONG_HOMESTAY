import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';
import { Package } from '../../model/secured/package';
import { GlobalsService } from '../common/globals.service';

@Injectable({
  providedIn: 'root'
})
export class PackageService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  InsertPackage(profile: Package): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Content-Type': 'application/json' });
    return this.httpclient.post<ApiResponse>(this.ApiUrl + 'Package/package/', JSON.stringify(profile), { headers: reqHeader });
  }
}
