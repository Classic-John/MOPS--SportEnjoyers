import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateGroup } from '../../../shared/interfaces/groups/create-group.interface';
import { GroupService } from '../../../shared/services/group/group.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent {
  createGroupForm: FormGroup;

  constructor(private readonly groupService: GroupService, fb: FormBuilder) {
    this.createGroupForm = fb.group({
      name: ["", Validators.compose([Validators.required, Validators.maxLength(25)])]
    });
  }

  createGroup(form: CreateGroup) {
    this.groupService.create(form).subscribe({
      next: () => {
        console.log("Ok!");
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }
}
