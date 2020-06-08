import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ProjectService } from "../project.service";
import { MatSnackBar } from "@angular/material";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-add-edit-project",
  templateUrl: "./add-edit-project.component.html",
  styleUrls: ["./add-edit-project.component.css"],
})
export class AddEditProjectComponent implements OnInit {
  public addProjectForm: FormGroup;
  editMode = false;
  id;

  constructor(
    private accountsService: ProjectService,
    private snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.fetchUrlParams();
    if (this.editMode) {
      this.addProjectForm = new FormGroup({
        id: new FormControl(this.id, Validators.required),
        name: new FormControl("", Validators.required),
      });
    } else {
      this.addProjectForm = new FormGroup({
        name: new FormControl("", Validators.required),
      });
    }
  }

  fetchUrlParams() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.editMode = this.id ? true : false;
  }

  ngOnInit() {}

  submitForm() {
    if (!this.addProjectForm.valid) {
      return;
    }

    if (this.editMode) {
      const body = {
        id: this.addProjectForm.get("id").value,
        name: this.addProjectForm.get("name").value,
      };

      this.accountsService.putProjects(body).subscribe((response) => {
        this.router.navigateByUrl("/projects");
        this.snackBar.open("Project has been edited.", "Close");
      });
    } else {
      const body = {
        name: this.addProjectForm.get("name").value,
      };

      this.accountsService.postProjects(body).subscribe((response) => {
        this.router.navigateByUrl("/projects");
        this.snackBar.open("Project has been created.", "Close");
      });
    }
  }
}
