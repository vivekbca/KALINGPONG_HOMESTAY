import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalsService } from '../../../services/common/globals.service';
import { FormBuilder } from '@angular/forms';
import { HomestayApprovalService } from '../../../services/secured/homestay-approval.service';
import Swal from 'sweetalert2';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-homestay-approval',
  templateUrl: './homestay-approval.component.html',
  styleUrls: ['./homestay-approval.component.css']
})
export class HomestayApprovalComponent implements OnInit {
  hsList: any = [];
  p: number = 1;
  hsDetails: any;
  acceptTure: boolean = false;
  constructor(private router: Router, private globalservice: GlobalsService, private formBuilder: FormBuilder, private hsApprovalService: HomestayApprovalService, private spinner: NgxSpinnerService) { }


  ngOnInit() {
    this.GetAllHomeStay();
  }
  HSDetailAction(hsId :any,p:any) {
    this.hsApprovalService.GetHSById(hsId).subscribe(data => {
      let res: any = data;
      if (res.result === 'Success') {
        if (p == 1) {
          this.hsDetails = res.data;
          this.acceptTure = true;
        }
        else {
          this.hsDetails = res.data;
          this.acceptTure = false;
        }
      } else {
        this.globalservice.showMessage(res.msg, res.result);
      }
    });
  }
  GetAllHomeStay() {
    this.hsApprovalService.getAllHomeStay().subscribe(resp => {
      if (resp.result === 'Success') {
        this.hsList = resp.data;
      }
      else {
        this.globalservice.showMessage(resp.msg, resp.result);
      }

    });
  }
  accept(hsId: any) {
      let hsCompObj: any = {};
      hsCompObj.hsId = hsId;
      hsCompObj.approvalType = "A";
      hsCompObj.userId = window.sessionStorage.getItem('userId');
    
    Swal.fire({
      title: 'Are You Sure Want To Approve this Homestay ?',
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: 'Yes',
      denyButtonText: 'No',
      customClass: {
        actions: 'my-actions',
        cancelButton: 'order-1 right-gap',
        confirmButton: 'order-2',
        denyButton: 'order-3',
      }
    }).then((result) => {
      if (result.isConfirmed) {
        this.spinner.show();
        this.hsApprovalService.ApproveRejectHS(hsCompObj).subscribe(resp => {

          if (resp.result == "Success") {
            this.hsList = resp.data;
            this.spinner.hide();
            this.globalservice.swalSuccess(resp.msg);
            window.location.reload();
          }
          else {
            this.globalservice.showMessage(resp.msg, resp.result);
          }
        });
      } else if (result.isDenied) {
        Swal.fire('Changes are not saved', '', 'info')
      }
    })
  }
  reject(hsId: any) {
    let hsCompObj: any = {};
    hsCompObj.hsId = hsId;
    hsCompObj.approvalType = "R";
    hsCompObj.userId = window.sessionStorage.getItem('userId');

    Swal.fire({
      title: 'Are You Sure Want To Reject this Homestay ?',
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: 'Yes',
      denyButtonText: 'No',
      customClass: {
        actions: 'my-actions',
        cancelButton: 'order-1 right-gap',
        confirmButton: 'order-2',
        denyButton: 'order-3',
      }
    }).then((result) => {
      if (result.isConfirmed) {
        this.spinner.show();
        this.hsApprovalService.ApproveRejectHS(hsCompObj).subscribe(resp => {

          if (resp.result == "Success") {
            this.hsList = resp.data;
            this.spinner.hide();
            this.globalservice.swalSuccess(resp.msg);
            window.location.reload();
          }
          else {
            this.globalservice.showMessage(resp.msg, resp.result);
          }
        });
      } else if (result.isDenied) {
        Swal.fire('Changes are not saved', '', 'info')
      }
    })
 
  }
  view(hsId: any) {
     this.router.navigate(['homestayapproval-detail'], { queryParams: { hsId: hsId }, skipLocationChange: false });
  }
  back() {
    this.router.navigate(['admin-dashboard']);
  }
}
