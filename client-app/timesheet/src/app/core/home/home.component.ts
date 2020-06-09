import { Router } from '@angular/router';
import { AuthService } from './../../shared/auth.service';
import { OnInit, Component } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html'
})
export class HomeComponent implements OnInit {
  constructor(public authService: AuthService, private snackBar: MatSnackBar, private router: Router) {}

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigateByUrl('/login');
    }
  }
}