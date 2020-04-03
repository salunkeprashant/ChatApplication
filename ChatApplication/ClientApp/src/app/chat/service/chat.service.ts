import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from '../models/message';
import { environment } from 'src/environments/environment';

@Injectable()
export class ChatService {
  messageReceived = new EventEmitter<Message>();
  connected = new EventEmitter<Boolean>();

  private isConnected = false;
  private hubConnection: HubConnection;

  constructor() {
    this.buildConnection();
    this.startConnection();

    this.hubConnection.on('MessageReceived', (data: any) => {
      this.messageReceived.emit(data);
    });
  }

  sendMessage(message: Message) {
    this.hubConnection.invoke('SendPrivateMessage', message);
  }

  private buildConnection() {
    this.hubConnection = new HubConnectionBuilder().withUrl(environment.hubUrl + 'ChatHub').build();
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
