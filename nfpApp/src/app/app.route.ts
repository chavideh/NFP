import { Routes } from '@angular/router';

import { AppLayout } from './layouts/app-layout';
import { AuthLayout } from './layouts/auth-layout';
import { AuthGuard } from './auth/auth.guard';

export const routes: Routes = [
    {
        path: '',
        component: AppLayout,
        canActivate: [AuthGuard],
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
