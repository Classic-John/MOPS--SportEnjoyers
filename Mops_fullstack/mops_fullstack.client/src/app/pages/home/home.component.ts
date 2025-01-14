import { Component } from '@angular/core';
import { PlayerService } from '../../shared/services/player/player.service';
import { AuthorizationService } from '../../shared/services/auth/authorization.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import PasswordValidator from '../../shared/validators/password-validator.validator';
import { Equals } from '../../shared/validators/equals-validator.validator';
import { UpdatePlayer } from '../../shared/interfaces/players/update-player.interface';
import { LoggedPlayer } from '../../shared/interfaces/players/logged-player.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  updatePlayerForm: FormGroup;
  loggedInPlayer = AuthorizationService.getLoggedInPlayer;
  oldPasswordVisible: Boolean = false;
  passwordVisible: Boolean = false;
  passwordConfirmationVisible: Boolean = false;

  constructor(
    private readonly playerService: PlayerService,
    private readonly router: Router,
    private readonly authorizationService: AuthorizationService,
    formBuilder: FormBuilder
  ) {
    this.updatePlayerForm = formBuilder.group({
      name: [this.loggedInPlayer()!.name, Validators.maxLength(25)],
      age: [this.loggedInPlayer()!.age, Validators.min(16)],
      oldPassword: ["", PasswordValidator.passwordStrength],
      password: ["", PasswordValidator.passwordStrength],
      passwordConfirm: ["", PasswordValidator.passwordStrength]
    });
    this.updatePlayerForm.addValidators(Equals('password', 'passwordConfirm'));
  }

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

  updatePlayer(player: UpdatePlayer) {
    if (player.name === this.loggedInPlayer()!.name) {
      player.name = undefined;
    }
    if (player.age === this.loggedInPlayer()!.age) {
      player.age = undefined;
    }
    if (player.oldPassword === "" || player.password === "") {
      player.oldPassword = undefined;
      player.password = undefined;
    }

    this.playerService.update(player).subscribe({
      next: (newPlayer: LoggedPlayer) => {
        this.authorizationService.setPlayer(JSON.stringify(newPlayer));
        console.log("Successfully updated player data!");
        window.location.reload();
      }
    })
  }

  toggleOldPasswordVisible() {
    this.oldPasswordVisible = this.oldPasswordVisible ? false : true;
  }

  togglePasswordVisible() {
    this.passwordVisible = this.passwordVisible ? false : true;
  }

  togglePasswordConfirmationVisible() {
    this.passwordConfirmationVisible = this.passwordConfirmationVisible ? false : true;
  }
}
