import { Component, OnInit, Inject } from '@angular/core';
import { User } from "../account/account.component";
import { ImageService } from "../imageservice/imageservice.component";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    private _localStorage: Storage;
    name: string;
    private _imageService: ImageService;
    private _file: any;

    constructor( @Inject('LOCALSTORAGE') localStorage: Storage, imageService: ImageService) {
        this._localStorage = localStorage;
        this._imageService = imageService;
        let user = localStorage.getItem("user");
        console.log(user);
        if (user != null) {
            let u = JSON.parse(user) as User;
            this.name = u.name;
        }
    }

    ngOnInit() {
        
    }

    myFunc() {
        this._imageService.uploadImage(this._file).subscribe(response => { alert("Success"); }, error => { alert("Not Success");});
    }

    fileEvent(fileInput: any) {
        this._file = fileInput.target.files[0];
        let filename = this._file.name;
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
