import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, NavigationEnd } from '@angular/router';
import { Observable } from 'rxjs';
import { MenuModel } from '../model/secured/common/menu-model';
import { AuthGuardService } from '../services/unsecured/authentication/auth-guard.service';
import { filter } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {
  private isChecked: boolean;
    //authGuardService: any;
  constructor(private authGuardService: AuthGuardService, private router: Router) { }

  //canActivate(
  //  next: ActivatedRouteSnapshot,
  //  state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
  //  return true;
  //}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const hasToken = this.authGuardService.getToken();
    if (!hasToken) {
      this.router.navigateByUrl("/adminLogin");
    }
    else {
      const menuArr = this.authGuardService.getUsrMenu() as MenuModel[];
      this.isChecked = false;

      this.router.events
        .pipe(
          filter(event => event instanceof NavigationEnd)
        )
        .subscribe(
          (event: NavigationEnd) => {
            if (event.url != '/') {
              if (event.url != '/home') {
                if (event.url != '/adminLogin') {
                  if (!this.isChecked) {
                    this.isChecked = true;
                    const testUrl = '.' + event.url;

                    var isPresent = menuArr.some(function (el) { return el.url === testUrl });
                    if (!isPresent)
                      this.authGuardService.Logout();
                    //console.log(window.location.pathname);

                  }

                }
              }


            }
          }
        )

    }
    return hasToken;
  }
  
}
