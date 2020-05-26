import { AddAccountComponent } from './add-account/add-account.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountsComponent } from './accounts/accounts.component';
import { AccountComponent } from './account/account.component';


const routes: Routes = [
  {path: '', component: AccountsComponent},
  {path: 'add', component: AddAccountComponent},
  {path: ':id', component: AccountComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountsRoutingModule { }
