import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { PopularityService } from '../services/unsecured/Popularity/popularity.service';
import { VillageService } from '../services/secured/village.service';
import { GlobalsService } from '../services/common/globals.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeService } from '../services/unsecured/home/home.service';
import { interval, merge, Observable, Subscription } from 'rxjs';
import Swal from 'sweetalert2';
/*import * as $ from "jquery";*/
declare var $: any;

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  @ViewChild("flexslider", { static: true }) flexslider: ElementRef;
  @ViewChild("parentHorizontalTab", { static: true }) parentHorizontalTab: ElementRef;
  @ViewChild("owlcarousel", { static: true }) owlcarousel: ElementRef;
  @ViewChild("loop", { static: true }) loop: ElementRef;
  @ViewChild("l1", { static: true }) l1: ElementRef;
  PupularityImage: any;
  offbeat1: any;
  offbeat2: any;
  villList: any = [];
  hsList: any = [];
  marqueeList: any = [];
  villId: any;
  TabId: any;
  Ourproperty: any;
  ourpropertydata: any
  otherProperties: any;
  iterate: any
  iterate2: any
  iterate3: any
  filteredData: any;
  newArr: any;
  marqueeHeading: any;
  bookNowForm: FormGroup;
  otherDestination: any;
  mySubscription: Subscription | undefined
  AllPopularDestination: any;
  PopularHometsayList: any;
  popDestAreaForm: FormGroup;
  submitted = false;
  constructor(public route: Router, public popularityService: PopularityService, private homeService: HomeService, private formBuilder: FormBuilder, public villageService: VillageService, public globalService: GlobalsService) { }
  
  ngOnInit() {
    this.popDestAreaForm = this.formBuilder.group({
      popularArea: ['', Validators.required],
      homestay: ['']
    })
    this.iterate = 0
    this.iterate2 = 0
    this.iterate3 = 0
    $(document).ready(function () {

      /*FLEX SLIDER*/

      $("#flexslider").flexslider({
        animation: "fade"
      });
      /*RESPONSIVE TABS*/

      $("#parentHorizontalTab").easyResponsiveTabs({
        type: 'default', //Types: default, vertical, accordion
        width: 'auto', //auto or any width like 600px
        fit: true, // 100% fit in a container
        tabidentify: 'hor_1', // The tab groups identifier
        activate: function (event) { // Callback function if tab is switched
          var $tab = $(this);

          var $info = $('#nested-tabInfo');
          var $name = $('span', $info);
          $name.text($tab.text());
          $info.show();
        }
      });
      /*owl carousel*/

      var owl = $("#owlcarousel");
      owl.owlCarousel({
        margin: 10,
        nav: true,
        loop: true,
        autoplay: true,
        responsive: {
          0: {
            items: 1
          },
          600: {
            items: 3
          },
          1000: {
            items: 4
          }
        }
      })

      /*LOOP*/
      $("#loop").textMarquee({
        mode: 'loop'
      });

    });
    
    this.popularityService.OtherDestination().subscribe(resp => {
      this.otherDestination = resp.data;
    });
    this.mySubscription = interval(30000).subscribe((x => {
      this.popularityService.OtherDestination().subscribe(resp => {
        this.otherDestination = resp.data;
      });
    }));
    this.popularityService.OtherProperties().subscribe(resp => {
      this.otherProperties = resp.data;
    });
    this.popularityService.GetAllPopularity().subscribe(resp => {
      this.PupularityImage = resp.data;
    });
    this.popularityService.offbeat1().subscribe(resp => {
      this.offbeat1 = resp.data;
    });
    this.popularityService.allPopularDestination().subscribe(resp => {
      this.AllPopularDestination = resp.data;
    });
    this.TabId = "286a5567-3a9a-413d-aec2-09b7b7836dcd";
    this.popularityService.OurProperty().subscribe(resp => {
    this.Ourproperty = resp.data;   
    });
    let property = {
      VillageCategoryId: this.TabId,
      VillageCategoryName: null
    }
    this.popularityService.InserOurPropertyByID(property).subscribe(resp => {
      this.filteredData = resp.data;
      this.ourpropertydata = this.filteredData.reduce((accumalator, current) => {
        if (!accumalator.some(item => item.hsId === current.hsId)) {
          accumalator.push(current);
        }
        return accumalator;
      }, []);
      console.log("OurPropertyDataonload", resp)
    });   
    this.getAllVill();
    this.initForm();
    this.getAllMarquee();
  }
 
  get f() { return this.bookNowForm.controls; }
  getAllVill() {
    this.villageService.getAllVillage().subscribe(resp => {
      if (resp.result === 'Success') {
        this.villList = resp.data.result;
      }
      else {
        this.globalService.showMessage(resp.msg, resp.result);
      }

    });
  }
  onChange(value: any) {
    let property = {
      VillageCategoryId: value,
      VillageCategoryName: null
    }
    this.popularityService.InserOurPropertyByID(property).subscribe(resp => {
      this.ourpropertydata = resp.data;
    });
  }
  villChange(villId: any) {
    //alert(villId);
    this.submitted = false;
    this.villageService.getAllHSByVillId(villId).subscribe(resp => {
      if (resp.result === 'Success') {
        this.hsList = resp.data;
      }
      else {
        this.globalService.showMessage(resp.msg, resp.result);
      }
    });
  }
  initForm() {
    this.bookNowForm = this.formBuilder.group({
      villId: ['', Validators.required],
      hsId: ['', Validators.required],
    });
  }
  OnBooking() {
    this.submitted = true;
    if (this.bookNowForm.invalid) {
      return;
    } else {
      let bookCompObj: any = {};
      bookCompObj.villId = this.f.villId.value;
      bookCompObj.hsId = this.f.hsId.value;
      this.homeService.fetchAvail(bookCompObj).subscribe(
        resp => {
          this.globalService.showMessage(resp.msg, resp.result);
          if (resp.result === 'Success') {

            //alert("Saved Successfully");
            //this.globalservice.swalSuccess("Tender Add Successfully");
            this.route.navigate(['/check-availability', { hsId: resp.data.hsId }]);
          }
        });
    }
  }
  OffbitChange(value: any) {
    this.route.navigate(['/homestay-detail', { hsId: value }]);
  }
  VillageChange(value: any) {
    this.route.navigate(['/searchlist', { villageid: value, name: 11 }]);
  }
  onPopularChange(value: any) {
    this.popularityService.PopularHomestay(value).subscribe(resp => {
      this.PopularHometsayList = resp.data;
    });
  }
  onHomestayChange(value: any) {
    this.route.navigate(['/searchlist', { villageid: value, name: 'HomestayID' }]);
  }
  get f1() { return this.popDestAreaForm.controls; }
  PopDestArea() {
    this.submitted = true;
    this.popDestAreaForm.markAllAsTouched();
    if (this.popDestAreaForm.invalid) {
      return;
    }
    let data = {
      populararea: this.popDestAreaForm.value.popularArea,
      homestayid: this.popDestAreaForm.value.homestay
    }
    if (data.homestayid == '') {
      this.route.navigate(['/searchlist', { villageid: data.populararea, name: 'PopularArea' }]);
    }
    else {
      this.route.navigate(['/searchlist', { villageid: data.homestayid, name: 'HomestayID' }]);
    }
    

  }
  getAllMarquee() {
    this.homeService.marqueeList().subscribe(resp => {
      if (resp.result === 'Success') {
        let marqueeHeading = resp.data.heading;
        this.marqueeHeading = marqueeHeading;
      }
      else {
        this.globalService.showMessage(resp.msg, resp.result);
      }
    });
  }
  packageTourList() {
    this.route.navigate(['packagetour-booking']);
  }
}
  

