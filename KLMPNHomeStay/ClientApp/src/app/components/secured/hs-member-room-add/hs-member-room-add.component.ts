import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { GlobalsService } from '../../../services/common/globals.service';
import { Router } from '@angular/router';
import { HsMemberRoomAddService } from '../../../services/secured/hs-member-room-add.service'
import { IDropdownSettings, } from 'ng-multiselect-dropdown';
import { HsMemberDetailService } from '../../../services/secured/hs-member-detail.service';
import { HsMemberDetails, RoomDetails } from '../../../model/secured/hs-member-details';


@Component({
  selector: 'app-hs-member-room-add',
  templateUrl: './hs-member-room-add.component.html',
  styleUrls: ['./hs-member-room-add.component.css']
})
export class HsMemberRoomAddComponent implements OnInit {
  roomList: any = [];
  p: number = 1;
  roomForm: FormGroup;
  dropdownList = [];
  dropdownSettings: IDropdownSettings = {};
  roomFacilities: any;
  roomCategories: any;
  submitted = false;
  roomImageObj: any;
  Base64Image: any;
  Base64TempVariable: any;
  imageError: string;

  constructor(private memberDetailService: HsMemberDetailService, private router: Router, private globalservice: GlobalsService, private formBuilder: FormBuilder, private roomService: HsMemberRoomAddService) { }

  ngOnInit() {

    this.roomForm = this.formBuilder.group({

      roomCategory: ['', Validators.required],
      roomFacility: ['', Validators.required],
      roomNo: ['', Validators.required],
      roomRate: ['', Validators.required],
      roomFloor: ['', Validators.required],
      noOfBeds: ['', Validators.required],
      roomSize: ['', Validators.required],
      roomImage: [''],
      isAvailable: ['', Validators.required]
    });
    this.loadMultiSelect();
    var hsIds = window.sessionStorage.getItem('hsId');

    this.GetRoomsById(hsIds);

    this.memberDetailService.GetRoomFacility().subscribe(response => {
      this.roomFacilities = response.data;
    });

    this.memberDetailService.GetRoomCategory().subscribe(response => {
      this.roomCategories = response.data;
    });

    this.roomForm = this.formBuilder.group({
      roomAddArray: this.formBuilder.array([]),
    });

    this.addRooms();
  }

  get f() { return this.roomForm.controls; }

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

  //MultiSelect
  loadMultiSelect() {
    this.dropdownSettings = {
      idField: 'hsFacilityId',
      textField: 'hsFacilityName',
      allowSearchFilter: true

    };
  }

  GetRoomsById(id:string) {
    this.roomService.getAllRooms(id).subscribe(resp => {
      if (resp.result === 'Success') {
        this.roomList = resp.data;
        console.log(this.roomList);
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }

    });
  }

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
  }

  _handleReaderLoaded(e) {
    let reader = e.target;
    this.Base64Image = reader.result;
    if (this.Base64TempVariable != undefined)
      this.Base64TempVariable += "@@@" + this.Base64Image;
    else
      this.Base64TempVariable = this.Base64Image;
  }

  onSubmit() {
    this.submitted = true;
    this.roomForm.markAllAsTouched();
    const pre = this.roomForm.value as HsMemberDetails;

    if (this.roomForm.invalid) {
      return;
    }
    //room info
    this.roomForm.markAllAsTouched();
    const roomValue = this.roomForm.value.roomAddArray as RoomDetails;
    pre.roomImageStrings = this.Base64TempVariable;

    if (this.roomForm.invalid) {
      return;
    }
    pre.roomDetailsList = roomValue;
    pre.hsId = window.sessionStorage.getItem('hsId');

    this.memberDetailService.InsertHSRoomDtl(pre).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        if (resp.msg == 'Room No can not be duplicate!!') {
          this.globalservice.swalError("Room No can not be duplicate!!");
        }
        else if (resp.msg == 'Error in Saving Data') {
          this.globalservice.swalError("Error in Saving Data!!");
        }
        else if (resp.msg == 'HS Details Save successfully') {
          this.globalservice.swalSuccess("HS Details Save successfully");
          this.router.navigate(['']);
        }
        else {
          this.globalservice.swalError("Error in Saving Data!!");
        }
      });
  }


  close() {
    this.router.navigate(['']);
  } 

  onClickEdit(hsRoomNo: any) {
    
  }  

}

