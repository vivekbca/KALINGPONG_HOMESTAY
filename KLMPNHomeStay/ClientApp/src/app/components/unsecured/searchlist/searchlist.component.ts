import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { SearchModel } from '../../../model/unsecured/Search/search-model';
import { PopularityService } from '../../../services/unsecured/Popularity/popularity.service';

@Component({
  selector: 'app-searchlist',
  templateUrl: './searchlist.component.html',
  styleUrls: ['./searchlist.component.css']
})
export class SearchlistComponent implements OnInit {
  ParamID: any;
  SearchID: any;
  villagelistdata: any;
  searchForm: FormGroup;
  submitted = false;
  VillageList: any;
  BlockList: any;
  HomestayList: any;
 
  constructor(private formBuilder: FormBuilder,private route: ActivatedRoute, private popularityService: PopularityService, public router: Router, private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.popularityService.DDLBlockList().subscribe(resp => {
      this.BlockList = resp.data;
    });
    this.searchForm = this.formBuilder.group({
      blockId: ['', Validators.required],
      villageId: ['', [Validators.required]],
      homestayId: [''],
    })
    this.route.params.subscribe((params) => {
      this.ParamID = params.villageid;
      this.SearchID = params.name;
    });
    let data = {
      ID: this.ParamID,
      Name: this.SearchID
    }
    this.spinner.show()
    if (this.SearchID == 'PopularArea') {
      this.popularityService.HomestaySearch(data).subscribe(
        resp => {
          this.villagelistdata = resp.data;
          this.spinner.hide();
        });
      
    }
    if (this.SearchID == 'VillageID' || this.SearchID == 'HomestayID') {
      this.popularityService.HomestaySearch(data).subscribe(
        resp => {
          this.villagelistdata = resp.data;
          this.spinner.hide();
        });
    }
    else {
      this.popularityService.Villagelist(this.ParamID).subscribe(resp => {
        this.villagelistdata = resp.data;
        this.spinner.hide();
      });
    }
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
      let data = {
        ID: pre.villageId,
        Name: 'VillageID'
      }
      this.spinner.show();
      this.popularityService.HomestaySearch(data).subscribe(
        resp => {
          this.villagelistdata = resp.data;
          this.spinner.hide();
        });
    }
    else {
      let data = {
        ID: pre.homestayId,
        Name: 'HomestayID'
      }
      this.spinner.show();
      this.popularityService.HomestaySearch(data).subscribe(
        resp => {
          this.villagelistdata = resp.data;
          this.spinner.hide();
        });
    }

  }
  VillageChange(value: any) {    
    this.router.navigate(['/homestay-detail', { hsId: value }]);
  }
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
  onVillageChange(value: any) {
    this.popularityService.DDLHomastayList(value).subscribe(resp => {
      this.HomestayList = resp.data;
    });
  }
  packagetour() {
    this.router.navigate(['packagetour-booking']);
  }
}
