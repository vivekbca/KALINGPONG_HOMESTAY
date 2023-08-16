import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmedValidator } from '../../../services/common/confirmed.validator';
import { GlobalsService } from '../../../services/common/globals.service';
import { MemberWithUSService } from '../../../services/unsecured/memberwithus/member-with-us.service';
import { MemberWithUsDto } from '../../../model/unsecured/mamberwithus/member-with-us-dto';
import { HsMemberDetailService } from '../../../services/secured/hs-member-detail.service';
import { HomestayApprovalService } from '../../../services/secured/homestay-approval.service';


@Component({
  selector: 'app-mamber-with-us',
  templateUrl: './mamber-with-us.component.html',
  styleUrls: ['./mamber-with-us.component.css']
})
export class MamberWithUsComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  countries: any;
  states: any;
  districts: any;
  blocks: any;
  villages: any;
  villagesCat: any;
  myDefaultValue: string = "Darjeeling District Central Co-operative Bank Ltd";
  HSName: string;
  hsList: any = [];


  constructor(private router: Router, private apicall: GlobalsService, private formBuilder: FormBuilder, private memberWithUsService: MemberWithUSService, private globalservice: GlobalsService, private memberDetailService: HsMemberDetailService, private hsApprovalService: HomestayApprovalService) { }
  ngOnInit() {

    this.registerForm = this.formBuilder.group({
      HsId: [''],
      hsName: ['', Validators.required],
      homeStayDesc: ['', Validators.required],
      GuCountry: [''],
      txtEmailId: ['', [Validators.required, Validators.email]],
      address1: ['', Validators.required],
      address2: ['', Validators.required],
      addonService: [''],
      localAttraction: ['', Validators.required],
      hwtReach: ['', Validators.required],
      address3: [''],
      ddlState: [''],
      ddlDistrict: ['', Validators.required],
      ddlBlock: ['', Validators.required],
      ddlVillageCat: ['', Validators.required],
      ddlVillageName: ['', Validators.required],
      txtContactPerson: ['', Validators.required],
      txtContactNo1: ['', Validators.required],
      txtContactNo2: [''],
      txtPinCode: ['', [Validators.required, Validators.maxLength(6)]],
      HsBankName: [''],
      HsBankBranch: ['', Validators.required],
      HsAccountNo: ['', Validators.required],
      HsAccountType: ['', Validators.required],
      HsIfsc: ['', Validators.required],
      HsMicr: ['', Validators.required],
      userName: ['', Validators.required]
    })

    this.memberWithUsService.GetAllCountry().subscribe(response => {
      this.countries = response.data;
    });
    this.memberWithUsService.GetAllState().subscribe(response => {
      this.states = response.data;
    });
    this.memberWithUsService.GetVillCat().subscribe(response => {
      this.villagesCat = response.data;
    });
    this.memberWithUsService.GetAllDistrictByDefault().subscribe(response => {
      this.districts = response.data;
    });

    this.onPageLoad();
  }
  onStateChange(value: any) {
    this.memberWithUsService.GetAllDistrict(value).subscribe(resp => {
      this.districts = resp.data;
    });
  }
  onDistrictChange(value: any) {
    this.memberWithUsService.GetAllBlock(value).subscribe(resp => {
      this.blocks = resp.data;
    });
  }
  onBlockChange(value: any) {
    this.memberWithUsService.GetAllVillage(value).subscribe(resp => {
      this.villages = resp.data;
    });
  }
  onPageLoad() {
    var userId = window.sessionStorage.getItem('userId');
    if (userId != undefined || userId != null || userId != "") {
      this.memberDetailService.GetHSDetails(userId).subscribe(response => {
        this.HSName = response.data[0].hsName;
        window.sessionStorage.setItem('hsId', response.data[0].hsId);
        //Getting HS Data...
        this.GetHSbyID(response.data[0].hsId);
      });
    /*load ddl by default*/
      //Load Block
      this.memberWithUsService.GetBlockByDefault().subscribe(response => {
        this.blocks = response.data;
      });
      //Load Village
      this.memberWithUsService.GetVillageByDefault().subscribe(response => {
        this.villages = response.data;
      });
      
    }
  }
  GetHSbyID(id: any) {
    this.hsApprovalService.GetHSByIdNew(id).subscribe(resp => {
      if (resp.result === 'Success') {
        this.hsList = resp.data;
        //binding data...
        console.log(this.hsList[0]);
        this.f.HsId.setValue(this.hsList[0].hsId);
        this.f.hsName.setValue(this.hsList[0].hsName);
        this.f.homeStayDesc.setValue(this.hsList[0].homeStayDesc);
        this.f.localAttraction.setValue(this.hsList[0].localAttraction);
        this.f.hwtReach.setValue(this.hsList[0].hwtReach);
        this.f.address1.setValue(this.hsList[0].address1);
        this.f.address2.setValue(this.hsList[0].address2);
        this.f.address3.setValue(this.hsList[0].address3);
        this.f.txtPinCode.setValue(this.hsList[0].txtPinCode);
        //this.f.destinationId.setValue(this.hsList[0].destinationId);
        this.f.addonService.setValue(this.hsList[0].addonService);
        this.f.ddlVillageCat.setValue(this.hsList[0].ddlVillageCat);
        this.f.ddlVillageName.setValue(this.hsList[0].ddlVillageId);
        this.f.ddlBlock.setValue(this.hsList[0].ddlBlock);
        this.f.ddlDistrict.setValue(this.hsList[0].ddlDistrict);
        this.f.ddlState.setValue(this.hsList[0].ddlState);
        this.f.GuCountry.setValue(this.hsList[0].guCountry);
        this.f.txtContactPerson.setValue(this.hsList[0].txtContactPerson);
        this.f.txtContactNo1.setValue(this.hsList[0].txtContactNo1);
        this.f.txtContactNo2.setValue(this.hsList[0].txtContactNo2);
        this.f.txtEmailId.setValue(this.hsList[0].txtEmailId);
        //----this.f.HsNoOfRooms.setValue(this.hsList[0].hsNoOfRooms);
        this.f.HsBankName.setValue(this.hsList[0].hsBankName);
        this.f.HsBankBranch.setValue(this.hsList[0].hsBankBranch);
        this.f.HsAccountNo.setValue(this.hsList[0].hsAccountNo);
        this.f.HsAccountType.setValue(this.hsList[0].hsAccountType);
        this.f.HsIfsc.setValue(this.hsList[0].hsIfsc);
        this.f.HsMicr.setValue(this.hsList[0].hsMicr);
        //this.f.IsActive.setValue(this.hsList[0].isActive);
        //this.f.ActiveSince.setValue(this.hsList[0].activeSince);
        this.f.userName.setValue(this.hsList[0].userName);


      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    });

  }

  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }
  onSubmit() {
    this.submitted = true;
    this.registerForm.markAllAsTouched();
    const pre = this.registerForm.value as MemberWithUsDto;
    if (this.registerForm.invalid) {
      return;
    }



    this.memberWithUsService.InsertProfile(pre).subscribe(
      resp => {
        this.globalservice.showMessage(resp.msg, resp.result);
        console.log(resp.msg)
        if (resp.msg == 'Email ID Already Exist!!') {
          this.apicall.swalError("Email ID Already Exist!!");
        }
        else if (resp.msg == 'User ID Already Exist!!') {
          this.apicall.swalError("User ID Already Exist!!");
        }
        else if (resp.msg == 'Error in Saving Data') {
          this.apicall.swalError("Error in Saving Data!!");
          console.log(resp);
        }
        else {
          this.apicall.swalSuccess("Records Save Successfully...!!!");
          //this.router.navigate(['']);
          this.router.navigate(['hs-member-details']);
        }
      });
  }
}
