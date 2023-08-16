import { Component, OnInit } from '@angular/core';
import { PackageDateAdd } from '../../../services/secured/package-date-add.service';
import { GlobalsService } from '../../../services/common/globals.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';

@Component({
  selector: 'app-package-date-add',
  templateUrl: './package-date-add.component.html',
  styleUrls: ['./package-date-add.component.css']
})
export class PackageDateAddComponent implements OnInit {
  tourList: any = [];
  tourDetail: any;
  tourDateAddForm: FormGroup;


  constructor(private formBuilder: FormBuilder,private dateAddService: PackageDateAdd, private spinner: NgxSpinnerService, private globalservice: GlobalsService, private router: Router) { }
  get DateFormAdd()
  {
    return this.tourDateAddForm.controls;
  }
  GetAllTour() {
    this.spinner.show();
    this.dateAddService.getAllTour().subscribe(resp => {
      if (resp.result === 'Success') {
        this.tourList = resp.data;
        this.spinner.hide();
        console.log(resp.data);
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }
    }
    )
  }
  ngOnInit() {
    this.GetAllTour();

    this.tourDateAddForm = this.formBuilder.group({
      dateAddArray: this.formBuilder.array([]),
    });

    this.addDates();
  }

  get dateAddArray(): FormArray {
    return this.tourDateAddForm.get("dateAddArray") as FormArray
  }
  addDates() {
    this.dateAddArray.push(this.newdateAddArray());
  }
  removeDates(this) {
    console.log(this);
  }
  saveDate() {
    console.log(this.dateAddArray.value);
  }
  newdateAddArray(): FormGroup {
    return this.formBuilder.group({
      fromDate: '',
      toDate: '',
      isActive: ''
    });
  }

  

  
}
