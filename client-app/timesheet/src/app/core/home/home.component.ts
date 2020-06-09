import { Router } from '@angular/router';
import { AuthService } from './../../shared/auth.service';
import { OnInit, Component } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html'
})
export class HomeComponent implements OnInit {
  monday = new Date();
  tuesday = new Date();
  wedensday = new Date();
  thursday = new Date();
  friday = new Date();
  saturday = new Date();
  sunday = new Date();
  currentDate = new Date(Date.now());

  projects = [
    { name: 'Restaurant Review App', description: 'Fixing Bugs', class: 'alert alert-success'},
    { name: 'Restaurant Review App', description: 'New features', class: 'alert alert-success'},
    { name: 'Restaurant Review App', description: 'Code refactor', class: 'alert alert-success'},
    { name: 'Personal budget manager', description: 'Write unit tests', class: 'alert alert-primary'},
    { name: 'Personal budget manager', description: 'Write documentation', class: 'alert alert-primary'},
    { name: 'Personal budget manager', description: 'Deploy app', class: 'alert alert-primary'},
    { name: 'Personal budget manager', description: 'Making demo', class: 'alert alert-primary'},
  ];

  constructor(public authService: AuthService, private snackBar: MatSnackBar, private router: Router) {}

   getMonday(d) {
    d = new Date(d);
    const day = d.getDay();
    const diff = d.getDate() - day + (day === 0 ? -6 : 1);

    return new Date(d.setDate(diff));
  }


  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigateByUrl('/login');
    }

    this.monday = this.getMonday(new Date());
    this.tuesday.setDate(this.monday.getDate() + 1);
    this.wedensday.setDate(this.monday.getDate() + 2);
    this.thursday.setDate(this.monday.getDate() + 3);
    this.friday.setDate(this.monday.getDate() + 4);
    this.saturday.setDate(this.monday.getDate() + 5);
    this.sunday.setDate(this.monday.getDate() + 6);
  }
}
