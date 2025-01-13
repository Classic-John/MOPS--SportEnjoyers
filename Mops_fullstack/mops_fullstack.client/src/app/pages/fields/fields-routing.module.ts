import { NgModule } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterModule, Routes } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { CreateComponent } from './create/create.component';
import { ViewComponent } from './view/view.component';
import { intParamGuard } from '../../shared/guards/intParam/int-param.guard';
import { loggedGuard } from '../../shared/guards/logged/logged.guard';

const routes: Routes = [
  {
    path: '',
    component: SearchComponent
  },
  {
    path: 'create',
    component: CreateComponent,
    canActivate: [loggedGuard]
  },
  {
    path: ':id',
    component: ViewComponent,
    canActivate: [intParamGuard],
    data: {
      param: 'id',
      fallback: (_route: ActivatedRouteSnapshot, router: Router) => router.createUrlTree(['/fields'])
    }
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
export class FieldsRoutingModule { }
