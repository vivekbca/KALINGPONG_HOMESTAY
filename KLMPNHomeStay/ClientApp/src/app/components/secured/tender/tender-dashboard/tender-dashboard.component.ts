import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { GlobalsService } from '../../../../services/common/globals.service';
import { Router } from '@angular/router';
import { TenderService } from '../../../../services/secured/tender.service';

@Component({
  selector: 'app-tender-dashboard',
  templateUrl: './tender-dashboard.component.html',
  styleUrls: ['./tender-dashboard.component.css']
})
export class TenderDashboardComponent implements OnInit {
  tenderList: any = [];
  p: number = 1;
  constructor(private router: Router, private globalservice: GlobalsService, private formBuilder: FormBuilder, private tenderService: TenderService) { }

  ngOnInit() {
    this.GetAllTender();
  }
  GetAllTender() {
    this.tenderService.getAllTender().subscribe(resp => {
      if (resp.result === 'Success') {
        this.tenderList = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }

    });
  }
  onTenderAdd() {
    this.router.navigate(['tender']);
  }
  back() {
    this.router.navigate(['admin-dashboard']);
  }
  viewFile(tenderId: any) {
    this.tenderService.viewTenderFile(tenderId).subscribe(res => {
      const fileURL = URL.createObjectURL(res);
      window.open(fileURL, '_blank');
    });
  }
  onClickEdit(tenderId: any) {
    this.router.navigate(['tender'], { queryParams: { tenderId: tenderId }, skipLocationChange: false });
  }
}
