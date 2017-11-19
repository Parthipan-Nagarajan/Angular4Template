import { ProfileComponent } from './components/profile/profile.component';
import { UserComponent } from './user.component';
import { AddressesComponent } from './components/addresses/addresses.component';

export const UserRoutes = [
  {
    path: '',
    component: UserComponent,
    children: [
      { path: '', redirectTo: 'profile' },
      { path: 'profile', component: ProfileComponent },
      { path: 'addresses', component: AddressesComponent}
    ]
  },
];
