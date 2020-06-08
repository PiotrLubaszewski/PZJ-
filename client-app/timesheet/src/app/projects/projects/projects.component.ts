import { Component, OnInit } from '@angular/core';
import { ProjectModel } from 'src/app/models/project.model';
import { ProjectService } from '../project.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  projects: ProjectModel[] = [];
  displayedColumns: string[] = ['id', 'name', 'actions'];


  constructor(private projectService: ProjectService, private router: Router) { }

  ngOnInit() {
    this.fetchProjects();
  }

  onEditClicked(element) {
    const id = element.id;
    this.router.navigateByUrl('/projects/edit/' + id );
  }


  fetchProjects() {
    this.projectService.getProjects()
      .subscribe(response => this.projects = [...response.result.data]
        ,
        err => console.error(err)
      );
  }

}
