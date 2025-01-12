import { Component } from '@angular/core';
import { Field } from '../../../shared/interfaces/fields/field.interface';
import { ActivatedRoute, Router } from '@angular/router';
import { FieldService } from '../../../shared/services/field/field.service';
import { GroupService } from '../../../shared/services/group/group.service';
import { Group } from '../../../shared/interfaces/groups/group.interface';
import { AuthorizationService } from '../../../shared/services/auth/authorization.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValueInArray } from '../../../shared/validators/in-array-validator.validator';
import { dateToString, stringToDate } from '../../../shared/utils/string-date-conversion.utils';
import { CreateMatch } from '../../../shared/interfaces/matches/create-match.interface';
import { MatchService } from '../../../shared/services/match/match.service';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrl: './view.component.css'
})
export class ViewComponent {
  field: Field | null = null;
  fieldId: Number | null = null;
  groupList: Group[] = [];
  isLoggedIn = AuthorizationService.isLoggedIn;
  reserveFieldForm!: FormGroup;
  reservedDates: (d: Date | null) => boolean = (_d) => true;
  today = new Date();

  constructor(
    route: ActivatedRoute,
    private readonly fieldService: FieldService,
    groupService: GroupService,
    private readonly matchService: MatchService,
    private readonly router: Router,
    fb: FormBuilder,
  ) {
    route.paramMap.subscribe({
      next: (params) => {
        let id: Number = Number(params.get('id'));
        this.fieldId = id;

        fieldService.get(id).subscribe({
          next: (field) => {
            this.field = field;
            console.log(this.field);
            this.setReservedDates();

            if (!this.isLoggedIn) {
              this.reserveFieldForm = fb.group({});
              return;
            }
            groupService.getAllThatMatch({ name: "", owner: "", yours: true }).subscribe({
              next: (groups) => {
                this.groupList = groups;
                console.log(this.groupList);

                this.reserveFieldForm = fb.group({
                  fieldId: id,
                  groupId: [0, Validators.compose([Validators.required, ValueInArray(this.groupList.map((group, _index, _array) => group.id))])],
                  matchDate: ["", Validators.required]
                });
              },
              error: (err) => {
                console.log("Error: ", err);
              }
            });
          },
          error: (err) => {
            console.log("Error: ", err);
          }
        });
      }
    });
  }

  reserveField(match: CreateMatch) {
    match.matchDate = dateToString(new Date(match.matchDate as string));
    this.matchService.create(match).subscribe({
      next: (match) => {
        console.log("Successfully created reservation.");
        this.router.navigate(["/groups/matches"]);
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }

  setReservedDates() {
    this.reservedDates = (d: Date | null) => {
      let search_date = dateToString(d ?? new Date());
      return !(this.field?.reservedDates ?? []).some((date, _index, _array) => date === search_date);
    };
  }

  deleteField() {
    this.fieldService.delete(this.fieldId!).subscribe({
      next: () => {
        console.log("Field deleted successfully!");
        this.router.navigate(["/fields"]);
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }
}
