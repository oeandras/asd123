import { Component } from '@angular/core';
import { User } from "../userservice/user";
import { UserService } from "../userservice/userservice.component";

@Component({
    selector: 'profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

    user: User;
    profilepic_src: string;

    constructor(userservice: UserService) {
        userservice
            .getLoggedInUser()
            .subscribe(result => {
                this.user = result;
                this.profilepic_src = "http://graph.facebook.com/" + result.id + "/picture?type=large";
            },
            error => console.log(error));
    }
}