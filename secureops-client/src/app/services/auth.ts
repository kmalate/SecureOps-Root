import { inject, Injectable, signal } from '@angular/core';
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
  currentUser = signal<any>(null);

  constructor() {
    const token = this.getAccessToken();
    if (token) {
      this.decodeAndSetUser(token);
    }
  }

  private decodeAndSetUser(token: string) {
    try {
      const decoded: any = jwtDecode(token);
      // 'decoded' will contain your C# Claims (sub, name, role, etc.)
      this.currentUser.set(decoded);
    } catch (e) {
      this.logout();
    }
  }

  login(email: string, password: string) {
    return this.http.post<TokenResponse>(`${environment.apiUrl}/authentication/login`, { email, password })
      .pipe(
        tap((response: TokenResponse) => {
          localStorage.setItem('accessToken', response.token);
          this.decodeAndSetUser(response.token);
        }
      ));
  }

  logout() {
    localStorage.removeItem('accessToken');
    this.currentUser.set(null);
  }

  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  isAuthenticated(): boolean {
    const token = this.getAccessToken();
    if (!token) {
      return false;
    }

    try {
      const decoded: any = jwtDecode(token);
      const now = Date.now().valueOf() / 1000; // Current time in seconds
      return decoded.exp > now; // Check if token is still valid
    } catch (e) {
      return false; // If token is invalid, treat as not authenticated
    }
  }

}
