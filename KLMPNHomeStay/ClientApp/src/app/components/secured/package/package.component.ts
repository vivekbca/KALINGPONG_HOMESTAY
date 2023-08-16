import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { NgxSpinnerService } from 'ngx-spinner';
import { Package } from '../../../model/secured/package';
import { GlobalsService } from '../../../services/common/globals.service';
import { HsMemberDetailService } from '../../../services/secured/hs-member-detail.service';
import { PackageService } from '../../../services/secured/package.service';

@Component({
  selector: 'app-package',
  templateUrl: './package.component.html',
  styleUrls: ['./package.component.css']
})
export class PackageComponent implements OnInit {
  roomFacilities: any;
  packageForm: FormGroup;
  submitted = false;
  dropdownSettings: IDropdownSettings = {};
  Base64Image1: any;
  Base64Image2: any;
  Base64Image3: any;
  Base64Image4: any;
  Base64Image5: any;
  Image1: any;
  Image2: any;
  Image3: any;
  Image4: any;
  Image5: any;
  pdf: any;
  pdfupload: any;
  constructor(private packageService: PackageService, private spinner: NgxSpinnerService, private router: Router, private apicall: GlobalsService, private formBuilder: FormBuilder, private memberDetailService: HsMemberDetailService, private globalservice: GlobalsService) { }

  ngOnInit() {
    this.memberDetailService.GetRoomFacility().subscribe(response => {
      this.roomFacilities = response.data;
    });
    this.loadMultiSelect();
    this.packageForm = this.formBuilder.group({
      PName: ['', Validators.required],
      DName: ['', Validators.required],
      Day: ['', Validators.required],
      Night: ['', Validators.required],
      Subject: ['', Validators.required],
      Description: ['', Validators.required],
      Cost: ['', Validators.required],
      ContactPerson: [''],
      ContactEmail: ['', [Validators.required, Validators.email]],
      ContactMobile: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
      Image1: ['', Validators.required],
      Image2: [''],
      Image3: [''],
      Image4: [''],
      Image5: [''],
      PackagePdf: ['', Validators.required],
      roomFacility: []
    })
  }
  loadMultiSelect() {
    this.dropdownSettings = {
      idField: 'hsFacilityId',
      textField: 'hsFacilityName',
      allowSearchFilter: true
    };
  }
  back() {
    this.router.navigate(['view-package']);
  }
  detectFiles(e,value:any) {
    if (value == 1) {
      this.Image1 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsDataURL(this.Image1);
    }
    if (value == 2) {
      this.Image2 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded2.bind(this);
      reader.readAsDataURL(this.Image2);
    }
    if (value == 3) {
      this.Image3 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded3.bind(this);
      reader.readAsDataURL(this.Image3);
    }
    if (value == 4) {
      this.Image4 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded4.bind(this);
      reader.readAsDataURL(this.Image4);
    }
    if (value == 5) {
      this.Image5 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded5.bind(this);
      reader.readAsDataURL(this.Image5);
    }
    if (value == 6) {
      this.pdf = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded6.bind(this);
      reader.readAsDataURL(this.pdf);
    }
  }
  _handleReaderLoaded(e) {
    let reader = e.target;
    this.Base64Image1 = reader.result;
  }
  _handleReaderLoaded2(e) {
    let reader = e.target;
    this.Base64Image2 = reader.result;
  }
  _handleReaderLoaded3(e) {
    let reader = e.target;
    this.Base64Image3 = reader.result;
  }
  _handleReaderLoaded4(e) {
    let reader = e.target;
    this.Base64Image4 = reader.result;
  }
  _handleReaderLoaded5(e) {
    let reader = e.target;
    this.Base64Image5 = reader.result;
  }
  _handleReaderLoaded6(e) {
    let reader = e.target;
    this.pdfupload = reader.result;
  }
  get f() { return this.packageForm.controls; }
  onSubmit() {
    this.submitted = true;
    this.packageForm.markAllAsTouched();
    const pre = this.packageForm.value as Package;
    pre.Image1 = this.Base64Image1;
    pre.Image2 = this.Base64Image2;
    pre.Image3 = this.Base64Image3;
    pre.Image4 = this.Base64Image4;
    pre.Image5 = this.Base64Image5;
    pre.PackagePdf = this.pdfupload;
    pre.CreatedBy = window.sessionStorage.getItem('userId');
    if (this.packageForm.invalid) {
      return;
    }
    if (pre.roomFacility.length > 5) {
      this.apicall.swalError("PLease Select Only 5 Facility");
      return;
    }
    this.spinner.show();
    this.packageService.InsertPackage(pre).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        if (resp.msg == 'Failure') {
          this.apicall.swalError("Something Went Wrong !");
          this.spinner.hide();
        }
        else {
          this.apicall.swalSuccess("Package Added Successfully");
          this.spinner.hide();
          this.router.navigate(['view-package']);
        }
      });
  }
}
