import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { SharedModule } from './../shared/shared.module';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
// Components
import { HomeComponent } from './home.component';

// Routes
import { HomeRoutes as routes } from './home.routes';

@NgModule({
  declarations: [
    HomeComponent
  ],
  exports: [  ],
  imports: [
    RouterModule.forChild(routes),
    NgbCarouselModule.forRoot(),
    SharedModule,
  ],
  providers: [   ]
})
export class HomeModule {}
