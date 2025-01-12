import { Injectable, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FieldsRoutingModule } from './fields-routing.module';
import { SearchComponent } from './search/search.component';
import { ViewComponent } from './view/view.component';
import { CreateComponent } from './create/create.component';
import { SharedComponentsModule } from '../../shared/components/shared-components.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    SearchComponent,
    ViewComponent,
    CreateComponent
  ],
  imports: [
    CommonModule,
    FieldsRoutingModule,
    MatFormFieldModule,
    MatDatepickerModule,
    FormsModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatInputModule,
    MatButtonModule,
    MatSlideToggleModule,
    MatCardModule,
    MatSelectModule,
    SharedComponentsModule,
  ],
  providers: [
    MatNativeDateModule
  ]
})
export class FieldsModule { }
