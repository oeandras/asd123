import { Component, OnInit, Inject } from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    private _localStorage: Storage
    name: string

    constructor( @Inject('LOCALSTORAGE') localStorage: Storage) {
        this._localStorage = localStorage;
        let tempname = localStorage.getItem("name");
        if (tempname != null)
            this.name = tempname;
    }

    ngOnInit() {
        alert("oninit! :)");
    }

    myFunc() {
        alert("Upload successful");
    }

    //$(function() {

    //    // We can attach the `fileselect` event to all file inputs on the page
    //    $(document).on('change', ':file', function () {
    //        var input = $(this),
    //            numFiles = input.get(0).files ? input.get(0).files.length : 1,
    //            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
    //        input.trigger('fileselect', [numFiles, label]);
    //    });

    //});
}
