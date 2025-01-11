import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { importProvidersFrom, NgModule } from '@angular/core';
import { JwtModule } from '@auth0/angular-jwt';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatRadioModule } from '@angular/material/radio';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { RegisterComponent } from './pages/auth/register/register.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { FieldsComponent } from './pages/fields/fields.component';
import { ThreadsComponent } from './pages/threads/threads.component';
import { AuthorizationService } from './shared/services/auth/authorization.service';
import { HomeComponent } from './pages/home/home.component';
import { SharedComponentsModule } from './shared/components/shared-components.module';
import { GroupsModule } from './pages/groups/groups.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    FieldsComponent,
    ThreadsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    AppRoutingModule,
    GroupsModule,
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatIconModule,
    MatRadioModule,
    SharedComponentsModule,
  ],
  providers: [
    importProvidersFrom(
      JwtModule.forRoot({
        config: {
          tokenGetter: AuthorizationService.getToken,
          allowedDomains: ['https://localhost:7225', 'https://localhost:63635']
        }
      })
    ),
    provideHttpClient(
      withInterceptorsFromDi()
    )
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
