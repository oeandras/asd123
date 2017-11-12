import { Component, Inject } from '@angular/core';
import { Http, Jsonp, Response, Headers, RequestOptions } from '@angular/http';
import { ActivatedRoute } from "@angular/router";



@Component({
    selector: 'account',
    templateUrl: './account.component.html'
})
export class AccountComponent {

    private _localStorage: Storage;
    private _baseUrl: string;
    private _http: Http;

    constructor(private activatedRoute: ActivatedRoute, @Inject('LOCALSTORAGE') localStorage: Storage, @Inject('BASE_URL') baseUrl: string, http: Http) {
        this._localStorage = localStorage;
        this._baseUrl = baseUrl;
        this._http = http;
        
    }
}