import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    templateUrl: './login.html',
})
export class LoginComponent {
    email = '';
    password = '';
    constructor(private router: Router) {}

    login() {
        console.log('login with', this.email);
        this.router.navigate(['/']);
    }
}
