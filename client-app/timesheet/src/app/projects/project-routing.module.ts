import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProjectsComponent } from './projects/projects.component';
import { AddEditProjectComponent } from './add-edit-project/add-edit-project.component';
import { ProjectComponent } from './project/project.component';

const routes: Routes = [
  {path: '', component: ProjectsComponent},
  {path: 'add', component: AddEditProjectComponent},
  {path: 'edit/:id', component: AddEditProjectComponent},
  {path: ':id', component: ProjectComponent},
  {path: 'task/', loadChildren: () => import('../tasks/tasks.module').then(m => m.TasksModule)},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectsRoutingModule { }
