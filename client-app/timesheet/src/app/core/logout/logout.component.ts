import { Router } from '@angular/router';
import { AuthService } from './../../shared/auth.service';
import { OnInit, Component } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-logout',
  template: ''
})
export class LogoutComponent implements OnInit {
  constructor(private authService: AuthService, private snackBar: MatSnackBar, private router: Router) {}

  ngOnInit(): void {
    this.authService.deauthenticate();
    this.router.navigateByUrl('/login');
    this.snackBar.open('You have been logged out.', 'Close');
  }
}