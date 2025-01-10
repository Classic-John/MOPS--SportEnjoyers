import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthorizationService } from '../../services/auth/authorization.service';

export const loggedGuard: CanActivateFn = (_route, _state) => {
    return AuthorizationService.isLoggedIn()
      ? true
      : inject(Router).parseUrl('/login');
};
