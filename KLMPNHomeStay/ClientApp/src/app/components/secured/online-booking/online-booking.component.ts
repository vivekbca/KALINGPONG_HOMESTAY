import { Component, OnInit } from '@angular/core';
import { GlobalsService } from '../../../services/common/globals.service';
import { VillageService } from '../../../services/secured/village.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HomeService } from '../../../services/unsecured/home/home.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-online-booking',
  templateUrl: './online-booking.component.html',
  styleUrls: ['./online-booking.component.css']
})
export class OnlineBookingComponent implements OnInit {
  hsList: any = [];
  villList: any = [];
  onlineBookForm: FormGroup;
  loginUserId: any;

  constructor(public route: Router, private formBuilder: FormBuilder, private homeService: HomeService, public villageService: VillageService, public globalService: GlobalsService, private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.getAllVill();
    this.initForm();
  }
  get f() { return this.onlineBookForm.controls; }
  initForm() {
    this.onlineBookForm = this.formBuilder.group({
      villId: [''],
      hsId: [''],
    });
  }
  getAllVill() {
    this.spinner.show();
    this.villageService.getAllVillage().subscribe(resp => {
      if (resp.result === 'Success') {
        this.villList = resp.data.result;
        this.spinner.hide();
      }
      else {
        this.globalService.showMessage(resp.msg, resp.result);
      }

    });
  }
  villChange(villId: any) {
    //alert(villId);
    this.villageService.getAllHSByVillId(villId).subscribe(resp => {
      if (resp.result === 'Success') {
        this.hsList = resp.data;
      }
      else {
        this.globalService.showMessage(resp.msg, resp.result);
      }
    });
  }
  OnBooking() {
    if (this.onlineBookForm.invalid) {
      return;
    } else {
      let bookCompObj: any = {};
      bookCompObj.villId = this.f.villId.value;
      bookCompObj.hsId = this.f.hsId.value;
      this.homeService.fetchAvail(bookCompObj).subscribe(
        resp => {
          this.globalService.showMessage(resp.msg, resp.result);
          if (resp.result === 'Success') {

            //alert("Saved Successfully");
            //this.globalservice.swalSuccess("Tender Add Successfully");
            this.route.navigate(['/check-availability', { hsId: resp.data.hsId }]);
          }
        });
    }
  }
}
