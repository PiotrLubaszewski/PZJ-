import { SummaryComponent } from './core/summary/summary.component';
import { HomeComponent } from './core/home/home.component';
import { LogoutComponent } from './core/logout/logout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './core/login/login.component';


const routes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'logout', component: LogoutComponent},
  {path: 'home', component: HomeComponent},
  {path: 'summary', component: SummaryComponent},
  {path: 'accounts', loadChildren: () => import('./accounts/accounts.module').then(m => m.AccountsModule)},
  {path: 'projects', loadChildren: () => import('./projects/projects.module').then(m => m.ProjectsModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
