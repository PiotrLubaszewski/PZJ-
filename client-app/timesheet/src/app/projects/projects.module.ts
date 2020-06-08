import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectsComponent } from './projects/projects.component';
import { ProjectComponent } from './project/project.component';
import { AddEditProjectComponent } from './add-edit-project/add-edit-project.component';
import { MaterialModule } from '../shared/material/material.module';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProjectsRoutingModule } from './project-routing.module';

@NgModule({
  declarations: [ProjectsComponent, ProjectComponent, AddEditProjectComponent],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    ProjectsRoutingModule
  ]
})

export class ProjectsModule { }
