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
  messages = new Array<Message>();
  message = new Message();

  senderId : string;
  users = new Array<User>();
  displayname: string;
  selectedUserId: string;

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
        if (message.SenderId != this.senderId) {
          message.Type = "received";
          this.messages.push(message);
        }
      });
    });

    this.chatService.getUser().subscribe(user => {
      if (user != null) {
        this.displayname = `${user.PersonalInformation.FirstName} ${user.PersonalInformation.LastName}`;
        this.senderId = user.Id;
        this.getUserList();
      }
    });
  }

  getUserList() {
    this.apiService.UserList().subscribe(users => {
      this.users = users;
    });
  }

  selectUser(usr) {
    this.selectedUserId = this.users.filter(user => user.Id == usr.Id).map(x => x.Id)[0];
  
    // TODO : Get selected peroson conversation history
  }

  getDislayNameById(id){
    let user = this.users.filter(x=>x.Id == id)[0];
    return `${user.PersonalInformation.FirstName} ${user.PersonalInformation.LastName}`;
  }

  sendMessage(): void {
    if (this.txtMessage) {
      this.message = new Message();
      this.message.SenderId = this.senderId;
      this.message.RecipientId = this.selectedUserId;
      this.message.Type = "sent";
      this.message.Content = this.txtMessage;
      this.message.SentOn = new Date();
      this.messages.push(this.message);
      this.chatService.sendMessage(this.message);
      this.txtMessage = '';
    }
  }
}  
