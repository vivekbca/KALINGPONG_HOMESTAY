import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { SearchModel } from '../../../model/unsecured/Search/search-model';
import { PopularityService } from '../../../services/unsecured/Popularity/popularity.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  searchForm: FormGroup;
  BlockList: any;
  VillageList: any;
  HomestayList: any;
  submitted = false;
  SearchResult: any;
 
  constructor(private router: Router, private formBuilder: FormBuilder, private popularityService: PopularityService, private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      blockId: ['', Validators.required],
      villageId: ['', [Validators.required]],
      homestayId: [''],    
    })

    this.popularityService.DDLBlockList().subscribe(resp => {
      this.BlockList = resp.data;
    });
  }
  get f() { return this.searchForm.controls; }

  onSubmit() {

    this.submitted = true;
    this.searchForm.markAllAsTouched();
    const pre = this.searchForm.value as SearchModel;
    // stop here if form is invalid
    if (this.searchForm.invalid) {
      return;
    }
    if (pre.homestayId == '') {
      this.router.navigate(['/searchlist', { villageid: pre.villageId, name: 'VillageID' }]);
    }
    else {
      this.router.navigate(['/searchlist', { villageid: pre.homestayId, name: 'HomestayID' }]);
    }
  }
  //tenderDashboard() {
  //  this.router.navigate(['tender-dashboard']);
  //}
  //noticeDashboard() {
  //  this.router.navigate(['notice-dashboard']);
  //}
  noticeList() {
    this.router.navigate(['notice-list']);
  }
  tenderList() {
    this.router.navigate(['tender-list']);
  }
  onBlockChange(value: any) {
    this.popularityService.DDLVillageList(value).subscribe(resp => {
      this.VillageList = resp.data;
    });
  }
    onVillageChange(value: any)
    {
      this.popularityService.DDLHomastayList(value).subscribe(resp => {
        this.HomestayList = resp.data;
      });
    }
  packagetour() {
    this.router.navigate(['packagetour-booking']);
  }
}
