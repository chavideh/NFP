import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface User {
    id?: string;
    userName: string;
    firstName: string;
    lastName: string;
    nationalCode: string;
    email: string;
    password?: string;
    phoneNumber?: string;
}

@Injectable({ providedIn: 'root' })
export class UserService {
    private baseUrl = `${environment.apiUrl}`;

    constructor(private http: HttpClient) {}

    getUsers(page: number, pageSize: number): Observable<any> {
        const params: any = { page, pageSize };
        return this.http.get<any>(`${this.baseUrl}/users`, { params });
    }

    createUser(user: User): Observable<any> {
        const payload = {
            username: user.userName,
            firstName: user.firstName,
            lastName: user.lastName,
            nationalCode: user.nationalCode,
            password: user.password,
            email: user.email,
        };
        return this.http.post<any>(`${this.baseUrl}/auth/register`, payload);
    }
}
