import { RouterModule } from '@angular/router';
import { UserRoutes } from './user.routes';
import { NgModule } from '@angular/core';

// components
import { ProfileComponent } from './components/profile/profile.component';
import { UserComponent } from './user.component';

// services
// import { UserService } from './services/user.service';

import { UserRoutes as routes } from './user.routes';
import { AddressesComponent } from './components/addresses/addresses.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    // components
    ProfileComponent,
    UserComponent,
    AddressesComponent
    // pipes

  ],
  exports: [
    // components
    // OverviewComponent,
    // OrderListItemComponent,
    // OrdersComponent,
    // ReturnsComponent,
    // ReturnListItemComponent,

  ],
  providers: [
  ],
  imports: [
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class UserModule {}
