import { Route } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { PrimeNumberComponent } from './prime-number/prime-number.component';
import { AtmComponent } from './atm/atm.component';
import { authGuard } from './core/guards/auth.guard';

export const routed: Route[] = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [authGuard] },
  {
    path: 'prime-number',
    component: PrimeNumberComponent,
    canActivate: [authGuard],
  },
  { path: 'atm', component: AtmComponent, canActivate: [authGuard] },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];
