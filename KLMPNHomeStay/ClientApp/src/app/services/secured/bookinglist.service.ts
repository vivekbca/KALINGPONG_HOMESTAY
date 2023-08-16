import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../model/common/api-response';
import { GlobalsService } from '../common/globals.service';

@Injectable({
  providedIn: 'root'
})
export class BookinglistService {

  ApiUrl = '';
  constructor(private httpclient: HttpClient, private globalService: GlobalsService) { this.ApiUrl = globalService.getApiUrl(); }

  GetAllBookingList(filtervalue:any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'BookingList/getBookinglist/' + filtervalue, { headers: reqHeader });
  }
  BookingDetail(bookingId: any): Observable<ApiResponse> {
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' });
    return this.httpclient.get<ApiResponse>(this.ApiUrl + 'BookingList/bookingDetail/' + bookingId, { headers: reqHeader });
  }
}
