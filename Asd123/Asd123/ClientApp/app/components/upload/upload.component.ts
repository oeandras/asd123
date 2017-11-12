import { Component, OnInit, Inject } from '@angular/core';
import { ImageService } from "../imageservice/imageservice.component";
import { UserService } from "../userservice/userservice.component";
import { User } from "../userservice/user";
import { ImageInfo } from "../imageservice/imageinfo";
import 'rxjs/add/operator/first';
import 'rxjs/add/operator/toarray';
import { Observable } from 'rxjs/observable';

@Component({
    selector: 'upload',
    templateUrl: './upload.component.html',
    styleUrls: ['./upload.component.css']
})
export class UploadComponent {

    name: string;
    email: string;
    private _imageService: ImageService;
    private _file: any;
    private user : User;
    private arr: Observable<ImageInfo[]>;
    private fileBrowser: HTMLInputElement;

    constructor( imageService: ImageService, private userservice : UserService) {
        this._imageService = imageService;
        userservice
            .getLoggedInUser()
            .subscribe(result => {this.user=result; this.name = result.name; this.email = result.email},
            error => console.log(error));
    }

    ngOnInit() {
        
    }

    uploadClick() {
        this._imageService.uploadImage(this._file).subscribe(response => { alert("Success"); this.fileBrowser.value="" }, error => { alert("Not Success"); });
    }

    fileEvent(fileInput: any) {
        this._file = fileInput.target.files[0];
        let filename = this._file.name;
        this.fileBrowser = <HTMLInputElement>document.getElementById("fileBrowser");
        this.fileBrowser.value = filename;
    }

    getClick() {
        this.arr = this._imageService.fetchImages();
        this.arr.subscribe(resp => console.log( resp),
            error => console.log("Error: " + error),
            () => console.log("completed"));
    }
}
