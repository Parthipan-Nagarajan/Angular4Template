import { HomeComponent } from './home.component';
import { CanActivateViaAuthGuard } from '../core/guards/auth.guard';


export const HomeRoutes = [
  { path: '', component: HomeComponent },
];
