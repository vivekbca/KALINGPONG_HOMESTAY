import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { FormBuilder } from '@angular/forms';
import { TenderService } from '../../../services/secured/tender.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-tender-list',
  templateUrl: './tender-list.component.html',
  styleUrls: ['./tender-list.component.css']
})
export class TenderListComponent implements OnInit {

  tenderList: any = [];
  p: number = 1;
  constructor(private router: Router, private globalservice: GlobalsService, private spinner: NgxSpinnerService, private formBuilder: FormBuilder, private tenderService: TenderService) { }

  ngOnInit() {
    this.GetAllTender();
  }
  GetAllTender() {
    this.spinner.show();
    this.tenderService.getAllTender().subscribe(resp => {
      if (resp.result === 'Success') {
        this.tenderList = resp.data;
        this.spinner.hide();
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }

    });
  }
  viewFile(tenderId: any) {
    this.tenderService.viewTenderFile(tenderId).subscribe(res => {
      const fileURL = URL.createObjectURL(res);
      window.open(fileURL, '_blank');
    });
  }
}
