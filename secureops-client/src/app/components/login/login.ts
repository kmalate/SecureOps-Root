import { Component, inject, signal } from '@angular/core';
import { Auth } from '../../services/auth'
import { Router } from '@angular/router';
import { email, form, FormField, required } from '@angular/forms/signals'

interface loginData {
  email: string;
  password: string;
}

@Component({
  selector: 'app-login',
  imports: [FormField],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private auth = inject(Auth);
  private router = inject(Router);

  loginModel= signal<loginData>({ email: '', password: '' });
  loginForm = form(this.loginModel, (schemaPath) => {
    required(schemaPath.email, {message: 'Email is required'});
    email(schemaPath.email, {message: 'Enter a valid email address'});
    required(schemaPath.password, {message: 'Password is required'});
  });

  onSubmit(event: Event) {
    event.preventDefault();
    const loginData = this.loginModel();
    this.auth.login(loginData.email, loginData.password).subscribe({
      next: () => {
        console.log('Login successful!');
        this.router.navigate(['/']);
      },
      error: (err) => console.error('Login failed', err)
    });

  }
}
