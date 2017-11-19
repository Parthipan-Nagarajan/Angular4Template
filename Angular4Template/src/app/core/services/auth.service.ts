import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';
import { HttpService } from './http';
import { AppState } from '../../app.state.interface';
import { Store } from '@ngrx/store';
import { AuthActions } from '../../auth/actions/auth.actions';
import { User } from '../../entities/user.entity';

@Injectable()
export class AuthService {

  /**
   * Creates an instance of AuthService.
   * @param {HttpService} http
   * @param {AuthActions} actions
   * @param {Store<AppState>} store
   *
   * @memberof AuthService
   */
  constructor(
    private http: HttpService,
    private actions: AuthActions,
    private store: Store<AppState>
  ) {

  }

  /**
   *
   *
   * @param {any} data
   * @returns {Observable<any>}
   *
   * @memberof AuthService
   */
  login(data: User): Observable<any> {
    const body = JSON.stringify(data);
    return this.http.post('customer/login', body).map((res: Response) => {
      data = res.json();
      if (data.returnStatus) {
          this.http.loading.next({
            loading: false,
            hasError: false,
            hasMsg: data.returnMessage.join(',')
          });
        this.setTokenInLocalStorage(data);
        this.store.dispatch(this.actions.loginSuccess());
      } else {
        this.http.loading.next({
            loading: false,
            hasError: true,
            hasMsg: data.returnMessage.join(',')
          });
      }
      return data;
    });
  }

  /**
   *
   *
   * @param {any} data
   * @returns {Observable<any>}
   *
   * @memberof AuthService
   */
  register(data: User): Observable<any> {
    return this.http.post(
      'customer/registerCustomer', data
    ).map((res: Response) => {
      data = res.json();
      if (data.returnStatus) {
          this.http.loading.next({
            loading: false,
            hasError: false,
            hasMsg: data.returnMessage.join(',')
          });
        this.setTokenInLocalStorage(data);
        this.store.dispatch(this.actions.loginSuccess());
      } else {
        this.http.loading.next({
          loading: false,
          hasError: true,
          hasMsg: data.returnMessage.join(',')
        });
      }
      return data;
    });
  }

  /**
   *
   *
   * @returns {Observable<any>}
   *
   * @memberof AuthService
   */
  authorized(): Observable<any> {
    return this.http
      .get('customer/authenticate')
      .map((res: Response) => {
        const data = res.json();
        if (data.returnStatus) {
            this.http.loading.next({
              loading: false,
              hasError: false,
              hasMsg: data.returnMessage.join(',')
            });
          this.setTokenInLocalStorage(data);
          this.store.dispatch(this.actions.loginSuccess());
        } else {
          this.http.loading.next({
            loading: false,
            hasError: true,
            hasMsg: data.returnMessage.join(',')
          });
        }
        return data;
      });
  }

  /**
   *
   *
   * @returns
   *
   * @memberof AuthService
   */
  logout() {
    return this.http.get('customer/logout')
      .map((res: Response) => {
        const data = res.json();
        if (data.returnStatus) {
            this.http.loading.next({
              loading: false,
              hasError: false,
              hasMsg: data.returnMessage.join(',')
            });
          this.removeTokenFromLocalStorage();
          this.store.dispatch(this.actions.logoutSuccess());
        } else {
          this.http.loading.next({
            loading: false,
            hasError: true,
            hasMsg: data.returnMessage.join(',')
          });
        }
        return data;
      });
  }

  /**
   *
   *
   * @private
   * @param {any} user
   *
   * @memberof AuthService
   */
  private setTokenInLocalStorage(user): void {
    const jsonData = JSON.stringify(user);
    localStorage.setItem('user', jsonData);
  }
/**
 * @private
 * @memberOf AuthService
 */
  private removeTokenFromLocalStorage(): void {
    localStorage.removeItem('user');
  }
}
