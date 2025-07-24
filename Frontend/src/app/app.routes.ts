import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './shared/not-found/not-found.component';
import { AuthGuard } from './Auth/auth.guard';


export const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'trains',
    loadComponent: () =>
      import('./pages/train/train.component').then((m) => m.TrainComponent),
  },
  {
    path: 'reservations/:id',
    loadComponent: () =>
      import('./pages/reservations/reservations.component').then((m) => m.ReservationComponent),
  },
  {
    path: 'login', loadComponent: () =>
      import('./pages/Authorize/login/login.component').then((m) => m.LoginComponent),
  },
  {path: 'register', loadComponent: () =>
      import('./pages/Authorize/register/register.component').then((m) => m.RegisterComponent),
  },
  { path: 'profile', 
    loadComponent: () => import('./pages/my-profile/my-profile.component').then(m => m.MyProfileComponent), 
    canActivate: [AuthGuard] 
  },
  { path: '**', component: NotFoundComponent },
];
