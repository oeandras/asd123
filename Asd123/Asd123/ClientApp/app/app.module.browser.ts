import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app.component';
import { OpaqueToken } from '@angular/core';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl },
        { provide: 'LOCALSTORAGE', useFactory: getLocalStorage }
    ]
})
export class AppModule {
}

export function getLocalStorage() {
    return window.localStorage;
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
