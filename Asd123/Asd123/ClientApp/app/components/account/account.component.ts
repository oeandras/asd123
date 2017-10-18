import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'account',
    templateUrl: './account.component.html'
})
export class AccountComponent {

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/account/loginfacebook').subscribe(result => {
            console.log( result )
        }, error => console.error(error));
    }

}