import { environment } from './../environments/environment';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './shared/material/material.module';
import { StartComponent } from './modules/start/start.component';
import { ToDoListComponent } from './modules/to-do-list/to-do-list.component';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { reducers } from './state/app.state';
import { API_SIGNALR_URL, NotesSignalService } from './services/signalR/notes.signal.service';
import { API_BASE_URL, BaseApiService } from './services/api/baseApi/baseApi.service';
import { SettingsComponent } from './modules/settings/settings.component';
import { ServiceProxyModule } from './services/api/service-proxy/service-proxy.module';
import { ToastrModule } from 'ngx-toastr';
import { HandleErrorsInterceptor } from './services/error-handler/handle-errors-interceptor';
import { NoteNameGuard } from './services/guard/note-name.guard';
import { JoinLandingGuard } from './services/guard/join-landing.guard';
import { NoteInformationComponent } from './modules/note-information/note-information.component';
import { PasswordCreatorComponent } from './modules/password-creator/password-creator.component';

@NgModule({
  declarations: [
    AppComponent,
    StartComponent,
    PasswordCreatorComponent,
    ToDoListComponent,
    SettingsComponent,
    NoteInformationComponent
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 5000
    }),
    HttpClientModule,
    ServiceProxyModule,
    StoreModule.forRoot(reducers),
    StoreDevtoolsModule.instrument({
      name: 'Domestic Organization Guru',
      maxAge: 25
    })
  ],
  providers: [
    NotesSignalService,
    BaseApiService,
    {
      provide: API_SIGNALR_URL,
      useValue: environment.signalRUrl
    },
    {
      provide: API_BASE_URL,
      useValue: environment.apiBaseUrl
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HandleErrorsInterceptor,
      multi: true
    },
    NoteNameGuard,
    JoinLandingGuard
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
