﻿import { Component, OnInit, Inject, ViewChild } from '@angular/core';
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
    private _files: File[];
    private user: User;
    public profilepic_src: string;
    public arr: Observable<ImageInfo[]>;
    private fileBrowser: HTMLInputElement;
    @ViewChild(UIMessageComponent) private messageComponent: UIMessageComponent;

    constructor(imageService: ImageService, private userservice: UserService) {
        userservice
            .getLoggedInUser()
            .subscribe(result => {
                this.user = result;
                this.profilepic_src = "http://graph.facebook.com/" + result.id + "/picture?type=large";
            },
            error => console.log(error));
      
        this._imageService = imageService;
        userservice
            .getLoggedInUser()
            .subscribe(result => { this.user = result; this.name = result.name; this.email = result.email },
            error => console.log(error));
        this.loadImages();

    }

    loadImages() {
        this.arr = this._imageService.fetchImages();
        this.arr.subscribe(resp => console.log(resp),
            error => console.log("Error: " + error),
            () => console.log("completed"));
    }

    uploadClick() {
        if (this.fileBrowser == null || this.fileBrowser.value == null || this.fileBrowser.value == "") {
            //alert("No file selected.");
            this.messageComponent.ShowMessage("No file selected.", MessageType.Error);
        }
        else {
            this._imageService.uploadImage(this._files).subscribe(
                response => {
                    this.messageComponent.ShowMessage("Upload successful", MessageType.Success);
                    this.fileBrowser.value = "";
                    this.loadImages();
                },
                error => {
                    this.messageComponent.ShowMessage("Upload failed. Please try again!", MessageType.Error);
                });
        }
     
        //this._imageService.uploadImage(this._files).subscribe(response => { alert("Success"); this.fileBrowser.value = ""; this.loadImages(); }, error => { alert("Not Success"); });

    }

    fileEvent(fileInput: any) {
        //this._file = fileInput.target.files[0];
        this._files = fileInput.target.files;
        this.fileBrowser = <HTMLInputElement>document.getElementById("fileBrowser");
        this.setFileBrowserString();
        this.uploadClick();
    }

    setFileBrowserString() {
        if (this._files.length == 1) {
            this.fileBrowser.value = this._files[0].name;
        }
        else if (this._files.length == 0) {
            this.fileBrowser.value = "";
        }
        else {
            this.fileBrowser.value = this._files.length.toString() + " files selected";
        }
    }

    isProfileVisible: boolean = false;
    profileClick($event: any) {
        //alert("My profile. Coming soon...");
        this.isProfileVisible = !this.isProfileVisible;
    }
}
