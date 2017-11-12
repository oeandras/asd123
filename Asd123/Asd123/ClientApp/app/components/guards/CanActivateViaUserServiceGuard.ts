import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { User } from '../userservice/user';
import { UserService } from "../userservice/userservice.component";

@Injectable()
export class CanActivateViaUserLoggedInGuard implements CanActivate {
    constructor(private userService: UserService) {
    }

    canActivate() {
        return this.userService.getLoggedInUser() != null;
    }
}