import { Routes } from '@angular/router';

import { AppLayout } from './layouts/app-layout';
import { AuthLayout } from './layouts/auth-layout';

export const routes: Routes = [
    {
        path: '',
        component: AppLayout,
        children: [
            { path: '', loadChildren: () => import('./products/product.module').then((d) => d.ProductModule) },
        ],
    },
    {
        path: '',
        component: AuthLayout,
        children: [
            { path: '', loadChildren: () => import('./auth/auth.module').then((d) => d.AuthModule) },
        ],
    },
];
