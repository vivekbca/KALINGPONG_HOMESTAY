import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { HomestayApprovalService } from '../../../services/secured/homestay-approval.service';

@Component({
  selector: 'app-homestayapproval-detail',
  templateUrl: './homestayapproval-detail.component.html',
  styleUrls: ['./homestayapproval-detail.component.css']
})
export class HomestayapprovalDetailComponent implements OnInit {
  hsId: any;
  hsForm: FormGroup;
  routeSub: any = {};
  hsDetails: any;
  constructor(private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute,private hsApprovalService: HomestayApprovalService, private router: Router, private globalservice: GlobalsService) { }

  ngOnInit() {
    this.routeSub = this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.hsId && params.hasOwnProperty('hsId')) {
          this.hsId = params.hsId;
        }
      });
    this.initForm();
    if (this.hsId) {
      this.initiateUpdateHs();
    }
  }
  get f() { return this.hsForm.controls; }
  initForm() {
    this.hsForm = this.formBuilder.group({
      hsName: [''],
      address: [''],
      hsId: [''],
      pincode: [''],
      block: [''],
      village: [''],
      district: [''],
      contactPersonName: [''],
      contactPersonEmail: [''],
      contactPersonMob: [''],
      noofrooms: [''],
      bankName: [''],
      branch: [''],
      accountNo: [''],
      accountType: [''],
      ifsc: [''],
      micr: [''],
    });
  }

  private initiateUpdateHs() {
    this.routeSub = this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.hsId && params.hasOwnProperty('hsId')) {
          this.hsId = params.hsId;
          
          this.hsApprovalService.GetHSById(this.hsId).subscribe(data => {
            let res: any = data;
            if (res.result === 'Success') {
              this.hsDetails = res.data;
              this.onModifyHSForm();

            } else {
              //this.toastr.errorToastr("Server Error", 'Error!');
            }
          });
        }
      });
  }

  private onModifyHSForm() {
    this.f.hsName.setValue(this.hsDetails.hsName);
    this.f.address.setValue(this.hsDetails.hsAddress1);
    this.f.hsId.setValue(this.hsDetails.hsId);
    this.f.pincode.setValue(this.hsDetails.pincode);
    this.f.block.setValue(this.hsDetails.hsBlockName);
    this.f.village.setValue(this.hsDetails.hsVillName);
    this.f.district.setValue(this.hsDetails.hsDistrictName);
    this.f.contactPersonName.setValue(this.hsDetails.hsContactName);
    this.f.contactPersonEmail.setValue(this.hsDetails.hsContactEmail);
    this.f.contactPersonMob.setValue(this.hsDetails.hsContactMob1);
    this.f.noofrooms.setValue(this.hsDetails.hsNoOfRooms);
    this.f.bankName.setValue(this.hsDetails.hsBankName);
    this.f.branch.setValue(this.hsDetails.hsBankBranch);
    this.f.accountNo.setValue(this.hsDetails.hsAccountNo);
    this.f.accountType.setValue(this.hsDetails.hsAccountType);
    this.f.ifsc.setValue(this.hsDetails.hsIfsc);
    this.f.micr.setValue(this.hsDetails.hsMicr);
  }

  back() {
    this.router.navigate(['homestay-approval']);
  }

}
