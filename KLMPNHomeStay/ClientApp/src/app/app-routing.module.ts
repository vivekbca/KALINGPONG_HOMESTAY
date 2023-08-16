import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';
import { GuestregistrationComponent } from './components/unsecured/guestregistration/guestregistration.component';
import { NoticeDashboardComponent } from './components/secured/notice/notice-dashboard/notice-dashboard.component';
import { NoticeAddComponent } from './components/secured/notice/notice-add/notice-add.component';
import { TenderDashboardComponent } from './components/secured/tender/tender-dashboard/tender-dashboard.component';
import { TenderAddComponent } from './components/secured/tender/tender-add/tender-add.component';
import { LoginComponent } from './components/unsecured/login/login.component';
import { CheckAvailabilityComponent } from './components/secured/booking/check-availability/check-availability.component';
import { BookingDetailComponent } from './components/secured/booking/booking-detail/booking-detail.component';
import { SearchlistComponent } from './components/unsecured/searchlist/searchlist.component';
import { HomestaydetailComponent } from './components/unsecured/homestaydetail/homestaydetail.component';
import { NoticeListComponent } from './components/unsecured/notice-list/notice-list.component';
import { TenderListComponent } from './components/unsecured/tender-list/tender-list.component';
import { MamberWithUsComponent } from './components/unsecured/mamber-with-us/mamber-with-us.component';
import { FeedbackComponent } from './components/secured/feedback/feedback.component';
import { UserProfileComponent } from './components/secured/user-profile/user-profile.component';
import { BookingListComponent } from './components/secured/booking-list/booking-list.component';
import { AdminLoginComponent } from './components/unsecured/admin-login/admin-login.component';
import { AdminDashboardComponent } from './components/secured/admin-dashboard/admin-dashboard.component';
import { BookingdetailsComponent } from './components/secured/bookingdetails/bookingdetails.component';
import { HSmemberDetailsComponent } from './components/secured/hsmember-details/hsmember-details.component';
import { AuthenticationGuard } from './guards/authentication.guard';
import { GuestuserHomestaysearchComponent } from './components/secured/guestuser-homestaysearch/guestuser-homestaysearch.component';
import { GuSearchlistComponent } from './components/secured/gu-searchlist/gu-searchlist.component';
import { OnlineBookingComponent } from './components/secured/online-booking/online-booking.component';
import { HsLoginComponent } from './components/unsecured/hs-login/hs-login.component';
import { HsMemberLoginComponent } from './components/secured/hs-member-login/hs-member-login.component';
import { HomestayApprovalComponent } from './components/secured/homestay-approval/homestay-approval.component';
import { HomestayapprovalDetailComponent } from './components/secured/homestayapproval-detail/homestayapproval-detail.component';
import { PackageComponent } from './components/secured/package/package.component';
import { PackagetourBookingComponent } from './components/secured/packagetour-booking/packagetour-booking.component';
import { TourDetailComponent } from './components/secured/tour-detail/tour-detail.component';
import { ViewPackageComponent } from './components/secured/view-package/view-package.component';
import { AdminTourDetailComponent } from './components/secured/admin-tour-detail/admin-tour-detail.component';
import { PackageFeedbackComponent } from './components/secured/package-feedback/package-feedback.component';
import { TourFeedbackAddComponent } from './components/secured/tour-feedback-add/tour-feedback-add.component';
import { BankuserLoginComponent } from './components/unsecured/bankuser-login/bankuser-login.component';
import { BankUserDashboardComponent } from './components/secured/bank-user-dashboard/bank-user-dashboard.component';
import { PackageDateAddComponent } from './components/secured/package-date-add/package-date-add.component';
import { HsMemberRoomAddComponent } from './components/secured/hs-member-room-add/hs-member-room-add.component';
import { PackagePaymentapprovalComponent } from './components/secured/package-paymentapproval/package-paymentapproval.component';
import { PackagePaymentapprovalBankComponent } from './components/secured/package-paymentapproval-bank/package-paymentapproval-bank.component';
import { HsPaymentapprovalComponent } from './components/secured/hs-paymentapproval/hs-paymentapproval.component';
import { HsPaymentapprovalBankComponent } from './components/secured/hs-paymentapproval-bank/hs-paymentapproval-bank.component';
import { BookingReportListComponent } from './components/secured/booking-report-list/booking-report-list.component';
import { TourBookingRefundAdminComponent } from './components/secured/tour-booking-refund-admin/tour-booking-refund-admin.component';
import { TourBookingRefundBankComponent } from './components/secured/tour-booking-refund-bank/tour-booking-refund-bank.component';
import { HsBookingRefundAdminComponent } from './components/secured/hs-booking-refund-admin/hs-booking-refund-admin.component';
import { HsBookingRefundBankComponent } from './components/secured/hs-booking-refund-bank/hs-booking-refund-bank.component';
import { ResetPasswordComponent } from './components/unsecured/reset-password/reset-password.component';
import { ResetPasswordUserComponent } from './components/unsecured/reset-password-user/reset-password-user.component';
import { PackageReportListComponent } from './components/secured/package-report-list/package-report-list.component';

const routes: Routes = [
  { path: '', component: HomepageComponent, pathMatch: 'full'},
  { path: 'guregistration', component: GuestregistrationComponent },
  { path: 'notice-dashboard', component: NoticeDashboardComponent, pathMatch: 'full' },
  { path: 'notice', component: NoticeAddComponent, pathMatch: 'full' },
  { path: 'tender-dashboard', component: TenderDashboardComponent, pathMatch: 'full' },
  { path: 'tender', component: TenderAddComponent, pathMatch: 'full' },
  { path: 'searchlist', component: SearchlistComponent, pathMatch: 'full' },
  { path: '', component: HomepageComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'check-availability', component: CheckAvailabilityComponent, pathMatch: 'full' },
  { path: 'booking-detail', component: BookingDetailComponent, pathMatch: 'full' },
  { path: 'homestay-detail', component: HomestaydetailComponent, pathMatch: 'full' },
  { path: 'user-profile', component: UserProfileComponent },
  { path: 'notice-list', component: NoticeListComponent, pathMatch: 'full' },
  { path: 'tender-list', component: TenderListComponent, pathMatch: 'full' },
  { path: 'Feedback', component: FeedbackComponent, pathMatch: 'full' },
  { path: 'booking-list', component: BookingListComponent, pathMatch: 'full' },
  { path: 'booking-details/:id', component: BookingdetailsComponent, pathMatch: 'full' },
  { path: 'adminLogin', component: AdminLoginComponent, pathMatch: 'full' },
  { path: 'admin-dashboard', component: AdminDashboardComponent, pathMatch: 'full' },
  { path: 'online-booking', component: OnlineBookingComponent, pathMatch: 'full' },
  { path: 'hs-login', component: HsLoginComponent, pathMatch: 'full' },
  { path: 'hs-member-login', component: HsMemberLoginComponent, pathMatch: 'full' },
  { path: 'Gu-Search', component: GuestuserHomestaysearchComponent, pathMatch: 'full' },
  { path: 'gusearchlist', component: GuSearchlistComponent, pathMatch: 'full' },
  { path: 'online-booking', component: OnlineBookingComponent, pathMatch: 'full' },
  { path: 'homestay-approval', component: HomestayApprovalComponent, pathMatch: 'full' },
  { path: 'homestayapproval-detail', component: HomestayapprovalDetailComponent, pathMatch: 'full' },
  { path: 'add-package', component: PackageComponent, pathMatch: 'full' },     
  { path: 'package-feedback', component: PackageFeedbackComponent, pathMatch: 'full' },
  { path: 'package-addFeedback', component: TourFeedbackAddComponent, pathMatch: 'full' },
  { path: 'mamber-with-us', component: MamberWithUsComponent },
  { path: 'hs-member-details', component: HSmemberDetailsComponent },
  { path: 'package-date-add', component: PackageDateAddComponent },
  { path: 'packagetour-booking', component: PackagetourBookingComponent, pathMatch: 'full' },
  { path: 'tour-detail', component: TourDetailComponent, pathMatch: 'full' },
  { path: 'view-package', component: ViewPackageComponent, pathMatch: 'full' },
  { path: 'admin-tour-detail', component: AdminTourDetailComponent, pathMatch: 'full' },
  { path: 'bankUserLogin', component: BankuserLoginComponent, pathMatch: 'full' },
  { path: 'bankUserDashboard', component: BankUserDashboardComponent, pathMatch: 'full' },
  { path: 'package-PaymentApproval', component: PackagePaymentapprovalComponent, pathMatch: 'full' },
  { path: 'package-PaymentApprovalBank', component: PackagePaymentapprovalBankComponent, pathMatch: 'full' },
  { path: 'booking-report-list', component: BookingReportListComponent, pathMatch: 'full' },
  { path: 'hs-PaymentApproval', component: HsPaymentapprovalComponent, pathMatch: 'full' },
  { path: 'hs-PaymentApprovalBank', component: HsPaymentapprovalBankComponent, pathMatch: 'full' },
  { path: 'package-RefundAdmin', component: TourBookingRefundAdminComponent, pathMatch: 'full' },
  { path: 'package-RefundBank', component: TourBookingRefundBankComponent, pathMatch: 'full' },
  { path: 'hs-RefundAdmin', component: HsBookingRefundAdminComponent, pathMatch: 'full' },
  { path: 'hs-RefundBank', component: HsBookingRefundBankComponent, pathMatch: 'full' },
  { path: 'reset-password', component: ResetPasswordComponent, pathMatch: 'full' },
  { path: 'reset-password-user', component: ResetPasswordUserComponent, pathMatch: 'full' },
  { path: 'bankUserDashboard', component: BankUserDashboardComponent, pathMatch: 'full' },
  { path: 'hs-room-add', component: HsMemberRoomAddComponent, pathMatch:'full' },
  
  

  { path: 'package-report-list', component: PackageReportListComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'enabled',
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
