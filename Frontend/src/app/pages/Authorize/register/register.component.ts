import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ApiServiceService } from '../../../services/api-service.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  registerData = {
    email: '',
    password: '',
    fullName: '',
    gender: '',
    age: 0,
    address: '',
  };

  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private apiService: ApiServiceService,
    private http: HttpClient,
    private router: Router
  ) {}

  register() {
    this.apiService.post('Auth/register', this.registerData).subscribe({
      next: (res: any) => {
        // User registered successfully
        this.successMessage = res.message || 'Registration successful!';
        alert(this.successMessage);
        this.router.navigate(['/login']);
        this.resetForm();
      },
      error: (err) => {
        console.error('Registration error:', err);

        // Check for foreign key constraint error
        if (
          err.status === 500 &&
          typeof err.error === 'string' &&
          err.error.includes('FK_PassengerDetails_Reservations')
        ) {
          this.errorMessage =
            'Your account was created successfully, but passenger details could not be linked. Please contact support or book a reservation later.';
        } else {
          this.errorMessage =
            err.error || 'Failed to register. Please try again.';
        }

        // Optional: show alert for user-friendly error message
        alert(this.errorMessage);
      },
    });
  }

  resetForm() {
    this.registerData = {
      email: '',
      password: '',
      fullName: '',
      gender: '',
      age: 0,
      address: '',
    };
  }
}