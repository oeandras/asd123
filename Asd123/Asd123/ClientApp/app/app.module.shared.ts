import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { AccountComponent } from './components/account/account.component';
import { LogoutComponent } from './components/logout/logout.component';
import { UploadComponent } from './components/upload/upload.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AccountComponent,
        LogoutComponent,
        UploadComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'upload', component: UploadComponent },
            { path: 'account', component: AccountComponent },
            { path: 'logout', component: LogoutComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
})
export class AppModuleShared {
}
