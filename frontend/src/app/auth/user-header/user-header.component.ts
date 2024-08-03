import { AsyncPipe, NgComponentOutlet, NgFor, NgIf } from '@angular/common';
import { Component, OnInit, Type } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

import { Observable } from 'rxjs';

import { AuthenticationService } from '../authentication.service';
import { UserClaim } from '../user-claim';
import { UserProfile } from '../user-profile';
import { LoggedInComponent } from '../logged-in/logged-in.component';
import { LoggedOutComponent } from '../logged-out/logged-out.component';

@Component({
  selector: 'app-user-header',
  standalone: true,
  imports: [
    AsyncPipe,
    NgFor, 
    NgIf,
    RouterModule,
    NgComponentOutlet
  ],
  templateUrl: './user-header.component.html',
  styleUrl: './user-header.component.scss'
})
export class UserHeaderComponent {

  constructor(private authService: AuthenticationService) { }

  public getUserHeaderComponent() {
    return this.authService.isLoggedIn ? LoggedInComponent : LoggedOutComponent;
  }
}
