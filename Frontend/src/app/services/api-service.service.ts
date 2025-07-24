import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ApiServiceService {
  private baseUrl = 'http://localhost:5237/api';

  constructor(private http: HttpClient) {}

  // 🔐 Constructs headers with token and content type
  private getHeaders(): HttpHeaders {
    const token = this.getToken();
    if (!token) return new HttpHeaders();

    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  // 📦 Universal GET request
  get(endpoint: string): Observable<any> {
    const headers = this.getHeaders();
    console.log('✅ GET Headers:', headers);
    return this.http.get(`${this.baseUrl}/${endpoint}`, { headers }).pipe(
      catchError((error) => {
        console.error('🔴 API GET Error:', error);
        return throwError(() => error);
      })
    );
  }

  // ✉️ Universal POST request
  post(endpoint: string, body: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/${endpoint}`, body, {
      headers: this.getHeaders(),
      responseType: 'text',
    }).pipe(
      catchError((error) => {
        console.error('🔴 API POST Error:', error);
        return throwError(() => error);
      })
    );
  }

  // ✍️ Universal PUT request
  put(endpoint: string, body: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${endpoint}`, body, {
      headers: this.getHeaders(),
    }).pipe(
      catchError((error) => {
        console.error('🔴 API PUT Error:', error);
        return throwError(() => error);
      })
    );
  }

  // ❌ Universal DELETE request
  delete(endpoint: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${endpoint}`, {
      headers: this.getHeaders(),
    }).pipe(
      catchError((error) => {
        console.error('🔴 API DELETE Error:', error);
        return throwError(() => error);
      })
    );
  }

  // 🔐 Token utilities
  setToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  // 🧠 Token decoding helpers
  getUserId(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const decoded: any = jwtDecode(token);
      return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"] || null;
    } catch (err) {
      console.error("JWT Decoding Error:", err);
      return null;
    }
  }

  getRole(): string | null {
    const token = this.getToken();
    if (!token) return null;

    const decoded: any = jwtDecode(token);
    return decoded.role || 'Unknown';
  }

  // 🚆 Train search helper
  getTrainsByRoute(source: string, destination: string): Observable<any> {
    const endpoint = `Trains/search?source=${source}&destination=${destination}`;
    return this.http.get(`${this.baseUrl}/${endpoint}`, {
      headers: this.getHeaders(),
    }).pipe(
      catchError((error) => {
        console.error('🔴 Train Search Error:', error);
        return throwError(() => error);
      })
    );
  }

  // 🌿 Book a Wellness Kit
bookWellnessKit(payload: any): Observable<any> {
  return this.http.post(`${this.baseUrl}/WellnessKitRequest`, payload, {
    headers: this.getHeaders(),
    responseType: 'text',
  }).pipe(
    catchError((error) => {
      console.error('🔴 Booking Kit Error:', error);
      return throwError(() => error);
    })
  );
}

// 🔍 Get User's Booked Wellness Kits
getMyWellnessKits(): Observable<any> {
  return this.http.get(`${this.baseUrl}/WellnessKitRequest/my-requests`, {
    headers: this.getHeaders(),
  }).pipe(
    catchError((error) => {
      console.error('🔴 Fetch Wellness Kits Error:', error);
      return throwError(() => error);
    })
  );
}
deleteWellnessKit(id: number): Observable<any> {
  return this.http.delete(`${this.baseUrl}/WellnessKitRequest/${id}`, {
    headers: this.getHeaders()
  }).pipe(
    catchError((error) => {
      console.error('🗑️ Delete Kit Error:', error);
      return throwError(() => error);
    })
  );
}

bookCateringOrder(payload: any): Observable<any> {
  return this.http.post(`${this.baseUrl}/CateringOrder`, payload, {
    headers: this.getHeaders(),
    responseType: 'text',
  }).pipe(
    catchError((error) => {
      console.error('🔴 Booking Catering Error:', error);
      return throwError(() => error);
    })
  );
}
getMyCateringOrders(): Observable<any> {
  return this.http.get(`${this.baseUrl}/CateringOrder/my-orders`, {
    headers: this.getHeaders()
  }).pipe(
    catchError((error) => {
      console.error('🔴 Get Catering Orders Error:', error);
      return throwError(() => error);
    })
  );
}
}