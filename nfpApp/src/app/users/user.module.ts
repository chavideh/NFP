import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SharedModule } from 'src/shared.module';

import { UserListComponent } from './user-list';
import { UserFormComponent } from './user-form';

const routes: Routes = [
    { path: 'users', component: UserListComponent, data: { title: 'Users' } },
    { path: 'users/new', component: UserFormComponent, data: { title: 'Create User' } },
];

@NgModule({
    imports: [RouterModule.forChild(routes), CommonModule, FormsModule, SharedModule.forRoot()],
    declarations: [UserListComponent, UserFormComponent],
})
export class UserModule {}
