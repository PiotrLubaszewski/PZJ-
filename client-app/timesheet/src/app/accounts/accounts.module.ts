import { AddAccountComponent } from './add-account/add-account.component';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountsComponent } from './accounts/accounts.component';
import { AccountsRoutingModule } from './accounts-routing.module';
import { MaterialModule } from '../shared/material/material.module';
import { AccountComponent } from './account/account.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [AccountsComponent, AccountComponent, AddAccountComponent],
  imports: [
    CommonModule,
    MaterialModule,
    AccountsRoutingModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ]
})

export class AccountsModule { }
