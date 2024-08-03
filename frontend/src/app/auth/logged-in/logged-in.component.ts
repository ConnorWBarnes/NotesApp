import { Component, OnInit } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';

import { AuthenticationService } from '../authentication.service';
import { UserProfile } from '../user-profile';

@Component({
  selector: 'app-logged-in',
  standalone: true,
  imports: [
    AsyncPipe
  ],
  templateUrl: './logged-in.component.html',
  styleUrl: './logged-in.component.scss'
})
export class LoggedInComponent implements OnInit {
  userProfile$!: Observable<UserProfile>;
  userProfile!: UserProfile;

  constructor(private authService: AuthenticationService, private router: Router) { }

  async ngOnInit(): Promise<void> {
    this.userProfile$ = this.authService.getUserProfile$();
    this.userProfile = await this.authService.getUserProfileAsync();
  }

  async logOutAsync() {
    await this.authService.logOutAsync();
    this.router.navigateByUrl('/notes');
  }
}
