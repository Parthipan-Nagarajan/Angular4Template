import { Injectable } from '@angular/core';
import { HttpService } from '../../core/services/http';
import { UserActions } from '../actions/user.actions';
import { Store } from '@ngrx/store';
import { AppState } from '../../app.state.interface';
import { Order } from '../../entities/order.entity';
import { Response } from '@angular/http';
import { User } from '../../entities/user.entity';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class UserService {

  constructor(
    private http: HttpService,
    private actions: UserActions,
    private store: Store<AppState>
  ) { }

  /**
   *
   *
   * @returns {Observable<Order[]>}
   *
   * @memberof UserService
   */
  getOrders(): Observable<Order[]> {
    return this.http.get('api/orders')
      .map((res: Response) => res.json());
  }

  /**
   *
   *
   * @param {any} orderNumber
   * @returns {Observable<Order>}
   *
   * @memberof UserService
   */
  getOrderDetail(orderNumber): Observable<Order> {
    return this.http.get(`api/orders/${orderNumber}`)
      .map((res: Response) => res.json());
  }

  /**
   *
   *
   * @returns {Observable<User>}
   *
   * @memberof UserService
   */
  getUser(): Observable<User> {
    const user_id = JSON.parse(localStorage.getItem('user')).id;
    return this.http.get(`api/customer/${user_id}`)
      .map(res => res.json());
  }

}
