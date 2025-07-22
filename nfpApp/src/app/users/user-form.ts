import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService, User } from '../service/user.service';

@Component({
    templateUrl: './user-form.html',
})
export class UserFormComponent {
    user: User = {
        userName: '',
        firstName: '',
        lastName: '',
        nationalCode: '',
        email: '',
        password: '',
    };

    constructor(private userService: UserService, private router: Router) {}

    save() {
        this.userService.createUser(this.user).subscribe(() => {
            this.router.navigate(['/users']);
        });
    }
}
