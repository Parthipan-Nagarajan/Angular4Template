import { RouterModule, Routes } from '@angular/router';
import { CanActivateViaAuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadChildren: './home/home.module#HomeModule',
    data: { preload: true }
  },
  {
    path: 'auth',
    loadChildren: './auth/auth.module#AuthModule',
    data: { preload: true }
  },
  {
    path: 'user',
    loadChildren: './user/user.module#UserModule',
    canActivate: [ CanActivateViaAuthGuard ]
  },
];
