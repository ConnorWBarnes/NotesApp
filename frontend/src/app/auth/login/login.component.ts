import { Component, OnInit } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-login-modal-content',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginModalContentComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
    rememberMe: new FormControl(false)
  });

  constructor(private activeModal: NgbActiveModal, private authService: AuthenticationService, private router: Router) { }

  public get email() {
    return this.loginForm.get('email');
  }

  public get password() {
    return this.loginForm.get('password');
  }

  private _formSubmitted = false;
  public get formSubmitted(): boolean {
    return this._formSubmitted;
  }

  private _loginFailed = false;
  public get loginFailed(): boolean {
    return this._loginFailed;
  }

  async login() {
    this._formSubmitted = true;
    if (!this.loginForm.valid) {
      return;
    }

    await this.authService.logInAsync(this.loginForm.value.email!, this.loginForm.value.password!, this.loginForm.value.rememberMe!);
    if (this.authService.isLoggedIn) {
      // Redirect the user
      this.router.navigateByUrl(this.authService.redirectUrl);
    } else {
      // Display error message
      this._loginFailed = true;
    }
  }
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="min-vh-100"></div>
  `
})
export class LoginComponent implements OnInit {
  constructor(private modalService: NgbModal) { }

  ngOnInit(): void {
    this.modalService.open(LoginModalContentComponent, { animation: false, fullscreen: true, keyboard: false });
  }
}
