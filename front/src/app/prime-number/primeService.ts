import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PrimeService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5255/api/Prime/calculate';

  calculate(numberInput: number): Observable<any> {
    const requestData = { numberInput: numberInput };
    return this.http.post(this.apiUrl, requestData);
  }
}
