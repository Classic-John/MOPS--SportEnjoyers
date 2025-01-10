import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { RequestsComponent } from './requests/requests.component';
import { CreateComponent } from './create/create.component';
import { ViewComponent } from './view/view.component';
import { intParamGuard } from '../../shared/guards/intParam/int-param.guard';

const routes: Routes = [
  {
    path: '',
    component: SearchComponent
  },
  {
    path: 'requests',
    component: RequestsComponent
  },
  {
    path: 'create',
    component: CreateComponent
  },
  {
    path: ':id',
    component: ViewComponent,
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
