import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs';
import { TokenResponse } from '../model/token-response';
import {jwtDecode} from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private http = inject(HttpClient);

  login(email: string, password: string) {
    return this.http.post<TokenResponse>(`${environment.apiUrl}/login`, { email, password })
      .pipe(
        tap((response: TokenResponse) => {
          localStorage.setItem('accessToken', response.accessToken);
          localStorage.setItem('refreshToken', response.refreshToken);
          var test = jwtDecode(response.accessToken);
          // var test3 = '';
        }
      ));
  }

  logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }

  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }
}
