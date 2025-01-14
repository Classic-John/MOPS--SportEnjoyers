import { AbstractControl, ValidationErrors } from "@angular/forms";

function passwordStrength(control: AbstractControl): ValidationErrors | null {
  const password = control.value;
  if (password === "") {
    return null;
  }

  const hasUpperCase = /[A-Z]/.test(password);
  const hasLowerCase = /[a-z]/.test(password);
  const hasNumericChar = /[0-9]/.test(password);
  const hasSpecialChar = /[!@#$%^&*(),.?:{}|<>]/.test(password);

  return hasUpperCase && hasLowerCase && hasNumericChar && hasSpecialChar ?
    null :
    {
      value: "Password invalid."
    };
}

const PasswordValidator = {
  passwordStrength
};

export default PasswordValidator;
