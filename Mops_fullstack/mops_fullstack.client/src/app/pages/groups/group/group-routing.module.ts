import { inject, NgModule } from '@angular/core';
import { CanActivateChildFn, Router, RouterModule, Routes, UrlTree } from '@angular/router';
import { ViewComponent } from './view/view.component';
import { MatchesComponent } from './matches/matches.component';
import { GroupService } from '../../../shared/services/group/group.service';
import { Group } from '../../../shared/interfaces/groups/group.interface';
import { map, Observable } from 'rxjs';
import { AuthorizationService } from '../../../shared/services/auth/authorization.service';

let isMember: CanActivateChildFn = (route, _state): Observable<boolean | UrlTree> | UrlTree => {
  let groupId = Number(route.parent?.paramMap.get('id'));
  let router = inject(Router);
  if (!AuthorizationService.isLoggedIn()) {
    return router.createUrlTree([`/groups/${groupId}`]);
  }

  return inject(GroupService).getAllThatMatch({ yours: true, name: "", owner: "" }).pipe(
    map((groups: Group[]) =>
      (groups.some((group, _index, _array) => group.id == groupId)) ?
        true :
        router.createUrlTree([`/groups/${groupId}`])
    )
  );
}

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
    path: 'threads',
    loadChildren: () => import('./threads/threads.module').then(p => p.ThreadsModule),
    canActivateChild: [isMember]
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
