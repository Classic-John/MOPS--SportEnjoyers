import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FieldService } from '../../../shared/services/field/field.service';
import { CreateField } from '../../../shared/interfaces/fields/create-field.interface';
import { Field } from '../../../shared/interfaces/fields/field.interface';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent {
  createFieldForm: FormGroup;

  constructor(
    private readonly router: Router,
    private readonly fieldService: FieldService,
    fb: FormBuilder
  ) {
    this.createFieldForm = fb.group({
      name: ["", Validators.compose([Validators.required, Validators.maxLength(25)])],
      location: ["", Validators.compose([Validators.required, Validators.maxLength(50)])]
    });
  }

  createField(form: CreateField) {
    this.fieldService.create(form).subscribe({
      next: (field: Field) => {
        console.log("Created new field successfully!");
        this.router.navigate([`/fields/${field.id}`]);
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    });
  }
}
