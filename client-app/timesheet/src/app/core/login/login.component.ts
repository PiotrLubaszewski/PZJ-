import { AuthService } from './../../shared/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AccountsService } from 'src/app/accounts/accounts.service';
import { MatSnackBar } from '@angular/material';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  signinForm: FormGroup;
  constructor(
    private accountsService: AccountsService,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar) {
    this.signinForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }

  ngOnInit() {
    if (this.authService.isAuthenticated()) {
      this.router.navigateByUrl('/summary');
    }
  }

  submitForm(form) {
    if (!this.signinForm.valid) {
      return;
    }

    const user = {
      userName: form.value.email,
      password: form.value.password
    };

    this.accountsService.authorize(user).subscribe(
      response => {
        this.authService.authenticate(response.result);
        this.router.navigateByUrl('/summary');
        this.snackBar.open('You have been logged in.', 'Close');
      },
      err => console.error(err)
    );

  }

}
