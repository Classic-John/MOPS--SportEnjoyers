import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ThreadsRoutingModule } from './threads-routing.module';
import { ThreadsComponent } from './threads/threads.component';
import { ThreadComponent } from './thread/thread.component';
import { SharedComponentsModule } from '../../../../shared/components/shared-components.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';


@NgModule({
  declarations: [
    ThreadsComponent,
    ThreadComponent
  ],
  imports: [
    CommonModule,
    ThreadsRoutingModule,
    SharedComponentsModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatExpansionModule,
    MatInputModule,
    MatButtonModule,
  ]
})
export class ThreadsModule { }
