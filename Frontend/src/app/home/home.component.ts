import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private toastr: ToastrService) {}

  onSearch() {
    this.toastr.error('Site is busy, please search after some time', 'Error');
  }
}
