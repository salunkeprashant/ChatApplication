import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ChatareaComponent } from './chat/chatarea/chatarea.component';
import { ChatService } from './chat/service/chat.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ChatareaComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
          { path: 'chat', component: ChatareaComponent },
      ],{ initialNavigation: false })
  ],
  providers: [ChatService],
  bootstrap: [AppComponent]
})
export class AppModule { }
