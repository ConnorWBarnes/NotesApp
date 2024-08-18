import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';

import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate, CanActivateChild {
  constructor(private authService: AuthenticationService, private router: Router) {}

  /**
   * Redirects unauthenticated users to the login page when attempting to access note(s).
   * @param route The future route that will be activated.
   * @param state The future RouterState.
   * @returns true if the user is signed in; otherwise the URL for the login page.
   */
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    const url: string = state.url;
    return this.checkLogin(url);
  }

  /**
   * Redirects unauthenticated users to the login page when attempting to access note(s).
   * @param route The future route that will be activated.
   * @param state The future RouterState.
   * @returns true if the user is signed in; otherwise the URL for the login page.
   */
  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    return this.canActivate(childRoute, state);
  }

  private checkLogin(url: string): MaybeAsync<GuardResult> {
    // Ensure a user is authenticated
    if (!this.authService.isLoggedIn) {
      // Store attempted URL for redirecting
      this.authService.redirectUrl = url;
      
      // Redirect to login page
      return this.router.parseUrl('/Login');
    }
    
    // TODO: Create separate service and guard to check if the current user has permission to access the note(s)
    return true;
  }
}
