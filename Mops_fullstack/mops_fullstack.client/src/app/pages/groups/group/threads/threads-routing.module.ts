import { NgModule } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterModule, Routes } from '@angular/router';
import { ThreadsComponent } from './threads/threads.component';
import { ThreadComponent } from './thread/thread.component';
import { intParamGuard } from '../../../../shared/guards/intParam/int-param.guard';

const routes: Routes = [
  {
    path: '',
    component: ThreadsComponent
  },
  {
    path: ':id',
    component: ThreadComponent,
    canActivate: [intParamGuard],
    data: {
      param: 'id',
      fallback: (route: ActivatedRouteSnapshot, router: Router) => {
        let groupId = Number(route.parent?.parent?.paramMap.get('id'));
        return router.createUrlTree([`/groups/${groupId}/threads`]);
      }
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
export class ThreadsRoutingModule { }
