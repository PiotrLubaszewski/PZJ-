import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ProjectService } from '../project.service';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-edit-project',
  templateUrl: './add-edit-project.component.html',
  styleUrls: ['./add-edit-project.component.css']
})
export class AddEditProjectComponent implements OnInit {
  public addProjectForm: FormGroup;

  constructor(private accountsService: ProjectService, private snackBar: MatSnackBar, private router: Router) {
    this.addProjectForm = new FormGroup({
      name: new FormControl('', Validators.required)
    });
  }

  ngOnInit() {
  }

  submitForm() {
    if (!this.addProjectForm.valid) {
      return;
    }

    const body = {
      name: this.addProjectForm.get('name').value
    };

    this.accountsService.postProjects(body).subscribe(response => {
      this.router.navigateByUrl('/projects');
      this.snackBar.open('Project has been created.', 'Close');
    });
  }

}
