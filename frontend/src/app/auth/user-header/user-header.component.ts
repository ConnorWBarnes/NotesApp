import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

import { Observable } from 'rxjs';

import { UserClaim } from '../user-claim';
import { AuthenticationService } from '../authentication.service';

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

  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {
    if (this.authService.isLoggedIn) {
      this.userClaims$ = this.authService.getUser$();
    }
  }

  public get isLoggedIn(): boolean {
    return this.authService.isLoggedIn;
  }
}
