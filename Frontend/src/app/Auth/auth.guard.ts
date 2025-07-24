import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ApiServiceService } from '../services/api-service.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private api: ApiServiceService, private router: Router) {}

  canActivate(): boolean {
    if (this.api.isLoggedIn()) {
      return true; // ✅ Allow route access
    }

    // 🚫 Redirect to login if not authenticated
    this.router.navigate(['/login']);
    return false;
  }
}