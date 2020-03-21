import { Component, OnInit, NgZone } from '@angular/core';
import { Message } from '../models/message'
import { ChatService } from '../service/chat.service';

@Component({
  selector: 'chatarea',
  templateUrl: './chatarea.component.html',
  styleUrls: ['./chatarea.component.css']
})
export class ChatareaComponent {

  txtMessage: string = '';
  username: string = '';
  messages = new Array<Message>();
  message = new Message();

  constructor(
    private chatService: ChatService,
    private ngZone: NgZone
  ) {
    // Subscribe to events
    this.chatService.messageReceived.subscribe((message: Message) => {
      this.ngZone.run(() => {
        if (message.username !== this.username) {
          message.type = "received";
          this.messages.push(message);
        }
      });
    });
  }
  sendMessage(): void {
    if (this.txtMessage) {
      this.message = new Message();
      this.message.username = this.username;
      this.message.type = "sent";
      this.message.message = this.txtMessage;
      this.message.date = new Date();
      this.messages.push(this.message);
      this.chatService.sendMessage(this.message);
      this.txtMessage = '';
    }
  }
}  
