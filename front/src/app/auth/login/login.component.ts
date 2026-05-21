import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { debounceTime, of } from 'rxjs';
import { AuthService } from '../auth.service';

let initalEmailValue = '';
const savedForm = window.localStorage.getItem('saved-login-form');

if (savedForm) {
  const loadedForm = JSON.parse(savedForm);
  initalEmailValue = loadedForm.email;
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  private router = inject(Router);
  private authService = inject(AuthService);

  isWrongPasswordAndEmail = false;
  form = new FormGroup({
    email: new FormControl(initalEmailValue, {
      validators: [Validators.email, Validators.required],
    }),
    password: new FormControl('', {
      validators: [Validators.required],
    }),
  });

  get emailIsInvalid() {
    return (
      this.form.controls.email.touched &&
      this.form.controls.email.invalid &&
      this.form.controls.email.dirty
    );
  }
  get passwordIsInvalid() {
    return (
      this.form.controls.password.touched &&
      this.form.controls.password.invalid &&
      this.form.controls.password.dirty
    );
  }

  ngOnInit() {
    const subscription = this.form.valueChanges
      .pipe(debounceTime(500))
      .subscribe({
        next: (value) => {
          window.localStorage.setItem(
            'saved-login-form',
            JSON.stringify(value),
          );
        },
      });
    this.destroyRef.onDestroy(() => subscription.unsubscribe());
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    const enteredData = {
      Email: this.form.value.email,
      Password: this.form.value.password,
    };
    this.authService
      .login(enteredData.Email ?? '', enteredData.Password ?? '')
      .subscribe({
        next: (response: any) => {
          console.log('Login successful:', response);
          window.localStorage.removeItem('saved-login-form');
          this.isWrongPasswordAndEmail = false;
          window.localStorage.setItem('token', response.token);
          this.router.navigate(['/home']);
        },
        error: (error) => {
          console.error('Login Failed', error);
          this.isWrongPasswordAndEmail = true;
        },
      });
  }
}
