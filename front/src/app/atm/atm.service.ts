import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AtmService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5255/api/Atm/withdraw';

  withdraw(amount: number): Observable<any> {
    const requestData = { Amount: amount };
    return this.http.post(this.apiUrl, requestData);
  }
}
