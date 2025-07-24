import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { FooterComponent } from './shared/footer/footer.component';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, FooterComponent, HttpClientModule], // Add HttpClientModule here
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Frontend';

  constructor(private router: Router) { }
  get isLoggedIn(): boolean{
    return !!localStorage.getItem('token');
  }
  logout() {
    localStorage.removeItem('token');
    alert('Logged out successfully');
    this.router.navigate(['/login']);
  }
}