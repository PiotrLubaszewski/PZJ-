import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TasksComponent } from './tasks/tasks.component';
import { AddEditTaskComponent } from './add-edit-task/add-edit-task.component';
import { TaskComponent } from './task/task.component';


const routes: Routes = [
  {path: '', component: TasksComponent},
  {path: 'add', component: AddEditTaskComponent},
  {path: 'edit/:id', component: AddEditTaskComponent},
  {path: ':id', component: TaskComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TasksRoutingModule { }
