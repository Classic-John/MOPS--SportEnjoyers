import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { RequestsComponent } from './requests/requests.component';
import { CreateComponent } from './create/create.component';
import { ViewComponent } from './group/view/view.component';
import { intParamGuard } from '../../shared/guards/intParam/int-param.guard';
import { loggedGuard } from '../../shared/guards/logged/logged.guard';

const routes: Routes = [
  {
    path: '',
    component: SearchComponent
  },
  {
    path: 'requests',
    component: RequestsComponent,
    canActivate: [loggedGuard]
  },
  {
    path: 'create',
    component: CreateComponent,
    canActivate: [loggedGuard]
  },
  {
    path: ':id',
    loadChildren: () => import('./group/group.module').then(p => p.GroupModule),
    canActivate: [intParamGuard],
    data: { param: 'id', fallback: '/groups' }
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
export class GroupsRoutingModule { }
