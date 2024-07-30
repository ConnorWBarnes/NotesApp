import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

import { Observable } from 'rxjs';

import { AuthenticationService } from '../authentication.service';
import { UserClaim } from '../user-claim';
import { UserProfile } from '../user-profile';

@Component({
  selector: 'app-user-header',
  standalone: true,
  imports: [
    AsyncPipe,
    NgFor, 
    NgIf,
    RouterModule
  ],
  templateUrl: './user-header.component.html',
  styleUrl: './user-header.component.scss'
})
export class UserHeaderComponent implements OnInit {
  userClaims$!: Observable<UserClaim[]>;
  userProfile$!: Observable<UserProfile>;
  userProfile!: UserProfile;

  constructor(private authService: AuthenticationService, private router: Router) { }

  // TODO: Populating user profile on init only works if the component isn't already loaded when navigating from the login component
  async ngOnInit(): Promise<void> {
    if (this.authService.isLoggedIn) {
      this.userClaims$ = this.authService.getUser$();
      this.userProfile$ = this.authService.getUserProfile$();
      this.userProfile = await this.authService.getUserProfileAsync();
      console.log('User profile: ', this.userProfile);
    }
  }

  public get isLoggedIn(): boolean {
    return this.authService.isLoggedIn;
  }

  async logOutAsync() {
    await this.authService.logOutAsync();
    this.router.navigateByUrl('/notes');
  }
}
