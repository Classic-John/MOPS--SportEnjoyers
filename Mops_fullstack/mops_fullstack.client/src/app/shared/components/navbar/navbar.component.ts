import { Component } from '@angular/core';
import { AuthorizationService } from '../../services/auth/authorization.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  isLoggedIn = AuthorizationService.isLoggedIn;

  logout() {
    AuthorizationService.logout();
    console.log("Successfully logged out!");
    window.location.reload();
  }
}
