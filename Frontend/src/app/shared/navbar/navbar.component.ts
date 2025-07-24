import { Component, ElementRef, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  constructor(private router: Router, private eRef: ElementRef) {
    this.router.events.subscribe(() => {
      this.closeDropdown();
    });
  }

  isMenuOpen = false;
  showDropdown = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  toggleDropdown() {
    this.showDropdown = !this.showDropdown;
  }

  closeDropdown() {
    this.showDropdown = false;
  }

  // 👇 Automatically closes dropdown when clicking outside
  @HostListener('document:click', ['$event'])
  onClickOutside(event: Event) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.closeDropdown();
    }
  }

  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  get userRole(): string | null {
    return localStorage.getItem('role'); // Retrieve stored role
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    alert('Logged out successfully');
    this.router.navigate(['/']);
  }

  // Optional dashboard navigation logic is still commented for future use
}