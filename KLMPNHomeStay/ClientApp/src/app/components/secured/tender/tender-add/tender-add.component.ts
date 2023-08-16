import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GlobalsService } from '../../../../services/common/globals.service';
import { TenderService } from '../../../../services/secured/tender.service';
import { FinancialyearService } from '../../../../services/secured/financialyear.service';

@Component({
  selector: 'app-tender-add',
  templateUrl: './tender-add.component.html',
  styleUrls: ['./tender-add.component.css']
})
export class TenderAddComponent implements OnInit {

  tenderForm: FormGroup;
  submitted = false;
  loading = false;
  refdoc1: File = null;
  tenderId: any;
  finYrList: any = [];
  routeSub: any = {};
  tenderDetails: any;
  updatingTender: boolean = false;
  additonalFd = new FormData();
  constructor(private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router, private globalservice: GlobalsService, private tenderService: TenderService, private finYrService: FinancialyearService) { }


  ngOnInit() {
    this.routeSub = this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.tenderId && params.hasOwnProperty('tenderId')) {
          this.tenderId = params.tenderId;
        }
      });
    this.initForm();
    this.getAllFinYr();
    if (this.tenderId) {
      this.initiateUpdateTender();
    }
  }
  get f() { return this.tenderForm.controls; }

  initForm() {
    this.tenderForm = this.formBuilder.group({
      subject: ['', Validators.required],
      memoNo: ['', Validators.required],
      finYrId: [''],
      closingDate: ['', Validators.required],
      tenderId: [''],
      refdoc1: [null],
    });
  }

  onChangeFile(event) {
    this.refdoc1 = <File>event.target.files[0];
  }

  onSubmit() {
    if (this.tenderForm.invalid) {
      return;
    } else {
      let fd = new FormData();
      fd.append('tenderId', null);
      fd.append('subject', this.f.subject.value);
      fd.append('memoNo', this.f.memoNo.value);
      fd.append('finYrId', this.f.finYrId.value);
      fd.append('closingDate', this.f.closingDate.value);
      if (this.refdoc1) { fd.append('tenderFile', this.refdoc1, this.refdoc1.name); }

      this.tenderService.InsertTender(fd).subscribe(
        resp => {
          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.result === 'Success') {
            //alert("Saved Successfully");
            this.globalservice.swalSuccess("Tender Add Successfully");
            this.router.navigate(['tender-dashboard']);
          }
        });
    }
  }

  private initiateUpdateTender() {
    this.routeSub = this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.tenderId && params.hasOwnProperty('tenderId')) {
          this.tenderId = params.tenderId;
          this.updatingTender = true;
          this.tenderService.GetTenderById(this.tenderId).subscribe(data => {
            let res: any = data;
            if (res.result === 'Success') {
              this.tenderDetails = res.data;
              this.onModifyTenderForm();

            } else {
              //this.toastr.errorToastr("Server Error", 'Error!');
            }
          });
        }
      });
  }

  private onModifyTenderForm() {
    this.f.subject.setValue(this.tenderDetails.subject);
    this.f.memoNo.setValue(this.tenderDetails.memoNo);
    this.f.finYrId.setValue(this.tenderDetails.finYrId);
    this.f.tenderId.setValue(this.tenderDetails.tenderId);
    this.f.closingDate.setValue(this.tenderDetails.closingDate);
  }
  onUpdateTender() {
    if (this.tenderForm.invalid) {
      return;
    } else {

      let fd = new FormData();
      fd.append('tenderId', this.f.tenderId.value);
      fd.append('subject', this.f.subject.value);
      fd.append('memoNo', this.f.memoNo.value);
      fd.append('finYrId', this.f.finYrId.value);
      fd.append('closingDate', this.f.closingDate.value);
      //fd.append('RequestType', this.RequestType);
      if (this.refdoc1) { fd.append('tenderFile', this.refdoc1, this.refdoc1.name); }

      this.tenderService.UpdateTender(fd).subscribe(
        resp => {
          this.globalservice.showMessage(resp.msg, resp.result);
          //this.dialogRef.close(resp.result === 'Success');
          if (resp.result === 'Success') {
            //alert("Update Successfully");
            this.globalservice.swalSuccess("Tender Edit Successfully");
            this.router.navigate(['tender-dashboard']);
          }
        });
      //this.loading = false;
    }
  }
  getAllFinYr() {
    this.finYrService.getAllFinYr().subscribe(resp => {
      if (resp.result === 'Success') {
        this.finYrList = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }

    });
  }
  back() {
    this.router.navigate(['tender-dashboard']);
  }
}
