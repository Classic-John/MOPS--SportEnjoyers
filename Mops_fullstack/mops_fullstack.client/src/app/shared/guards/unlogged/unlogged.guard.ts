import { CanActivateFn, Router } from '@angular/router';
import { AuthorizationService } from '../../services/auth/authorization.service';
import { inject } from '@angular/core';

export const unloggedGuard: CanActivateFn = (_route, _state) => {
  return AuthorizationService.isLoggedIn()
    ? inject(Router).parseUrl('')
    : true;
};
