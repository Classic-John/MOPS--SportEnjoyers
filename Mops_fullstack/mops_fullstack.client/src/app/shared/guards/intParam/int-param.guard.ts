import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const intParamGuard: CanActivateFn = (route, state) => {
  return !isNaN(Number(route.params[route.data['param']]))
    ? true
    : route.data['fallback'](route, inject(Router));
};
