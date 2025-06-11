import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface Product {
    id?: number;
    code: string;
    title: string;
    iranCode: string;
    sepidarCode: string;
    quantity: number;
    description: string;
    publish: boolean;
}

@Injectable({ providedIn: 'root' })
export class ProductService {
    private baseUrl = `${environment.apiUrl}/products`;

    constructor(private http: HttpClient) {}

    getProducts(page: number, pageSize: number, code?: string, title?: string): Observable<any> {
        const params: any = { page, pageSize };
        if (code) params.code = code;
        if (title) params.title = title;
        return this.http.get<any>(this.baseUrl, { params });
    }

    getProductById(id: number): Observable<any> {
        return this.http.get<any>(`${this.baseUrl}/${id}`);
    }

    createProduct(product: Product): Observable<any> {
        return this.http.post<any>(this.baseUrl, product);
    }

    updateProduct(product: Product): Observable<any> {
        return this.http.put<any>(this.baseUrl, product);
    }

    deleteProduct(id: number): Observable<any> {
        return this.http.delete<any>(`${this.baseUrl}/${id}`);
    }
}
