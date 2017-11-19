import { NgModule } from '@angular/core';

// Components
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import {ProfileDropdownComponent} from './header/profile-dropdown/profile-dropdown.component';

// Modules
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    // components
    HeaderComponent,
    FooterComponent,
    ProfileDropdownComponent
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    ProfileDropdownComponent
  ],
  imports: [
    SharedModule,
    RouterModule
  ]
})
export class LayoutModule {}
