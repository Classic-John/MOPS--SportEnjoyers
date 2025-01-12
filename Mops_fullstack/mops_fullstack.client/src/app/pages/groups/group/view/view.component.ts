import { Component } from '@angular/core';
import { Group } from '../../../../shared/interfaces/groups/group.interface';
import { GroupService } from '../../../../shared/services/group/group.service';
import { GroupJoinStatus, GroupJoinStatusType } from '../../../../shared/interfaces/requests/join-status.interface';
import { AuthorizationService } from '../../../../shared/services/auth/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FieldService } from '../../../../shared/services/field/field.service';
import { Field } from '../../../../shared/interfaces/fields/field.interface';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrl: './view.component.css'
})
export class ViewComponent {
  group: Group | null = null;
  groupId: Number | null = null;
  joinStatus: GroupJoinStatusType = GroupJoinStatusType.NoRequest;
  isLoggedIn = AuthorizationService.isLoggedIn;

  constructor(route: ActivatedRoute, private readonly groupService: GroupService, private readonly router: Router) {
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
            router.navigate(["/groups"]);
          }
        });

        this.groupService.getJoinStatus(id).subscribe({
          next: (join: GroupJoinStatus) => {
            this.joinStatus = join.status;
          },
          error: (err) => {
            console.log("Error: ", err);
          }
        });
      }
    });
  }

  isPartOf(): Boolean {
    return this.isOwner() || this.joinStatus == GroupJoinStatusType.Joined;
  }

  isPendingJoin(): Boolean {
    return this.joinStatus == GroupJoinStatusType.Pending;
  }

  isOwner(): Boolean {
    return this.group?.isYours ?? false;
  }

  sendJoinRequest() {
    this.groupService.sendJoinRequest(this.groupId!).subscribe({
      next: () => {
        console.log("Send join request successfully!");
        this.joinStatus = GroupJoinStatusType.Pending;
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }

  leaveGroup() {
    this.groupService.leaveGroup(this.groupId!).subscribe({
      next: () => {
        this.joinStatus = GroupJoinStatusType.NoRequest;
        window.location.reload();
        console.log("Left group successfully!");
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    })
  }

  kickPlayer(playerId: Number) {
    this.groupService.kickFromGroup(this.groupId!, playerId).subscribe({
      next: () => {
        window.location.reload();
        console.log("Kicked player from group successfully!");
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }

  deleteGroup() {
    this.groupService.deleteGroup(this.groupId!).subscribe({
      next: () => {
        console.log("Deleted group successfully!");
        this.router.navigate(["/groups"]);
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }
}
