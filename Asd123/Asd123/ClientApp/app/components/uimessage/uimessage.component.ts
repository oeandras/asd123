import { Component } from '@angular/core';
import { Observable } from 'rxjs/Rx';

@Component({
    selector: 'uimessage',
    templateUrl: './uimessage.component.html',
    styleUrls: ['./uimessage.component.css']
})
export class UIMessageComponent {

    private isVisible: boolean = false;
    private messageType: MessageType;
    private messageText: string;

    ShowMessage(messageText: string, messageType: MessageType) {
        this.showMessage(messageText, messageType);
    }

    HideMessage() {
        this.hideMessage();
    }

    private showMessage(messageText: string, messageType: MessageType) {
        this.messageText = messageText;
        this.messageType = messageType;
        this.isVisible = true;
        this.hideMessageTimer(5000);
    }
    private hideMessage() {
        this.isVisible = false;
    }

    private hideMessageTimer(hideInterval: number) {
        let timer = Observable.timer(hideInterval);
        timer.subscribe(() => this.hideMessage());
    }

    private setAlertClass() {
        switch (this.messageType) {
            case MessageType.Error:
                return "alert alert-danger";
            case MessageType.Neutral:
                return "alert alert-primary"
            case MessageType.Success:
                return "alert alert-success"
            case MessageType.Warning:
                return "alert alert-warning"
            default:
                return "alert alert-secondary"
        }
    }
}

export enum MessageType {
    Error, Warning, Success, Neutral
}