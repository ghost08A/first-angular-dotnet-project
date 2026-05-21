import { Component, inject } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AtmService } from './atm.service';

function mustMoreThen0(control: AbstractControl) {
  if (control.value > 0) {
    return null;
  }
  return { mustBePositive: true };
}

@Component({
  selector: 'app-atm',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './atm.component.html',
  styleUrl: '../shared/components/form/form.component.css',
})
export class AtmComponent {
  thousand: null | number = null;
  fiveHundred: null | number = null;
  oneHundred: null | number = null;

  private atmService = inject(AtmService);

  isSubmitted = false;

  form = new FormGroup({
    numberInput: new FormControl<number | null>(null, {
      validators: [Validators.required, mustMoreThen0],
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
    this.isSubmitted = true;
    const amountToWithdraw = Number(this.form.value.numberInput);
    this.atmService.withdraw(amountToWithdraw).subscribe({
      next: (response) => {
        this.thousand = response.thousand;
        this.fiveHundred = response.fiveHundred;
        this.oneHundred = response.oneHundred;
      },
      error: (error) => {
        console.error('Error withdrawing money:', error);
      },
    });
  }
}
