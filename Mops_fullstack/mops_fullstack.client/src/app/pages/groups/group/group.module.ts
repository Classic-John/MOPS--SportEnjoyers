import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GroupRoutingModule } from './group-routing.module';
import { SharedComponentsModule } from '../../../shared/components/shared-components.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    GroupRoutingModule,
    SharedComponentsModule
  ]
})
export class GroupModule { }
