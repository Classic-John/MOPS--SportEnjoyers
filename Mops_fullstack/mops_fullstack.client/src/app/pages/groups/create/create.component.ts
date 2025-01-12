import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateGroup } from '../../../shared/interfaces/groups/create-group.interface';
import { GroupService } from '../../../shared/services/group/group.service';
import { Router } from '@angular/router';
import { Group } from '../../../shared/interfaces/groups/group.interface';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent {
  createGroupForm: FormGroup;

  constructor(
    private readonly router: Router,
    private readonly groupService: GroupService,
    fb: FormBuilder
  ) {
    this.createGroupForm = fb.group({
      name: ["", Validators.compose([Validators.required, Validators.maxLength(25)])]
    });
  }

  createGroup(form: CreateGroup) {
    this.groupService.create(form).subscribe({
      next: (group: Group) => {
        console.log("Created new group successfully!");
        this.router.navigate([`/groups/${group.id}`]);
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }
}
