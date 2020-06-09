import { AccountsService } from 'src/app/accounts/accounts.service';
import { Router } from '@angular/router';
import { AuthService } from './../../shared/auth.service';
import { OnInit, Component } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { MonthlySalaryModel } from 'src/app/models/monthly-salary.model';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html'
})
export class HomeComponent implements OnInit {
  monthlySalary: MonthlySalaryModel;
  
  constructor(public authService: AuthService, private accountService: AccountsService, private snackBar: MatSnackBar, private router: Router) {}

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigateByUrl('/login');
    }

    this.accountService.getMonthlySalary(this.authService.getUser().userId, 2020, 6)
    .subscribe(response => this.monthlySalary = response.result);

  }

  public getMonthlySalaryChartUrl() : string
  {
    const monthLabels = this.monthlySalary.days
    .map((x, i) => {
      const day = i + 1;
      if (day < 10)  {
        return "'0" + day.toString() + "'";
      }

      return "'" + day.toString() + "'";
    })
    .join(',');

    const estimatedHoursDataList: number[] = [];
    const workedHoursDataList: number[] = [];

    this.monthlySalary.days.forEach(day => {
      const lastEstimated = estimatedHoursDataList.length === 0 ? 0 : estimatedHoursDataList[estimatedHoursDataList.length - 1];
      const lastWorked = workedHoursDataList.length === 0 ? 0 : workedHoursDataList[workedHoursDataList.length - 1];

      estimatedHoursDataList.push(lastEstimated + day.predictedHours);
      workedHoursDataList.push(lastWorked + day.workedHours);
    });

    const estimatedDataLabel = estimatedHoursDataList.join(',');
    const workDataLabel = workedHoursDataList.join(',');

      const result =  
          "https://quickchart.io/chart?c={type:'line',data:{labels:["
          + monthLabels
          + "], datasets:[{label:'Estimated hours', data: ["
          + estimatedDataLabel
          + "], fill:false,borderColor:'blue'},{label:'Worked hours', data:["
          + workDataLabel
          + "], fill:false,borderColor:'green'}]}}";

    return result;

  }

  public downloadSalaryReport() {
    this.accountService.downloadMonthlySalary(this.authService.getUser().userId, 2020, 6)
    .subscribe(response => {
        const blob = new Blob([response], { type: 'application/pdf' });
        const downloadURL = URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = downloadURL;
        link.download = `Monhtly_Salary_Report_${this.monthlySalary.fullname.replace(' ', '_')}_June_2020.pdf`;
        link.click();
    });
  }

  public downloadTimeConsumingReport() {
    this.accountService.downloadMonthlyTimeConsumingSummary(this.authService.getUser().userId, 2020, 6)
    .subscribe(response => {
        const blob = new Blob([response], { type: 'application/pdf' });
        const downloadURL = URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = downloadURL;
        link.download = `Monhtly_Time_Consuming_Report_${this.monthlySalary.fullname.replace(' ', '_')}_June_2020.pdf`;
        link.click();
    });
  }
}