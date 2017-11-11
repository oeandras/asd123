import { Component, Inject } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {

    private user: any;

    constructor( @Inject('LOCALSTORAGE') localStorage: Storage) {

        this.user = localStorage.getItem("user");
    }

    isLoggedInUser() {
        return this.user != null;
    }
}
