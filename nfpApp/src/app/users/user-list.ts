import { Component, OnInit } from '@angular/core';
import { UserService, User } from '../service/user.service';
import { Router } from '@angular/router';

@Component({
    templateUrl: './user-list.html',
})
export class UserListComponent implements OnInit {
    users: User[] = [];

    constructor(private userService: UserService, private router: Router) {}

    ngOnInit(): void {
        this.load();
    }

    load() {
        this.userService.getUsers(1, 50).subscribe((res) => {
            this.users = res.data?.items || [];
        });
    }
}
