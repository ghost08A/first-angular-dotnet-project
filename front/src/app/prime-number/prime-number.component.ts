import { Component } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

function mustMoreThen1(control: AbstractControl) {
  if (Number(control.value) > 1) {
    return null;
  }
  return { mustMoreThen1: true };
}

@Component({
  selector: 'app-prime-number',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './prime-number.component.html',
  styleUrl: '../shared/components/form/form.component.css',
})
export class PrimeNumberComponent {
  PreviousPrime: number | null = null;
  NextPrime: number | null = null;

  form = new FormGroup({
    numberInput: new FormControl<number | null>(null, {
      validators: [Validators.required, mustMoreThen1],
    }),
  });

  get numberInputIsInvalid() {
    return (
      this.form.controls.numberInput.touched &&
      this.form.controls.numberInput.invalid &&
      this.form.controls.numberInput.dirty
    );
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }
    const inputValue = Number(this.form.value.numberInput);
    this.PreviousPrime = inputValue - 1;
    this.NextPrime = inputValue + 1;
  }
}
