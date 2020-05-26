import { ProjectModel } from "./../../models/project.model";
import { AuthService } from "./../../shared/auth.service";
import { RoleModel } from "./../../models/roleModel.model";
import { AccountModel } from "src/app/models/account.model";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { AccountsService } from "../accounts.service";
import { MatSnackBar } from "@angular/material";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-account",
  templateUrl: "./account.component.html",
  styleUrls: ["./account.component.css"],
})
export class AccountComponent implements OnInit {
  public account: AccountModel;
  public userRoles: RoleModel[];
  public allRoles: RoleModel[];
  public selectedRoleId: string;

  public userProjects: ProjectModel[];
  public allProjects: ProjectModel[];
  public selectedProjectId: number;

  displayedColumns: string[] = ["id", "name", "actions"];

  constructor(
    public authService: AuthService,
    private accountsService: AccountsService,
    private snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      const userId = params["id"];
      this.accountsService.getUser(userId).subscribe((response) => {
        this.account = response.result;
      });

      this.getUserRoles(userId);
      this.accountsService.getRoles().subscribe((response) => {
        this.allRoles = response.result.data;
      });

      this.getUserProjects(userId);
      this.accountsService.getProjects().subscribe((response) => {
        this.allProjects = response.result.data;
      });
    });
  }

  getRolesSelectList(): RoleModel[] {
    return this.allRoles.filter((role) => {
      return !this.userRoles.find((x) => x.id === role.id);
    });
  }

  onAddRoleClick() {
    if (!this.selectedRoleId) {
      return;
    }
    const userId = this.route.snapshot.params["id"];
    if (this.userRoles.length > 0) {
      const newUserRolesIds = this.userRoles.map((x) => x.id);
      newUserRolesIds.push(this.selectedRoleId);

      this.accountsService
        .editUserRoles(userId, newUserRolesIds)
        .subscribe((response) => {
          this.getUserRoles(userId);
          this.snackBar.open("Role has been added.", "Close");
        });
    } else {
      this.accountsService
        .addUserRoles(userId, [this.selectedRoleId])
        .subscribe((response) => {
          this.getUserRoles(userId);
          this.snackBar.open("Role has been added.", "Close");
        });
    }
  }

  getUserRoles(userId: string) {
    this.accountsService.getUserRoles(userId).subscribe((response) => {
      this.userRoles = response.result.data;
    });
  }

  onDeleteRole(id: string) {
    const newUserRolesIds = this.userRoles
      .filter((x) => x.id !== id)
      .map((x) => x.id);
    const userId = this.route.snapshot.params["id"];

    this.accountsService
      .editUserRoles(userId, newUserRolesIds)
      .subscribe((response) => {
        this.getUserRoles(userId);
        this.snackBar.open("Role has been deleted.", "Close");
      });
  }

  getUserProjects(userId: string) {
    this.accountsService.getUserProjects(userId).subscribe((response) => {
      this.userProjects = response.result.data;
    });
  }

  getProjectsSelectList(): ProjectModel[] {
    return this.allProjects.filter((project) => {
      return !this.userProjects.find((x) => x.id === project.id);
    });
  }

  onAddProjectClick() {
    if (!this.selectedProjectId || this.selectedProjectId < 1) {
      return;
    }

    const userId = this.route.snapshot.params["id"];

    this.accountsService
      .addUserProject(userId, this.selectedProjectId)
      .subscribe((response) => {
        this.getUserProjects(userId);
      });
  }

  onDeleteProjectClick(id: number) {
    const userId = this.route.snapshot.params["id"];

    this.accountsService.deleteUserProject(userId, id).subscribe((response) => {
      this.getUserProjects(userId);
    });
  }
}
