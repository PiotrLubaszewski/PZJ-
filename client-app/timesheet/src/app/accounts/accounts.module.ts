import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountsComponent } from './accounts/accounts.component';
import { AccountsRoutingModule } from './accounts-routing.module';
import { MaterialModule } from '../shared/material/material.module';

@NgModule({
  declarations: [AccountsComponent],
  imports: [
    CommonModule,
    MaterialModule,
    AccountsRoutingModule
  ]
})

export class AccountsModule { }
