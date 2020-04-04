import { Component, OnInit, NgZone } from '@angular/core';
import { Message } from '../models/message'
import { ChatService } from '../service/chat.service';
import { User } from '../models/user';
import { ApiService } from 'src/app/shared/service/api.service';

@Component({
  selector: 'chatarea',
  templateUrl: './chatarea.component.html',
  styleUrls: ['./chatarea.component.css']
})
export class ChatareaComponent implements OnInit {
  txtMessage: string = '';
  username: string = '';
  messages = new Array<Message>();
  message = new Message();

  users = new Array<User>();
  displayname: string;

  constructor(
    private chatService: ChatService,
    private ngZone: NgZone,
    private apiService: ApiService
  ) {
    this.init();
  }

  ngOnInit(): void {
    this.init();
  }

  init() {
    // Subscribe to events
    this.chatService.messageReceived.subscribe((message: Message) => {
      this.ngZone.run(() => {
        if (message.username !== this.username) {
          message.type = "received";
          this.messages.push(message);
        }
      });
    });

    this.chatService.getUser().subscribe(user => {
      if (user != null) {
        this.username = user.username;
        this.displayname = `${user.personalInformation.firstName} ${user.personalInformation.lastName}`;
        this.getUserList();
      }
    });
  }

  getUserList() {
    this.apiService.UserList().subscribe(users => {
      this.users = users;
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
