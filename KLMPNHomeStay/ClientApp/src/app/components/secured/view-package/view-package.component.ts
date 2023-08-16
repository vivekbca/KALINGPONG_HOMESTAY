import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { GlobalsService } from '../../../services/common/globals.service';
import { PackageTourService } from '../../../services/secured/package-tour.service';

@Component({
  selector: 'app-view-package',
  templateUrl: './view-package.component.html',
  styleUrls: ['./view-package.component.css']
})
export class ViewPackageComponent implements OnInit {

  tourList: any = [];
  tourDetail: any;
  constructor(private tourService: PackageTourService, private spinner: NgxSpinnerService, private globalservice: GlobalsService, private router: Router) { }

  ngOnInit() {
    this.GetAllTour();
  }
  GetAllTour() {
    this.spinner.show();
    this.tourService.getAllTour().subscribe(resp => {
      if (resp.result === 'Success') {
        this.tourList = resp.data;
        this.spinner.hide();
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    }
    )
  }
  back() {
    this.router.navigate(['admin-dashboard']);
  }
  bookNow(tourId: any) {
    this.router.navigate(['admin-tour-detail'], { queryParams: { tourId: tourId }, skipLocationChange: false });
  }
  packageAdd() {
    this.router.navigate(['add-package']);
  }
}
