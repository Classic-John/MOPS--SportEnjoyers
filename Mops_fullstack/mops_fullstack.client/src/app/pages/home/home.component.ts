import { Component } from '@angular/core';
import { PlayerService } from '../../shared/services/player/player.service';
import { AuthorizationService } from '../../shared/services/auth/authorization.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(private readonly playerService: PlayerService, private readonly authService: AuthorizationService) { }

  getAllPlayers() {
    this.playerService.getAll().subscribe({
      next: players => {
        console.log(players);
      },
      error: err => {
        console.log("Error: ", err);
      }
    })
  }
}
