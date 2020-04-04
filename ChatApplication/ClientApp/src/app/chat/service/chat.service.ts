import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from '../models/message';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';
import { Subject, BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class ChatService {

  messageReceived = new EventEmitter<Message>();
  connected = new EventEmitter<Boolean>();

  private isConnected = false;
  private hubConnection: HubConnection;
  private user = new BehaviorSubject<User>(null);

  constructor() {
    this.getUser().subscribe(user => {
      if (user != null) {
        this.buildConnection();
        this.startConnection();
        
        this.hubConnection.on('MessageReceived', (data: any) => {
          this.messageReceived.emit(data);
        });
      }
    });

    
  }

  setUser(user: User) {
    localStorage.setItem('BearerToken', user.bearerToken);
    this.user.next(user);
  }

  getUser(): Observable<User> {
    return this.user.asObservable();
  }

  sendMessage(message: Message) {
    this.hubConnection.invoke('SendPrivateMessage', message);
  }

  private buildConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(environment.hubUrl + 'ChatHub', { accessTokenFactory: () => localStorage.getItem('BearerToken') })
      .build();
  }

  private startConnection(): void {
    this.hubConnection
      .start()
      .then(() => {
        this.isConnected = true;
        console.log('Hub connection started');
        this.connected.emit(true);
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
        setTimeout(function () { this.startConnection(); }, 5000);
      });
  }
}    
