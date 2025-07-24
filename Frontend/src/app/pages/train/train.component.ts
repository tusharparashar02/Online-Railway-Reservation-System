import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiServiceService } from '../../services/api-service.service';
import { Router } from '@angular/router';

interface FareDto {
  class: string;
  adultFare: number;
  childFare: number;
}
interface TrainDto {
  id: number;
  trainNumber: string;
  name: string;
  sourceStation: string;
  destinationStation: string;
  departureTime: string;
  arrivalTime: string;
  fares: FareDto[];
}

@Component({
  selector: 'app-train',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './train.component.html',
  styleUrls: ['./train.component.css']
})
export class TrainComponent {
  private api = inject(ApiServiceService);
  private router = inject(Router);

  trains: TrainDto[] = [];
  selectedTrain?: TrainDto;

  source: string = '';
  destination: string = '';

  ngOnInit() {
    this.getAllTrains();
  }

  getAllTrains() {
    this.api.get('Trains').subscribe({
      next: data => this.trains = data,
      error: err => console.error('Failed to load trains', err)
    });
  }

  getTrainById(id: number) {
    this.api.get(`Trains/${id}`).subscribe({
      next: data => {
        console.log('Train Details:', data); // Check fare structure here
        this.selectedTrain = data;
      },
      error: err => console.error('Failed to fetch train details', err)
    });
  }

  searchTrains() {
    if (!this.source || !this.destination) return;

    this.api.getTrainsByRoute(this.source, this.destination).subscribe({
      next: data => this.trains = data,
      error: err => {
        console.error('Search failed:', err);
        alert('No trains found or server error. Please check your input or try again.');
      }
    });
  }

  resetSearch() {
    this.source = '';
    this.destination = '';
    this.selectedTrain = undefined;
    this.getAllTrains();
  }
  bookTicket(trainId: number) {
    if (!this.api.isLoggedIn()) {
      alert('⚠️ Please login first to book a ticket.');
      this.router.navigate(['/login']);
      return;
    }  
    this.router.navigate(['/reservations', trainId]);
  }

}