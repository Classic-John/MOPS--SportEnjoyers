import { AbstractControl, ValidationErrors } from "@angular/forms";

export function ValueInArray(values: any[]): (control: AbstractControl) => ValidationErrors | null {
  return (control) => {
    return values.some((value, _index, _array) => value === control.value) ? null : { value: "Input invalid." };
  }
}
