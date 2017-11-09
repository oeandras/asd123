import { Component, OnInit, Inject } from '@angular/core';
import { User } from "../account/account.component";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    private _localStorage: Storage
    name: string

    constructor( @Inject('LOCALSTORAGE') localStorage: Storage) {
        this._localStorage = localStorage;
        let user = localStorage.getItem("user");
        console.log(user);
        if (user != null) {
            let u = JSON.parse(user) as User;
            this.name = u.name;
        }
    }

    ngOnInit() {
        alert("oninit! :)");
    }

    myFunc() {
        alert("Upload successful");
    }

    fileEvent(fileInput: any) {
        let file = fileInput.target.files[0];
        let filename = file.name;
        let fileBrowser = <HTMLInputElement>document.getElementById("fileBrowser");
        fileBrowser.value = filename;
    }

    //$(function() {

    //    //  
    //    $(document).on('change', ':file', function () {
    //        var input = $(this),
    //            numFiles = input.get(0).files ? input.get(0).files.length : 1,
    //            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
    //        input.trigger('fileselect', [numFiles, label]);
    //    });

    //});
}
