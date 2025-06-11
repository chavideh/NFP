import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private baseUrl = `${environment.apiUrl}/auth`;

    constructor(private http: HttpClient, private router: Router) {}

    login(username: string, password: string): Observable<any> {
        return this.http
            .post<any>(`${this.baseUrl}/login`, { username, password })
            .pipe(
                tap((res) => {
                    const token = res?.data?.token;
                    if (token) {
                        localStorage.setItem('token', token);
                    }
                }),
            );
    }

    logout() {
        localStorage.removeItem('token');
        this.router.navigate(['/auth/cover-login']);
    }

    isAuthenticated(): boolean {
        return !!localStorage.getItem('token');
    }
}
