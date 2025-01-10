import { Component } from '@angular/core';
import { Group } from '../../../shared/interfaces/groups/group.interface';
import { GroupService } from '../../../shared/services/group/group.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizationService } from '../../../shared/services/auth/authorization.service';
import { PlayerService } from '../../../shared/services/player/player.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrl: './view.component.css'
})
export class ViewComponent {
  group: Group | null = null;
  groupId: Number | null = null;
  isPartOf: Boolean = false;
  pendingJoinRequest: Boolean = true;

  constructor(route: ActivatedRoute, private readonly groupService: GroupService, private readonly playerService: PlayerService) {
    route.paramMap.subscribe({
      next: (params) => {
        let id: Number = Number(params.get('id'));


        this.groupService.get(id).subscribe({
          next: (group) => {
            this.group = group;
            this.groupId = id;
          },
          error: (err) => {
            console.log("Error: ", err);
          }
        });

        this.playerService.isMemberOf(id).subscribe({
          next: () => {
            this.isPartOf = true;
            this.pendingJoinRequest = false;
          },
          error: (err: HttpErrorResponse) => {
            if (err.status === 404) {
              this.isPartOf = false;
              this.pendingJoinRequest = false;
            } else if (err.status === 403) {
              this.isPartOf = false;
              this.pendingJoinRequest = true;
            } else{ 
              console.log("Error: ", err);
            }
          }
        });
      }
    });
  }

  sendJoinRequest() {
    this.playerService.joinGroup(this.groupId!).subscribe({
      next: () => {
        console.log("Send join request successfully!");
        this.pendingJoinRequest = true;
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }
}
