import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { FormBuilder } from '@angular/forms';
import { NoticeService } from '../../../services/secured/notice.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-notice-list',
  templateUrl: './notice-list.component.html',
  styleUrls: ['./notice-list.component.css']
})
export class NoticeListComponent implements OnInit {

  noticeList: any = [];
  p: number = 1;
  constructor(private router: Router, private globalservice: GlobalsService, private spinner: NgxSpinnerService,private formBuilder: FormBuilder, private noticeService: NoticeService) { }

  ngOnInit() {
    this.GetAllNotice();
  }
  GetAllNotice() {
    this.spinner.show();
    this.noticeService.getAllNotice().subscribe(resp => {
      if (resp.result === 'Success') {
        this.noticeList = resp.data;
        this.spinner.hide();
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }

    });
  }
  viewFile(noticeId: any) {
    this.noticeService.viewNoticeFile(noticeId).subscribe(res => {
      const fileURL = URL.createObjectURL(res);
      window.open(fileURL, '_blank');
    });
  }
}
