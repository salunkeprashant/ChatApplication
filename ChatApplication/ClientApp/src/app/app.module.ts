import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ChatareaComponent } from './chat/chatarea/chatarea.component';
import { ChatService } from './chat/service/chat.service';
import { TimeAgoPipe } from 'time-ago-pipe';
import { LoginComponent } from './users/login/login.component';
import { RegisterComponent } from './users/register/register.component';
import { HttpService } from './shared/service/http.service';
import { AuthGuardService } from './shared/service/auth-guard.service';
import { AuthService } from './shared/service/auth.service';
import { TokenInterceptor } from './shared/interceptor/token.inteceptor';

const ROUTES: Routes = [
  {
    path: 'chat',
    component: ChatareaComponent,
    canActivate: [AuthGuardService]
  },
]

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ChatareaComponent,
    TimeAgoPipe,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(ROUTES)
  ],
  providers: [
    ChatService, 
    HttpService, 
    AuthService , 
    AuthGuardService,
    {
			provide: HTTP_INTERCEPTORS,
			useClass: TokenInterceptor,
			multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


