import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

// shared module
import { SharedModule } from 'src/shared.module';

import { CoverLockscreenComponent } from './cover-lockscreen';
import { CoverLoginComponent } from './cover-login';
import { CoverPasswordResetComponent } from './cover-password-reset';
import { CoverRegisterComponent } from './cover-register';

const routes: Routes = [
    { path: 'auth/cover-lockscreen', component: CoverLockscreenComponent, data: { title: 'Cover Lockscreen' } },
    { path: 'auth/cover-login', component: CoverLoginComponent, data: { title: 'Cover Login' } },
    {
        path: 'auth/cover-password-reset',
        component: CoverPasswordResetComponent,
        data: { title: 'Cover Password Reset' },
    },
    { path: 'auth/cover-register', component: CoverRegisterComponent, data: { title: 'Cover Register' } },
];
@NgModule({
    imports: [RouterModule.forChild(routes), CommonModule, FormsModule, SharedModule.forRoot()],
    declarations: [
        CoverLockscreenComponent,
        CoverLoginComponent,
        CoverPasswordResetComponent,
        CoverRegisterComponent,
    ],
})
export class AuthModule {}
