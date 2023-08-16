import { Injectable } from '@angular/core'
import { Location } from '@angular/common'
import { Router, NavigationEnd } from '@angular/router'

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  private history: string[] = []

  constructor(private router: Router, private location: Location) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.history.push(event.urlAfterRedirects)
      }
    })
  }

  back(): void {
    this.history.pop();
    let url: string = 'home';
    if (this.history.length > 0) {
      url = this.history[this.history.length - 1];
    }
    console.log(url);
    this.router.navigate([url]);
  }
}
