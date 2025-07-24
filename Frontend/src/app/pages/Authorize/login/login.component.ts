import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ApiServiceService } from '../../../services/api-service.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginData = { email: '', password: '' };
  errorMessage: string = '';
  //apiUrl = 'http://localhost:5237/api/auth/login'; // Replace with your backend URL

  constructor(private apiService: ApiServiceService, private router: Router) {}

  login() {
    this.apiService.post('auth/login', this.loginData).subscribe({
      next: (res: any) => {
        const token = typeof res === 'string' ? res : res.token;
        console.log('Token received:');
        if (token) {
          console.log('Token:', res.token || token);
          this.apiService.setToken(token);
          alert('Login successful!');
          this.router.navigate(['/trains']);
        } else {
          console.error('Token is missing in the response');
          this.errorMessage = 'Failed to login. Please try again.';
        }
      },
      error: (err) => {
        console.error('Login error:', err);
        this.errorMessage = 'Invalid credentials. Please try again.';
      },
    });
  }
}