import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { SearchModel } from '../../../model/unsecured/Search/search-model';
import { PopularityService } from '../../../services/unsecured/Popularity/popularity.service';

@Component({
  selector: 'app-guestuser-homestaysearch',
  templateUrl: './guestuser-homestaysearch.component.html',
  styleUrls: ['./guestuser-homestaysearch.component.css']
})
export class GuestuserHomestaysearchComponent implements OnInit {
  searchForm: FormGroup;
  BlockList: any;
  VillageList: any;
  HomestayList: any;
  submitted = false;
  SearchResult: any;
  popularHomestay: any;
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
    this.popularityService.popularHomestayGU().subscribe(resp => {
      this.popularHomestay = resp.data;
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
      this.router.navigate(['/gusearchlist', { villageid: pre.villageId, name: 'VillageID' }]);
    }
    else {
      this.router.navigate(['/gusearchlist', { villageid: pre.homestayId, name: 'HomestayID' }]);
    }
  }
  onBlockChange(value: any) {
    this.popularityService.DDLVillageList(value).subscribe(resp => {
      this.VillageList = resp.data;
    });
  }
  onVillageChange(value: any) {
    this.popularityService.DDLHomastayList(value).subscribe(resp => {
      this.HomestayList = resp.data;
    });
  }
}
