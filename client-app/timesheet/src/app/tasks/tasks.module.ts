import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddEditTaskComponent } from './add-edit-task/add-edit-task.component';
import { TaskComponent } from './task/task.component';
import { TasksComponent } from './tasks/tasks.component';
import { TasksRoutingModule } from './task-routing.module';



@NgModule({
  declarations: [TasksComponent, AddEditTaskComponent, TaskComponent],
  imports: [
    CommonModule,
    TasksRoutingModule
  ]
})
export class TasksModule { }
