import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { User } from '../userservice/user';
import { UserService } from "../userservice/userservice.component";
import { Observable } from "rxjs/Observable";

@Injectable()
export class CanActivateViaUserLoggedInGuard implements CanActivate {
    private user: User;
    constructor(private userService: UserService, private router: Router) {
    }

    canActivate() {
        return this.userService.getLoggedInUser().map(
            user => {
                if (user == null)
                    this.router.navigate(['/home']);
                return user != null;
            }
        );
    }
}