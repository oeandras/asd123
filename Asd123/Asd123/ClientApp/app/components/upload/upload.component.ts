import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { ImageService } from "../imageservice/imageservice.component";
import { UserService } from "../userservice/userservice.component";
import { User } from "../userservice/user";
import { ImageInfo } from "../imageservice/imageinfo";
import 'rxjs/add/operator/first';
import 'rxjs/add/operator/toarray';
import { Observable } from 'rxjs/observable';
import { UIMessageComponent, MessageType } from "../uimessage/uimessage.component";

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
    @ViewChild(UIMessageComponent) private messageComponent: UIMessageComponent;

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
        this._imageService.uploadImage(this._file).subscribe(
            response =>
            {
                this.messageComponent.ShowMessage("Upload succeeded", MessageType.Success);
                this.fileBrowser.value = ""
            },
            error =>
            {
                this.messageComponent.ShowMessage("Upload failed. Please try again!", MessageType.Error);
            });
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
