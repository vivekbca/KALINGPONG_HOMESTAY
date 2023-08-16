import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GlobalsService } from '../../../../services/common/globals.service';
import { NoticeService } from '../../../../services/secured/notice.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-notice-add',
  templateUrl: './notice-add.component.html',
  styleUrls: ['./notice-add.component.css']
})
export class NoticeAddComponent implements OnInit {
  noticeForm: FormGroup;
  submitted = false;
  loading = false;
  refdoc1: File = null;
  noticeId: any;
  routeSub: any = {};
  noticeDetails: any;
  updatingNotice: boolean = false;
  additonalFd = new FormData();
  constructor(private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router, private globalservice: GlobalsService, private noticeService: NoticeService) { }

  ngOnInit() {
    this.routeSub = this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.noticeId && params.hasOwnProperty('noticeId')) {
          this.noticeId = params.noticeId;
        }
      });
    this.initForm();
    if (this.noticeId) {
      this.initiateUpdateNotice();
    }
  }
  get f() { return this.noticeForm.controls; }
  initForm() {
    this.noticeForm = this.formBuilder.group({
      heading: ['', Validators.required],
      subject: ['', Validators.required],
      noticeId: [''],
      refdoc1: [null],
    });
  }
  onChangeFile(event) {
    this.refdoc1 = <File>event.target.files[0];
  }
  onSubmit() {
    if (this.noticeForm.invalid) {
      return;
    } else {
      let fd = new FormData();
      fd.append('noticeId', null);
      fd.append('heading', this.f.heading.value);
      fd.append('subject', this.f.subject.value);
      if (this.refdoc1) { fd.append('noticeFile', this.refdoc1, this.refdoc1.name); }

      this.noticeService.InsertNotice(fd).subscribe(
            resp => {
          this.globalservice.showMessage(resp.msg, resp.result);
          if (resp.result === 'Success') {
            //alert("Saved Successfully");
            this.globalservice.swalSuccess("Notice Add Successfully");
            this.router.navigate(['notice-dashboard']);
          }
        });
    }
  }

  private initiateUpdateNotice() {
    this.routeSub = this.activatedRoute.queryParams
      .subscribe(params => {
        if (params.noticeId && params.hasOwnProperty('noticeId')) {
          this.noticeId = params.noticeId;
          this.updatingNotice = true;
          this.noticeService.GetNoticeById(this.noticeId).subscribe(data => {
            let res: any = data;
            if (res.result === 'Success') {
              this.noticeDetails = res.data;
              this.onModifyNoticeForm();

            } else {
              //this.toastr.errorToastr("Server Error", 'Error!');
            }
          });
        }
      });
  }


  private onModifyNoticeForm() {
    this.f.heading.setValue(this.noticeDetails.heading);
    this.f.subject.setValue(this.noticeDetails.subject);
    this.f.noticeId.setValue(this.noticeDetails.noticeId);
    //this.f.unladenWt.setValue(this.truckDetails.unladenWeight);
    
  }

  onUpdateNotice() {
    if (this.noticeForm.invalid) {
      return;
    } else {
      
      let fd = new FormData();
      fd.append('noticeId', this.f.noticeId.value);
      fd.append('heading', this.f.heading.value);
      fd.append('subject', this.f.subject.value);
      //fd.append('RequestType', this.RequestType);
      if (this.refdoc1) { fd.append('noticeFile', this.refdoc1, this.refdoc1.name); }
     
      this.noticeService.UpdateNotice(fd).subscribe(
        resp => {
          this.globalservice.showMessage(resp.msg, resp.result);
          //this.dialogRef.close(resp.result === 'Success');
          if (resp.result === 'Success') {
            //alert("Update Successfully");
            this.globalservice.swalSuccess("Notice Edit Successfully");
            this.router.navigate(['notice-dashboard']);
          }
        });
      //this.loading = false;
    }
  }

  back() {
    this.router.navigate(['notice-dashboard']);
  }
}
