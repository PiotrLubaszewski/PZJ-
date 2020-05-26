import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { AccountsService } from 'src/app/accounts/accounts.service';
import { AccountModel } from 'src/app/models/account.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent implements OnInit {
  public addAccountForm: FormGroup;

  constructor(private accountsService: AccountsService, private snackBar: MatSnackBar, private router: Router) { 
    this.addAccountForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required)
    });
  }

  ngOnInit() {
  }

  submitForm() {
    if (!this.addAccountForm.valid) {
      return;
    }

    const body = {
      userName: this.addAccountForm.get('username').value,
      email: this.addAccountForm.get('email').value,
      firstName: this.addAccountForm.get('firstName').value,
      lastName: this.addAccountForm.get('lastName').value,
      password: this.addAccountForm.get('password').value
    };

    this.accountsService.postAccounts(body).subscribe(response => {
      this.router.navigateByUrl('/accounts');
      this.snackBar.open('Account has been created.', 'Close');
    });
  }
}
