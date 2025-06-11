import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SharedModule } from 'src/shared.module';

import { ProductListComponent } from './product-list';
import { ProductFormComponent } from './product-form';

const routes: Routes = [
    { path: 'products', component: ProductListComponent, data: { title: 'Products' } },
    { path: 'products/new', component: ProductFormComponent, data: { title: 'Create Product' } },
    { path: 'products/:id', component: ProductFormComponent, data: { title: 'Edit Product' } },
];

@NgModule({
    imports: [RouterModule.forChild(routes), CommonModule, FormsModule, SharedModule.forRoot()],
    declarations: [ProductListComponent, ProductFormComponent],
})
export class ProductModule {}
