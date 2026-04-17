import { Component, inject } from '@angular/core';
import { Auth } from '../../services/auth'
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private auth = inject(Auth);
  private router = inject(Router);


  loginData = { email: '', password: '' };

  onSubmit() {
    this.auth.login(this.loginData.email, this.loginData.password).subscribe({
      next: () => {
        console.log('Login successful!');
        this.router.navigate(['/']);
      },
      error: (err) => console.error('Login failed', err)
    });

  }
}
