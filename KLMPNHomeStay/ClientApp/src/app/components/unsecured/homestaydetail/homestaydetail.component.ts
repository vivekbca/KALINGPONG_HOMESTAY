import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable, observable } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { PopularityService } from '../../../services/unsecured/Popularity/popularity.service';

@Component({
  selector: 'app-homestaydetail',
  templateUrl: './homestaydetail.component.html',
  styleUrls: ['./homestaydetail.component.css']
})
export class HomestaydetailComponent implements OnInit {
  HomestayID: any;
  HomestayDetails: any;
  HomestayAminities: [];
  HomestayImages: any[]= [];
  lat: any;
  lng: any;
  FullMapUrl: any;
  constructor(private router: Router,private route: ActivatedRoute, private popularityService: PopularityService, private spinner: NgxSpinnerService, private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.HomestayID = params.hsId;
    });
    this.spinner.show();
      this.popularityService.HomestayDetails(this.HomestayID).subscribe(resp => {   
        this.HomestayDetails = resp.data;
        console.log("Details", this.HomestayDetails)
        this.HomestayAminities = this.HomestayDetails.hsAmImg.reduce((accumalator, current) => {
          if (!accumalator.some(item => item.amin === current.amin)) {
            accumalator.push(current);
          }
          return accumalator;
        }, []);
      this.lat =Object.values(this.HomestayDetails)[53];
      this.lng = Object.values(this.HomestayDetails)[54];
      var url = "https://maps.google.com/maps?q=" + this.lat + "," + this.lng + "&z=16&output=embed";
      this.FullMapUrl = this.sanitizer.bypassSecurityTrustResourceUrl(url);
        this.spinner.hide();
        /*console.log("unique", this.HomestayAminities)*/
    });
  }
  OnBooking(value: any) {
    this.router.navigate(['/check-availability', { hsId: value }]);
  }
}
