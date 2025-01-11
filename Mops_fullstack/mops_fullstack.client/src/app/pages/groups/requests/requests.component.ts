import { Component } from '@angular/core';
import { PlayerService } from '../../../shared/services/player/player.service';
import { GroupService } from '../../../shared/services/group/group.service';
import { JoinRequest } from '../../../shared/interfaces/requests/join-request.interface';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrl: './requests.component.css'
})
export class RequestsComponent {
  requestList: JoinRequest[] = []

  constructor(playerService: PlayerService, private readonly groupService: GroupService) {
    playerService.getRequests().subscribe({
      next: (requestList) => {
        this.requestList = requestList;
        console.log(this.requestList);
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  sendVerdict(groupId: Number, playerId: Number, verdict: Boolean) {
    this.groupService.sendJoinVerdict(groupId, { playerId: playerId, accepted: verdict }).subscribe({
      next: () => {
        this.requestList = this.requestList.filter((request, _index, _array) => 
          request.group.id != groupId || request.player.id != playerId
        );
        console.log("Verdict submitted successfully!");
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    })
  }
}
