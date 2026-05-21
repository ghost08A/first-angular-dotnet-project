import { Component, inject } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { PrimeService } from './primeService';
import { AtmComponent } from '../atm/atm.component';
import { HeaderComponent } from '../header/header.component';

function mustMoreThen1(control: AbstractControl) {
  if (Number(control.value) > 1) {
    return null;
  }
  return { mustMoreThen1: true };
}

@Component({
  selector: 'app-prime-number',
  standalone: true,
  imports: [ReactiveFormsModule, AtmComponent, HeaderComponent],
  templateUrl: './prime-number.component.html',
  styleUrl: '../shared/components/form/form.component.css',
})
export class PrimeNumberComponent {
  PreviousPrime: number | null = null;
  NextPrime: number | null = null;
  private primeService = inject(PrimeService);
  isSubmitted = false;

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
    this.isSubmitted = true;
    this.primeService.calculate(inputValue).subscribe({
      next: (response) => {
        this.PreviousPrime = response.previousPrime;
        this.NextPrime = response.nextPrime;
      },
      error: (error) => {
        console.error('Error calculating prime numbers:', error);
      },
    });
  }
}
