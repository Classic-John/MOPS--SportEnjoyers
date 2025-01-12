import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { GroupFilter } from '../../../shared/interfaces/groups/group-filter.interface';
import { Group } from '../../../shared/interfaces/groups/group.interface';
import { GroupService } from '../../../shared/services/group/group.service';
import { AuthorizationService } from '../../../shared/services/auth/authorization.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
  groupList: Group[] = [];
  filterForm!: FormGroup;
  isLoggedIn = AuthorizationService.isLoggedIn;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly groupService: GroupService,
    fb: FormBuilder
  ) {
    this.route.queryParamMap.subscribe(params => {
      let filter = new GroupFilter(params);
      this.filterForm = fb.group({
        name: filter.name ?? "",
        owner: filter.owner ?? "",
        yours: filter.yours
      });

      this.groupService.getAllThatMatch(filter).subscribe({
        next: (groups) => {
          this.groupList = groups;
          console.log(groups);
        },
        error: (err) => {
          console.log("Error: ", err);
        }
      });
    });
  }

  search(groupFilter: GroupFilter) {
    this.router.navigate(
      [],
      {
        relativeTo: this.route,
        queryParams: groupFilter,
        queryParamsHandling: 'merge'
      }
    );
  }
}
