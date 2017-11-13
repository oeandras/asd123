import { Component } from '@angular/core';
import { Injectable, Inject } from '@angular/core';
import { Http, Jsonp, Response, Headers, RequestOptions } from '@angular/http';
import { ActivatedRoute } from "@angular/router";
import { UserService } from "../userservice/userservice.component";
import { User } from "../userservice/user";

@Component({
    selector: 'logout',
    templateUrl: './logout.component.html'
})

@Injectable()
export class LogoutComponent {

    private _http: Http;
    private _baseUrl: string;
    private userservice: UserService;

    constructor( userservice: UserService) {

        this.userservice = userservice;
        /*http: Http, @Inject('BASE_URL') baseUrl: string*/
       /* this._http = http;
        this._baseUrl = baseUrl;*/
        //   this._http.post(this._baseUrl + 'api/account/logout', null);
    }

    logOutUser() {
      return this.userservice.logOutUser;
      
       
    }


   
}



