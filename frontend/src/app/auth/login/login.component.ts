import { Component } from '@angular/core';
import { ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
    rememberMe: new FormControl(false)
  });

  constructor(private authService: AuthenticationService, private router: Router) { }

  async login() {
    await this.authService.signInAsync(this.loginForm.value.email!, this.loginForm.value.password!, this.loginForm.value.rememberMe!);
    if (this.authService.isLoggedIn) {
      // Redirect the user
      this.router.navigateByUrl(this.authService.redirectUrl);
    } else {
      // Display error message
    }
  }
}
