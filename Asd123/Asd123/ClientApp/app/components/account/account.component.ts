import { Component, Inject } from '@angular/core';
import { Http, Jsonp, Response, Headers, RequestOptions } from '@angular/http';
import { ActivatedRoute } from "@angular/router";
import { JwtHelper } from "angular2-jwt";


@Component({
    selector: 'account',
    templateUrl: './account.component.html'
})
export class AccountComponent {

    jwtHelper: JwtHelper = new JwtHelper();
    private _localStorage: Storage;
    private _baseUrl: string;
    private _http: Http;

    constructor(private activatedRoute: ActivatedRoute, @Inject('LOCALSTORAGE') localStorage: Storage, @Inject('BASE_URL') baseUrl: string, http: Http) {
        this._localStorage = localStorage;
        this._baseUrl = baseUrl;
        this._http = http;
        this.activatedRoute.queryParams.subscribe(params => {
            //let token = params['token'];
            //
            //this._localStorage.setItem('JWT', token);
            //let user = this.jwtHelper.decodeToken(token)
            //localStorage.setItem('name', user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"])
            //console.log(localStorage.getItem("name")); // Print the parameter to the console.
            let loggedIn = params["login"];
            if (loggedIn) {
                this._http.post(this._baseUrl + 'api/account/getloggedinuserinfo', null, {}).subscribe(result => {
                    localStorage.setItem('user', JSON.stringify(result.json()));
                }, error => console.error(error));
            }
        });
    }

    showEmail() {

        let token = this._localStorage.getItem('JWT')
        var headers = new Headers();
        console.log(token)
        headers.append('Authorization', 'Bearer ' + token as string);
        headers.append('Content-Type', 'application/json');

        let options = new RequestOptions({ headers: headers, method: 'post' });

        this._http.post(this._baseUrl + 'api/account/showemail', null, options).subscribe(result => {
            console.log(result);
        }, error => console.error(error));
    }
}

export interface User {
    name: string;
    email: string;
}