import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { FieldsComponent } from './pages/fields/fields.component';
import { ThreadsComponent } from './pages/threads/threads.component';
import { unloggedGuard } from './shared/guards/unlogged/unlogged.guard';
import { loggedGuard } from './shared/guards/logged/logged.guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [loggedGuard],
    component: HomeComponent
  },
  {
    path: 'login',
    canActivate: [unloggedGuard],
    component: LoginComponent
  },
  {
    path: 'register',
    canActivate: [unloggedGuard],
    component: RegisterComponent
  },
  {
    path: 'fields',
    component: FieldsComponent,
    loadChildren: () => import('./pages/fields/fields.module').then(p => p.FieldsModule)
  },
  {
    path: 'groups',
    loadChildren: () => import('./pages/groups/groups.module').then(p => p.GroupsModule)
  },
  {
    path: 'threads',
    component: ThreadsComponent,
    loadChildren: () => import('./pages/threads/threads.module').then(p => p.ThreadsModule)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
