import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GroupsRoutingModule } from './groups-routing.module';
import { SearchComponent } from './search/search.component';
import { RequestsComponent } from './requests/requests.component';
import { CreateComponent } from './create/create.component';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { SharedComponentsModule } from '../../shared/components/shared-components.module';


@NgModule({
  declarations: [
    SearchComponent,
    RequestsComponent,
    CreateComponent,
  ],
  imports: [
    CommonModule,
    GroupsRoutingModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatIconModule,
    SharedComponentsModule,
  ]
})
export class GroupsModule { }
