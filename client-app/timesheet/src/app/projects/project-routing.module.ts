import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProjectsComponent } from './projects/projects.component';
import { AddEditProjectComponent } from './add-edit-project/add-edit-project.component';


const routes: Routes = [
  {path: '', component: ProjectsComponent},
  {path: 'add', component: AddEditProjectComponent},
  {path: ':id', component: ProjectsComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectsRoutingModule { }
