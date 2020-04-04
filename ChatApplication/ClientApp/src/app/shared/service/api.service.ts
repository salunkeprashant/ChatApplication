import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/app/chat/models/user';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private httpService: HttpService) { }

  public Login(data: any): Observable<User> {
    return this.httpService.post(environment.baseUrl, environment.users.login, data);
  }

  public UserList() : Observable<Array<User>> {
    return this.httpService.get(environment.baseUrl, environment.users.list);
  }

}


