import { Component, Inject } from '@angular/core';
import { Http, Jsonp, Response, Headers, RequestOptions } from '@angular/http';
import { ActivatedRoute } from "@angular/router";
import { UserService } from "../userservice/userservice.component";
import { User } from "../userservice/user";

@Component({
    selector: 'account',
    templateUrl: './account.component.html'
})

export class AccountComponent {
    profilepic_src: string;
    user: User;

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