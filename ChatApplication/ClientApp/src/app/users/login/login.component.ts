import { Component, OnInit } from '@angular/core';
import { ICredentials } from 'src/app/shared/interface/icredentials';
import { ApiService } from 'src/app/shared/service/api.service';
import { Router } from '@angular/router';

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


  constructor(private apiService: ApiService, private router: Router) {
  }

  ngOnInit() {
  }

  login() {
    if ((this.credentials.Username == null) || (this.credentials.Password == null)) {
      this.errors = 'you must enter username and password';
      return;
    }

    this.apiService.Login(this.credentials).subscribe(respnse => {
      localStorage.setItem('BearerToken', respnse.bearerToken);    
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
