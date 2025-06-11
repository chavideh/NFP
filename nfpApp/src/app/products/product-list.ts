import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService, Product } from '../service/product.service';

@Component({
    templateUrl: './product-list.html',
})
export class ProductListComponent implements OnInit {
    products: Product[] = [];

    constructor(private productService: ProductService, private router: Router) {}

    ngOnInit(): void {
        this.load();
    }

    load() {
        this.productService.getProducts(1, 50).subscribe((res) => {
            this.products = res.data?.items || [];
        });
    }

    delete(id: number) {
        if (!confirm('Are you sure?')) return;
        this.productService.deleteProduct(id).subscribe(() => this.load());
    }
}
