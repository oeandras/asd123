import { Injectable, Inject } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import {Observable} from 'rxjs/Observable';
import { User } from "./user";

@Injectable()
export class UserService {

    private _http: Http;
    private _baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._baseUrl = baseUrl;
    }

    getLoggedInUser() : Observable<User>{
        return this._http
          .post(this._baseUrl + 'api/account/getloggedinuserinfo', null)
          .map(result => {return result.json() as User}); 
    }

    logOutUser(){
        
    }
}