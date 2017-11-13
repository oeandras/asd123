import { Component, Inject } from '@angular/core';
import { User } from '../userservice/user';
import { UserService } from '../userservice/userservice.component';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {

    private user: User;

    constructor(private userservice: UserService) {
        userservice
            .getLoggedInUser()
            .subscribe(result => { this.user = result },
            error => console.log(error));
    }

    isLoggedInUser() {
        return this.user != null;
    }
}
