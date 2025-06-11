import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService, Product } from '../service/product.service';

@Component({
    templateUrl: './product-form.html',
})
export class ProductFormComponent implements OnInit {
    product: Product = {
        code: '',
        title: '',
        iranCode: '',
        sepidarCode: '',
        quantity: 0,
        description: '',
        publish: false,
    };

    constructor(private route: ActivatedRoute, private router: Router, private productService: ProductService) {}

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');
        if (id) {
            this.productService.getProductById(+id).subscribe((res) => {
                if (res.data) this.product = res.data;
            });
        }
    }

    save() {
        if (this.product.id) {
            this.productService.updateProduct(this.product).subscribe(() => this.router.navigate(['/products']));
        } else {
            this.productService.createProduct(this.product).subscribe(() => this.router.navigate(['/products']));
        }
    }
}
