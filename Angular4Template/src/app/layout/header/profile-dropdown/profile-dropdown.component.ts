import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { debounce } from 'Rxjs/operators/debounce';

@Component({
  selector: 'app-profile-dropdown',
  templateUrl: './profile-dropdown.component.html',
  styleUrls: ['./profile-dropdown.component.scss']
})
export class ProfileDropdownComponent implements OnInit {
  @Input() isAuthenticated: boolean;

  constructor(private authService: AuthService) {
  }

  ngOnInit() {
  }
  
  logout() {
    this.authService.logout().subscribe(data => {
      console.log('Logout Success');
    });
  }
}
