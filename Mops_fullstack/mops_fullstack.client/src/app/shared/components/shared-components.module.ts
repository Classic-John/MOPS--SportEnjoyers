import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AutomenuComponent } from './automenu/automenu.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { NavbarComponent } from './navbar/navbar.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';
import { MatPaginatorModule } from '@angular/material/paginator';
import { RouterModule } from '@angular/router';
import { CardlistComponent } from './cardlist/cardlist.component';
import { ListComponent } from './list/list.component';
import { CenteredComponent } from './centered/centered.component';
import { GoogleAuthComponent } from './google-auth/google-auth.component';


@NgModule({
  declarations: [
    AutomenuComponent,
    SidenavComponent,
    NavbarComponent,
    CardlistComponent,
    ListComponent,
    CenteredComponent,
    GoogleAuthComponent,
  ],
  imports: [
    CommonModule,
    MatMenuModule,
    MatToolbarModule,
    MatButtonModule,
    MatRippleModule,
    MatPaginatorModule,
    RouterModule,
  ],
  exports: [
    AutomenuComponent,
    SidenavComponent,
    NavbarComponent,
    CardlistComponent,
    ListComponent,
    CenteredComponent,
    GoogleAuthComponent,
  ]
})
export class SharedComponentsModule { }
