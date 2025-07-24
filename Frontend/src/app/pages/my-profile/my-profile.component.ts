import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiServiceService } from '../../services/api-service.service';
import { RouterModule, Router } from '@angular/router';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-my-profile',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent {
  private api = inject(ApiServiceService);
  private router = inject(Router);

  passenger: any = null;
  reservations: any[] = [];
  loading: boolean = true;
  error: string = '';

  // 🩺 Wellness Kit Booking
  kitOptions = [
    { value: 'Basic', description: 'Mask + Sanitizer + Water Bottle' },
    { value: 'Comfort', description: 'Neck Pillow + Napkins + Herbal Tea' },
    { value: 'Medical', description: 'First Aid + Pain Reliever' },
    { value: 'Deluxe', description: 'All-in-One + Travel Snacks' }
  ];
  noteOptions = [
    'Require contactless delivery',
    'Prefer eco-friendly packaging',
    'Include printed travel tips'
  ];
  selectedKit = '';
  selectedNotes: string[] = [];
  stationInput = '';
  bookingMessage = '';

  ngOnInit() {
    if (!this.api.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }

    this.api.get('PassengerDetail/profile').subscribe({
      next: (data) => {
        this.passenger = data?.passengerDetail || null;
        this.reservations = data?.reservations || [];
        this.error = '';
        this.loading = false;
      },
      error: (err) => {
        this.error = err?.error || 'Unauthorized. Please login again.';
        this.loading = false;
        console.error('Profile error:', err);
      }
    });
    this.loadBookedKits();
    this.loadCateringOrders();

  }

  onNoteToggle(note: string, checked: boolean) {
    if (checked) {
      if (!this.selectedNotes.includes(note)) {
        this.selectedNotes.push(note);
      }
    } else {
      this.selectedNotes = this.selectedNotes.filter(n => n !== note);
    }
  }

  handleNoteCheckboxChange(event: Event, note: string): void {
    const target = event.target as HTMLInputElement;
    const isChecked = target.checked;
    this.onNoteToggle(note, isChecked);
  }

  bookWellnessKit() {
    const userId = this.api.getUserId();
    const payload = {
      kitType: this.selectedKit,
      notes: this.selectedNotes.join(', '),
      stationName: this.stationInput,
      applicationUserId: userId
    };

    this.api.bookWellnessKit(payload).subscribe({
      next: () => {
        this.bookingMessage = '✅ Wellness kit booked successfully!';
        this.selectedKit = '';
        this.selectedNotes = [];
        this.stationInput = '';
      },
      error: (err) => {
        this.bookingMessage = '❌ Failed to book wellness kit.';
        console.error('Booking error:', err);
      }
    });
  }

  bookedKits: any[] = [];

loadBookedKits() {
  this.api.getMyWellnessKits().subscribe({
    next: (data) => {
      this.bookedKits = data || [];
    },
    error: (err) => {
      console.error('Failed to load booked kits:', err);
    }
  });
}
deleteKit(id: number) {
  if (!confirm('Are you sure you want to cancel this wellness kit booking?')) return;

  this.api.deleteWellnessKit(id).subscribe({
    next: () => {
      this.bookingMessage = '🗑️ Kit cancelled successfully.';
      this.bookedKits = this.bookedKits.filter(k => k.id !== id);
    },
    error: (err) => {
      this.bookingMessage = '❌ Failed to cancel kit.';
      console.error('Delete error:', err);
    }
  });
}
  formatDate(dateStr: string): string {
    const date = new Date(dateStr);
    return date.toLocaleDateString('en-IN', {
      day: 'numeric', month: 'long', year: 'numeric'
    });
  }
  mealTypes = ['Breakfast', 'Lunch', 'Dinner', 'Snacks'];
  preferences = ['Vegetarian', 'Non-Vegetarian', 'Vegan', 'Gluten-Free'];
  
  selectedMealType = '';
  selectedPreference = '';
  deliveryDate: string = '';
  deliveryStation = '';
  cateringMessage = '';
  
  bookCateringOrder() {
    const userId = this.api.getUserId();
  
    const payload = {
      mealType: this.selectedMealType,
      preference: this.selectedPreference,
      deliveryDate: this.deliveryDate,
      deliveryStation: this.deliveryStation,
      applicationUserId: userId
    };
  
    this.api.bookCateringOrder(payload).subscribe({
      next: () => {
        this.cateringMessage = '✅ Meal booked successfully!';
        this.selectedMealType = '';
        this.selectedPreference = '';
        this.deliveryDate = '';
        this.deliveryStation = '';
      },
      error: (err) => {
        this.cateringMessage = '❌ Failed to book meal.';
        console.error('Meal booking error:', err);
      }
    });
  }
cateringOrders: any[] = [];

loadCateringOrders() {
  this.api.getMyCateringOrders().subscribe({
    next: (data) => {
      this.cateringOrders = data || [];
    },
    error: (err) => {
      console.error('❌ Failed to fetch catering orders:', err);
    }
  });
}
  

  generateInvoicePDF(reservation: any) {
    const doc = new jsPDF();
    const passengers = reservation.passengers || [];
  
    // 🎟️ Header Branding
    doc.setFillColor(240, 248, 255); // background
doc.rect(10, 10, 190, 20, 'F'); // header box

doc.setFontSize(16);
doc.setTextColor('#003366');

// Get page and text width
const pageWidth = doc.internal.pageSize.getWidth();
const title = 'Indian Railways Digital Reservation Ticket';
const textWidth = doc.getTextWidth(title);

// Calculate center position
const centerPosition = (pageWidth - textWidth) / 2;

// 🎯 Centered title
doc.text(title, centerPosition, 23);

doc.setFontSize(10);
    // doc.setTextColor('#333');
    // doc.text('Digital Railway Reservation Ticket', 150, 23);
  
    doc.setFontSize(12);
    doc.setTextColor('#000000');
    doc.text(`PNR #: ${reservation.pnr}`, 15, 40);
    doc.text(`Travel Date: ${this.formatDate(reservation.travelDate)}`, 140, 40);
  
    // 👥 Passenger List
    let y = 48;
    passengers.forEach((p: any, i: number) => {
      doc.text(`Passenger ${i + 1}: ${p.name}, ${p.gender}, Age ${p.age}`, 15, y);
      y += 8;
    });
  
    // 🛤️ Train Details Box
    doc.setDrawColor(200, 200, 200);
    doc.rect(10, y, 190, 70);
    y += 10;
    doc.text(`Train: ${reservation.train.name} (${reservation.train.trainNumber})`, 15, y);
    doc.text(`Class: ${reservation.travelClass}`, 15, y + 8);
    doc.text(`From: ${reservation.train.sourceStation}`, 15, y + 16);
    doc.text(`To: ${reservation.train.destinationStation}`, 15, y + 24);
    doc.text(`Departure: ${reservation.train.departureTime}`, 15, y + 32);
    doc.text(`Arrival: ${reservation.train.arrivalTime}`, 15, y + 40);
    doc.text(`Coach/Berth: Assigned at boarding`, 15, y + 48);
  
    // 💰 Payment Details
    doc.text(`Bank: ${reservation.payment.bankName}`, 120, y + 16);
    doc.text(`Card #: ${reservation.payment.creditCardNumber}`, 120, y + 24);
    doc.text(`Paid: ₹${Number(reservation.payment.amountPaid).toFixed(2)}`, 120, y + 32);
    doc.text(`Payment Date: ${this.formatDate(reservation.payment.paymentDate)}`, 120, y + 40);
  
    // 🔻 Footer
    doc.line(10, y + 60, 200, y + 60);
    doc.setFontSize(9);
    doc.setTextColor('#444');
    doc.text('This is a digital ticket. No physical print required.', 15, y + 70);
    doc.text('Please carry a valid ID proof during travel.', 15, y + 77);
    doc.text('Need help? Visit: www.indianrailways.gov.in', 15, y + 84);
  
    doc.save(`TrainTicket_${reservation.pnr}.pdf`);
  }

}

