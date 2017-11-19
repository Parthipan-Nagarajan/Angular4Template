import { Component, OnInit, OnDestroy, transition } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { HttpService } from '../../../core/services/http';
import { Subject } from 'rxjs/Subject';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit, OnDestroy {
  loading: any;
  notiSubs: Subscription;

  constructor(private httpInterceptor: HttpService) {
    this.notiSubs = this.httpInterceptor.loading.subscribe(
      data => this.loading = data
    );
  }

  ngOnInit() {
  }

  ngOnDestroy() {
  }
}
