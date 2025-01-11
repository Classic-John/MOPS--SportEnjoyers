import { Component, signal } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import PasswordValidator from '../../../shared/validators/password-validator.validator';
import { AuthorizationService } from '../../../shared/services/auth/authorization.service';
import { Router } from '@angular/router';
import { RegisterModel } from '../../../shared/interfaces/players/register.interface';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;
  passwordHidden = signal(true);

  constructor(public readonly router: Router, public readonly authService: AuthorizationService, fb: FormBuilder) {
    this.registerForm = fb.group({
      email: ["", Validators.compose([Validators.required, Validators.email])],
      name: ["", Validators.compose([Validators.required, Validators.maxLength(15)])],
      password: ["", Validators.compose([Validators.required, Validators.minLength(8), Validators.maxLength(15), PasswordValidator.passwordStrength])],
      age: [16, Validators.compose([Validators.required, Validators.min(16)])]
    });
  }

  register(registerModel: RegisterModel) {
    this.authService.register(registerModel).subscribe({
      next: () => {
        console.log("Registered successfully!");
        this.router.navigate(["/login"]);
      },
      error: err => {
        console.log("Error: ", err);
      }
    });
  }

  toggleHidden(event: MouseEvent) {
    this.passwordHidden.set(!this.passwordHidden());
    event.stopPropagation();
  }
}
