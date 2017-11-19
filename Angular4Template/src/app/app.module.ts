import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, PreloadingStrategy } from '@angular/router';
import { ModulePreloader } from './shared/loaders/module.preloader';
import { CanActivateViaAuthGuard } from './core/guards/auth.guard';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// Components
import { AppComponent } from './app.component';
// Routes
import { routes } from './app.routes';
// Modules
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './home/home.module';
import { LayoutModule } from './layout/layout.module';
import { CoreModule } from './core/core.module';
import { StoreModule } from '@ngrx/store';
import { reducer } from './app.reducers';

// adding rx operators
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/finally';
import 'rxjs/add/observable/of';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: ModulePreloader }),
    StoreModule.provideStore(reducer),
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpModule,
    HomeModule,
    LayoutModule,
    CoreModule,
    SharedModule
  ],
  providers: [ModulePreloader],
  bootstrap: [AppComponent]
})
export class AppModule { }
