import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule, Router } from '@angular/router';
import { ApiServiceService } from '../../services/api-service.service';

@Component({
  selector: 'app-reservations',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationComponent {
  private api = inject(ApiServiceService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  trainId: number = 0;
  travelDate: string = '';
  travelClass: string = '';
  userId: string = '';

  passengers = [
    {
      name: '',
      gender: '',
      age: 0,
      address: '',
      reservationId: 0,
      applicationUserId: ''
    }
  ];

  payment = {
    id: 0,
    creditCardNumber: '',
    bankName: '',
    //amountPaid: 0,
    paymentDate: ''
  };

  ngOnInit() {
    this.trainId = Number(this.route.snapshot.paramMap.get('id'));

    this.userId = this.api.getUserId() || '';
    if (!this.userId) {
      alert("User ID missing. Please log in again.");
      this.router.navigate(['/login']);
      return;
    }

    this.payment.paymentDate = new Date().toISOString();
    this.passengers[0].applicationUserId = this.userId;
  }

  addPassenger() {
    this.passengers.push({
      name: '',
      gender: '',
      age: 0,
      address: '',
      reservationId: 0,
      applicationUserId: this.userId
    });
  }

  removePassenger(index: number) {
    this.passengers.splice(index, 1);
  }

  submitReservation() {
    const payload = {
      trainId: this.trainId,
      travelDate: new Date(this.travelDate),
      userId: this.userId,
      travelClass: this.travelClass,
      passengers: this.passengers,
      payment: {
        ...this.payment,
        paymentDate: new Date().toISOString()
      }
    };

    this.api.post('Reservations', payload).subscribe({
      next: res => {
        const amount = res?.payment?.amountPaid ?? 'Unknown';
        alert(`🎉 Reservation confirmed. Total Fare: ₹${amount}`);
        // alert(`🎉 Reservation confirmed. Total Fare: ₹${res.payment.amountPaid}`);
        console.log('Reservation:', res);
        this.router.navigate(['/profile']); // or another desired route
      },
      error: err => {
        console.error('Reservation failed:', err);
        alert('Something went wrong while booking. Please try again.');
      }
    });
  }
  
}