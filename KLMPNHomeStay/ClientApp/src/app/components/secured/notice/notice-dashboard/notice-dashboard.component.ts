import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../../services/common/globals.service';
import { NoticeService } from '../../../../services/secured/notice.service';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-notice-dashboard',
  templateUrl: './notice-dashboard.component.html',
  styleUrls: ['./notice-dashboard.component.css']
})
export class NoticeDashboardComponent implements OnInit {
 
  noticeList: any = [];
  p: number = 1;
  constructor(private router: Router, private globalservice: GlobalsService, private formBuilder: FormBuilder,private noticeService: NoticeService) { }

  ngOnInit() {
    this.GetAllNotice();
  }
  onNoticeAdd() {
    this.router.navigate(['notice']);
  }
  GetAllNotice() {
    this.noticeService.getAllNotice().subscribe(resp => {
      if (resp.result === 'Success') {
        this.noticeList = resp.data;
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
  onClickEdit(noticeId: any) {
    this.router.navigate(['notice'], { queryParams: { noticeId: noticeId }, skipLocationChange: false });
  }
  back() {
    this.router.navigate(['admin-dashboard']);
  }
}
