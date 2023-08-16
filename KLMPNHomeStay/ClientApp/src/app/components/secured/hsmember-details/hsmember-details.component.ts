import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmedValidator } from '../../../services/common/confirmed.validator';
import { GlobalsService } from '../../../services/common/globals.service';
import { IDropdownSettings, } from 'ng-multiselect-dropdown';
import { HsMemberDetailService } from '../../../services/secured/hs-member-detail.service';
import { HsMemberDetails, RoomDetails } from '../../../model/secured/hs-member-details';

@Component({
  selector: 'app-hsmember-details',
  templateUrl: './hsmember-details.component.html',
  styleUrls: ['./hsmember-details.component.css']
})
export class HSmemberDetailsComponent implements OnInit {
  dropdownList = [];
  dropdownSettings: IDropdownSettings = {};
  registerForm: FormGroup;
  roomForm: FormGroup;
  submitted = false;
  roomCategories: any;
  roomFacilities: any;
  userId: string;
  //hsId: string;
  HSName: string;
  roomImageObj: any;
  Base64Image: any;
  Base64TempVariable: any;
  imageError: string;

  hsBase64Image1: any;
  hsBase64Image2: any;
  hsBase64Image3: any;
  hsBase64Image4: any;
  hsBase64Image5: any;
  hsBase64Image6: any;
  hsBase64Image7: any;
  hsBase64Image8: any;
  hsBase64Image9: any;
  hsBase64Image10: any;

  riBase64Image1: any;
  riBase64Image2: any;
  riBase64Image3: any;
  riBase64Image4: any;
  riBase64Image5: any;
  riBase64Image6: any;
  riBase64Image7: any;
  riBase64Image8: any;
  riBase64Image9: any;
  riBase64Image10: any;

  hsImage1: any;
  hsImage2: any;
  hsImage3: any;
  hsImage4: any;
  hsImage5: any;
  hsImage6: any;
  hsImage7: any;
  hsImage8: any;
  hsImage9: any;
  hsImage10: any;

  riImage1: any;
  riImage2: any;
  riImage3: any;
  riImage4: any;
  riImage5: any;
  riImage6: any;
  riImage7: any;
  riImage8: any;
  riImage9: any;
  riImage10: any;


  constructor(private router: Router, private apicall: GlobalsService, private formBuilder: FormBuilder, private memberDetailService: HsMemberDetailService, private globalservice: GlobalsService) { }

  ngOnInit() {

    //this.roomForm = this.formBuilder.group({

    //  roomCategory: ['', Validators.required],
    //  roomFacility: ['', Validators.required],
    //  roomNo: ['', Validators.required],
    //  roomRate: ['', Validators.required],
    //  roomFloor: ['', Validators.required],
    //  noOfBeds: ['', Validators.required],
    //  roomSize: ['', Validators.required],
    //  roomImage: [''],
    //  isAvailable: ['', Validators.required]
    //});

    this.registerForm = this.formBuilder.group({

      hsName: [''],
      hsImage1: ['', Validators.required],
      hsImage2: [''],
      hsImage3: [''],
      hsImage4: [''],
      hsImage5: [''],
      hsImage6: [''],
      hsImage7: [''],
      hsImage8: [''],
      hsImage9: [''],
      hsImage10: [''],

      roomImage1: ['', Validators.required],
      roomImage2: [''],
      roomImage3: [''],
      roomImage4: [''],
      roomImage5: [''],
      roomImage6: [''],
      roomImage7: [''],
      roomImage8: [''],
      roomImage9: [''],
      roomImage10: [''],

      latitude: [''],
      longitude: [''],

      hsId: ['']
    });

    this.memberDetailService.GetRoomCategory().subscribe(response => {
      this.roomCategories = response.data;
    });
    this.memberDetailService.GetRoomFacility().subscribe(response => {
      this.roomFacilities = response.data;
    });
    this.loadMultiSelect();

    this.userId = window.sessionStorage.getItem('userId');
    this.memberDetailService.GetHSDetails(this.userId).subscribe(response => {
      this.HSName = response.data[0].hsName;
      //this.hsId = response.data[0].hsId;
      window.sessionStorage.setItem('hsId', response.data[0].hsId);
    });


    this.roomForm = this.formBuilder.group({
      roomAddArray: this.formBuilder.array([]),
    });
    this.addRooms();
    //sessionStorage.clear();
  }

  //room info

  get roomAddArray(): FormArray {
    return this.roomForm.get("roomAddArray") as FormArray
  }
  addRooms() {
    this.roomAddArray.push(this.newRoomAddArray());
  }
  newRoomAddArray(): FormGroup {
    return this.formBuilder.group({
      roomNo: '',
      roomCategory: this.roomCategories,
      roomRate: '',
      roomFacility: this.roomFacilities,
      roomFloor: '',
      noOfBeds: '',
      roomSize: '',
      roomImage: '',
      isAvailable: ''
    });
  }

  //room info end

  detectFiles(e, value: any) {

    //checking file size
    const max_size = 1000000;
    if (e.target.files[0].size > max_size) {
      this.imageError =
        'Maximum size allowed is 1 Mb';
      return false;
    }
    else {
      this.imageError = '';
    }

    if (value == 'room0') {
      this.roomImageObj = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsDataURL(this.roomImageObj);
    }
    if (value == 'hs1') {
      this.hsImage1 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded1.bind(this);
      reader.readAsDataURL(this.hsImage1);
    }
    if (value == 'hs2') {
      this.hsImage2 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded2.bind(this);
      reader.readAsDataURL(this.hsImage2);
    }
    if (value == 'hs3') {
      this.hsImage3 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded3.bind(this);
      reader.readAsDataURL(this.hsImage3);
    }
    if (value == 'hs4') {
      this.hsImage4 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded4.bind(this);
      reader.readAsDataURL(this.hsImage4);
    }
    if (value == 'hs5') {
      this.hsImage5 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded5.bind(this);
      reader.readAsDataURL(this.hsImage5);
    }
    if (value == 'hs6') {
      this.hsImage6 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded6.bind(this);
      reader.readAsDataURL(this.hsImage6);
    }
    if (value == 'hs7') {
      this.hsImage7 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded7.bind(this);
      reader.readAsDataURL(this.hsImage7);
    }
    if (value == 'hs8') {
      this.hsImage8 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded8.bind(this);
      reader.readAsDataURL(this.hsImage8);
    }
    if (value == 'hs9') {
      this.hsImage9 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded9.bind(this);
      reader.readAsDataURL(this.hsImage9);
    }
    if (value == 'hs10') {
      this.hsImage10 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded10.bind(this);
      reader.readAsDataURL(this.hsImage10);
    }
    //for Room image
    if (value == 'ri1') {
      this.riImage1 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI1.bind(this);
      reader.readAsDataURL(this.riImage1);
    }
    if (value == 'ri2') {
      this.riImage2 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI2.bind(this);
      reader.readAsDataURL(this.riImage2);
    }
    if (value == 'ri3') {
      this.riImage3 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI3.bind(this);
      reader.readAsDataURL(this.riImage3);
    }
    if (value == 'ri4') {
      this.riImage4 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI4.bind(this);
      reader.readAsDataURL(this.riImage4);
    }
    if (value == 'ri5') {
      this.riImage5 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI5.bind(this);
      reader.readAsDataURL(this.riImage5);
    }
    if (value == 'ri6') {
      this.riImage6 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI6.bind(this);
      reader.readAsDataURL(this.riImage6);
    }
    if (value == 'ri7') {
      this.riImage7 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI7.bind(this);
      reader.readAsDataURL(this.riImage7);
    }
    if (value == 'ri8') {
      this.riImage8 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI8.bind(this);
      reader.readAsDataURL(this.riImage8);
    }
    if (value == 'ri9') {
      this.riImage9 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI9.bind(this);
      reader.readAsDataURL(this.riImage9);
    }
    if (value == 'ri10') {
      this.riImage10 = e.target.files[0];
      var reader = new FileReader();
      reader.onload = this._handleReaderLoadedRI10.bind(this);
      reader.readAsDataURL(this.riImage10);
    }
  }
  _handleReaderLoaded(e) {
    let reader = e.target;
    this.Base64Image = reader.result;
    if (this.Base64TempVariable != undefined)
      this.Base64TempVariable += "@@@" + this.Base64Image;
    else
      this.Base64TempVariable = this.Base64Image;
  }

  _handleReaderLoaded1(e) {
    let reader = e.target;
    this.hsBase64Image1 = reader.result;
  }
  _handleReaderLoaded2(e) {
    let reader = e.target;
    this.hsBase64Image2 = reader.result;
  }
  _handleReaderLoaded3(e) {
    let reader = e.target;
    this.hsBase64Image3 = reader.result;
  }
  _handleReaderLoaded4(e) {
    let reader = e.target;
    this.hsBase64Image4 = reader.result;
  }
  _handleReaderLoaded5(e) {
    let reader = e.target;
    this.hsBase64Image5 = reader.result;
  }
  _handleReaderLoaded6(e) {
    let reader = e.target;
    this.hsBase64Image6 = reader.result;
  }
  _handleReaderLoaded7(e) {
    let reader = e.target;
    this.hsBase64Image7 = reader.result;
  }
  _handleReaderLoaded8(e) {
    let reader = e.target;
    this.hsBase64Image8 = reader.result;
  }
  _handleReaderLoaded9(e) {
    let reader = e.target;
    this.hsBase64Image9 = reader.result;
  }
  _handleReaderLoaded10(e) {
    let reader = e.target;
    this.hsBase64Image10 = reader.result;
  }

  //Room Image 
  _handleReaderLoadedRI1(e) {
    let reader = e.target;
    this.riBase64Image1 = reader.result;
  }
  _handleReaderLoadedRI2(e) {
    let reader = e.target;
    this.riBase64Image2 = reader.result;
  }
  _handleReaderLoadedRI3(e) {
    let reader = e.target;
    this.riBase64Image3 = reader.result;
  }
  _handleReaderLoadedRI4(e) {
    let reader = e.target;
    this.riBase64Image4 = reader.result;
  }
  _handleReaderLoadedRI5(e) {
    let reader = e.target;
    this.riBase64Image5 = reader.result;
  }
  _handleReaderLoadedRI6(e) {
    let reader = e.target;
    this.riBase64Image6 = reader.result;
  }
  _handleReaderLoadedRI7(e) {
    let reader = e.target;
    this.riBase64Image7 = reader.result;
  }
  _handleReaderLoadedRI8(e) {
    let reader = e.target;
    this.riBase64Image8 = reader.result;
  }
  _handleReaderLoadedRI9(e) {
    let reader = e.target;
    this.riBase64Image9 = reader.result;
  }
  _handleReaderLoadedRI10(e) {
    let reader = e.target;
    this.riBase64Image10 = reader.result;
  }


  //MultiSelect
  loadMultiSelect() {
    this.dropdownSettings = {
      idField: 'hsFacilityId',
      textField: 'hsFacilityName',
      allowSearchFilter: true

    };
  }

  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }
  get f1() { return this.roomForm.controls; }

  onSubmit() {
    this.submitted = true;
    this.registerForm.markAllAsTouched();
    const pre = this.registerForm.value as HsMemberDetails;

    if (this.registerForm.invalid) {
      return;
    }
    //room info
    this.roomForm.markAllAsTouched();
    const roomValue = this.roomForm.value.roomAddArray as RoomDetails;

    if (this.roomForm.invalid) {
      return;
    }
    pre.roomDetailsList = roomValue;
    pre.roomImageStrings = this.Base64TempVariable;
    pre.hsId = window.sessionStorage.getItem('hsId');

    pre.hsImage1 = this.hsBase64Image1;
    pre.hsImage2 = this.hsBase64Image2;
    pre.hsImage3 = this.hsBase64Image3;
    pre.hsImage4 = this.hsBase64Image4;
    pre.hsImage5 = this.hsBase64Image5;
    pre.hsImage6 = this.hsBase64Image6;
    pre.hsImage7 = this.hsBase64Image7;
    pre.hsImage8 = this.hsBase64Image8;
    pre.hsImage9 = this.hsBase64Image9;
    pre.hsImage10 = this.hsBase64Image10;

    pre.roomImage1 = this.riBase64Image1;
    pre.roomImage2 = this.riBase64Image2;
    pre.roomImage3 = this.riBase64Image3;
    pre.roomImage4 = this.riBase64Image4;
    pre.roomImage5 = this.riBase64Image5;
    pre.roomImage6 = this.riBase64Image6;
    pre.roomImage7 = this.riBase64Image7;
    pre.roomImage8 = this.riBase64Image8;
    pre.roomImage9 = this.riBase64Image9;
    pre.roomImage10 = this.riBase64Image10;
    console.log(pre);

    this.memberDetailService.InsertHSmemberDtl(pre).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        console.log(resp.msg)
        if (resp.msg == 'Room No can not be duplicate!!') {
          this.apicall.swalError("Room No can not be duplicate!!");
        }
        else if (resp.msg == 'Error in Saving Data') {
          this.apicall.swalError("Error in Saving Data!!");
        }
        else if (resp.msg == 'HS Details Save successfully') {
          this.apicall.swalSuccess("HS Details Save successfully");
          this.router.navigate(['hs-room-add']);
        }
        else {
          this.apicall.swalError("Error in Saving Data!!");
        }
      });
  }
}
