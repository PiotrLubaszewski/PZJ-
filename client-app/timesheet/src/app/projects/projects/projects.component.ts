import { Component, OnInit } from '@angular/core';
import { ProjectModel } from 'src/app/models/project.model';
import { ProjectService } from '../project.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  projects: ProjectModel[] = [];
  displayedColumns: string[] = ['id', 'name'];


  constructor(private projectService: ProjectService) { }

  ngOnInit() {
    this.fetchProjects();
  }


  fetchProjects() {
    this.projectService.getProjects()
      .subscribe(response => this.projects = [...response.result.data]
        ,
        err => console.error(err)
      );
  }

}
