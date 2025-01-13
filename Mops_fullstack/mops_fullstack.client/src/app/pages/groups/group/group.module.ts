import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GroupRoutingModule } from './group-routing.module';
import { SharedComponentsModule } from '../../../shared/components/shared-components.module';
import { ViewComponent } from './view/view.component';
import { MatchesComponent } from './matches/matches.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';


@NgModule({
  declarations: [
    ViewComponent,
    MatchesComponent
  ],
  imports: [
    CommonModule,
    GroupRoutingModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    SharedComponentsModule
  ]
})
export class GroupModule { }
