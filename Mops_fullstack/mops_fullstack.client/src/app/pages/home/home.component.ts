import { Component } from '@angular/core';
import { PlayerService } from '../../shared/services/player/player.service';
import { AuthorizationService } from '../../shared/services/auth/authorization.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  loggedInPlayer = AuthorizationService.getLoggedInPlayer

  constructor(private readonly playerService: PlayerService, private readonly router: Router) { }

  deleteAccount() {
    this.playerService.delete().subscribe({
      next: () => {
        console.log("Deleted account successfully!");
        AuthorizationService.logout();
        this.router.navigate(["/login"]);
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }
}
