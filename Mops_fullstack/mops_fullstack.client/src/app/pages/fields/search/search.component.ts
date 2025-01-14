import { Component } from '@angular/core';
import { FieldFilter } from '../../../shared/interfaces/fields/field-filter.interface';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthorizationService } from '../../../shared/services/auth/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { dateToString, stringToDate } from '../../../shared/utils/string-date-conversion.utils';
import { FieldService } from '../../../shared/services/field/field.service';
import { Field } from '../../../shared/interfaces/fields/field.interface';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
  fieldList: Field[] = [];
  filterForm!: FormGroup;
  isLoggedIn = AuthorizationService.isLoggedIn;
  searchByDay: Boolean = false

  constructor(
    fieldService: FieldService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    fb: FormBuilder
  ) {
    this.route.queryParamMap.subscribe(params => {
      let filter = new FieldFilter(params);
      filter.yours = filter.yours && this.isLoggedIn();

      this.filterForm = fb.group({
        name: filter.name ?? "",
        owner: filter.owner ?? "",
        location: filter.location ?? "",
        yours: filter.yours,
        freeOnDay: stringToDate(filter.freeOnDay as string)
      });
      if (filter.freeOnDay != "") {
        this.searchByDay = true;
      }

      fieldService.getAllThatMatch(filter).subscribe({
        next: (fields) => {
          this.fieldList = fields;
        },
        error: (err) => {
          console.log("Error: ", err);
        }
      });
    });
  }

  search(fieldFilter: FieldFilter) {
    fieldFilter.freeOnDay = dateToString(new Date(fieldFilter.freeOnDay as string));
    if (fieldFilter.yours) {
      fieldFilter.owner = "";
    }
    if (!this.searchByDay) {
      fieldFilter.freeOnDay = "";
    }

    this.router.navigate(
      [],
      {
        relativeTo: this.route,
        queryParams: fieldFilter,
        queryParamsHandling: 'merge'
      }
    );
  }

  isYoursActivated() {
    let form: FieldFilter = this.filterForm.getRawValue();
    return form.yours == true;
  }
}
