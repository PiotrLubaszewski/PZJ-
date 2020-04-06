import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AccountsService } from 'src/app/accounts/accounts.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  signinForm: FormGroup;
  constructor(
    private accountsService: AccountsService,
    private router: Router) {
    this.signinForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }

  ngOnInit() {

  }

  submitForm(form) {
    const user = {
      userName: form.value.email,
      password: form.value.password
    };

    this.accountsService.authorize(user).subscribe(
      response => {
        sessionStorage.setItem('user', response.result);
      },
      err => console.error(err)
    );

  }

}
