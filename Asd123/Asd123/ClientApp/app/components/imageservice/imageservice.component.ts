import { Injectable, Inject } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { User } from "../userservice/user";
import { ImageInfo } from "./imageinfo";

@Injectable()
export class ImageService {

    private _http: Http;
    private _baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._baseUrl = baseUrl;
    }

    uploadImage(image: any) {
        let input = new FormData();
        input.append("file", image);
        //let headers = new Headers({ 'Content-Type': undefined });
        //let options = new RequestOptions({ headers: headers });
        return this._http.post(this._baseUrl + 'api/image/upload', input/*, options*/);
    }

    fetchImages() {
        return this._http.get(this._baseUrl + 'api/image/getuserimages').map(result => {
                    return result.json() as ImageInfo;
           });;
    }
}