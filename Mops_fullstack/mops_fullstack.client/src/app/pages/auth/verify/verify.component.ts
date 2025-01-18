import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizationService } from '../../../shared/services/auth/authorization.service';

@Component({
  selector: 'app-verify',
  templateUrl: './verify.component.html',
  styleUrl: './verify.component.css'
})
export class VerifyComponent {
  accepted: Boolean | null = null;

  constructor(route: ActivatedRoute, router: Router, authService: AuthorizationService) {
    route.queryParamMap.subscribe({
      next: (params) => {
        let code: string | null = params.get("code");

        if (code === null) {
          this.accepted = false;
          setTimeout(this.goToLogin(router), 5000);
          return;
        }

        authService.verify({ verificationCode: code }).subscribe({
          next: () => {
            this.accepted = true;
            setTimeout(this.goToLogin(router), 5000);
          },
          error: (err) => {
            console.log("Error: ", err);
            this.accepted = false;
            setTimeout(this.goToLogin(router), 5000);
          }
        })
      }
    })
  }

  goToLogin(router: Router) {
    return () => {
      router.navigate(["login"]);
    }
  }
}
