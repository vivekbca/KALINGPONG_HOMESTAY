import { Directive, Output, OnInit, OnDestroy, EventEmitter } from '@angular/core';
import { Subscription } from 'rxjs';
import { SortService } from '../../../services/common/sort.service';

//@Directive({
//  selector: '[appSortableTable]'
//})
@Directive({
  selector: '[sortable-table]'
})
export class SortableTableDirective implements OnInit, OnDestroy {

  constructor(private sortService: SortService) { }

  @Output()
  sorted = new EventEmitter();

  private columnSortedSubscription: Subscription;

  ngOnInit() {
    this.columnSortedSubscription = this.sortService.columnSorted$.subscribe(event => {
      this.sorted.emit(event);
    });
  }

  ngOnDestroy() {
    this.columnSortedSubscription.unsubscribe();
  }

}
