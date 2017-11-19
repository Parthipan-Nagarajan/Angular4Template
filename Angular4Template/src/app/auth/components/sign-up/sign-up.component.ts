import { Component, OnInit, OnDestroy } from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import { environment} from '../../../../environments/environment';
import {Subscription} from 'Rxjs';
import {Store} from '@ngrx/store';
import {AuthService} from '../../../core/services/auth.service';
import {Router} from '@angular/router';
import {AppState} from '../../../app.state.interface';
import {getAuthStatus} from '../../reducers/auth.selector';
import {ViewEncapsulation} from '@angular/core';
import {routerTransition} from '../../../shared/loaders/angular.animations';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
  animations: [routerTransition()],
  encapsulation: ViewEncapsulation.None
})
export class SignUpComponent implements OnInit, OnDestroy {
  signUpForm: FormGroup;
  formSubmit = false;
  title = environment.AppName;
  registerSubs: Subscription;

  constructor(
    private fb: FormBuilder,
    private store: Store<AppState>,
    private router: Router,
    private authService: AuthService
  ) {
    this.redirectIfUserLoggedIn();
  }

  ngOnInit() {
    this.initForm();
  }

  onSubmit() {
    const values = this.signUpForm.value;
    const keys = Object.keys(values);
    this.formSubmit = true;

    if (this.signUpForm.valid) {
      this.registerSubs = this.authService.register(values).subscribe(data => {
        const errors = data.ValidationErrors;
        if (errors) {
          keys.forEach(val => {
            if (errors[val]) { this.pushErrorFor(val, errors[val][0]); }
          });
        }
      });
    } else {
      keys.forEach(val => {
        const ctrl = this.signUpForm.controls[val];
        if (!ctrl.valid) {
          this.pushErrorFor(val, null);
          ctrl.markAsTouched();
        }
      });
    }
  }

  private pushErrorFor(ctrl_name: string, msg: string) {
    this.signUpForm.controls[ctrl_name].setErrors({'msg': msg});
  }

  initForm() {
    const emailAddress = '';
    const password = '';
    const passwordConfirmation = '';
    const firstName = '';
    const lastName = '';
    this.signUpForm = this.fb.group({
      'emailAddress': [emailAddress, Validators.compose([Validators.required, Validators.email]) ],
      'firstName': [firstName, Validators.required],
      'lastName': [lastName, Validators.required],
      'password': [password, Validators.compose([Validators.required, Validators.minLength(6)]) ],
      'passwordConfirmation': [passwordConfirmation, Validators.compose([Validators.required, Validators.minLength(6)]) ],
         }, {validator: this.matchingPasswords('password', 'passwordConfirmation')});
  }

  redirectIfUserLoggedIn() {
    this.store.select(getAuthStatus).subscribe(
      data => {
        if (data === true) { this.router.navigateByUrl('/'); }
      }
    );
  }

  ngOnDestroy() {
    if (this.registerSubs) { this.registerSubs.unsubscribe(); }
  }

  matchingPasswords(passwordKey: string, confirmPasswordKey: string) {
  return (group: FormGroup): {[key: string]: any} => {
    const password = group.controls[passwordKey];
    const confirmPassword = group.controls[confirmPasswordKey];

    if (password.value !== confirmPassword.value) {
      return {
        mismatchedPasswords: true
      };
    }
  };
}
}
