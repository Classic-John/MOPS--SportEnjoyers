import { Component } from '@angular/core';
import { Match } from '../../../../shared/interfaces/matches/match.interface';
import { GroupService } from '../../../../shared/services/group/group.service';
import { MatchService } from '../../../../shared/services/match/match.service';
import { ActivatedRoute } from '@angular/router';
import { Group } from '../../../../shared/interfaces/groups/group.interface';
import { stringToDate } from '../../../../shared/utils/string-date-conversion.utils';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrl: './matches.component.css'
})
export class MatchesComponent {
  matchList: Match[] = [];
  isYours: Boolean = false;

  constructor(
    route: ActivatedRoute,
    groupService: GroupService,
    private readonly matchService: MatchService
  ) {
    route.paramMap.subscribe({
      next: (params) => {
        let groupId = Number(params.get("id"));

        groupService.getMatches(groupId).subscribe({
          next: (matches: Match[]) => {
            this.matchList = matches
              .filter((match, _index, _array) => stringToDate(match.matchDate as string) != null)
              .sort((match_left, match_right) => {
                return stringToDate(match_right.matchDate as string)!.getTime() - stringToDate(match_left.matchDate as string)!.getTime();
             });
          },
          error: (err) => {
            console.log("Error: ", err);
          }
        });

        groupService.get(groupId).subscribe({
          next: (group: Group) => {
            this.isYours = group.isYours ?? false;
          },
          error: (err) => {
            console.log("Error: ", err);
          }
        });
      }
    });
  }

  deleteMatch(id: Number) {
    this.matchService.delete(id).subscribe({
      next: () => {
        this.matchList = this.matchList.filter((match, _index, _array) => match.id != id);
        console.log("Deleted match successfully!");
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }

  isUpcoming(matchDate: String) {
    return new Date().getTime() < stringToDate(matchDate as string)!.getTime();
  }
}
