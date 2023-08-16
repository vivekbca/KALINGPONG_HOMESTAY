import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';
//import {
//  MatSnackBar,
//  MatSnackBarConfig,
//  MatSnackBarHorizontalPosition,
//  MatSnackBarVerticalPosition,
//  } from '@angular/material/snack-bar';


@Injectable({
  providedIn: 'root'
})
export class GlobalsService {

  action: boolean = true;
  setAutoHide: boolean = true;
  autoHide: number = 5000;
  //horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  //verticalPosition: MatSnackBarVerticalPosition = 'top';

  addExtraClass: boolean = false;

  //constructor(public snackBar: MatSnackBar) { }
  constructor() { }

  getApiUrl() {
    let ApiUrl = 'https://localhost:44314/api/';
    //let ApiUrl = 'http://103.107.66.140:8092/api/';
    return ApiUrl;
  }

  /*
    .green-snackbar {
      background: #18F211;
    }
    .yellow-snackbar {
      background: #FFFF08;
    }
    .red-snackbar {
      background: #F21129;
    }

  */
  showMessage(message: string, actionButtonLabel: string) {
    //let config = new MatSnackBarConfig();
    //config.verticalPosition = this.verticalPosition;
    //config.horizontalPosition = this.horizontalPosition;
    //config.duration = this.setAutoHide ? this.autoHide : 0;
    //let backColorCssclass: string;
    //if (actionButtonLabel.indexOf('Success') > -1)
    //  backColorCssclass = 'green-snackbar';
    //else if (actionButtonLabel.indexOf('Info') > -1)
    //  backColorCssclass = 'yellow-snackbar';
    //else if (actionButtonLabel.indexOf('Error') > -1 || actionButtonLabel.indexOf('ModelErr') > -1)
    //  backColorCssclass = 'red-snackbar';

    //config.panelClass = backColorCssclass;
    //this.snackBar.open(message, this.action ? actionButtonLabel : undefined, config);
  }

  getPageSizeList() {
    let pageSizes = [5, 10, 15];
    return pageSizes;
  }

  getSearchQueryString(obj: any): string {
    const qs = Object.keys(obj)
      .map(key => `${key}=${obj[key]}`)
      .join('&');
    return `?${qs}`;
  }
  public swalError(arg: any) {
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: arg,
      showConfirmButton: false,
      timer: 1500
    })
  }

  public swalSuccess(arg: any) {
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: arg,
      showConfirmButton: false,
      timer: 1500
    })
  }
}
