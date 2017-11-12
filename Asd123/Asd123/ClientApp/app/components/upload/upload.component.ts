import { Component, OnInit, Inject } from '@angular/core';
import { User } from "../account/account.component";
import { ImageService } from "../imageservice/imageservice.component";

@Component({
    selector: 'upload',
    templateUrl: './upload.component.html',
    styleUrls: ['./upload.component.css']
})
export class UploadComponent {

    private _localStorage: Storage;
    name: string;
    email: string;
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
            this.email = u.email;
        }
    }

    ngOnInit() {

    }

    uploadClick() {
        this._imageService.uploadImage(this._file).subscribe(response => { alert("Success"); }, error => { alert("Not Success"); });
    }

    fileEvent(fileInput: any) {
        this._file = fileInput.target.files[0];
        let filename = this._file.name;
        let fileBrowser = <HTMLInputElement>document.getElementById("fileBrowser");
        fileBrowser.value = filename;
    }
}
