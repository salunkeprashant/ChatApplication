import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private httpService: HttpService) { }

  public Login(data: any): Observable<any> {
    return this.httpService.post(environment.baseUrl, environment.users.login, data);
  }

}


