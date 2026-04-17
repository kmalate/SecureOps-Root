import { CanActivateFn } from '@angular/router';
import { Auth } from "../services/auth";
import { inject } from "@angular/core";
import { Router } from "@angular/router";

export const guestGuard: CanActivateFn = (route, state) => {
  const authService = inject(Auth);
  const router = inject(Router);

    if (authService.isAuthenticated()) {
        return router.parseUrl('/');
    } else {
        return true;
    }
}