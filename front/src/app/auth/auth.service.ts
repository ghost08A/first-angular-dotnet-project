import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private httpClient = inject(HttpClient);
  // Backend launchSettings.json exposes HTTP on 5255 (and HTTPS on 7190).
  // Using HTTP in dev avoids local HTTPS cert issues.
  private apiUrl = 'http://localhost:5255/api/Auth';

  login(email: string, password: string) {
    return this.httpClient.post(`${this.apiUrl}/login`, {
      Email: email,
      Password: password,
    });
  }
}
