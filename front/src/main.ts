import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';

import { AppComponent } from './app/app.component';
import { routed } from './app/app.routes';

bootstrapApplication(AppComponent, {
  providers: [provideRouter(routed), provideHttpClient()],
}).catch((err) => console.error(err));
