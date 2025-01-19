import { Component, OnInit, signal } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import PasswordValidator from '../../../shared/validators/password-validator.validator';
import { AuthorizationService } from '../../../shared/services/auth/authorization.service';
import { Router } from '@angular/router';
import { LoginModel } from '../../../shared/interfaces/players/login.interface';
import { GoogleLoginProvider, SocialAuthService } from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  passwordHidden = signal(true);

  constructor(
    private readonly router: Router,
    private readonly authService: AuthorizationService,
    private readonly socialAuthService: SocialAuthService,
    fb: FormBuilder
  ) {
    this.loginForm = fb.group({
      email: ["", Validators.compose([Validators.required, Validators.email])],
      password: ["", Validators.compose([Validators.required, Validators.minLength(8), Validators.maxLength(15), PasswordValidator.passwordStrength])]
    });
  }

  login(loginModel: LoginModel) {
    this.authService.login(loginModel).subscribe({
      next: () => {
        console.log("Logged in successfully!");
        this.router.navigate([""]);
      },
      error: err => {
        console.log("Error: ", err);
      }
    });
  }

  loginWithGoogle() {

  }

  toggleHidden(event: MouseEvent) {
    this.passwordHidden.set(!this.passwordHidden());
    event.stopPropagation();
  }
}
