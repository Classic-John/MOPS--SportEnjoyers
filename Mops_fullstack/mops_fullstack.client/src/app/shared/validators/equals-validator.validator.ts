import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function Equals(param1: string, param2: string): ValidatorFn {
  return (control: AbstractControl) => {
    return control.root.get(param1)?.value === control.root.get(param2)?.value ?
      null :
      { value: `Fields ${param1} and ${param2} need to be equal.` };
  }
}
