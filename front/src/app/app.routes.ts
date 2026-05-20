import { Route } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { PrimeNumberComponent } from './prime-number/prime-number.component';
import { AtmComponent } from './atm/atm.component';

export const routed: Route[] = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'prime-number', component: PrimeNumberComponent },
  { path: 'atm', component: AtmComponent },
];
