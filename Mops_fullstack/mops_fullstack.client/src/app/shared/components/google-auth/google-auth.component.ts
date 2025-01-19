import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from '../../services/auth/authorization.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-google-auth',
  templateUrl: './google-auth.component.html',
  styleUrl: './google-auth.component.css'
})
export class GoogleAuthComponent implements OnInit {
  constructor(
    private readonly authService: AuthorizationService,
    private readonly router: Router
  ) { }

  ngOnInit() {
    google.accounts.id.initialize({
      client_id: "48746027409-if10lmj8kh5bfhr9l3pr7q47poiaucjs.apps.googleusercontent.com",
      callback: (response: google.accounts.id.CredentialResponse) => {
        this.authService.googleAuth({ key: response.credential }).subscribe({
          next: () => {
            console.log("Logged in via Google successfully!");
            this.router.navigate([""]);
          },
          error: (err) => {
            console.log("Error: ", err);
          }
        });
      }
    });
    google.accounts.id.renderButton(
      document.getElementById('googleLogin')!,
      { locale: "en", theme: "outline", size: "large", type: "standard" }
    );
    google.accounts.id.prompt();
  }
}
