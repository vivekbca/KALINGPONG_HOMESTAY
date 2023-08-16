import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule, ɵNgSelectMultipleOption } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
/*import { RouterModule } from '@angular/router';*/
import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LoaderService } from './services/common/loader.service';
/*import { GlobalsService } from './services/common/globals.service';*/
import { FooterComponent } from './components/common/footer/footer.component';
import { HeaderComponent } from './components/common/header/header.component';
import { LoaderComponent } from './components/common/loader/loader.component';
import { ValidationMessagesComponent } from './components/common/validation-messages/validation-messages.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './components/unsecured/login/login.component';
import { AppRoutingModule } from './app-routing.module';
import { BackButtonDirective } from './directives/common/back-button.directive';
import { SortableTableDirective } from './directives/common/sorting/sortable-table.directive';
import { NoticeDashboardComponent } from './components/secured/notice/notice-dashboard/notice-dashboard.component';
import { NoticeAddComponent } from './components/secured/notice/notice-add/notice-add.component';
import { TenderDashboardComponent } from './components/secured/tender/tender-dashboard/tender-dashboard.component';
import { TenderAddComponent } from './components/secured/tender/tender-add/tender-add.component';
import { GuestregistrationComponent } from './components/unsecured/guestregistration/guestregistration.component';
import { CheckAvailabilityComponent } from './components/secured/booking/check-availability/check-availability.component';
import { BookingDetailComponent } from './components/secured/booking/booking-detail/booking-detail.component';
import { NoticeListComponent } from './components/unsecured/notice-list/notice-list.component';
import { TenderListComponent } from './components/unsecured/tender-list/tender-list.component';
import { UserProfileComponent } from './components/secured/user-profile/user-profile.component';
import { HomestaydetailComponent } from './components/unsecured/homestaydetail/homestaydetail.component';
import { SearchlistComponent } from './components/unsecured/searchlist/searchlist.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FeedbackComponent } from './components/secured/feedback/feedback.component';
import { AdminLoginComponent } from './components/unsecured/admin-login/admin-login.component';
import { AdminDashboardComponent } from './components/secured/admin-dashboard/admin-dashboard.component';
import { MamberWithUsComponent } from './components/unsecured/mamber-with-us/mamber-with-us.component';
import { HSmemberDetailsComponent } from './components/secured/hsmember-details/hsmember-details.component';
import { BookingListComponent } from './components/secured/booking-list/booking-list.component';
import { BookingdetailsComponent } from './components/secured/bookingdetails/bookingdetails.component';
import { LoginHeaderComponent } from './components/common/login-header/login-header.component';
import { OnlineBookingComponent } from './components/secured/online-booking/online-booking.component';
import { HsLoginComponent } from './components/unsecured/hs-login/hs-login.component';
import { HsMemberLoginComponent } from './components/secured/hs-member-login/hs-member-login.component';
import { GuestuserHomestaysearchComponent } from './components/secured/guestuser-homestaysearch/guestuser-homestaysearch.component';
import { GuSearchlistComponent } from './components/secured/gu-searchlist/gu-searchlist.component';
import { HomestayApprovalComponent } from './components/secured/homestay-approval/homestay-approval.component';
import { HomestayapprovalDetailComponent } from './components/secured/homestayapproval-detail/homestayapproval-detail.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { PackageComponent } from './components/secured/package/package.component';
import { PackagetourBookingComponent } from './components/secured/packagetour-booking/packagetour-booking.component';
import { TourDetailComponent } from './components/secured/tour-detail/tour-detail.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ViewPackageComponent } from './components/secured/view-package/view-package.component';
import { AdminTourDetailComponent } from './components/secured/admin-tour-detail/admin-tour-detail.component';
import { PackageFeedbackComponent } from './components/secured/package-feedback/package-feedback.component';
import { TourFeedbackAddComponent } from './components/secured/tour-feedback-add/tour-feedback-add.component';
import { BankuserLoginComponent } from './components/unsecured/bankuser-login/bankuser-login.component';
import { BankUserDashboardComponent } from './components/secured/bank-user-dashboard/bank-user-dashboard.component';
import { PackagePaymentapprovalComponent } from './components/secured/package-paymentapproval/package-paymentapproval.component';
import { PackagePaymentapprovalBankComponent } from './components/secured/package-paymentapproval-bank/package-paymentapproval-bank.component';
import { BookingReportListComponent } from './components/secured/booking-report-list/booking-report-list.component';
import { DatePipe } from '@angular/common';

import { HsPaymentapprovalComponent } from './components/secured/hs-paymentapproval/hs-paymentapproval.component';
import { HsPaymentapprovalBankComponent } from './components/secured/hs-paymentapproval-bank/hs-paymentapproval-bank.component';
import { TourBookingRefundAdminComponent } from './components/secured/tour-booking-refund-admin/tour-booking-refund-admin.component';
import { TourBookingRefundBankComponent } from './components/secured/tour-booking-refund-bank/tour-booking-refund-bank.component';
import { HsBookingRefundAdminComponent } from './components/secured/hs-booking-refund-admin/hs-booking-refund-admin.component';
import { HsBookingRefundBankComponent } from './components/secured/hs-booking-refund-bank/hs-booking-refund-bank.component';
import { PackageDateAddComponent } from './components/secured/package-date-add/package-date-add.component';
import { HsMemberRoomAddComponent } from './components/secured/hs-member-room-add/hs-member-room-add.component';
import { ResetPasswordComponent } from './components/unsecured/reset-password/reset-password.component';
import { ResetPasswordUserComponent } from './components/unsecured/reset-password-user/reset-password-user.component';
import { PackageReportListComponent } from './components/secured/package-report-list/package-report-list.component';




@NgModule({
  declarations: [
    GuestregistrationComponent,
    AppComponent,
    CounterComponent,
    HomepageComponent,
    FooterComponent,
    HeaderComponent,
    LoaderComponent,
    ValidationMessagesComponent,
    LoginComponent,
    BackButtonDirective,
    SortableTableDirective,
    NoticeDashboardComponent,
    NoticeAddComponent,
    TenderDashboardComponent,
    CheckAvailabilityComponent,
    BookingDetailComponent,   
    UserProfileComponent,
    NoticeListComponent,
    TenderListComponent,
    BookingdetailsComponent,
    MamberWithUsComponent,
    HomestaydetailComponent,
    SearchlistComponent,
    FeedbackComponent,
    AdminLoginComponent,
    AdminDashboardComponent,
    BookingdetailsComponent,
    LoginHeaderComponent,
    OnlineBookingComponent,
    HsLoginComponent,
    HsMemberLoginComponent,
    GuestuserHomestaysearchComponent,
    GuSearchlistComponent,
    HomestayApprovalComponent,
    HomestayapprovalDetailComponent,
    SearchlistComponent,
    HSmemberDetailsComponent,   
    HomestayapprovalDetailComponent,
    TenderAddComponent,
    BookingListComponent,
    PackageComponent,
    PackagetourBookingComponent,
    TourDetailComponent,
    ViewPackageComponent,
    AdminTourDetailComponent,
    PackageFeedbackComponent,
    TourFeedbackAddComponent,
    BankuserLoginComponent,
    BankUserDashboardComponent,
    PackagePaymentapprovalComponent,
    PackagePaymentapprovalBankComponent,
    BookingReportListComponent,
    HsPaymentapprovalComponent,
    HsPaymentapprovalBankComponent,
    TourBookingRefundAdminComponent,
    TourBookingRefundBankComponent,
    HsBookingRefundAdminComponent,
    HsBookingRefundBankComponent,
    PackageDateAddComponent,
    HsMemberRoomAddComponent,
    ResetPasswordComponent,
    ResetPasswordUserComponent,
    PackageReportListComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    NgxPaginationModule,
    NgMultiSelectDropDownModule.forRoot()
  ],
  providers: [
    LoaderService,
    DatePipe
    //GlobalsService,
    //{
    //  provide: HTTP_INTERCEPTORS,
    //  //useClass: AuthInterceptor,
    //  multi: true
    //},
  
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
