import { Component, OnInit } from '@angular/core';
import { AsyncPipe, NgIf } from '@angular/common';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';

import { AuthenticationService } from '../authentication.service';
import { UserProfile } from '../user-profile';

@Component({
  selector: 'app-logged-in',
  standalone: true,
  imports: [
    AsyncPipe,
    NgIf
  ],
  templateUrl: './logged-in.component.html',
  styleUrl: './logged-in.component.scss'
})
export class LoggedInComponent implements OnInit {
  userProfile$!: Observable<UserProfile>;

  constructor(private authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
    this.userProfile$ = this.authService.getUserProfile$();
  }

  async logOutAsync() {
    await this.authService.logOutAsync();
    this.router.navigateByUrl('/notes');
  }
}
