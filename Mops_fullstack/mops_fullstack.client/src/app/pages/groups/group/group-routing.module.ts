import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewComponent } from './view/view.component';
import { MatchesComponent } from './matches/matches.component';

const routes: Routes = [
  {
    path: '',
    component: ViewComponent
  },
  {
    path: 'matches',
    component: MatchesComponent
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GroupRoutingModule { }
