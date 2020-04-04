import { Component, OnInit } from '@angular/core';
import { ICredentials } from 'src/app/shared/interface/icredentials';
import { ApiService } from 'src/app/shared/service/api.service';
import { Router } from '@angular/router';
import { ChatService } from 'src/app/chat/service/chat.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public credentials: ICredentials = {} as ICredentials;
  public isRequesting: false;
  public errors: string;
  public defaultPasswordFieldType = 'password';
  public passwordFieldType = true;
  public isLoogedIn: boolean = false;


  constructor(private apiService: ApiService, private router: Router, private chatService : ChatService) {
  }

  ngOnInit() {
  }

  login() {
    if ((this.credentials.Username == null) || (this.credentials.Password == null)) {
      this.errors = 'you must enter username and password';
      return;
    }

    this.apiService.Login(this.credentials).subscribe(response => {
      this.chatService.setUser(response);
      this.isLoogedIn = true;
      this.router.navigate(['chat']);
    });
  }

  public changePasswordFieldType() {
    if (this.passwordFieldType) {
      this.defaultPasswordFieldType = 'text';
      this.passwordFieldType = false;
    } else {
      this.defaultPasswordFieldType = 'password';
      this.passwordFieldType = true;
    }
  }

}
